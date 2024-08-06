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

        #region "Retornando tabla con data de escuelas"
        public DataTable getEscuelas()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = "SELECT * FROM Escuela";
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
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
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

        #region "Retornando tabla con data de comités"
        public DataTable getComites()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = "SELECT * FROM Comite";
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
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
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
