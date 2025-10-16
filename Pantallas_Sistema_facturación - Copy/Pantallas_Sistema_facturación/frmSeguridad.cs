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
    public partial class frmSeguridad : MaterialForm
    {
        private Seguridad seguridad;
        private Empleados empleados;
        private int idSeguridad = 0;
        private bool esEdicion = false;

        public frmSeguridad()
        {
            InitializeComponent();
            seguridad = new Seguridad();
            empleados = new Empleados();
            ConfigurarMaterial();
            CargarEmpleados();
            CargarUsuarios();
            ConfigurarFormulario();
        }

        public frmSeguridad(int idSeguridad)
        {
            InitializeComponent();
            this.idSeguridad = idSeguridad;
            this.esEdicion = true;
            seguridad = new Seguridad();
            empleados = new Empleados();
            ConfigurarMaterial();
            CargarEmpleados();
            CargarUsuarios();
            ConfigurarFormulario();
            CargarDatosUsuario();
        }

        private void ConfigurarMaterial()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void ConfigurarFormulario()
        {
            if (esEdicion)
            {
                this.Text = "Editar Usuario";
                btnGuardar.Text = "Actualizar";
            }
            else
            {
                this.Text = "Nuevo Usuario";
                btnGuardar.Text = "Guardar";
            }
        }

        private void CargarEmpleados()
        {
            try
            {
                DataTable dt = empleados.ConsultarEmpleados();
                if (dt != null)
                {
                    cmbEmpleado.DataSource = dt;
                    cmbEmpleado.DisplayMember = "strNombre";
                    cmbEmpleado.ValueMember = "IdEmpleado";
                    cmbEmpleado.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                DataTable dt = seguridad.ConsultarUsuarios();
                if (dt != null)
                {
                    dgvUsuarios.DataSource = dt;
                    ConfigurarDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
        }

        private void CargarDatosUsuario()
        {
            try
            {
                DataTable dt = seguridad.BuscarUsuario(idSeguridad);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    cmbEmpleado.SelectedValue = Convert.ToInt32(row["IdEmpleado"]);
                    txtUsuario.Text = row["StrUsuario"].ToString();
                    txtClave.Text = row["StrClave"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (cmbEmpleado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un empleado", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEmpleado.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUsuario.Text.Trim()))
            {
                MessageBox.Show("El usuario es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtClave.Text))
            {
                MessageBox.Show("La clave es obligatoria", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClave.Focus();
                return false;
            }

            if (txtClave.Text.Length < 4)
            {
                MessageBox.Show("La clave debe tener al menos 4 caracteres", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClave.Focus();
                return false;
            }

            // Validar que el usuario no exista (solo para nuevos usuarios)
            if (!esEdicion && seguridad.ValidarUsuarioExistente(txtUsuario.Text.Trim()))
            {
                MessageBox.Show("El usuario ya existe", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                seguridad.IdEmpleado = Convert.ToInt32(cmbEmpleado.SelectedValue);
                seguridad.StrUsuario = txtUsuario.Text.Trim();
                seguridad.StrClave = txtClave.Text;
                seguridad.StrUsuarioModifico = "Admin"; // Aquí deberías usar el usuario actual

                string resultado;
                if (esEdicion)
                {
                    seguridad.IdSeguridad = idSeguridad;
                    resultado = seguridad.ActualizarUsuario();
                }
                else
                {
                    resultado = seguridad.InsertarUsuario();
                }

                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (resultado.Contains("actualizados"))
                {
                    LimpiarFormulario();
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["IdSeguridad"].Value);
                    seguridad.IdSeguridad = id;
                    string resultado = seguridad.EliminarUsuario();
                    MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUsuarios();
                    LimpiarFormulario();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            cmbEmpleado.SelectedIndex = -1;
            txtUsuario.Text = "";
            txtClave.Text = "";
            esEdicion = false;
            idSeguridad = 0;
            btnGuardar.Text = "Guardar";
            this.Text = "Nuevo Usuario";
        }

        private void dgvUsuarios_DoubleClick(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                idSeguridad = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["IdSeguridad"].Value);
                esEdicion = true;
                btnGuardar.Text = "Actualizar";
                this.Text = "Editar Usuario";
                CargarDatosUsuario();
            }
        }
    }
}