using BPT.Test.DIBL.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPT.Test.DIBL.API.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public List<AssignmentDTO> Asignaciones {get;set;}
    }
}
