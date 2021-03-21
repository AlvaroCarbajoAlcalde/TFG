namespace Pokemon
{
    partial class Pokedex_Objeto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pokedex_Objeto));
            this.panelBolsa = new System.Windows.Forms.FlowLayoutPanel();
            this.labelMostrarDescripcion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelBolsa
            // 
            this.panelBolsa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBolsa.AutoScroll = true;
            this.panelBolsa.BackColor = System.Drawing.Color.White;
            this.panelBolsa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBolsa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBolsa.Location = new System.Drawing.Point(12, 88);
            this.panelBolsa.Name = "panelBolsa";
            this.panelBolsa.Size = new System.Drawing.Size(415, 116);
            this.panelBolsa.TabIndex = 62;
            // 
            // labelMostrarDescripcion
            // 
            this.labelMostrarDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMostrarDescripcion.BackColor = System.Drawing.Color.White;
            this.labelMostrarDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMostrarDescripcion.Font = new System.Drawing.Font("OCR A Extended", 9.25F);
            this.labelMostrarDescripcion.Location = new System.Drawing.Point(13, 9);
            this.labelMostrarDescripcion.Name = "labelMostrarDescripcion";
            this.labelMostrarDescripcion.Size = new System.Drawing.Size(414, 76);
            this.labelMostrarDescripcion.TabIndex = 63;
            this.labelMostrarDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pokedex_Objeto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 214);
            this.Controls.Add(this.labelMostrarDescripcion);
            this.Controls.Add(this.panelBolsa);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(453, 253);
            this.Name = "Pokedex_Objeto";
            this.Text = "Pokedex";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelBolsa;
        public System.Windows.Forms.Label labelMostrarDescripcion;
    }
}