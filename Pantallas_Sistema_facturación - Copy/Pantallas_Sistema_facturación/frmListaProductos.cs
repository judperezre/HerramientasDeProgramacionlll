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
    public partial class frmListaProductos : MaterialForm
    {
        private Productos productos;

        public frmListaProductos()
        {
            InitializeComponent();
            productos = new Productos();
            ConfigurarMaterial();
            CargarProductos();
        }

        private void ConfigurarMaterial()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void CargarProductos()
        {
            try
            {
                DataTable dt = productos.ConsultarProductos();
                if (dt != null)
                {
                    dgvProductos.DataSource = dt;
                    ConfigurarDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;

            // Configurar encabezados
            if (dgvProductos.Columns.Contains("IdProducto"))
                dgvProductos.Columns["IdProducto"].HeaderText = "ID";
            if (dgvProductos.Columns.Contains("StrNombre"))
                dgvProductos.Columns["StrNombre"].HeaderText = "Nombre";
            if (dgvProductos.Columns.Contains("StrCodigo"))
                dgvProductos.Columns["StrCodigo"].HeaderText = "Código";
            if (dgvProductos.Columns.Contains("NumPrecioCompra"))
                dgvProductos.Columns["NumPrecioCompra"].HeaderText = "Precio Compra";
            if (dgvProductos.Columns.Contains("NumPrecioVenta"))
                dgvProductos.Columns["NumPrecioVenta"].HeaderText = "Precio Venta";
            if (dgvProductos.Columns.Contains("Categoria"))
                dgvProductos.Columns["Categoria"].HeaderText = "Categoría";
            if (dgvProductos.Columns.Contains("NumStock"))
                dgvProductos.Columns["NumStock"].HeaderText = "Stock";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmProductos frm = new frmProductos();
            frm.ShowDialog();
            CargarProductos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int idProducto = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value);
                frmProductos frm = new frmProductos(idProducto);
                frm.ShowDialog();
                CargarProductos();
            }
            else
            {
                MessageBox.Show("Seleccione un producto para editar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int idProducto = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value);
                    productos.IdProducto = idProducto;
                    string resultado = productos.EliminarProducto();
                    MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                try
                {
                    DataTable dt = productos.ConsultarProductos();
                    if (dt != null)
                    {
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = $"StrNombre LIKE '%{txtBuscar.Text}%' OR StrCodigo LIKE '%{txtBuscar.Text}%'";
                        dgvProductos.DataSource = dv;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                CargarProductos();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarProductos();
            txtBuscar.Text = "";
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }
    }
}