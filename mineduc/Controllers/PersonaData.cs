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
    public class PersonaData
    {

        public DataSet getPersonas(Persona per)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_persona", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        if (per != null)
                        {
                            command.Parameters.Add(new SqlParameter("@idComite", per.IdComite));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Persona");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public void PersonaCRUD(Persona per, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_persona", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@nombre", per.Nombre));
                            command.Parameters.Add(new SqlParameter("@dpi", per.DPI));
                            command.Parameters.Add(new SqlParameter("@idComite", per.IdComite));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@idPersona", per.IdPersona));
                            command.Parameters.Add(new SqlParameter("@nombre", per.Nombre));
                            command.Parameters.Add(new SqlParameter("@dpi", per.DPI));

                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@idPersona", per.IdPersona));
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
