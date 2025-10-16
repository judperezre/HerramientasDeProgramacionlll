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
    public partial class frmListaEmpleados : MaterialForm
    {
        private Empleados empleados;

        public frmListaEmpleados()
        {
            InitializeComponent();
            empleados = new Empleados();
            ConfigurarMaterial();
            CargarEmpleados();
        }

        private void ConfigurarMaterial()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void CargarEmpleados()
        {
            try
            {
                DataTable dt = empleados.ConsultarEmpleados();
                if (dt != null)
                {
                    dgvEmpleados.DataSource = dt;
                    ConfigurarDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.MultiSelect = false;
            dgvEmpleados.ReadOnly = true;
            dgvEmpleados.AllowUserToAddRows = false;
            dgvEmpleados.AllowUserToDeleteRows = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEmpleados frm = new frmEmpleados();
            frm.ShowDialog();
            CargarEmpleados();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                int idEmpleado = Convert.ToInt32(dgvEmpleados.SelectedRows[0].Cells["IdEmpleado"].Value);
                frmEmpleados frm = new frmEmpleados(idEmpleado);
                frm.ShowDialog();
                CargarEmpleados();
            }
            else
            {
                MessageBox.Show("Seleccione un empleado para editar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este empleado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int idEmpleado = Convert.ToInt32(dgvEmpleados.SelectedRows[0].Cells["IdEmpleado"].Value);
                    empleados.IdEmpleado = idEmpleado;
                    string resultado = empleados.EliminarEmpleado();
                    MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarEmpleados();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un empleado para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                try
                {
                    DataTable dt = empleados.ConsultarEmpleados();
                    if (dt != null)
                    {
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = $"strNombre LIKE '%{txtBuscar.Text}%' OR NumDocumento LIKE '%{txtBuscar.Text}%'";
                        dgvEmpleados.DataSource = dv;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                CargarEmpleados();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarEmpleados();
            txtBuscar.Text = "";
        }

        private void dgvEmpleados_DoubleClick(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }
    }
}