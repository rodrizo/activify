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
    public partial class frmComprobante : Form
    {
        Comprobante comp = new Comprobante();
        ComprobanteData compData = new ComprobanteData();
        private int Id;
        string imgLocation = "";

        public frmComprobante(int idGasto)
        {
            comp.GastoId = idGasto;
            InitializeComponent();
        }

        #region "Llenando grid de Comprobantes"
        private void getComprobantes()
        {
            DataSet ds = compData.getComprobante(comp);
            gridComprobantes.DataSource = ds;
            gridComprobantes.DataMember = "Comprobante";
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

            comp.ComprobanteId = Id;
            comp.Nombre = txtNombre.Text;
            comp.Imagen = images;
            comp.GastoId = comp.GastoId;
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
            getComprobantes();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            compData.ComprobanteCRUD(comp, "C");
            MessageBox.Show("Comprobante creada con éxito");
            cleanFields();
            getComprobantes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningun comprobante");
            }
            else
            {
                getData();
                compData.ComprobanteCRUD(comp, "U");
                cleanFields();
                MessageBox.Show("El comprobante se editó con éxito");
                getComprobantes();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningun Comprobante");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar este Comprobante?\n", "Eliminar Com", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    compData.ComprobanteCRUD(comp, "D");
                    cleanFields();
                    MessageBox.Show("El Comprobante se eliminó con éxito");
                    getComprobantes();
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
            pictureBoxShow.Image = compData.getImage(comp, "S");
        }

        private void gridFacturas_DoubleClick(object sender, EventArgs e)
        {
            if(gridComprobantes.CurrentRow.Cells["Id"].Value.ToString() is null ||
                 gridComprobantes.CurrentRow.Cells["Id"].Value.ToString().Equals(""))
            {
                Id = 0;
            }
            else
            {
                Id = Convert.ToInt32(gridComprobantes.CurrentRow.Cells["Id"].Value.ToString());
            }
            txtNombre.Text = gridComprobantes.CurrentRow.Cells["Nombre"].Value.ToString();
            if (gridComprobantes.CurrentRow.Cells["Imagen"].Value is null ||
                gridComprobantes.CurrentRow.Cells["Imagen"].Value.ToString().Equals(""))
            {
                pictureBoxShow.Image = null;
            }
            else
            {
                MemoryStream strm = new MemoryStream((byte[])gridComprobantes.CurrentRow.Cells["Imagen"].Value);
                pictureBoxShow.Image = (strm == null) ? null : Image.FromStream(strm);
                pictureBoxAdd.Image = (strm == null) ? null : Image.FromStream(strm);
            }
        }

    }
}
