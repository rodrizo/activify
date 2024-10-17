using mineduc.Data;
using mineduc.Models;
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
    public class GastoData
    {
        public DataSet getGastos(Gasto gas)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_gastos", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        if (gas != null)
                        {
                            command.Parameters.Add(new SqlParameter("@idActividad", gas.IdActividad));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Gasto");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public string GastoCRUD(Gasto gas, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_gastos", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@descripcion", gas.Descripcion));
                            command.Parameters.Add(new SqlParameter("@monto", gas.Monto));
                            command.Parameters.Add(new SqlParameter("@idActividad", gas.IdActividad));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@idGasto", gas.IdGasto));
                            command.Parameters.Add(new SqlParameter("@descripcion", gas.Descripcion));
                            command.Parameters.Add(new SqlParameter("@monto", gas.Monto));
                            command.Parameters.Add(new SqlParameter("@idActividad", gas.IdActividad));

                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@idGasto", gas.IdGasto));
                        }
                        command.ExecuteNonQuery();
                        return "1";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return ex.Message.ToString();
                }
            }

        }
    }
}
