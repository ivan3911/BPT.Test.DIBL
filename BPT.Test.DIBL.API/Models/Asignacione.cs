using System;
using System.Collections.Generic;

#nullable disable

namespace BPT.Test.DIBL.API.Models
{
    public partial class Asignacione
    {
        public Asignacione()
        {
            AsignacionesEstudiantes = new HashSet<AsignacionesEstudiante>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<AsignacionesEstudiante> AsignacionesEstudiantes { get; set; }
    }
}
