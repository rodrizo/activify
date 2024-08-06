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
    public partial class frmResultados : Form
    {
        Comite com = new Comite();
        ComiteData comData = new ComiteData();

        public frmResultados(int idComite)
        {
            com.IdComite = idComite;
            InitializeComponent();
        }

        #region "Llenando grid de resultados"
        private void getPersonas()
        {
            DataSet ds = comData.getResultados(com, "M");
            gridResults.DataSource = ds;
            gridResults.DataMember = "Comite";
        }
        #endregion

        private void frmResultados_Load(object sender, EventArgs e)
        {
            getPersonas();
        }
    }
}
