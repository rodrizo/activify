using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Models
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public string Nombre { get; set; }
        public int IdGasto { get; set; }
        public byte[] Imagen { get; set; }
    }
}
