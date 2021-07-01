using System;
using System.Collections.Generic;

#nullable disable

namespace BPT.Test.DIBL.API.Models
{
    public partial class AsignacionesEstudiante
    {
        public int Id { get; set; }
        public int? IdAsignacion { get; set; }
        public int? IdEstudiante { get; set; }

        public virtual Asignacione IdAsignacionNavigation { get; set; }
        public virtual Estudiante IdEstudianteNavigation { get; set; }
    }
}
