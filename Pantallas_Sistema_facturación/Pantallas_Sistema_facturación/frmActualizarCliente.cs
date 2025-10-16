using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pantallas_Sistema_facturación
{
    public partial class frmActualizarCliente : Form
    {
        // Constructor del formulario
        public frmActualizarCliente()
        {
            InitializeComponent();
        }

        // Manejador del evento Load del formulario
        private void frmProductos_Load(object sender, EventArgs e)
        {
            // Aquí puedes llamar a métodos para cargar datos iniciales,
            // como las categorías en el ComboBox.
            CargarCategorias();
        }

        // Método para cargar datos en el ComboBox de categorías
        private void CargarCategorias()
        {
            // Ejemplo de cómo cargar categorías desde una base de datos
            // SqlConnection conexion = new SqlConnection("CadenaDeConexion");
            // SqlCommand comando = new SqlCommand("SELECT NombreCategoria FROM Categorias", conexion);
            // SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            // DataTable dtCategorias = new DataTable();
            // adaptador.Fill(dtCategorias);

            // cmbCategoria.DataSource = dtCategorias;
            // cmbCategoria.DisplayMember = "NombreCategoria";
            // cmbCategoria.ValueMember = "IdCategoria"; // Asume que la tabla tiene una columna IdCategoria
        }

        // Manejador de eventos para el botón 'Actualizar'
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Producto actualizado exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Manejador de eventos para el botón 'Salir'
        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual
            this.Close();
        }

        // Método para limpiar todos los campos del formulario


        private void frmProductos_Load_1(object sender, EventArgs e)
        {

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
