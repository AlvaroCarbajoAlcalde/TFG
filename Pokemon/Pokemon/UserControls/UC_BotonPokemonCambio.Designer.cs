namespace Pokemon
{
    partial class UC_BotonPokemonCambio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_BotonPokemonCambio));
            this.barraDeVida = new System.Windows.Forms.PictureBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.labelVida = new System.Windows.Forms.Label();
            this.picBoxIcono = new System.Windows.Forms.PictureBox();
            this.picBoxEstado = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.barraDeVida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxIcono)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEstado)).BeginInit();
            this.SuspendLayout();
            // 
            // barraDeVida
            // 
            this.barraDeVida.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("barraDeVida.BackgroundImage")));
            this.barraDeVida.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.barraDeVida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.barraDeVida.Location = new System.Drawing.Point(77, 32);
            this.barraDeVida.MaximumSize = new System.Drawing.Size(62, 5);
            this.barraDeVida.MinimumSize = new System.Drawing.Size(0, 5);
            this.barraDeVida.Name = "barraDeVida";
            this.barraDeVida.Size = new System.Drawing.Size(62, 5);
            this.barraDeVida.TabIndex = 3;
            this.barraDeVida.TabStop = false;
            this.barraDeVida.Click += new System.EventHandler(this.SelectorCambio_Click);
            this.barraDeVida.MouseEnter += new System.EventHandler(this.SelectorCambio_MouseEnter);
            this.barraDeVida.MouseLeave += new System.EventHandler(this.SelectorCambio_MouseLeave);
            // 
            // labelNombre
            // 
            this.labelNombre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelNombre.Font = new System.Drawing.Font("OCR A Extended", 9.75F);
            this.labelNombre.Location = new System.Drawing.Point(58, 7);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(93, 21);
            this.labelNombre.TabIndex = 6;
            this.labelNombre.Text = "Charizard";
            this.labelNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelNombre.Click += new System.EventHandler(this.SelectorCambio_Click);
            this.labelNombre.MouseEnter += new System.EventHandler(this.SelectorCambio_MouseEnter);
            this.labelNombre.MouseLeave += new System.EventHandler(this.SelectorCambio_MouseLeave);
            // 
            // labelVida
            // 
            this.labelVida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelVida.Font = new System.Drawing.Font("OCR A Extended", 8.25F);
            this.labelVida.Location = new System.Drawing.Point(72, 38);
            this.labelVida.Name = "labelVida";
            this.labelVida.Size = new System.Drawing.Size(73, 17);
            this.labelVida.TabIndex = 5;
            this.labelVida.Text = "PV: 321";
            this.labelVida.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelVida.Click += new System.EventHandler(this.SelectorCambio_Click);
            this.labelVida.MouseEnter += new System.EventHandler(this.SelectorCambio_MouseEnter);
            this.labelVida.MouseLeave += new System.EventHandler(this.SelectorCambio_MouseLeave);
            // 
            // picBoxIcono
            // 
            this.picBoxIcono.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxIcono.BackgroundImage")));
            this.picBoxIcono.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxIcono.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBoxIcono.Location = new System.Drawing.Point(5, 3);
            this.picBoxIcono.Name = "picBoxIcono";
            this.picBoxIcono.Size = new System.Drawing.Size(50, 44);
            this.picBoxIcono.TabIndex = 4;
            this.picBoxIcono.TabStop = false;
            this.picBoxIcono.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SelectorCambio_Click);
            this.picBoxIcono.MouseEnter += new System.EventHandler(this.SelectorCambio_MouseEnter);
            this.picBoxIcono.MouseLeave += new System.EventHandler(this.SelectorCambio_MouseLeave);
            // 
            // picBoxEstado
            // 
            this.picBoxEstado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxEstado.BackgroundImage")));
            this.picBoxEstado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxEstado.Location = new System.Drawing.Point(61, 41);
            this.picBoxEstado.Name = "picBoxEstado";
            this.picBoxEstado.Size = new System.Drawing.Size(26, 13);
            this.picBoxEstado.TabIndex = 7;
            this.picBoxEstado.TabStop = false;
            this.picBoxEstado.Click += new System.EventHandler(this.SelectorCambio_Click);
            // 
            // SelectorCambio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.picBoxEstado);
            this.Controls.Add(this.barraDeVida);
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.labelVida);
            this.Controls.Add(this.picBoxIcono);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Name = "SelectorCambio";
            this.Size = new System.Drawing.Size(156, 60);
            this.Click += new System.EventHandler(this.SelectorCambio_Click);
            this.MouseEnter += new System.EventHandler(this.SelectorCambio_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.SelectorCambio_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.barraDeVida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxIcono)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEstado)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox barraDeVida;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.Label labelVida;
        private System.Windows.Forms.PictureBox picBoxIcono;
        private System.Windows.Forms.PictureBox picBoxEstado;
    }
}
