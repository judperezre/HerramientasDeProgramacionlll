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
    public partial class frmEmpleados : MaterialForm
    {
        private Empleados empleados;
        private Roles roles;
        private int idEmpleado = 0;
        private bool esEdicion = false;

        public frmEmpleados()
        {
            InitializeComponent();
            empleados = new Empleados();
            roles = new Roles();
            ConfigurarMaterial();
            CargarRoles();
            ConfigurarFormulario();
        }

        public frmEmpleados(int idEmpleado)
        {
            InitializeComponent();
            this.idEmpleado = idEmpleado;
            this.esEdicion = true;
            empleados = new Empleados();
            roles = new Roles();
            ConfigurarMaterial();
            CargarRoles();
            ConfigurarFormulario();
            CargarDatosEmpleado();
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
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Enabled = false;
            
            if (esEdicion)
            {
                materialLabel1.Text = "Editar Empleado";
                materialRaisedButton1.Text = "Actualizar";
                dateTimePicker2.Enabled = true;
            }
            else
            {
                materialLabel1.Text = "Nuevo Empleado";
                materialRaisedButton1.Text = "Guardar";
            }
        }

        private void CargarRoles()
        {
            try
            {
                DataTable dt = roles.ConsultarRoles();
                if (dt != null)
                {
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "StrDescripcion";
                    comboBox1.ValueMember = "IdRolEmpleado";
                    comboBox1.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosEmpleado()
        {
            try
            {
                DataTable dt = empleados.BuscarEmpleado(idEmpleado);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    materialSingleLineTextField1.Text = row["strNombre"].ToString();
                    materialSingleLineTextField2.Text = row["NumDocumento"].ToString();
                    materialSingleLineTextField3.Text = row["StrDireccion"].ToString();
                    materialSingleLineTextField4.Text = row["StrTelefono"].ToString();
                    materialSingleLineTextField5.Text = row["StrEmail"].ToString();
                    
                    if (row["IdRolEmpleado"] != DBNull.Value)
                        comboBox1.SelectedValue = Convert.ToInt32(row["IdRolEmpleado"]);
                    
                    if (row["DtmIngreso"] != DBNull.Value)
                        dateTimePicker1.Value = Convert.ToDateTime(row["DtmIngreso"]);
                    
                    if (row["DtmRetiro"] != DBNull.Value)
                        dateTimePicker2.Value = Convert.ToDateTime(row["DtmRetiro"]);
                    
                    textBox1.Text = row["strDatosAdicionales"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(materialSingleLineTextField1.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                materialSingleLineTextField1.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(materialSingleLineTextField2.Text))
            {
                MessageBox.Show("El documento es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                materialSingleLineTextField2.Focus();
                return false;
            }

            long documento;
            if (!long.TryParse(materialSingleLineTextField2.Text, out documento))
            {
                MessageBox.Show("El documento debe ser numérico", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                materialSingleLineTextField2.Focus();
                return false;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un rol", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }

            return true;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                empleados.StrNombre = materialSingleLineTextField1.Text;
                empleados.NumDocumento = Convert.ToInt64(materialSingleLineTextField2.Text);
                empleados.StrDireccion = materialSingleLineTextField3.Text;
                empleados.StrTelefono = materialSingleLineTextField4.Text;
                empleados.StrEmail = materialSingleLineTextField5.Text;
                empleados.IdRolEmpleado = Convert.ToInt32(comboBox1.SelectedValue);
                empleados.DtmIngreso = dateTimePicker1.Value;
                empleados.StrDatosAdicionales = textBox1.Text;
                empleados.StrUsuarioModifico = "Admin"; // Aquí deberías usar el usuario actual

                string resultado;
                if (esEdicion)
                {
                    empleados.IdEmpleado = idEmpleado;
                    if (dateTimePicker2.Enabled && dateTimePicker2.Value != dateTimePicker2.MinDate)
                        empleados.DtmRetiro = dateTimePicker2.Value;
                    resultado = empleados.ActualizarEmpleado();
                }
                else
                {
                    resultado = empleados.InsertarEmpleado();
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

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialSingleLineTextField2_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField7_Click(object sender, EventArgs e)
        {

        }
    }
}
