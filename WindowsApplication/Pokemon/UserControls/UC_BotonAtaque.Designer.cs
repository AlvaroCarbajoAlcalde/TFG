namespace Pokemon
{
    partial class UC_BotonAtaque
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_BotonAtaque));
            this.panelMov = new System.Windows.Forms.Panel();
            this.labelPP = new System.Windows.Forms.Label();
            this.labelNombreAtaque = new System.Windows.Forms.Label();
            this.panelMov.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMov
            // 
            this.panelMov.BackColor = System.Drawing.Color.Transparent;
            this.panelMov.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMov.BackgroundImage")));
            this.panelMov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelMov.Controls.Add(this.labelPP);
            this.panelMov.Controls.Add(this.labelNombreAtaque);
            this.panelMov.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelMov.Location = new System.Drawing.Point(0, 0);
            this.panelMov.Name = "panelMov";
            this.panelMov.Size = new System.Drawing.Size(135, 64);
            this.panelMov.TabIndex = 50;
            this.panelMov.Click += new System.EventHandler(this.OnClick);
            this.panelMov.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.panelMov.MouseHover += new System.EventHandler(this.OnHover);
            this.panelMov.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // labelPP
            // 
            this.labelPP.BackColor = System.Drawing.Color.Transparent;
            this.labelPP.Font = new System.Drawing.Font("OCR A Extended", 8.25F);
            this.labelPP.Location = new System.Drawing.Point(36, 34);
            this.labelPP.Name = "labelPP";
            this.labelPP.Size = new System.Drawing.Size(85, 21);
            this.labelPP.TabIndex = 42;
            this.labelPP.Text = "PP 21/22";
            this.labelPP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelPP.Click += new System.EventHandler(this.OnClick);
            this.labelPP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.labelPP.MouseHover += new System.EventHandler(this.OnHover);
            this.labelPP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // labelNombreAtaque
            // 
            this.labelNombreAtaque.BackColor = System.Drawing.Color.Transparent;
            this.labelNombreAtaque.Font = new System.Drawing.Font("OCR A Extended", 8.25F);
            this.labelNombreAtaque.Location = new System.Drawing.Point(3, 11);
            this.labelNombreAtaque.Name = "labelNombreAtaque";
            this.labelNombreAtaque.Size = new System.Drawing.Size(129, 23);
            this.labelNombreAtaque.TabIndex = 41;
            this.labelNombreAtaque.Text = "Rayo Hielo";
            this.labelNombreAtaque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNombreAtaque.Click += new System.EventHandler(this.OnClick);
            this.labelNombreAtaque.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.labelNombreAtaque.MouseHover += new System.EventHandler(this.OnHover);
            this.labelNombreAtaque.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // Movimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelMov);
            this.Name = "Movimiento";
            this.Size = new System.Drawing.Size(135, 64);
            this.panelMov.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMov;
        private System.Windows.Forms.Label labelPP;
        private System.Windows.Forms.Label labelNombreAtaque;
    }
}
