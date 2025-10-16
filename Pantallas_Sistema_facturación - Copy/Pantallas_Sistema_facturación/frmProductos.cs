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
    public partial class frmProductos : MaterialForm
    {
        private Productos productos;
        private CategoriasProductos categorias;
        public int IdProducto { get; set; } = 0;
        private bool esEdicion = false;

        public frmProductos()
        {
            InitializeComponent();
            productos = new Productos();
            categorias = new CategoriasProductos();
            ConfigurarMaterial();
        }

        public frmProductos(int idProducto)
        {
            InitializeComponent();
            this.IdProducto = idProducto;
            this.esEdicion = true;
            productos = new Productos();
            categorias = new CategoriasProductos();
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
            CargarCategorias();
            
            if (esEdicion && IdProducto > 0)
            {
                CargarDatosProducto();
                this.Text = "Editar Producto";
            }
            else
            {
                this.Text = "Nuevo Producto";
            }
        }

        private void CargarCategorias()
        {
            try
            {
                DataTable dt = categorias.ConsultarCategorias();
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Buscar el ComboBox de categorías en el formulario
                    ComboBox cmbCategoria = BuscarComboBoxCategoria();
                    if (cmbCategoria != null)
                    {
                        cmbCategoria.DataSource = dt;
                        cmbCategoria.DisplayMember = "StrDescripcion";
                        cmbCategoria.ValueMember = "IdCategoria";
                        cmbCategoria.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el ComboBox de categorías", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron categorías en la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ComboBox BuscarComboBoxCategoria()
        {
            // Buscar el ComboBox de categorías en todos los controles del formulario
            foreach (Control control in this.Controls)
            {
                ComboBox combo = BuscarComboBoxEnControl(control);
                if (combo != null) return combo;
            }
            return null;
        }

        private ComboBox BuscarComboBoxEnControl(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox)
                {
                    return control as ComboBox;
                }
                else if (control.HasChildren)
                {
                    ComboBox combo = BuscarComboBoxEnControl(control);
                    if (combo != null) return combo;
                }
            }
            return null;
        }

        private void CargarDatosProducto()
        {
            try
            {
                DataTable dt = productos.BuscarProducto(IdProducto);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    
                    // Buscar y llenar los controles con los datos
                    LlenarControlesConDatos(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarControlesConDatos(DataRow row)
        {
            // Buscar y llenar los controles de texto
            var textFields = this.Controls.OfType<MaterialSingleLineTextField>().ToList();
            foreach (Control control in this.Controls)
            {
                textFields.AddRange(BuscarTextFieldsEnControl(control));
            }

            // Asignar valores según el orden típico de los campos
            if (textFields.Count >= 5)
            {
                textFields[0].Text = row["StrNombre"].ToString(); // Nombre
                textFields[1].Text = row["StrCodigo"].ToString(); // Código
                textFields[2].Text = row["NumPrecioCompra"].ToString(); // Precio Compra
                textFields[3].Text = row["NumPrecioVenta"].ToString(); // Precio Venta
                textFields[4].Text = row["NumStock"].ToString(); // Stock
            }

            // Buscar y configurar el ComboBox de categorías
            ComboBox cmbCategoria = BuscarComboBoxCategoria();
            if (cmbCategoria != null && row["IdCategoria"] != DBNull.Value)
            {
                cmbCategoria.SelectedValue = Convert.ToInt32(row["IdCategoria"]);
            }

            // Buscar y llenar TextBox de detalles
            var textBoxes = this.Controls.OfType<TextBox>().ToList();
            foreach (Control control in this.Controls)
            {
                textBoxes.AddRange(BuscarTextBoxesEnControl(control));
            }

            if (textBoxes.Count > 0)
            {
                textBoxes[0].Text = row["StrDetalle"].ToString();
            }
        }

        private List<MaterialSingleLineTextField> BuscarTextFieldsEnControl(Control parent)
        {
            var textFields = new List<MaterialSingleLineTextField>();
            foreach (Control control in parent.Controls)
            {
                if (control is MaterialSingleLineTextField)
                {
                    textFields.Add(control as MaterialSingleLineTextField);
                }
                else if (control.HasChildren)
                {
                    textFields.AddRange(BuscarTextFieldsEnControl(control));
                }
            }
            return textFields;
        }

        private List<TextBox> BuscarTextBoxesEnControl(Control parent)
        {
            var textBoxes = new List<TextBox>();
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    textBoxes.Add(control as TextBox);
                }
                else if (control.HasChildren)
                {
                    textBoxes.AddRange(BuscarTextBoxesEnControl(control));
                }
            }
            return textBoxes;
        }

        private bool ValidarDatos()
        {
            var textFields = this.Controls.OfType<MaterialSingleLineTextField>().ToList();
            foreach (Control control in this.Controls)
            {
                textFields.AddRange(BuscarTextFieldsEnControl(control));
            }

            if (textFields.Count < 5)
            {
                MessageBox.Show("No se encontraron todos los campos necesarios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validar nombre
            if (string.IsNullOrEmpty(textFields[0].Text.Trim()))
            {
                MessageBox.Show("El nombre del producto es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textFields[0].Focus();
                return false;
            }

            // Validar código
            if (string.IsNullOrEmpty(textFields[1].Text.Trim()))
            {
                MessageBox.Show("El código del producto es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textFields[1].Focus();
                return false;
            }

            // Validar precios
            double precioCompra, precioVenta;
            if (!double.TryParse(textFields[2].Text.Trim(), out precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show("El precio de compra debe ser un número mayor a 0", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textFields[2].Focus();
                return false;
            }

            if (!double.TryParse(textFields[3].Text.Trim(), out precioVenta) || precioVenta <= 0)
            {
                MessageBox.Show("El precio de venta debe ser un número mayor a 0", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textFields[3].Focus();
                return false;
            }

            // Validar stock
            int stock;
            if (!int.TryParse(textFields[4].Text.Trim(), out stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un número mayor o igual a 0", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textFields[4].Focus();
                return false;
            }

            // Validar categoría
            ComboBox cmbCategoria = BuscarComboBoxCategoria();
            if (cmbCategoria == null || cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una categoría", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoria?.Focus();
                return false;
            }

            return true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                var textFields = this.Controls.OfType<MaterialSingleLineTextField>().ToList();
                foreach (Control control in this.Controls)
                {
                    textFields.AddRange(BuscarTextFieldsEnControl(control));
                }

                var textBoxes = this.Controls.OfType<TextBox>().ToList();
                foreach (Control control in this.Controls)
                {
                    textBoxes.AddRange(BuscarTextBoxesEnControl(control));
                }

                ComboBox cmbCategoria = BuscarComboBoxCategoria();

                // Asignar valores al objeto productos
                productos.IdProducto = IdProducto;
                productos.StrNombre = textFields[0].Text.Trim();
                productos.StrCodigo = textFields[1].Text.Trim();
                productos.NumPrecioCompra = Convert.ToDouble(textFields[2].Text.Trim());
                productos.NumPrecioVenta = Convert.ToDouble(textFields[3].Text.Trim());
                productos.NumStock = Convert.ToInt32(textFields[4].Text.Trim());
                productos.IdCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                productos.StrDetalle = textBoxes.Count > 0 ? textBoxes[0].Text.Trim() : "";
                productos.StrFoto = ""; // Campo opcional
                productos.StrUsuarioModifica = "Admin"; // Aquí deberías usar el usuario actual

                string resultado;
                if (esEdicion && IdProducto > 0)
                {
                    resultado = productos.ActualizarProducto();
                }
                else
                {
                    resultado = productos.InsertarProducto();
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

        private void pnlProducto_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField6_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
