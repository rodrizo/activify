using mineduc.Controllers;
using mineduc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mineduc.Forms
{
    public partial class frmFactura : Form
    {
        Factura fac = new Factura();
        FacturaData facData = new FacturaData();
        private int Id;
        string imgLocation = "";

        public frmFactura(int idGasto)
        {
            fac.IdGasto = idGasto;
            InitializeComponent();
        }

        #region "Llenando grid de facturas"
        private void getFacturas()
        {
            DataSet ds = facData.getFactura(fac);
            gridFacturas.DataSource = ds;
            gridFacturas.DataMember = "Factura";
        }
        #endregion

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                imgLocation = OD.FileName.ToString();
                pictureBoxAdd.ImageLocation = imgLocation;
            }
        }

        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            byte[] images = null;
            FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(stream);
            images = br.ReadBytes((int)stream.Length);

            fac.IdFactura = Id;
            fac.Nombre = txtNombre.Text;
            fac.Imagen = images;
            fac.IdGasto = fac.IdGasto;
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtNombre.Text = string.Empty;
            pictureBoxAdd.Image = null;
            pictureBoxShow.Image = null;
        }
        #endregion
        private void frmFactura_Load(object sender, EventArgs e)
        {
            getFacturas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            facData.FacturaCRUD(fac, "C");
            MessageBox.Show("Factura creada con éxito");
            cleanFields();
            getFacturas();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna factura");
            }
            else
            {
                getData();
                facData.FacturaCRUD(fac, "U");
                cleanFields();
                MessageBox.Show("La factura se editó con éxito");
                getFacturas();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna factura");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar esta factura?\n", "Eliminar Factura", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    facData.FacturaCRUD(fac, "D");
                    cleanFields();
                    MessageBox.Show("La factura se eliminó con éxito");
                    getFacturas();
                }
                else
                {
                    txtNombre.Focus();
                }

            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            getData();
            pictureBoxShow.Image = facData.getImage(fac, "S");
        }

        private void gridFacturas_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridFacturas.CurrentRow.Cells["Id"].Value.ToString());
            txtNombre.Text = gridFacturas.CurrentRow.Cells["Nombre"].Value.ToString();
            if (!Convert.IsDBNull((byte[])gridFacturas.CurrentRow.Cells["Imagen"].Value))
            {
                MemoryStream strm = new MemoryStream((byte[])gridFacturas.CurrentRow.Cells["Imagen"].Value);
                pictureBoxShow.Image = (strm == null) ? null : Image.FromStream(strm);
                pictureBoxAdd.Image = (strm == null) ? null : Image.FromStream(strm);
            }
            else
            {
                pictureBoxShow.Image = null;
            }
        }

    }
}
