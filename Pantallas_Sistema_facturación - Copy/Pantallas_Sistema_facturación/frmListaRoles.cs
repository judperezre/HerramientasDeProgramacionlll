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
    public partial class frmListaRoles : MaterialForm
    {
        private Roles roles;

        public frmListaRoles()
        {
            InitializeComponent();
            roles = new Roles();
            ConfigurarMaterial();
            CargarRoles();
        }

        private void ConfigurarMaterial()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void CargarRoles()
        {
            try
            {
                DataTable dt = roles.ConsultarRoles();
                if (dt != null)
                {
                    dgvRoles.DataSource = dt;
                    ConfigurarDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.MultiSelect = false;
            dgvRoles.ReadOnly = true;
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.AllowUserToDeleteRows = false;
            
            if (dgvRoles.Columns.Contains("IdRolEmpleado"))
                dgvRoles.Columns["IdRolEmpleado"].HeaderText = "ID";
            if (dgvRoles.Columns.Contains("StrDescripcion"))
                dgvRoles.Columns["StrDescripcion"].HeaderText = "Descripción";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRoles frm = new frmRoles();
            frm.ShowDialog();
            CargarRoles();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0)
            {
                int idRol = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["IdRolEmpleado"].Value);
                frmRoles frm = new frmRoles(idRol);
                frm.ShowDialog();
                CargarRoles();
            }
            else
            {
                MessageBox.Show("Seleccione un rol para editar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este rol?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int idRol = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["IdRolEmpleado"].Value);
                    roles.IdRolEmpleado = idRol;
                    string resultado = roles.EliminarRol();
                    MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarRoles();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un rol para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarRoles();
        }

        private void dgvRoles_DoubleClick(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }
    }
}