using AutoMapper;
using BPT.Test.DIBL.API.DTOs;
using BPT.Test.DIBL.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPT.Test.DIBL.API.Utilities
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<StudentCreationDTO, Estudiante>();
            CreateMap<Estudiante, StudentDTO>()
                .ForMember(StudentDTO => StudentDTO.Asignaciones, opciones => opciones.MapFrom(MapStudentDTOAssignmen));


            CreateMap<AssignmentCreationDTO,Asignacion>();
            CreateMap<Asignacion, AssignmentDTO>();

            CreateMap<AssignmentStudentCreationDTO, AsignacionesEstudiante>();
            CreateMap<AsignacionesEstudiante, AssignmentStudentDTO>();
        }

        private List<AssignmentDTO> MapStudentDTOAssignmen(Estudiante estudiante, StudentDTO studentDTO) 
        {

            var resultado = new List<AssignmentDTO>();

            if (estudiante.AsignacionesEstudiantes == null) { return resultado; }

            foreach (var asignacionesEstudiante in estudiante.AsignacionesEstudiantes)
            {
                resultado.Add(new AssignmentDTO()
                {
                    Id = asignacionesEstudiante.Id,
                    Nombre = asignacionesEstudiante.IdAsignacionNavigation.Nombre
                });
            }

            return resultado;
        }
    }
}
