using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BPT.Test.DIBL.API.DTOs
{
    public class StudentCreationDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} no debe tener mas de {1} carácteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime FechaNacimiento { get; set; }
    }
}
