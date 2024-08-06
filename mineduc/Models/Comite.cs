using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Models
{
    public class Comite
    {
        public int IdComite { get; set; }
        public string Nombre { get; set; }
        public decimal Fondo { get; set; }
        public int IdEscuela { get; set; }
    }
}
