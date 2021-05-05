namespace Pokemon
{
    partial class Pokedex_Movimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pokedex_Movimiento));
            this.panelMovimientos = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelPP = new System.Windows.Forms.Label();
            this.labelPre = new System.Windows.Forms.Label();
            this.labelPot = new System.Windows.Forms.Label();
            this.labelDescripcion = new System.Windows.Forms.Label();
            this.labelNombreAtaque = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMovimientos
            // 
            this.panelMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMovimientos.AutoScroll = true;
            this.panelMovimientos.BackColor = System.Drawing.Color.White;
            this.panelMovimientos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMovimientos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMovimientos.Location = new System.Drawing.Point(14, 124);
            this.panelMovimientos.Name = "panelMovimientos";
            this.panelMovimientos.Size = new System.Drawing.Size(339, 347);
            this.panelMovimientos.TabIndex = 63;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labelPP);
            this.panel1.Controls.Add(this.labelPre);
            this.panel1.Controls.Add(this.labelPot);
            this.panel1.Controls.Add(this.labelDescripcion);
            this.panel1.Controls.Add(this.labelNombreAtaque);
            this.panel1.Location = new System.Drawing.Point(14, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 100);
            this.panel1.TabIndex = 64;
            // 
            // labelPP
            // 
            this.labelPP.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelPP.Location = new System.Drawing.Point(3, 51);
            this.labelPP.Name = "labelPP";
            this.labelPP.Size = new System.Drawing.Size(86, 21);
            this.labelPP.TabIndex = 4;
            this.labelPP.Text = "PP";
            // 
            // labelPre
            // 
            this.labelPre.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelPre.Location = new System.Drawing.Point(3, 30);
            this.labelPre.Name = "labelPre";
            this.labelPre.Size = new System.Drawing.Size(86, 21);
            this.labelPre.TabIndex = 3;
            this.labelPre.Text = "PRE";
            // 
            // labelPot
            // 
            this.labelPot.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelPot.Location = new System.Drawing.Point(3, 9);
            this.labelPot.Name = "labelPot";
            this.labelPot.Size = new System.Drawing.Size(86, 21);
            this.labelPot.TabIndex = 2;
            this.labelPot.Text = "POT";
            // 
            // labelDescripcion
            // 
            this.labelDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescripcion.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelDescripcion.Location = new System.Drawing.Point(100, 30);
            this.labelDescripcion.Name = "labelDescripcion";
            this.labelDescripcion.Size = new System.Drawing.Size(232, 62);
            this.labelDescripcion.TabIndex = 1;
            this.labelDescripcion.Text = "Descripcion";
            // 
            // labelNombreAtaque
            // 
            this.labelNombreAtaque.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelNombreAtaque.Location = new System.Drawing.Point(100, 9);
            this.labelNombreAtaque.Name = "labelNombreAtaque";
            this.labelNombreAtaque.Size = new System.Drawing.Size(232, 21);
            this.labelNombreAtaque.TabIndex = 0;
            this.labelNombreAtaque.Text = "Nombre Ataque";
            // 
            // Pokedex_Movimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 478);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelMovimientos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(378, 517);
            this.Name = "Pokedex_Movimiento";
            this.Text = "Pokedex";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelMovimientos;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label labelPP;
        public System.Windows.Forms.Label labelPre;
        public System.Windows.Forms.Label labelPot;
        public System.Windows.Forms.Label labelDescripcion;
        public System.Windows.Forms.Label labelNombreAtaque;
    }
}