using mineduc.Data;
using mineduc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mineduc.Controllers
{
    public class ComprobanteData
    {
        public DataSet getComprobante(Comprobante comp)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_comprobante", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        if (comp != null)
                        {
                            command.Parameters.Add(new SqlParameter("@GastoId", comp.GastoId));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Comprobante");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public void ComprobanteCRUD(Comprobante comp, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_comprobante", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@Nombre", comp.Nombre));
                            command.Parameters.Add(new SqlParameter("@Imagen", comp.Imagen));
                            command.Parameters.Add(new SqlParameter("@GastoId", comp.GastoId));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@ComprobanteId", comp.ComprobanteId));
                            command.Parameters.Add(new SqlParameter("@Nombre", comp.Nombre));
                            command.Parameters.Add(new SqlParameter("@Imagen", comp.Imagen));

                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@ComprobanteId", comp.ComprobanteId));
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

        public Image getImage(Comprobante comp, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbActivify")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_comprobante", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "S")
                        {
                            command.Parameters.Add(new SqlParameter("@ComprobanteId", comp.ComprobanteId));
                            SqlDataReader reader = command.ExecuteReader(); 
                            if(reader.Read())
                            {
                                MemoryStream stream = new MemoryStream(reader.GetSqlBytes(1).Buffer);
                                return Image.FromStream(stream);
                            }
                            return null;
                        }
                        command.ExecuteNonQuery();
                        connection.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return null;
        }
    }
}
