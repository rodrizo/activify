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
    public class FacturaData
    {
        public DataSet getFactura(Factura fac)
        {
            Conexion cn = new Conexion();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_factura", connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", "R"));
                        if (fac != null)
                        {
                            command.Parameters.Add(new SqlParameter("@idGasto", fac.IdGasto));
                        }
                        adapter.SelectCommand = command;
                        adapter.Fill(ds, "Factura");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ds;
            }
        }

        public void FacturaCRUD(Factura fac, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_factura", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "C")
                        {
                            command.Parameters.Add(new SqlParameter("@nombre", fac.Nombre));
                            command.Parameters.Add(new SqlParameter("@imagen", fac.Imagen));
                            command.Parameters.Add(new SqlParameter("@idGasto", fac.IdGasto));
                        }
                        else if (action == "U")
                        {
                            command.Parameters.Add(new SqlParameter("@idFactura", fac.IdFactura));
                            command.Parameters.Add(new SqlParameter("@nombre", fac.Nombre));
                            command.Parameters.Add(new SqlParameter("@imagen", fac.Imagen));

                        }
                        else if (action == "D")
                        {
                            command.Parameters.Add(new SqlParameter("@idFactura", fac.IdFactura));
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

        public Image getImage(Factura fac, string action)
        {
            Conexion cn = new Conexion();
            using (SqlConnection connection = new SqlConnection(cn.conStrin("dbMineduc")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_crud_factura", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@action", action));
                        if (action == "S")
                        {
                            command.Parameters.Add(new SqlParameter("@idFactura", fac.IdFactura));
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
