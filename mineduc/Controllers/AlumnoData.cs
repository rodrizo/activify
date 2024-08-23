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
    public class AlumnoData
    {

        public DataSet getAlmunos(Alumno alm)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_alumno", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        if (alm != null)
                        {
                            command.Parameters.Add(new SqlParameter("@SeccionId", alm.SeccionId));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Alumno");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public void AlumnoCRUD(Alumno alm, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_alumno", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@Carnet", alm.Carnet));
                            command.Parameters.Add(new SqlParameter("@Nombre", alm.Nombre));
                            command.Parameters.Add(new SqlParameter("@Telefono", alm.Telefono));
                            command.Parameters.Add(new SqlParameter("@SeccionId", alm.SeccionId));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@Carnet", alm.Carnet));
                            command.Parameters.Add(new SqlParameter("@Nombre", alm.Nombre));
                            command.Parameters.Add(new SqlParameter("@Telefono", alm.Telefono));
                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@AlumnoId", alm.AlumnoId));
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
