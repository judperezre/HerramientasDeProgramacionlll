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
    public partial class frmEditarCliente : MaterialForm
    {
        public int IdCliente {get;set;}
        public frmEditarCliente()
        {
            InitializeComponent();
        }

        private void frmEditarClientecs_Load(object sender, EventArgs e)
        {
            if (IdCliente == 0)
            {
                lblTitulo.Text = "Ingreso nuevo cliente";
            }
            else 
            {
                lblTitulo.Text = "Modificar cliente";
                txtIdCliente.Text = IdCliente.ToString();
                txtNombreCliente.Text = "nombre 1 apellido";
                txtDocumento.Text = "343534460";
                txtDireccion.Text = "Calle 123 falsa";
                txtTelefono.Text = "3105909910";

            }
        }
        private void btnActualizar_Click(object sender, EventArgs e) 
        {
            MessageBox.Show("Datos Actualizados");
        }
        private void BtnSalir_Click(object sender, EventArgs e) 
        {
            this.Close();
        }
        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
