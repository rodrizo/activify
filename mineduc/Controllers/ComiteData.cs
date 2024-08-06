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
    public class ComiteData
    {
        public DataSet getComites(Comite com)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_comite", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        if(com != null)
                        {
                            command.Parameters.Add(new SqlParameter("@idEscuela", com.IdEscuela));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Comite");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public void ComiteCRUD(Comite com, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_comite", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@nombre", com.Nombre));
                            command.Parameters.Add(new SqlParameter("@fondo", com.Fondo));
                            command.Parameters.Add(new SqlParameter("@idEscuela", com.IdEscuela));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@idComite", com.IdComite));
                            command.Parameters.Add(new SqlParameter("@nombre", com.Nombre));
                            command.Parameters.Add(new SqlParameter("@fondo", com.Fondo));

                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@idComite", com.IdComite));
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

        public DataSet getResultados(Comite com, string action)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_comite", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "M"));
                        if (com != null)
                        {
                            command.Parameters.Add(new SqlParameter("@idComite", com.IdComite));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Comite");
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
