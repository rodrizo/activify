using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Models
{
    public class Actividad
    {
        public int IdActividad { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Estimado { get; set; }
        public string DetalleActividades { get; set; }
        public string Observaciones { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdComite { get; set; }
    }
}
