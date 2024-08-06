using mineduc.Controllers;
using mineduc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace mineduc.Forms
{
    public partial class frmComite : Form
    {
        Comite cm = new Comite();
        ComiteData cmData = new ComiteData();
        private int Id;

        #region "Llenando grid de comités"
        private void getComites(Comite comite)
        {
            DataSet ds = cmData.getComites(comite);
            gridComites.DataSource = ds;
            gridComites.DataMember = "Comite";
        }
        #endregion


        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            cm.IdComite = Id;
            cm.Nombre = txtName.Text;
            cm.Fondo = (txtFondo.Text == string.Empty) ? 0 : Convert.ToDecimal(txtFondo.Text);
            cm.IdEscuela = Convert.ToInt32(cmbEscuelas.SelectedValue);
        }
        #endregion

        #region "Obteniendo y llenando combobox de escuelas"
        private void getEscuelas()
        {
            ComboData cmb = new ComboData();
            cmbEscuelas.DataSource = cmb.getEscuelas();
            cmbEscuelas.DisplayMember = "Nombre";
            cmbEscuelas.ValueMember = "EscuelaId";
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtName.Text = string.Empty;
            txtFondo.Text = string.Empty;
            cmbEscuelas.Text = string.Empty;
            getEscuelas();
        }
        #endregion

        public frmComite()
        {
            InitializeComponent();
        }

        private void frmComite_Load(object sender, EventArgs e)
        {
            getComites(null);
            getEscuelas();
        }

        private void gridComites_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridComites.CurrentRow.Cells["Id"].Value.ToString());
            txtName.Text = gridComites.CurrentRow.Cells["Nombre"].Value.ToString();
            txtFondo.Text = gridComites.CurrentRow.Cells["Fondo"].Value.ToString();
            cmbEscuelas.Text = gridComites.CurrentRow.Cells["Escuela"].Value.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            cmData.ComiteCRUD(cm, "C");
            MessageBox.Show("Comité creado con éxito");
            cleanFields();
            getComites(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningún comité");
            }
            else
            {
                getData();
                cmData.ComiteCRUD(cm, "U");
                cleanFields();
                MessageBox.Show("El comité se editó con éxito");
                getComites(null);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningún comité");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar este comité?\n", "Eliminar Comité", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    cmData.ComiteCRUD(cm, "D");
                    cleanFields();
                    MessageBox.Show("El comité se eliminó con éxito");
                    getComites(null);
                }
                else
                {
                    txtName.Focus();
                }

            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            getData();
            getComites(cm); //filtrar comité por escuela
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnPersona_Click(object sender, EventArgs e)
        {
            getData();
            if(cm is null || cm.IdComite == 0)
            {
                MessageBox.Show("No se seleccionó ningún comité");
            }
            else
            {
                frmPersona frm = new frmPersona(cm.IdComite);
                frm.Show();
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            getData();
            if (cm is null || cm.IdComite == 0)
            {
                MessageBox.Show("No se seleccionó ningún comité");
            }
            else
            {
                frmResultados frm = new frmResultados(cm.IdComite);
                frm.Show();
            }
        }
    }
}
