using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public class BarraDeVida
    {

        #region Propiedades

        private int vidaActual, vidaMaxima, ticksAnimacion, ticksTotales = 15, maximoBarra, vidaCambio, vidaAux;
        private Rectangle vidaVerde, vidaRoja;
        private PictureBox picBoxBarraDeVida, picBoxBarraDeVidaRoja;
        private Label labelVida;
        private Timer timerAumentarVida, timerReducirVida;
        private Form_Combate combate;
        private string posicion;

        #endregion

        #region Constructor

        public BarraDeVida(string posicion, Pokemon pokemon, Form_Combate combate)
        {
            this.posicion = posicion;
            this.combate = combate;
            if (posicion == "Front")
            {
                labelVida = null;
                picBoxBarraDeVida = combate.vidaVerdeRival;
                picBoxBarraDeVidaRoja = combate.vidaRojaRival;
                vidaVerde = combate.recVidaVerdeRival;
                vidaRoja = combate.recVidaRojaRival;
            }
            else
            {
                labelVida = combate.labelPvTu;
                picBoxBarraDeVida = combate.vidaVerdeTu;
                picBoxBarraDeVidaRoja = combate.vidaRojaTu;
                vidaVerde = combate.recVidaVerdeTu;
                vidaRoja = combate.recVidaRojaTu;
            }
            this.vidaMaxima = pokemon.vidaMax;
            this.vidaActual = pokemon.vidaActual;

            //Timers
            timerAumentarVida = new Timer();
            timerAumentarVida.Interval = 84;
            timerAumentarVida.Tick += new System.EventHandler(this.TimerAumentarVida_Tick);
            timerReducirVida = new Timer();
            timerReducirVida.Interval = 84;
            timerReducirVida.Tick += new System.EventHandler(this.TimerReducirVida_Tick);

            maximoBarra = 86;

            vidaVerde.Size = vidaRoja.Size = new Size(maximoBarra * vidaActual / vidaMaxima, vidaVerde.Height);
            if (labelVida != null)
                labelVida.Text = "PV " + vidaActual + "/" + vidaMaxima;
            if (vidaActual < 0.3 * vidaMaxima) picBoxBarraDeVida.BackgroundImage = Image.FromFile(@"Img\Recursos\VidaAmarilla.png");
            else picBoxBarraDeVida.BackgroundImage = Image.FromFile(@"Img\Recursos\VidaVerde.png");

            combate.ResizeControl(vidaVerde, picBoxBarraDeVida);
            combate.ResizeControl(vidaRoja, picBoxBarraDeVidaRoja);
        }

        #endregion

        #region Metodos

        public void SetVida(int vida)
        {
            //No se cambia la vida
            if (vida == vidaActual)
            {
                combate.recVidaVerdeTu.Size = new Size((int)(vidaActual / (double)vidaMaxima * maximoBarra), vidaVerde.Height);
                combate.ResizeControl(vidaVerde, picBoxBarraDeVida);
                combate.ResizeControl(vidaRoja, picBoxBarraDeVidaRoja);
                return;
            }
            ticksAnimacion = 0;

            //Le bajan vida
            if (vida < vidaActual)
            {
                vidaCambio = vidaActual - vida;
                vidaAux = vidaActual;
                vidaActual = vida;
                timerReducirVida.Enabled = true;
            }

            //Se sube vida
            else
            {
                vidaCambio = vida - vidaActual;
                vidaAux = vidaActual;
                vidaActual = vida;
                timerAumentarVida.Enabled = true;
            }
        }

        private void TimerReducirVida_Tick(object sender, EventArgs e)
        {
            ticksAnimacion++;

            //Parada
            if (ticksAnimacion > 25)
            {
                vidaVerde.Size = vidaRoja.Size = new Size(maximoBarra * vidaActual / vidaMaxima, vidaVerde.Height);
                if (labelVida != null)
                    labelVida.Text = "PV " + vidaActual + "/" + vidaMaxima;
                if (vidaActual < 0.3 * vidaMaxima) picBoxBarraDeVida.BackgroundImage = Image.FromFile(@"Img\Recursos\VidaAmarilla.png");
                else picBoxBarraDeVida.BackgroundImage = Image.FromFile(@"Img\Recursos\VidaVerde.png");
                timerReducirVida.Enabled = false;
            }
            //Animacion
            else if (ticksAnimacion < 17)
            {
                if (labelVida != null)
                    labelVida.Text = "PV " + (vidaAux - ticksAnimacion * vidaCambio / 16) + "/" + vidaMaxima;
                vidaVerde.Size = new Size(vidaVerde.Width - maximoBarra * (vidaCambio / 15) / vidaMaxima, vidaVerde.Height);
            }
            //Animacion parte roja
            else
            {
                vidaRoja.Size = new Size(vidaRoja.Width - maximoBarra * (vidaCambio / 9) / vidaMaxima, vidaVerde.Height);
            }
            combate.ResizeControl(vidaVerde, picBoxBarraDeVida);
            combate.ResizeControl(vidaRoja, picBoxBarraDeVidaRoja);
            if (posicion == "Front")
            {
                combate.recVidaVerdeRival = vidaVerde;
                combate.recVidaRojaRival = vidaRoja;
            }
            else
            {
                combate.recVidaVerdeTu = vidaVerde;
                combate.recVidaRojaTu = vidaRoja;
            }
        }

        private void TimerAumentarVida_Tick(object sender, EventArgs e)
        {
            ticksAnimacion++;

            //Parada
            if (ticksAnimacion > ticksTotales)
            {
                vidaVerde.Size = vidaRoja.Size = new Size(maximoBarra * vidaActual / vidaMaxima, vidaVerde.Height);
                if (labelVida != null)
                    labelVida.Text = "PV " + vidaActual + "/" + vidaMaxima;
                if (vidaActual < 0.3 * vidaMaxima) picBoxBarraDeVida.BackgroundImage = Image.FromFile(@"Img\Recursos\VidaAmarilla.png");
                else picBoxBarraDeVida.BackgroundImage = Image.FromFile(@"Img\Recursos\VidaVerde.png");
                timerAumentarVida.Enabled = false;
            }
            //Animacion
            else
            {
                if (labelVida != null)
                    labelVida.Text = "PV " + (vidaAux + ticksAnimacion * vidaCambio / ticksTotales) + "/" + vidaMaxima;
                vidaVerde.Size = new Size(vidaVerde.Width + maximoBarra * (vidaCambio / ticksTotales) / vidaMaxima, vidaVerde.Height);
            }
            combate.ResizeControl(vidaVerde, picBoxBarraDeVida);
            combate.ResizeControl(vidaRoja, picBoxBarraDeVidaRoja);
            if (posicion == "Front")
            {
                combate.recVidaVerdeRival = vidaVerde;
                combate.recVidaRojaRival = vidaRoja;
            }
            else
            {
                combate.recVidaVerdeTu = vidaVerde;
                combate.recVidaRojaTu = vidaRoja;
            }
        }

        #endregion

    }
}
