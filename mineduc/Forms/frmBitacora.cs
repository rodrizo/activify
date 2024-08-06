using mineduc.Controllers;
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
    public partial class frmBitacora : Form
    {
        BitacoraData bitData = new BitacoraData();
        string actionBit;

        #region "Llenando grid de bitácora"
        private void getBitacora()
        {
            DataSet ds = bitData.getBitacora(actionBit);
            gridBitacoras.DataSource = ds;
            gridBitacoras.DataMember = "Bitacora";
        }
        #endregion


        #region "Obteniendo y llenando combobox de bitácoras"
        private void getActions()
        {
            ComboData cmb = new ComboData();
            cmbAcciones.DataSource = cmb.getAction();
            cmbAcciones.DisplayMember = "Action";
            cmbAcciones.ValueMember = "Action";
        }
        #endregion

        #region "Obteniendo los datos del combo box"
        private void getData()
        {
            this.actionBit = cmbAcciones.SelectedValue.ToString();
        }
        #endregion
        public frmBitacora()
        {
            InitializeComponent();
        }

        private void frmBitacora_Load(object sender, EventArgs e)
        {
            getBitacora();
            getActions();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            getData();
            bitData.getBitacora(this.actionBit);
            getBitacora();
        }
    }
}
