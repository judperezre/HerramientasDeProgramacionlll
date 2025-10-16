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
    public partial class frmClienteCompleto : MaterialForm
    {
        private Clientes clientes;
        private int idCliente = 0;
        private bool esEdicion = false;

        public frmClienteCompleto()
        {
            InitializeComponent();
            clientes = new Clientes();
            ConfigurarMaterial();
            ConfigurarFormulario();
        }

        public frmClienteCompleto(int idCliente)
        {
            InitializeComponent();
            this.idCliente = idCliente;
            this.esEdicion = true;
            clientes = new Clientes();
            ConfigurarMaterial();
            ConfigurarFormulario();
            CargarDatosCliente();
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
                this.Text = "Editar Cliente";
                btnGuardar.Text = "Actualizar";
            }
            else
            {
                this.Text = "Nuevo Cliente";
                btnGuardar.Text = "Guardar";
            }
        }

        private void CargarDatosCliente()
        {
            try
            {
                DataTable dt = clientes.BuscarCliente(idCliente);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtNombre.Text = row["StrNombre"].ToString();
                    txtDocumento.Text = row["NumDocumento"].ToString();
                    txtDireccion.Text = row["StrDireccion"].ToString();
                    txtTelefono.Text = row["StrTelefono"].ToString();
                    txtEmail.Text = row["StrEmail"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                MessageBox.Show("El nombre es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDocumento.Text.Trim()))
            {
                MessageBox.Show("El documento es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            long documento;
            if (!long.TryParse(txtDocumento.Text.Trim(), out documento))
            {
                MessageBox.Show("El documento debe ser numérico", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                clientes.IdCliente = idCliente;
                clientes.StrNombre = txtNombre.Text.Trim();
                clientes.NumDocumento = Convert.ToInt64(txtDocumento.Text.Trim());
                clientes.StrDireccion = txtDireccion.Text.Trim();
                clientes.StrTelefono = txtTelefono.Text.Trim();
                clientes.StrEmail = txtEmail.Text.Trim();
                clientes.StrUsuarioModifica = "Admin"; // Aquí deberías usar el usuario actual

                string resultado;
                if (esEdicion)
                {
                    resultado = clientes.ActualizarCliente();
                }
                else
                {
                    resultado = clientes.InsertarCliente();
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