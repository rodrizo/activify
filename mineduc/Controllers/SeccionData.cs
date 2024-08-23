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
    public class SeccionData
    {
        public DataSet getSecciones(Seccion sec)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_seccion", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        if(sec != null)
                        {
                            command.Parameters.Add(new SqlParameter("@seccionId", sec.SeccionId));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Seccion");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public void SeccionCRUD(Seccion sec, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_seccion", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@grado", sec.Grado));
                            command.Parameters.Add(new SqlParameter("@aula", sec.Aula));
                            command.Parameters.Add(new SqlParameter("@profesorId", sec.ProfesorId));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@seccionId", sec.SeccionId));
                            command.Parameters.Add(new SqlParameter("@grado", sec.Grado));
                            command.Parameters.Add(new SqlParameter("@aula", sec.Aula));
                            command.Parameters.Add(new SqlParameter("@profesorId", sec.ProfesorId));

                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@seccionId", sec.SeccionId));
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
        /*
        public DataSet getResultados(Seccion sec, string action)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_seccion", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "M"));
                        if (com != null)
                        {
                            command.Parameters.Add(new SqlParameter("@seccionId", com.SeccionId));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Seccion");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }
        */
    }
}
