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

namespace mineduc.Forms
{
    public partial class frmProfesor : Form
    {
        Profesor pro = new Profesor();
        ProfesorData proData = new ProfesorData();
        private int Id;

        #region "Llenando grid de profesores"
        private void getProfesores()
        {
            DataSet ds = proData.getProfesores();
            gridProfesores.DataSource = ds;
            gridProfesores.DataMember = "Profesor";
        }
        #endregion

        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            pro.ProfesorId = Id;
            pro.Nombre = txtName.Text;
            pro.Email = txtEmail.Text;
            pro.Telefono = txtTelefono.Text;
            pro.DPI = txtDPI.Text;
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtName.Text = string.Empty;
            getProfesores();
        }
        #endregion
        public frmProfesor()
        {
            InitializeComponent();
        }

        private void frmEscuela_Load(object sender, EventArgs e)
        {
            getProfesores();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            proData.ProfesorCRUD(pro, "C");
            MessageBox.Show("Profesor creado con éxito");
            cleanFields();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningún profesor");
            }
            else
            {
                getData();
                proData.ProfesorCRUD(pro, "U");
                cleanFields();
                MessageBox.Show("El profesor se editó con éxito");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningún profesor");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar a este profesor?\n", "Eliminar profesor", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    proData.ProfesorCRUD(pro, "D");
                    cleanFields();
                    MessageBox.Show("El profesor se eliminó con éxito");
                }
                else
                {
                    txtName.Focus();
                }
            }
        }

        private void gridProfesores_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridProfesores.CurrentRow.Cells["Id"].Value.ToString());
            txtName.Text = gridProfesores.CurrentRow.Cells["Nombre"].Value.ToString();
            txtEmail.Text = gridProfesores.CurrentRow.Cells["Email"].Value.ToString();
            txtTelefono.Text = gridProfesores.CurrentRow.Cells["Telefono"].Value.ToString();
            txtDPI.Text = gridProfesores.CurrentRow.Cells["DPI"].Value.ToString();
        }
    }
}
