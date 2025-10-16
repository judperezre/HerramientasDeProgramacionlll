using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaDeNegocio;

namespace Pantallas_Sistema_facturación
{
    public partial class frmListaClientes : Form
    {
        private Clientes clientes;

        public frmListaClientes()
        {
            InitializeComponent();
            clientes = new Clientes();
        }

        private void frmListaClientes_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            llenar_grid();
        }

        public void llenar_grid() 
        {
            try
            {
                // Limpiar el DataGridView
                dgClientes.DataSource = null;
                dgClientes.Rows.Clear();

                // Obtener datos reales de la base de datos
                DataTable dt = clientes.ConsultarClientes();
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Si el DataGridView tiene columnas definidas manualmente, llenar fila por fila
                    if (dgClientes.Columns.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            dgClientes.Rows.Add(
                                row["IdCliente"],
                                row["StrNombre"],
                                row["NumDocumento"],
                                row["StrTelefono"]
                            );
                        }
                    }
                    else
                    {
                        // Si no hay columnas definidas, usar DataSource
                        dgClientes.DataSource = dt;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron clientes en la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e) 
        {
            frmEditarCliente Cliente = new frmEditarCliente();
            Cliente.IdCliente = 0;
            Cliente.ShowDialog();
            llenar_grid(); // Actualizar la lista después de agregar
        }

        private void dgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgClientes.Columns[e.ColumnIndex].Name == "btnBorrar")
            {
                var clienteId = dgClientes.Rows[e.RowIndex].Cells[0].Value.ToString();
                DialogResult dialogResult = MessageBox.Show(
                    $"¿Estás seguro de que quieres borrar el cliente con ID: {clienteId}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        clientes.IdCliente = int.Parse(clienteId);
                        string resultado = clientes.EliminarCliente();
                        MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        llenar_grid(); // Actualizar la lista después de eliminar
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (dgClientes.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int posActual = e.RowIndex;
                var clienteId = dgClientes.Rows[posActual].Cells[0].Value.ToString();
                frmEditarCliente Cliente = new frmEditarCliente();
                Cliente.IdCliente = int.Parse(clienteId);
                Cliente.ShowDialog();
                llenar_grid(); // Actualizar la lista después de editar
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e) 
        {
            this.Close();
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            frmActualizarCliente frm = new frmActualizarCliente();
            frm.ShowDialog();
            llenar_grid(); // Actualizar la lista después de agregar
        }
    }
}
