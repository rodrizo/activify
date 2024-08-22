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
    //Clase encargada de retornar data para llenar combo boxes
    public class ComboData
    {
        private Conexion cn = new Conexion();
        private SqlCommand command = new SqlCommand();
        private SqlDataReader reader;

        #region "Retornando tabla con data de profesores"
        public DataTable getEscuelas()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = "SELECT * FROM Profesor";
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return dt;
            }
        }
        #endregion

        #region "Retornando tabla con data de TipoActividad"
        public DataTable getTipoActividad()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = "SELECT * FROM TipoActividad";
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return dt;
            }
        }
        #endregion

        #region "Retornando tabla con data de secciones"
        public DataTable getSecciones()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = "SELECT SeccionId, CONCAT(Grado, ' - ', Aula) [Nombre] FROM Seccion";
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return dt;
            }
        }
        #endregion

        #region "Retornando tabla con data de alumnos"
        public DataTable getAlumnos(int seccionId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = $"SELECT * FROM Alumno WHERE SeccionId = ISNULL({seccionId}, SeccionId)";
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return dt;
            }
        }
        #endregion

        #region "Retornando tabla con data de acción"
        public DataTable getAction()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = "SELECT Action FROM Bitacora GROUP BY Action";
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return dt;
            }
        }
        #endregion

    }
}
