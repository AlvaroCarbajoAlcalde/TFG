namespace Pokemon
{
    partial class PokemonPokedex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokemonPokedex));
            this.iconoPokemon = new System.Windows.Forms.PictureBox();
            this.nombrePokemon = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconoPokemon)).BeginInit();
            this.SuspendLayout();
            // 
            // iconoPokemon
            // 
            this.iconoPokemon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("iconoPokemon.BackgroundImage")));
            this.iconoPokemon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.iconoPokemon.Location = new System.Drawing.Point(0, 0);
            this.iconoPokemon.Name = "iconoPokemon";
            this.iconoPokemon.Size = new System.Drawing.Size(26, 23);
            this.iconoPokemon.TabIndex = 0;
            this.iconoPokemon.TabStop = false;
            this.iconoPokemon.Click += new System.EventHandler(this.OnClick);
            // 
            // nombrePokemon
            // 
            this.nombrePokemon.BackColor = System.Drawing.Color.Transparent;
            this.nombrePokemon.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombrePokemon.Location = new System.Drawing.Point(26, 3);
            this.nombrePokemon.Name = "nombrePokemon";
            this.nombrePokemon.Size = new System.Drawing.Size(108, 18);
            this.nombrePokemon.TabIndex = 26;
            this.nombrePokemon.Text = "Bulbasaurio";
            this.nombrePokemon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.nombrePokemon.Click += new System.EventHandler(this.OnClick);
            // 
            // PokemonPokedex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.nombrePokemon);
            this.Controls.Add(this.iconoPokemon);
            this.Name = "PokemonPokedex";
            this.Size = new System.Drawing.Size(134, 23);
            this.Click += new System.EventHandler(this.OnClick);
            ((System.ComponentModel.ISupportInitialize)(this.iconoPokemon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox iconoPokemon;
        private System.Windows.Forms.Label nombrePokemon;
    }
}
