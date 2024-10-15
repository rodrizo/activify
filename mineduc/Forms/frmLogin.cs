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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Recoger los valores ingresados
            string username = txtName.Text;
            string password = txtPassword.Text;

            // Validar las credenciales
            if (username == "admin" && password == "admin")
            {
                // Si el login es exitoso
                MessageBox.Show("Login exitoso. Bienvenido, admin!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Aquí puedes redirigir a la siguiente ventana o formulario
                this.Hide();
                frmPrincipal principal = new frmPrincipal(); // Ejemplo de otro formulario
                principal.Show();
            }
            else
            {
                // Si el login falla
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
