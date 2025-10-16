namespace Pantallas_Sistema_facturación
{
    partial class frmAyuda
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
            this.wbAyuda = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbAyuda
            // 
            this.wbAyuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbAyuda.Location = new System.Drawing.Point(0, 0);
            this.wbAyuda.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbAyuda.Name = "wbAyuda";
            this.wbAyuda.Size = new System.Drawing.Size(800, 450);
            this.wbAyuda.TabIndex = 0;
            this.wbAyuda.Url = new System.Uri("https://learn.microsoft.com/es-es/docs/", System.UriKind.Absolute);
            // 
            // frmAyuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.wbAyuda);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAyuda";
            this.Text = "frmAyuda";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbAyuda;
    }
}