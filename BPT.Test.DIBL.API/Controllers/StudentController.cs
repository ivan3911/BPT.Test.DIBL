using AutoMapper;
using BPT.Test.DIBL.API.DTOs;
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
        private readonly IMapper mapper;

        public StudentController(PagaTodoDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentDTO>>> Get()
        {
            var students= await context.Estudiantes.ToListAsync();
            return mapper.Map<List<StudentDTO>>(students);
        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<StudentDTO>> Get(int id)
        //{
        //    var student = await context.Estudiantes
        //        .Include(studentDB=> studentDB.AsignacionesEstudiantes)
        //        .ThenInclude(asignacionEstudianteDB=>asignacionEstudianteDB.IdAsignacionNavigation.Nombre)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    if (student == null)
        //        return NotFound();

        //    return mapper.Map<StudentDTO>(student);
        //}

        [HttpPost]
        public async Task<ActionResult> Post(StudentCreationDTO studentCreationDTO)
        {
            var exist = await context.Estudiantes.AnyAsync(x => x.Nombre == studentCreationDTO.Nombre);
            if (exist)
                return BadRequest($"Ya existe un estudiante con el nombre {studentCreationDTO.Nombre}");

            var student = mapper.Map<Estudiante>(studentCreationDTO);

            context.Add(student);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/estudiantes/id
        public async Task<ActionResult> Put(StudentCreationDTO studentCreationDTO, int id) 
        {
            var existe = await context.Estudiantes.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            var student = mapper.Map<Estudiante>(studentCreationDTO);
            student.Id = id;

            context.Update(student);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")] //api/estudiantes/2
        public async Task<ActionResult> Delete(int id) 
        {
            var existe = await context.Estudiantes.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            context.Remove(new Estudiante() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
