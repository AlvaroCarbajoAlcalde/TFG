namespace Pokemon
{
    partial class Form_MenuCombate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MenuCombate));
            this.panelRivales = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.picBoxEntrenadorTu = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBoxEntrenadorRival = new System.Windows.Forms.PictureBox();
            this.btnAceptar = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEntrenadorTu)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEntrenadorRival)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAceptar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRivales
            // 
            this.panelRivales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRivales.AutoScroll = true;
            this.panelRivales.Location = new System.Drawing.Point(332, 58);
            this.panelRivales.Name = "panelRivales";
            this.panelRivales.Size = new System.Drawing.Size(717, 547);
            this.panelRivales.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(332, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(717, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Elige a tu oponente:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picBoxEntrenadorTu
            // 
            this.picBoxEntrenadorTu.BackColor = System.Drawing.Color.Transparent;
            this.picBoxEntrenadorTu.Image = ((System.Drawing.Image)(resources.GetObject("picBoxEntrenadorTu.Image")));
            this.picBoxEntrenadorTu.Location = new System.Drawing.Point(19, 19);
            this.picBoxEntrenadorTu.Name = "picBoxEntrenadorTu";
            this.picBoxEntrenadorTu.Size = new System.Drawing.Size(107, 116);
            this.picBoxEntrenadorTu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxEntrenadorTu.TabIndex = 4;
            this.picBoxEntrenadorTu.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.picBoxEntrenadorRival);
            this.panel1.Controls.Add(this.picBoxEntrenadorTu);
            this.panel1.Location = new System.Drawing.Point(21, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 291);
            this.panel1.TabIndex = 6;
            // 
            // picBoxEntrenadorRival
            // 
            this.picBoxEntrenadorRival.BackColor = System.Drawing.Color.Transparent;
            this.picBoxEntrenadorRival.Image = ((System.Drawing.Image)(resources.GetObject("picBoxEntrenadorRival.Image")));
            this.picBoxEntrenadorRival.Location = new System.Drawing.Point(160, 155);
            this.picBoxEntrenadorRival.Name = "picBoxEntrenadorRival";
            this.picBoxEntrenadorRival.Size = new System.Drawing.Size(107, 116);
            this.picBoxEntrenadorRival.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxEntrenadorRival.TabIndex = 7;
            this.picBoxEntrenadorRival.TabStop = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAceptar.BackgroundImage")));
            this.btnAceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAceptar.Location = new System.Drawing.Point(21, 355);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 90);
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.TabStop = false;
            this.btnAceptar.Click += new System.EventHandler(this.Aceptar);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(127, 355);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 90);
            this.label2.TabIndex = 8;
            this.label2.Text = "Comenzar Combate";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.Aceptar);
            // 
            // MenuCombate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 617);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelRivales);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1077, 656);
            this.Name = "MenuCombate";
            this.Text = "MenuCombate";
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEntrenadorTu)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEntrenadorRival)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAceptar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelRivales;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox picBoxEntrenadorTu;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox picBoxEntrenadorRival;
        private System.Windows.Forms.PictureBox btnAceptar;
        private System.Windows.Forms.Label label2;
    }
}