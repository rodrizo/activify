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
    public class EscuelaData
    {

        public DataSet getEscuelas()
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_escuela", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Escuela");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public void EscuelaCRUD(Escuela esc, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_escuela", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@nombre", esc.Nombre));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@idEscuela", esc.IdEscuela));
                            command.Parameters.Add(new SqlParameter("@nombre", esc.Nombre));

                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@idEscuela", esc.IdEscuela));
                        }
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }


    }
}
