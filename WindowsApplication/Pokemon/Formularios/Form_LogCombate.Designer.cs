namespace Pokemon
{
    partial class Form_LogCombate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_LogCombate));
            this.label1 = new System.Windows.Forms.Label();
            this.panelPartidas = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonBorrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(727, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Partidas";
            // 
            // panelPartidas
            // 
            this.panelPartidas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPartidas.AutoScroll = true;
            this.panelPartidas.BackColor = System.Drawing.Color.White;
            this.panelPartidas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPartidas.Location = new System.Drawing.Point(15, 35);
            this.panelPartidas.Name = "panelPartidas";
            this.panelPartidas.Size = new System.Drawing.Size(1012, 403);
            this.panelPartidas.TabIndex = 1;
            // 
            // buttonBorrar
            // 
            this.buttonBorrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBorrar.Location = new System.Drawing.Point(952, 9);
            this.buttonBorrar.Name = "buttonBorrar";
            this.buttonBorrar.Size = new System.Drawing.Size(75, 23);
            this.buttonBorrar.TabIndex = 2;
            this.buttonBorrar.Text = "Borrar Todo";
            this.buttonBorrar.UseVisualStyleBackColor = true;
            this.buttonBorrar.Click += new System.EventHandler(this.BorrarTodo_Click);
            // 
            // Form_LogCombate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 450);
            this.Controls.Add(this.buttonBorrar);
            this.Controls.Add(this.panelPartidas);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1057, 489);
            this.Name = "Form_LogCombate";
            this.Text = "LogCombate";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel panelPartidas;
        private System.Windows.Forms.Button buttonBorrar;
    }
}