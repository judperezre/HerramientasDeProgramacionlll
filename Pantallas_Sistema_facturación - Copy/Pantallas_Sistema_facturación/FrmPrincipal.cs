using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;


namespace Pantallas_Sistema_facturación
{
    public partial class FrmPrincipal : MaterialForm
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
        private void AbrirForm(Form formHijo) 
        {
            if (this.Pnlcontenedor.Controls.Count > 0)
                this.Pnlcontenedor.Controls.RemoveAt(0);
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            this.Pnlcontenedor.Controls.Add(formHijo);
            formHijo.Show();
        }
        private void btnClientes_Click(object sender, EventArgs e) 
        {
            frmListaClientes listaCliente = new frmListaClientes();
            AbrirForm(listaCliente);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos productos = new frmProductos();
            AbrirForm(productos);
        }
        private void btnCategoria_Click(object sender, EventArgs e)
        {
            frmCategoriaProductos frmCategoriaProductos = new frmCategoriaProductos();
            AbrirForm(frmCategoriaProductos);
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            frmFacturas frmFacturas = new frmFacturas();
            AbrirForm(frmFacturas);
        }

        private void btnInformes_Click(object sender, EventArgs e)
        {
            frmInformes frmInforme = new frmInformes();
            AbrirForm(frmInforme);
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRolEmpleados rolEmpleados = new frmRolEmpleados();
            AbrirForm(rolEmpleados);
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            frmAdminSeguridad adminSeguridad = new frmAdminSeguridad();
            AbrirForm(adminSeguridad);
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            frmEmpleados empleados = new frmEmpleados();
            AbrirForm(empleados);
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            frmAyuda ayuda = new frmAyuda();
            AbrirForm(ayuda);
        }

        private void btnAcercaDe_Click(object sender, EventArgs e)
        {
            
        }
    }
}
