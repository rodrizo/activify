using mineduc.Data;
using mineduc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mineduc.Controllers
{
    public class ProfesorData
    {

        public DataSet getProfesores()
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_profesor", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Profesor");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public void ProfesorCRUD(Profesor pro, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_profesor", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@nombre", pro.Nombre));
                            command.Parameters.Add(new SqlParameter("@email", pro.Email));
                            command.Parameters.Add(new SqlParameter("@telefono", pro.Telefono));
                            command.Parameters.Add(new SqlParameter("@dpi", pro.DPI));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@profesorId", pro.ProfesorId));
                            command.Parameters.Add(new SqlParameter("@nombre", pro.Nombre));
                            command.Parameters.Add(new SqlParameter("@email", pro.Email));
                            command.Parameters.Add(new SqlParameter("@telefono", pro.Telefono));
                            command.Parameters.Add(new SqlParameter("@dpi", pro.DPI));
                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@profesorId", pro.ProfesorId));
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
