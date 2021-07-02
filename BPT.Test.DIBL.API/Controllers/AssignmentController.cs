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
    [Route("api/asignaciones")]
    public class AssignmentController : ControllerBase
    {
        private readonly PagaTodoDbContext context;
        private readonly IMapper mapper;

        public AssignmentController(PagaTodoDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Asignacion>>> Get()
        {
            return await context.Asignaciones.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<AssignmentDTO>> Get(int id)
        {
            var assignment = await context.Asignaciones.FirstOrDefaultAsync(x => x.Id == id);

            if (assignment == null)
                return NotFound();

            return mapper.Map<AssignmentDTO>(assignment);
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(AssignmentCreationDTO assignmentCreationDTO)
        {
            var assignment = mapper.Map<Asignacion>(assignmentCreationDTO);

            context.Add(assignment);
            await context.SaveChangesAsync();
            return assignment.Id;
        }

        [HttpPut("{id:int}")] //api/asignaciones/id
        public async Task<ActionResult<int>> Put(AssignmentCreationDTO assignmentCreationDTO, int id)
        {

            var existe = await context.Asignaciones.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            var assignment = mapper.Map<Asignacion>(assignmentCreationDTO);
            assignment.Id = id;

            context.Update(assignment);
            await context.SaveChangesAsync();
            return assignment.Id;
        }

        [HttpDelete("{id:int}")] //api/asignaciones/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Asignaciones.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            context.Remove(new Asignacion() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
