using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Models
{
    public class Actividad
    {
        public int ActividadId { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Observaciones { get; set; }
        public int TipoActividadId { get; set; }
        public int SeccionId { get; set; }
        public int AlumnoId { get; set; }
    }
}
