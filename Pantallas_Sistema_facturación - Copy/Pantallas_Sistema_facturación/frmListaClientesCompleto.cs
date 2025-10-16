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
    public partial class frmListaClientesCompleto : MaterialForm
    {
        private Clientes clientes;

        public frmListaClientesCompleto()
        {
            InitializeComponent();
            clientes = new Clientes();
            ConfigurarMaterial();
            CargarClientes();
        }

        private void ConfigurarMaterial()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void CargarClientes()
        {
            try
            {
                DataTable dt = clientes.ConsultarClientes();
                if (dt != null)
                {
                    dgvClientes.DataSource = dt;
                    ConfigurarDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;

            // Configurar encabezados
            if (dgvClientes.Columns.Contains("IdCliente"))
                dgvClientes.Columns["IdCliente"].HeaderText = "ID";
            if (dgvClientes.Columns.Contains("StrNombre"))
                dgvClientes.Columns["StrNombre"].HeaderText = "Nombre";
            if (dgvClientes.Columns.Contains("NumDocumento"))
                dgvClientes.Columns["NumDocumento"].HeaderText = "Documento";
            if (dgvClientes.Columns.Contains("StrDireccion"))
                dgvClientes.Columns["StrDireccion"].HeaderText = "Dirección";
            if (dgvClientes.Columns.Contains("StrTelefono"))
                dgvClientes.Columns["StrTelefono"].HeaderText = "Teléfono";
            if (dgvClientes.Columns.Contains("StrEmail"))
                dgvClientes.Columns["StrEmail"].HeaderText = "Email";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmClienteCompleto frm = new frmClienteCompleto();
            frm.ShowDialog();
            CargarClientes();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int idCliente = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["IdCliente"].Value);
                frmClienteCompleto frm = new frmClienteCompleto(idCliente);
                frm.ShowDialog();
                CargarClientes();
            }
            else
            {
                MessageBox.Show("Seleccione un cliente para editar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int idCliente = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["IdCliente"].Value);
                    clientes.IdCliente = idCliente;
                    string resultado = clientes.EliminarCliente();
                    MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarClientes();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                try
                {
                    DataTable dt = clientes.ConsultarClientes();
                    if (dt != null)
                    {
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = $"StrNombre LIKE '%{txtBuscar.Text}%' OR NumDocumento LIKE '%{txtBuscar.Text}%'";
                        dgvClientes.DataSource = dv;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                CargarClientes();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarClientes();
            txtBuscar.Text = "";
        }

        private void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }
    }
}