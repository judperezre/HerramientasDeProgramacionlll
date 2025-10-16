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
    public partial class frmPruebaConexion : MaterialForm
    {
        public frmPruebaConexion()
        {
            InitializeComponent();
            ConfigurarMaterial();
        }

        private void ConfigurarMaterial()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void btnProbarClientes_Click(object sender, EventArgs e)
        {
            try
            {
                Clientes clientes = new Clientes();
                DataTable dt = clientes.ConsultarClientes();
                
                if (dt != null)
                {
                    dgvResultados.DataSource = dt;
                    lblResultado.Text = $"Clientes encontrados: {dt.Rows.Count}";
                    lblResultado.ForeColor = Color.Green;
                }
                else
                {
                    lblResultado.Text = "No se pudieron cargar los clientes";
                    lblResultado.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Error: " + ex.Message;
                lblResultado.ForeColor = Color.Red;
                MessageBox.Show("Error detallado: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProbarEmpleados_Click(object sender, EventArgs e)
        {
            try
            {
                Empleados empleados = new Empleados();
                DataTable dt = empleados.ConsultarEmpleados();
                
                if (dt != null)
                {
                    dgvResultados.DataSource = dt;
                    lblResultado.Text = $"Empleados encontrados: {dt.Rows.Count}";
                    lblResultado.ForeColor = Color.Green;
                }
                else
                {
                    lblResultado.Text = "No se pudieron cargar los empleados";
                    lblResultado.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Error: " + ex.Message;
                lblResultado.ForeColor = Color.Red;
                MessageBox.Show("Error detallado: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProbarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                Productos productos = new Productos();
                DataTable dt = productos.ConsultarProductos();
                
                if (dt != null)
                {
                    dgvResultados.DataSource = dt;
                    lblResultado.Text = $"Productos encontrados: {dt.Rows.Count}";
                    lblResultado.ForeColor = Color.Green;
                }
                else
                {
                    lblResultado.Text = "No se pudieron cargar los productos";
                    lblResultado.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Error: " + ex.Message;
                lblResultado.ForeColor = Color.Red;
                MessageBox.Show("Error detallado: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsertarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                Clientes cliente = new Clientes();
                cliente.StrNombre = "Cliente de Prueba";
                cliente.NumDocumento = 123456789;
                cliente.StrDireccion = "Dirección de prueba";
                cliente.StrTelefono = "123456789";
                cliente.StrEmail = "prueba@test.com";
                cliente.StrUsuarioModifica = "Admin";

                string resultado = cliente.InsertarCliente();
                lblResultado.Text = resultado;
                
                if (resultado.Contains("actualizados"))
                {
                    lblResultado.ForeColor = Color.Green;
                    btnProbarClientes_Click(sender, e); // Actualizar la lista
                }
                else
                {
                    lblResultado.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Error: " + ex.Message;
                lblResultado.ForeColor = Color.Red;
                MessageBox.Show("Error detallado: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}