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
    public partial class frmAlumno : Form
    {
        Alumno alm = new Alumno();
        AlumnoData almData = new AlumnoData();
        private int Id;
        public frmAlumno(int seccionId)
        {
            alm.SeccionId = seccionId;
            InitializeComponent();
        }

        #region "Llenando grid de alumnos"
        private void getAlumnos()
        {
            DataSet ds = almData.getAlmunos(alm);
            gridAlumnos.DataSource = ds;
            gridAlumnos.DataMember = "Alumno";
        }
        #endregion

        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            alm.AlumnoId = Id;
            alm.Nombre = txtName.Text;
            alm.Carnet = txtCarnet.Text;
            alm.Telefono = txtTelefono.Text;
            alm.SeccionId = alm.SeccionId;
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtName.Text = string.Empty;
            txtCarnet.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }
        #endregion


        private void frmPersona_Load(object sender, EventArgs e)
        {
            getAlumnos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            almData.AlumnoCRUD(alm, "C");
            MessageBox.Show("Alumno creado con éxito");
            cleanFields();
            getAlumnos();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningún Alumno");
            }
            else
            {
                getData();
                almData.AlumnoCRUD(alm, "U");
                cleanFields();
                MessageBox.Show("El alumno se editó con éxito");
                getAlumnos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningún Alumno");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar a este Alumno?\n", "Eliminar Persona", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    almData.AlumnoCRUD(alm, "D");
                    cleanFields();
                    MessageBox.Show("El Alumno se eliminó con éxito");
                    getAlumnos();
                }
                else
                {
                    txtName.Focus();
                }

            }

        }

        private void gridPersonas_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridAlumnos.CurrentRow.Cells["Id"].Value.ToString());
            txtCarnet.Text = gridAlumnos.CurrentRow.Cells["Carnet"].Value.ToString();
            txtName.Text = gridAlumnos.CurrentRow.Cells["Nombre"].Value.ToString();
            txtTelefono.Text = gridAlumnos.CurrentRow.Cells["Telefono"].Value.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
