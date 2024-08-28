using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Models
{
    public class Comprobante
    {
        public int ComprobanteId { get; set; }
        public string Nombre { get; set; }
        public int GastoId { get; set; }
        public byte[] Imagen { get; set; }
    }
}
