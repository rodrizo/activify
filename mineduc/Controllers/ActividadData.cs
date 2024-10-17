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
    public class ActividadData
    {
        public DataSet getActividades(Actividad act)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_actividad", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        if (act != null)
                        {
                            command.Parameters.Add(new SqlParameter("@seccionId", act.SeccionId));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Actividad");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }
        public string ActividadCRUD(Actividad act, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_actividad", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@nombre", act.Nombre));
                            command.Parameters.Add(new SqlParameter("@fecha", act.Fecha));
                            command.Parameters.Add(new SqlParameter("@monto", act.Monto));
                            command.Parameters.Add(new SqlParameter("@observaciones", act.Observaciones));
                            command.Parameters.Add(new SqlParameter("@tipoActividadId", act.TipoActividadId));
                            command.Parameters.Add(new SqlParameter("@seccionId", act.SeccionId));
                            command.Parameters.Add(new SqlParameter("@alumnoId", act.AlumnoId));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@actividadId", act.ActividadId));
                            command.Parameters.Add(new SqlParameter("@nombre", act.Nombre));
                            command.Parameters.Add(new SqlParameter("@fecha", act.Fecha));
                            command.Parameters.Add(new SqlParameter("@monto", act.Monto));
                            command.Parameters.Add(new SqlParameter("@observaciones", act.Observaciones));
                            command.Parameters.Add(new SqlParameter("@tipoActividadId", act.TipoActividadId));
                            command.Parameters.Add(new SqlParameter("@seccionId", act.SeccionId));
                            command.Parameters.Add(new SqlParameter("@alumnoId", act.AlumnoId));
                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@actividadId", act.ActividadId));
                        }
                        command.ExecuteNonQuery();
                        return "1";
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    return ex.Message.ToString();
                }
            }
        }
    }
}
