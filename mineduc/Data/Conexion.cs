using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineduc.Data
{
    public class Conexion
    {
        private string connection;
        #region "Método para retornar cadena de conexión"
        //Al invocar al método conStrin, se retornará la cadena de conexión proveniente de App.config
        public string conStrin(string mineducDB)
        {
            connection = ConfigurationManager.ConnectionStrings[mineducDB].ConnectionString;

            return connection;
        }
        #endregion
    }
}
