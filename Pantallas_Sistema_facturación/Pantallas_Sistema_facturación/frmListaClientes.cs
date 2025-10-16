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


namespace Pantallas_Sistema_facturación
{
    public partial class frmListaClientes : Form
    {
        public frmListaClientes()
        {
            InitializeComponent();
        }

        private void frmListaClientes_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

        }
        public void llenar_grid() 
        {
            for (int i = 0; i < 10; i++)
            {
                dgClientes.Rows.Add(i,$"Nombre {i} Apellido1 Apellido2", $"{i * 12345}", $"{i * 12345}");
            }
        }
        private void BtnNuevo_Click(object sender, EventArgs e) 
        {
            frmEditarCliente Cliente = new frmEditarCliente();
            Cliente.IdCliente = 0;
            Cliente.ShowDialog();
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
                    MessageBox.Show($"Cliente con ID {clienteId} borrado.", "Información");
                }
            }

            else if (dgClientes.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int posActual = e.RowIndex;
                var clienteId = dgClientes.Rows[posActual].Cells[0].Value.ToString();
                frmEditarCliente Cliente = new frmEditarCliente();
                Cliente.IdCliente = int.Parse(clienteId);
                Cliente.ShowDialog();

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
        }
    }
}
