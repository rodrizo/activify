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
    public partial class frmActividad : Form
    {
        Actividad act = new Actividad();
        ActividadData actData = new ActividadData();
        private int Id;

        #region "Llenando grid de actividades"
        private void getActividades(Actividad actividad)
        {
            DataSet ds = actData.getActividades(actividad);
            gridActividades.DataSource = ds;
            gridActividades.DataMember = "Actividad";
        }
        #endregion


        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            act.IdActividad = Id;
            act.Nombre = txtName.Text;
            act.Fecha = fecha.Value;
            act.Estimado = (txtEstimado.Text == string.Empty) ? 0 : Convert.ToDecimal(txtEstimado.Text);
            act.DetalleActividades = txtDetalle.Text;
            act.Observaciones = txtObser.Text;
            act.IdTipoActividad = Convert.ToInt32(cmbActividad.SelectedValue);
            act.IdComite = Convert.ToInt32(cmbComite.SelectedValue);
        }
        #endregion

        #region "Obteniendo y llenando combobox de tipo de actividades"
        private void getTipoActividad()
        {
            ComboData cmb = new ComboData();
            cmbActividad.DataSource = cmb.getTipoActividad();
            cmbActividad.DisplayMember = "Descripcion";
            cmbActividad.ValueMember = "TipoActividadId";
        }
        #endregion

        #region "Obteniendo y llenando combobox de tipo de comités"
        private void getComites()
        {
            ComboData cmb = new ComboData();
            cmbComite.DataSource = cmb.getComites();
            cmbComite.DisplayMember = "Nombre";
            cmbComite.ValueMember = "ComiteId";
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtName.Text = string.Empty;
            fecha.Text = string.Empty;
            txtEstimado.Text = string.Empty;
            txtDetalle.Text = string.Empty;
            txtObser.Text = string.Empty;
            cmbActividad.Text = string.Empty;
            cmbComite.Text = string.Empty;
            getTipoActividad();
            getComites();
        }
        #endregion
        public frmActividad()
        {
            InitializeComponent();
        }

        private void frmActividad_Load(object sender, EventArgs e)
        {
            getActividades(null);
            getTipoActividad();
            getComites();
        }

        private void gridActividades_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridActividades.CurrentRow.Cells["Id"].Value.ToString());
            txtName.Text = gridActividades.CurrentRow.Cells["Nombre"].Value.ToString();
            fecha.Value = DateTime.Parse(gridActividades.CurrentRow.Cells["Fecha"].Value.ToString());
            txtEstimado.Text = gridActividades.CurrentRow.Cells["Estimado"].Value.ToString();
            txtDetalle.Text = gridActividades.CurrentRow.Cells["Detalle"].Value.ToString();
            txtObser.Text = gridActividades.CurrentRow.Cells["Observaciones"].Value.ToString();
            cmbActividad.Text = gridActividades.CurrentRow.Cells["Actividad"].Value.ToString();
            cmbComite.Text = gridActividades.CurrentRow.Cells["Comite"].Value.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            actData.ActividadCRUD(act, "C");
            MessageBox.Show("Actividad creada con éxito");
            cleanFields();
            getActividades(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna actividad");
            }
            else
            {
                getData();
                actData.ActividadCRUD(act, "U");
                cleanFields();
                MessageBox.Show("La actividad se editó con éxito");
                getActividades(null);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna actividad");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar esta actividad?\n", "Eliminar Actividad", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    actData.ActividadCRUD(act, "D");
                    cleanFields();
                    MessageBox.Show("La actividad se eliminó con éxito");
                    getActividades(null);
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
            getActividades(act); //filtrar actividad por comité
        }

        private void btnGasto_Click(object sender, EventArgs e)
        {
            getData();
            if (act is null || act.IdActividad == 0)
            {
                MessageBox.Show("No se seleccionó ninguna actividad");
            }
            else
            {
                frmGasto frm = new frmGasto(act.IdActividad);
                frm.Show();
            }
        }
    }
}
