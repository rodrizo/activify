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
    public partial class frmGasto : Form
    {
        Gasto gas = new Gasto();
        GastoData gasData = new GastoData();
        private int Id;

        public frmGasto(int idActividad)
        {
            gas.IdActividad = idActividad;
            InitializeComponent();
        }

        #region "Llenando grid de gastos"
        private void getGastos()
        {
            DataSet ds = gasData.getGastos(gas);
            gridGastos.DataSource = ds;
            gridGastos.DataMember = "Gasto";
        }
        #endregion

        #region "Obteniendo los datos ingresados en el form"
        private void getData()
        {
            gas.IdGasto = Id;
            gas.Descripcion = txtDescripcion.Text;
            gas.Monto = (txtMonto.Text == string.Empty) ? 0 : Convert.ToDecimal(txtMonto.Text);
            gas.IdActividad = gas.IdActividad;
        }
        #endregion

        #region "Limpiando los campos"
        private void cleanFields()
        {
            Id = 0;
            txtDescripcion.Text = string.Empty;
            txtMonto.Text = string.Empty;
        }
        #endregion
        private void frmGasto_Load(object sender, EventArgs e)
        {
            getGastos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            gasData.GastoCRUD(gas, "C");
            MessageBox.Show("Gasto creado con éxito");
            cleanFields();
            getGastos();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningún gasto");
            }
            else
            {
                getData();
                gasData.GastoCRUD(gas, "U");
                cleanFields();
                MessageBox.Show("El gasto se editó con éxito");
                getGastos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("No se seleccionó ningún gasto");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dg = MessageBox.Show("¿Desea eliminar este gasto?\n", "Eliminar Gasto", buttons);
                if (dg == DialogResult.Yes)
                {
                    getData();
                    gasData.GastoCRUD(gas, "D");
                    cleanFields();
                    MessageBox.Show("El gasto se eliminó con éxito");
                    getGastos();
                }
                else
                {
                    txtDescripcion.Focus();
                }

            }
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            getData();
            if (gas is null || gas.IdGasto == 0)
            {
                MessageBox.Show("No se seleccionó ningún gasto");
            }
            else
            {
                frmFactura frm = new frmFactura(gas.IdGasto);
                frm.Show();
            }
        }

        private void gridGastos_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(gridGastos.CurrentRow.Cells["Id"].Value.ToString());
            txtDescripcion.Text = gridGastos.CurrentRow.Cells["Descripcion"].Value.ToString();
            txtMonto.Text = gridGastos.CurrentRow.Cells["Monto"].Value.ToString();
        }
    }
}
