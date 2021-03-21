namespace Pokemon
{
    partial class Visualizador_Pokemon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizador_Pokemon));
            this.picBoxIcon = new System.Windows.Forms.PictureBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.picBoxTipo1 = new System.Windows.Forms.PictureBox();
            this.picBoxTipo2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTipo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTipo2)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxIcon
            // 
            this.picBoxIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxIcon.BackgroundImage")));
            this.picBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxIcon.Location = new System.Drawing.Point(0, 0);
            this.picBoxIcon.Name = "picBoxIcon";
            this.picBoxIcon.Size = new System.Drawing.Size(40, 33);
            this.picBoxIcon.TabIndex = 0;
            this.picBoxIcon.TabStop = false;
            this.picBoxIcon.Click += new System.EventHandler(this.OnClick);
            // 
            // labelNombre
            // 
            this.labelNombre.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelNombre.Location = new System.Drawing.Point(46, 0);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(106, 33);
            this.labelNombre.TabIndex = 1;
            this.labelNombre.Text = "Giratina";
            this.labelNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelNombre.Click += new System.EventHandler(this.OnClick);
            // 
            // picBoxTipo1
            // 
            this.picBoxTipo1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxTipo1.BackgroundImage")));
            this.picBoxTipo1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxTipo1.Location = new System.Drawing.Point(157, 4);
            this.picBoxTipo1.Name = "picBoxTipo1";
            this.picBoxTipo1.Size = new System.Drawing.Size(57, 25);
            this.picBoxTipo1.TabIndex = 2;
            this.picBoxTipo1.TabStop = false;
            this.picBoxTipo1.Click += new System.EventHandler(this.OnClick);
            // 
            // picBoxTipo2
            // 
            this.picBoxTipo2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxTipo2.BackgroundImage")));
            this.picBoxTipo2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxTipo2.Location = new System.Drawing.Point(220, 4);
            this.picBoxTipo2.Name = "picBoxTipo2";
            this.picBoxTipo2.Size = new System.Drawing.Size(57, 25);
            this.picBoxTipo2.TabIndex = 3;
            this.picBoxTipo2.TabStop = false;
            this.picBoxTipo2.Click += new System.EventHandler(this.OnClick);
            // 
            // VisualizadorPokemon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.picBoxTipo2);
            this.Controls.Add(this.picBoxTipo1);
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.picBoxIcon);
            this.Name = "VisualizadorPokemon";
            this.Size = new System.Drawing.Size(331, 31);
            this.Click += new System.EventHandler(this.OnClick);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTipo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTipo2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxIcon;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.PictureBox picBoxTipo1;
        private System.Windows.Forms.PictureBox picBoxTipo2;
    }
}
