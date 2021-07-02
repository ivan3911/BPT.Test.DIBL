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
    [Route("api/asignacionesestudiante")]
    public class AssignmentStudentController:ControllerBase
    {
        private readonly PagaTodoDbContext context;
        private readonly IMapper mapper;

        public AssignmentStudentController(PagaTodoDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AssignmentStudentDTO>>> Get()
        {
            var assignmentstudents = await context.AsignacionesEstudiantes.ToListAsync();
            return mapper.Map<List<AssignmentStudentDTO>>(assignmentstudents);
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(AssignmentStudentCreationDTO assignmentStudentCreationDTO)
        {
            var exist_idAssignment = await context.Asignaciones.AnyAsync(x => x.Id == assignmentStudentCreationDTO.IdAsignacion);
            if (!exist_idAssignment)
                return BadRequest($"No existe la asignación con id = {assignmentStudentCreationDTO.IdAsignacion}");

            var exist_idStudent = await context.Estudiantes.AnyAsync(x => x.Id == assignmentStudentCreationDTO.IdEstudiante);
            if (!exist_idStudent)
                return BadRequest($"No existe la el estudiante con id = {assignmentStudentCreationDTO.IdEstudiante}");

            var assignmentStudent = mapper.Map<AsignacionesEstudiante>(assignmentStudentCreationDTO);

            context.Add(assignmentStudent);
            await context.SaveChangesAsync();
            return assignmentStudent.Id;
        }

        [HttpDelete("{id:int}")] //api/asignacionesestudiante/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.AsignacionesEstudiantes.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            context.Remove(new AsignacionesEstudiante() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
