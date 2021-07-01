using System;
using System.Collections.Generic;

#nullable disable

namespace BPT.Test.DIBL.API.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            AsignacionesEstudiantes = new HashSet<AsignacionesEstudiante>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<AsignacionesEstudiante> AsignacionesEstudiantes { get; set; }
    }
}
