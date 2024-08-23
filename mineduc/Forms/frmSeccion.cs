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
    public partial class frmSeccion : Form
    {
        Seccion cm = new Seccion();
        SeccionData cmData = new SeccionData();
        private int Id;

        #region "Llenando grid de secciones"
        private void getSecciones(Seccion sec)
        {
            DataSet ds = cmData.getSecciones(sec);
            gridSecciones.DataSource = ds;
            gridSecciones.DataMember = "Seccion";
        }
        #endregion

        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            cm.SeccionId = Id;
            cm.Grado = txtGrado.Text;
            cm.Aula = txtAula.Text;
            cm.ProfesorId = Convert.ToInt32(cmbProfesores.SelectedValue);
        }
        #endregion

        #region "Obteniendo y llenando combobox de profesores"
        private void getProfesores()
        {
            ComboData cmb = new ComboData();
            cmbProfesores.DataSource = cmb.getProfesores();
            cmbProfesores.DisplayMember = "Nombre";
            cmbProfesores.ValueMember = "ProfesorId";
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtGrado.Text = string.Empty;
            txtAula.Text = string.Empty;
            cmbProfesores.Text = string.Empty;
            getProfesores();
        }
        #endregion

        public frmSeccion()
        {
            InitializeComponent();
        }

        private void frmSeccion_Load(object sender, EventArgs e)
        {
            getSecciones(null);
            getProfesores();
        }

        private void gridSeccions_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridSecciones.CurrentRow.Cells["Id"].Value.ToString());
            txtGrado.Text = gridSecciones.CurrentRow.Cells["Grado"].Value.ToString();
            txtAula.Text = gridSecciones.CurrentRow.Cells["Aula"].Value.ToString();
            cmbProfesores.Text = gridSecciones.CurrentRow.Cells["Profesor"].Value.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            cmData.SeccionCRUD(cm, "C");
            MessageBox.Show("Sección creada con éxito");
            cleanFields();
            getSecciones(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna sección");
            }
            else
            {
                getData();
                cmData.SeccionCRUD(cm, "U");
                cleanFields();
                MessageBox.Show("La sección se editó con éxito");
                getSecciones(null);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna sección");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar esta sección?\n", "Eliminar sección", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    cmData.SeccionCRUD(cm, "D");
                    cleanFields();
                    MessageBox.Show("La sección se eliminó con éxito");
                    getSecciones(null);
                }
                else
                {
                    txtGrado.Focus();
                }

            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            getData();
            getSecciones(cm); //filtrar comité por escuela
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnPersona_Click(object sender, EventArgs e)
        {
            getData();
            if(cm is null || cm.SeccionId == 0)
            {
                MessageBox.Show("No se seleccionó ninguna sección");
            }
            else
            {
                frmAlumno frm = new frmAlumno(cm.SeccionId);
                frm.Show();
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            getData();
            if (cm is null || cm.SeccionId == 0)
            {
                MessageBox.Show("No se seleccionó ningún comité");
            }
            else
            {
                frmResultados frm = new frmResultados(cm.SeccionId);
                frm.Show();
            }
        }
    }
}
