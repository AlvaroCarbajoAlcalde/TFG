namespace Pokemon
{
    partial class Visualizador_Turno
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
            this.labelTurno = new System.Windows.Forms.Label();
            this.labelSubturno = new System.Windows.Forms.Label();
            this.labelTexto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTurno
            // 
            this.labelTurno.Location = new System.Drawing.Point(4, 9);
            this.labelTurno.Name = "labelTurno";
            this.labelTurno.Size = new System.Drawing.Size(100, 23);
            this.labelTurno.TabIndex = 0;
            this.labelTurno.Text = "Turno: 999";
            // 
            // labelSubturno
            // 
            this.labelSubturno.Location = new System.Drawing.Point(4, 32);
            this.labelSubturno.Name = "labelSubturno";
            this.labelSubturno.Size = new System.Drawing.Size(100, 23);
            this.labelSubturno.TabIndex = 1;
            this.labelSubturno.Text = "Subturno: 1";
            // 
            // labelTexto
            // 
            this.labelTexto.Location = new System.Drawing.Point(110, 9);
            this.labelTexto.Name = "labelTexto";
            this.labelTexto.Size = new System.Drawing.Size(402, 97);
            this.labelTexto.TabIndex = 2;
            this.labelTexto.Text = "label3";
            // 
            // Visualizador_Turno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.labelTexto);
            this.Controls.Add(this.labelSubturno);
            this.Controls.Add(this.labelTurno);
            this.Name = "Visualizador_Turno";
            this.Size = new System.Drawing.Size(519, 112);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTurno;
        private System.Windows.Forms.Label labelSubturno;
        private System.Windows.Forms.Label labelTexto;
    }
}
