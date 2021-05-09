namespace Pokemon
{
    partial class Visualizador_Ataque
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizador_Ataque));
            this.labelNombre = new System.Windows.Forms.Label();
            this.picBoxTipo = new System.Windows.Forms.PictureBox();
            this.picBoxCategoria = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTipo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCategoria)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNombre
            // 
            this.labelNombre.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelNombre.Location = new System.Drawing.Point(3, -1);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(147, 25);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Ataque Rapido";
            this.labelNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelNombre.Click += new System.EventHandler(this.ONClick);
            this.labelNombre.MouseLeave += new System.EventHandler(this.On_MouseLeave);
            this.labelNombre.MouseHover += new System.EventHandler(this.OnMouseHover);
            // 
            // picBoxTipo
            // 
            this.picBoxTipo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxTipo.BackgroundImage")));
            this.picBoxTipo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxTipo.Location = new System.Drawing.Point(153, -1);
            this.picBoxTipo.Name = "picBoxTipo";
            this.picBoxTipo.Size = new System.Drawing.Size(57, 25);
            this.picBoxTipo.TabIndex = 4;
            this.picBoxTipo.TabStop = false;
            this.picBoxTipo.Click += new System.EventHandler(this.ONClick);
            this.picBoxTipo.MouseLeave += new System.EventHandler(this.On_MouseLeave);
            this.picBoxTipo.MouseHover += new System.EventHandler(this.OnMouseHover);
            // 
            // picBoxCategoria
            // 
            this.picBoxCategoria.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxCategoria.BackgroundImage")));
            this.picBoxCategoria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picBoxCategoria.Location = new System.Drawing.Point(216, 3);
            this.picBoxCategoria.Name = "picBoxCategoria";
            this.picBoxCategoria.Size = new System.Drawing.Size(57, 19);
            this.picBoxCategoria.TabIndex = 6;
            this.picBoxCategoria.TabStop = false;
            this.picBoxCategoria.Click += new System.EventHandler(this.ONClick);
            this.picBoxCategoria.MouseLeave += new System.EventHandler(this.On_MouseLeave);
            this.picBoxCategoria.MouseHover += new System.EventHandler(this.OnMouseHover);
            // 
            // VisualizadorAtaque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.picBoxCategoria);
            this.Controls.Add(this.picBoxTipo);
            this.Controls.Add(this.labelNombre);
            this.Name = "VisualizadorAtaque";
            this.Size = new System.Drawing.Size(298, 24);
            this.Click += new System.EventHandler(this.ONClick);
            this.MouseLeave += new System.EventHandler(this.On_MouseLeave);
            this.MouseHover += new System.EventHandler(this.OnMouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTipo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCategoria)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.PictureBox picBoxTipo;
        private System.Windows.Forms.PictureBox picBoxCategoria;
    }
}
