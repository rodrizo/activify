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
    public partial class frmEscuela : Form
    {
        Escuela esc = new Escuela();
        EscuelaData escData = new EscuelaData();
        private int Id;

        #region "Llenando grid de escuelas"
        private void getEscuelas()
        {
            DataSet ds = escData.getEscuelas();
            gridEscuelas.DataSource = ds;
            gridEscuelas.DataMember = "Escuela";
        }
        #endregion

        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            esc.IdEscuela = Id;
            esc.Nombre = txtName.Text;
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtName.Text = string.Empty;
            getEscuelas();
        }
        #endregion
        public frmEscuela()
        {
            InitializeComponent();
        }

        private void frmEscuela_Load(object sender, EventArgs e)
        {
            getEscuelas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            escData.EscuelaCRUD(esc, "C");
            MessageBox.Show("Escuela creada con éxito");
            cleanFields();
        }

        private void gridEscuelas_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridEscuelas.CurrentRow.Cells["Id"].Value.ToString());
            txtName.Text = gridEscuelas.CurrentRow.Cells["Nombre"].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna escuela");
            }
            else
            {
                getData();
                escData.EscuelaCRUD(esc, "U");
                cleanFields();
                MessageBox.Show("La escuela se editó con éxito");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna escuela");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar esta escuela?\n", "Eliminar Escuela", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    escData.EscuelaCRUD(esc, "D");
                    cleanFields();
                    MessageBox.Show("La escuela se eliminó con éxito");
                }
                else
                {
                    txtName.Focus();
                }
            }
        }
    }
}
