using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Models
{
    public class Alumno
    {
        public int AlumnoId { get; set; }
        public string Carnet { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int SeccionId { get; set; }
    }
}
