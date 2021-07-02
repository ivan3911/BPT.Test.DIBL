using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BPT.Test.DIBL.API.DTOs
{
    public class AssignmentStudentCreationDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int IdAsignacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int IdEstudiante { get; set; }
    }
}
