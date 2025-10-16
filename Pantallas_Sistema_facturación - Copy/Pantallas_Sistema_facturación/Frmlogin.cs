using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaDeNegocio;

namespace Pantallas_Sistema_facturación
{
    public partial class Frmlogin : Form
    {
        public Frmlogin()
        {
            InitializeComponent();
        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (TxtUsuario.Text != "" && TxtPassword.Text != string.Empty)
            {
                ValidarUsuario objValidar = new ValidarUsuario();

                objValidar.C_StrClave = TxtPassword.Text;
                objValidar.C_StrUsuario = TxtUsuario.Text;

                objValidar.ValidarUsuarios();

                if (objValidar.C_IdEmpleado != 0)
                {
                    MessageBox.Show("Datos de Verificación Validos ");
                    FrmPrincipal frmPrincipal = new FrmPrincipal();
                    this.Hide();
                    frmPrincipal.Show();
                }
                else
                {
                    MessageBox.Show("Usuarios y claves no encontrados");
                    TxtUsuario.Text = "";
                    TxtUsuario.Focus();
                    TxtPassword.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Debbes ingresar un usuario y una clave");
            }
        }

        private void Frmlogin_Load(object sender, EventArgs e)
        {

        }
    }

}
