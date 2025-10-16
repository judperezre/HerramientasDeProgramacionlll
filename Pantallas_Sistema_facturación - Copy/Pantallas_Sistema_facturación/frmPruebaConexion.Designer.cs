namespace Pantallas_Sistema_facturación
{
    partial class frmPruebaConexion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnProbarClientes = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnProbarEmpleados = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnProbarProductos = new MaterialSkin.Controls.MaterialRaisedButton();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.lblResultado = new System.Windows.Forms.Label();
            this.btnInsertarCliente = new MaterialSkin.Controls.MaterialRaisedButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProbarClientes
            // 
            this.btnProbarClientes.Depth = 0;
            this.btnProbarClientes.Location = new System.Drawing.Point(12, 80);
            this.btnProbarClientes.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnProbarClientes.Name = "btnProbarClientes";
            this.btnProbarClientes.Primary = true;
            this.btnProbarClientes.Size = new System.Drawing.Size(120, 36);
            this.btnProbarClientes.TabIndex = 0;
            this.btnProbarClientes.Text = "Probar Clientes";
            this.btnProbarClientes.UseVisualStyleBackColor = true;
            this.btnProbarClientes.Click += new System.EventHandler(this.btnProbarClientes_Click);
            // 
            // btnProbarEmpleados
            // 
            this.btnProbarEmpleados.Depth = 0;
            this.btnProbarEmpleados.Location = new System.Drawing.Point(150, 80);
            this.btnProbarEmpleados.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnProbarEmpleados.Name = "btnProbarEmpleados";
            this.btnProbarEmpleados.Primary = true;
            this.btnProbarEmpleados.Size = new System.Drawing.Size(120, 36);
            this.btnProbarEmpleados.TabIndex = 1;
            this.btnProbarEmpleados.Text = "Probar Empleados";
            this.btnProbarEmpleados.UseVisualStyleBackColor = true;
            this.btnProbarEmpleados.Click += new System.EventHandler(this.btnProbarEmpleados_Click);
            // 
            // btnProbarProductos
            // 
            this.btnProbarProductos.Depth = 0;
            this.btnProbarProductos.Location = new System.Drawing.Point(288, 80);
            this.btnProbarProductos.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnProbarProductos.Name = "btnProbarProductos";
            this.btnProbarProductos.Primary = true;
            this.btnProbarProductos.Size = new System.Drawing.Size(120, 36);
            this.btnProbarProductos.TabIndex = 2;
            this.btnProbarProductos.Text = "Probar Productos";
            this.btnProbarProductos.UseVisualStyleBackColor = true;
            this.btnProbarProductos.Click += new System.EventHandler(this.btnProbarProductos_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(12, 160);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.Size = new System.Drawing.Size(760, 300);
            this.dgvResultados.TabIndex = 3;
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultado.Location = new System.Drawing.Point(12, 130);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(182, 17);
            this.lblResultado.TabIndex = 4;
            this.lblResultado.Text = "Presiona un botón para probar";
            // 
            // btnInsertarCliente
            // 
            this.btnInsertarCliente.Depth = 0;
            this.btnInsertarCliente.Location = new System.Drawing.Point(426, 80);
            this.btnInsertarCliente.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnInsertarCliente.Name = "btnInsertarCliente";
            this.btnInsertarCliente.Primary = true;
            this.btnInsertarCliente.Size = new System.Drawing.Size(120, 36);
            this.btnInsertarCliente.TabIndex = 5;
            this.btnInsertarCliente.Text = "Insertar Cliente";
            this.btnInsertarCliente.UseVisualStyleBackColor = true;
            this.btnInsertarCliente.Click += new System.EventHandler(this.btnInsertarCliente_Click);
            // 
            // frmPruebaConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 481);
            this.Controls.Add(this.btnInsertarCliente);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.btnProbarProductos);
            this.Controls.Add(this.btnProbarEmpleados);
            this.Controls.Add(this.btnProbarClientes);
            this.MaximizeBox = false;
            this.Name = "frmPruebaConexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prueba de Conexión y CRUD";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnProbarClientes;
        private MaterialSkin.Controls.MaterialRaisedButton btnProbarEmpleados;
        private MaterialSkin.Controls.MaterialRaisedButton btnProbarProductos;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Label lblResultado;
        private MaterialSkin.Controls.MaterialRaisedButton btnInsertarCliente;
    }
}