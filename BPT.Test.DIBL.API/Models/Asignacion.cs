using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BPT.Test.DIBL.API.Models
{
    public partial class Asignacion
    {
        public Asignacion()
        {
            AsignacionesEstudiantes = new HashSet<AsignacionesEstudiante>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 60, ErrorMessage = "El campo {0} no debe tener mas de {1} carácteres.")]
        public string Nombre { get; set; }

        public virtual ICollection<AsignacionesEstudiante> AsignacionesEstudiantes { get; set; }
    }
}
