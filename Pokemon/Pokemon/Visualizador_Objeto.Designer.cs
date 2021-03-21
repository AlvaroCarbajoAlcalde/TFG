namespace Pokemon
{
    partial class Visualizador_Objeto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizador_Objeto));
            this.picBoxObjeto = new System.Windows.Forms.PictureBox();
            this.labelNombre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxObjeto)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxObjeto
            // 
            this.picBoxObjeto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxObjeto.BackgroundImage")));
            this.picBoxObjeto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxObjeto.Location = new System.Drawing.Point(0, 0);
            this.picBoxObjeto.Name = "picBoxObjeto";
            this.picBoxObjeto.Size = new System.Drawing.Size(40, 28);
            this.picBoxObjeto.TabIndex = 0;
            this.picBoxObjeto.TabStop = false;
            this.picBoxObjeto.Click += new System.EventHandler(this.On_Click);
            this.picBoxObjeto.MouseLeave += new System.EventHandler(this.On_MouseLeave);
            this.picBoxObjeto.MouseHover += new System.EventHandler(this.On_MouseHover);
            // 
            // labelNombre
            // 
            this.labelNombre.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelNombre.Location = new System.Drawing.Point(41, 0);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(133, 28);
            this.labelNombre.TabIndex = 1;
            this.labelNombre.Text = "Restaura Todo";
            this.labelNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelNombre.Click += new System.EventHandler(this.On_Click);
            this.labelNombre.MouseLeave += new System.EventHandler(this.On_MouseLeave);
            this.labelNombre.MouseHover += new System.EventHandler(this.On_MouseHover);
            // 
            // SelectorObjeto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.picBoxObjeto);
            this.Name = "SelectorObjeto";
            this.Size = new System.Drawing.Size(171, 28);
            this.Click += new System.EventHandler(this.On_Click);
            this.MouseLeave += new System.EventHandler(this.On_MouseLeave);
            this.MouseHover += new System.EventHandler(this.On_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxObjeto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxObjeto;
        private System.Windows.Forms.Label labelNombre;
    }
}
