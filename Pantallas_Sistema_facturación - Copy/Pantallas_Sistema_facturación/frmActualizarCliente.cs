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
    public partial class frmActualizarCliente : MaterialForm
    {
        private Clientes clientes;
        public int IdCliente { get; set; } = 0;
        private bool esEdicion = false;

        public frmActualizarCliente()
        {
            InitializeComponent();
            clientes = new Clientes();
            ConfigurarMaterial();
        }

        public frmActualizarCliente(int idCliente)
        {
            InitializeComponent();
            this.IdCliente = idCliente;
            this.esEdicion = true;
            clientes = new Clientes();
            ConfigurarMaterial();
        }

        private void ConfigurarMaterial()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            if (esEdicion && IdCliente > 0)
            {
                CargarDatosCliente();
                this.Text = "Editar Cliente";
            }
            else
            {
                this.Text = "Nuevo Cliente";
            }
        }

        private void CargarDatosCliente()
        {
            try
            {
                DataTable dt = clientes.BuscarCliente(IdCliente);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    
                    // Cargar datos en los controles (ya declarados en Designer.cs)
                    txtIdCliente.Text = row["IdCliente"].ToString();
                    txtNombreCliente.Text = row["StrNombre"].ToString();
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
            // Validar nombre
            if (string.IsNullOrEmpty(txtNombreCliente.Text.Trim()))
            {
                MessageBox.Show("El nombre es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCliente.Focus();
                return false;
            }

            // Validar documento
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                // Obtener valores de los controles
                clientes.IdCliente = IdCliente;
                clientes.StrNombre = txtNombreCliente.Text.Trim();
                clientes.NumDocumento = Convert.ToInt64(txtDocumento.Text.Trim());
                clientes.StrDireccion = txtDireccion.Text.Trim();
                clientes.StrTelefono = txtTelefono.Text.Trim();
                clientes.StrEmail = txtEmail.Text.Trim();
                clientes.StrUsuarioModifica = "Admin"; // Aquí deberías usar el usuario actual

                string resultado;
                if (esEdicion && IdCliente > 0)
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProductos_Load_1(object sender, EventArgs e)
        {
            frmProductos_Load(sender, e);
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField3_Click(object sender, EventArgs e)
        {

        }

        private void pnlContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
