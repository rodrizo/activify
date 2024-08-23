using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Models
{
    public class Seccion
    {
        public int SeccionId { get; set; }
        public string Grado { get; set; }
        public string Aula { get; set; }
        public int ProfesorId { get; set; }
    }
}
