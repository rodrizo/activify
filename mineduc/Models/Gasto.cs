using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Models
{
    public class Gasto
    {
        public int IdGasto { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int IdActividad { get; set; }
    }
}
