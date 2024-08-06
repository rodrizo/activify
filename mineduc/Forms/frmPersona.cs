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
    public partial class frmPersona : Form
    {
        Persona per = new Persona();
        PersonaData perData = new PersonaData();
        private int Id;
        public frmPersona(int comiteId)
        {
            per.IdComite = comiteId;
            InitializeComponent();
        }

        #region "Llenando grid de personas"
        private void getPersonas()
        {
            DataSet ds = perData.getPersonas(per);
            gridPersonas.DataSource = ds;
            gridPersonas.DataMember = "Persona";
        }
        #endregion

        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            per.IdPersona = Id;
            per.Nombre = txtName.Text;
            per.DPI = txtDpi.Text;
            per.IdComite = per.IdComite;
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtName.Text = string.Empty;
            txtDpi.Text = string.Empty;
        }
        #endregion


        private void frmPersona_Load(object sender, EventArgs e)
        {
            getPersonas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            perData.PersonaCRUD(per, "C");
            MessageBox.Show("Persona creada con éxito");
            cleanFields();
            getPersonas();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna persona");
            }
            else
            {
                getData();
                perData.PersonaCRUD(per, "U");
                cleanFields();
                MessageBox.Show("La persona se editó con éxito");
                getPersonas();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ninguna persona");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar a esta persona?\n", "Eliminar Persona", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    perData.PersonaCRUD(per, "D");
                    cleanFields();
                    MessageBox.Show("La persona se eliminó con éxito");
                    getPersonas();
                }
                else
                {
                    txtName.Focus();
                }

            }

        }

        private void gridPersonas_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridPersonas.CurrentRow.Cells["Id"].Value.ToString());
            txtName.Text = gridPersonas.CurrentRow.Cells["Nombre"].Value.ToString();
            txtDpi.Text = gridPersonas.CurrentRow.Cells["Dpi"].Value.ToString();
        }
    }
}
