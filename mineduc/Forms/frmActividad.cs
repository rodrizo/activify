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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace mineduc.Forms
{
    public partial class frmActividad : Form
    {
        Actividad act = new Actividad();
        ActividadData actData = new ActividadData();
        private int Id;
        private int seccionId;

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
            act.ActividadId = Id;
            act.Nombre = txtName.Text;
            act.Fecha = fecha.Value;
            act.Monto = (txtMonto.Text == string.Empty) ? 0 : Convert.ToDecimal(txtMonto.Text);
            act.Observaciones = txtObser.Text;
            act.TipoActividadId = Convert.ToInt32(cmbActividad.SelectedValue);
            act.SeccionId = Convert.ToInt32(cmbSeccion.SelectedValue);
            act.AlumnoId = Convert.ToInt32(cmbAlumno.SelectedValue);
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

        #region "Obteniendo y llenando combobox de tipo de secciones"
        private void getSecciones()
        {
            ComboData cmb = new ComboData();
            cmbSeccion.DataSource = cmb.getSecciones();
            cmbSeccion.DisplayMember = "Nombre";
            cmbSeccion.ValueMember = "SeccionId";
        }
        #endregion

        #region "Obteniendo y llenando combobox de tipo de alumnos"
        private void getAlumnos(int seccion)
        {
            ComboData cmb = new ComboData();
            cmbAlumno.DataSource = cmb.getAlumnos(seccion);
            cmbAlumno.DisplayMember = "Nombre";
            cmbAlumno.ValueMember = "AlumnoId";
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtName.Text = string.Empty;
            fecha.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtObser.Text = string.Empty;
            cmbActividad.Text = string.Empty;
            cmbSeccion.Text = string.Empty;
            cmbAlumno.Text = string.Empty;
            getTipoActividad();
            getSecciones();
            getAlumnos(0);
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
            getSecciones();
            getAlumnos(0);
        }

        private void gridActividades_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridActividades.CurrentRow.Cells["Id"].Value.ToString());
            txtName.Text = gridActividades.CurrentRow.Cells["Nombre"].Value.ToString();
            fecha.Value = DateTime.Parse(gridActividades.CurrentRow.Cells["Fecha"].Value.ToString());
            txtMonto.Text = gridActividades.CurrentRow.Cells["Monto"].Value.ToString();
            txtObser.Text = gridActividades.CurrentRow.Cells["Observaciones"].Value.ToString();
            cmbActividad.Text = gridActividades.CurrentRow.Cells["Actividad"].Value.ToString();
            cmbSeccion.Text = gridActividades.CurrentRow.Cells["Seccion"].Value.ToString();
            cmbAlumno.Text = gridActividades.CurrentRow.Cells["Alumno Responsable"].Value.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            actData.ActividadCRUD(act, "C");
            MessageBox.Show("Actividad creada con éxito");
            cleanFields();
            getActividades(null);
            cmbAlumno.Text = string.Empty;
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
                cmbAlumno.Text = string.Empty;
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
                    cmbAlumno.Text = string.Empty;
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
            getActividades(act); //filtrar actividad por sección
        }

        private void btnGasto_Click(object sender, EventArgs e)
        {
            getData();
            if (act is null || act.ActividadId == 0)
            {
                MessageBox.Show("No se seleccionó ninguna actividad");
            }
            else
            {
                frmGasto frm = new frmGasto(act.ActividadId);
                frm.Show();
            }
        }

        private void cmbSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {

            int seccion;
            seccion = 0;

            if (int.TryParse(cmbSeccion.SelectedValue.ToString(), out seccion))
            {
                seccion = Convert.ToInt32(cmbSeccion.SelectedValue.ToString());
                cmbAlumno.Text = string.Empty;
                getAlumnos(seccion);
            }

        }
    }
}
