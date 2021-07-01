using BPT.Test.DIBL.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPT.Test.DIBL.API.Controllers
{
    [ApiController]
    [Route("api/estudiantes")]
    public class StudentController : ControllerBase
    {
        private readonly PagaTodoDbContext context;

        public StudentController(PagaTodoDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Estudiante>>> Get()
        {
            return await context.Estudiantes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Estudiante estudiante)
        {
            context.Add(estudiante);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/estudiantes/id
        public async Task<ActionResult> Put(Estudiante estudiante, int id) 
        {
            if (estudiante.Id != id) 
                return BadRequest("El id del estudiante no coincide con el Id de la URL");
            
            var existe = await context.Estudiantes.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            context.Update(estudiante);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] //api/estudiantes/2
        public async Task<ActionResult> Delete(int id) 
        {
            var existe = await context.Estudiantes.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            context.Remove(new Estudiante() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
