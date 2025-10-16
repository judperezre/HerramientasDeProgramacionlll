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
using MaterialSkin;
using MaterialSkin.Controls;

namespace Pantallas_Sistema_facturación
{
    public partial class frmRoles : MaterialForm
    {
        private Roles roles;
        private int idRol = 0;
        private bool esEdicion = false;

        public frmRoles()
        {
            InitializeComponent();
            roles = new Roles();
            ConfigurarMaterial();
            ConfigurarFormulario();
        }

        public frmRoles(int idRol)
        {
            InitializeComponent();
            this.idRol = idRol;
            this.esEdicion = true;
            roles = new Roles();
            ConfigurarMaterial();
            ConfigurarFormulario();
            CargarDatosRol();
        }

        private void ConfigurarMaterial()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void ConfigurarFormulario()
        {
            if (esEdicion)
            {
                this.Text = "Editar Rol";
                btnGuardar.Text = "Actualizar";
            }
            else
            {
                this.Text = "Nuevo Rol";
                btnGuardar.Text = "Guardar";
            }
        }

        private void CargarDatosRol()
        {
            try
            {
                DataTable dt = roles.BuscarRol(idRol);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtDescripcion.Text = row["StrDescripcion"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del rol: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
            {
                MessageBox.Show("La descripción es obligatoria", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                roles.StrDescripcion = txtDescripcion.Text.Trim();

                string resultado;
                if (esEdicion)
                {
                    roles.IdRolEmpleado = idRol;
                    resultado = roles.ActualizarRol();
                }
                else
                {
                    resultado = roles.InsertarRol();
                }

                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (resultado.Contains("actualizados"))
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}