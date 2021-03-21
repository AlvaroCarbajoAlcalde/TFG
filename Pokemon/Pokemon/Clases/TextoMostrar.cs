using System;
using System.Windows.Forms;

namespace Pokemon
{
    public class TextoMostrar
    {

        #region Propiedades

        private Label labelTexto;
        private Timer timerAnimacion;
        public string texto;
        private int ticks;

        #endregion

        #region Constructor

        public TextoMostrar(Label labelTexto)
        {
            this.labelTexto = labelTexto;
            timerAnimacion = new Timer();
            timerAnimacion.Interval = 55;
            timerAnimacion.Tick += new EventHandler(this.TimerAnimacion_Tick);
        }

        #endregion

        #region Metodos

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            if (ticks >= texto.Length-1)
            {
                timerAnimacion.Enabled = false;
                labelTexto.Text = texto;
            }
            else
            {
                labelTexto.Text += texto.ToCharArray()[ticks++];
                labelTexto.Text += texto.ToCharArray()[ticks++];
            }
        }

        public void MostrarTexto(string texto)
        {
            this.texto = texto;
            labelTexto.Text = "";
            ticks = 0;
            timerAnimacion.Enabled = true;
        }

        #endregion

    }
}
