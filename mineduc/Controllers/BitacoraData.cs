using mineduc.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mineduc.Controllers
{
    public class BitacoraData
    {
        public DataSet getBitacora(string action)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_bitacora", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Bitacora");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }
    }
}
