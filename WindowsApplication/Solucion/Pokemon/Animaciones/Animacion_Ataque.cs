using System;
using System.Windows.Forms;

namespace Pokemon
{
    class AnimacionAtaque
    {

        #region Propiedades

        private readonly PictureBox pokemonAfectado;
        private readonly Timer timerAnimacion;
        private readonly PictureBox imagenAtaque;
        private int ticks;
        private readonly bool parpadeo, acabaDesaparecido;

        #endregion

        #region Constructores

        public AnimacionAtaque(PictureBox pokemonAfectado, PictureBox imagenAtaque)
        {
            timerAnimacion = new Timer();
            this.pokemonAfectado = pokemonAfectado;
            this.imagenAtaque = imagenAtaque;
            timerAnimacion.Interval = 75;
            ticks = 0;
            timerAnimacion.Tick += TimerAnimacionTick;
            parpadeo = true;
            acabaDesaparecido = false;
            timerAnimacion.Enabled = true;
        }

        public AnimacionAtaque(PictureBox pokemonAfectado, PictureBox imagenAtaque, bool parpadeo)
        {
            timerAnimacion = new Timer();
            this.pokemonAfectado = pokemonAfectado;
            this.imagenAtaque = imagenAtaque;
            timerAnimacion.Interval = 75;
            ticks = 0;
            timerAnimacion.Tick += TimerAnimacionTick;
            this.parpadeo = parpadeo;
            this.acabaDesaparecido = false;
            timerAnimacion.Enabled = true;
        }

        public AnimacionAtaque(PictureBox pokemonAfectado, PictureBox imagenAtaque, bool parpadeo, bool acabaDesaparecido)
        {
            timerAnimacion = new Timer();
            this.pokemonAfectado = pokemonAfectado;
            this.imagenAtaque = imagenAtaque;
            timerAnimacion.Interval = 75;
            ticks = 0;
            timerAnimacion.Tick += TimerAnimacionTick;
            this.parpadeo = parpadeo;
            this.acabaDesaparecido = acabaDesaparecido;
            timerAnimacion.Enabled = true;
        }

        #endregion

        #region Metodos

        private void TimerAnimacionTick(object sender, EventArgs e)
        {
            if (ticks < 16)
            {
                imagenAtaque.Visible = true;
                pokemonAfectado.Visible = false;
            }
            else if (ticks < 18)
            {
                imagenAtaque.Visible = false;
                if (!acabaDesaparecido)
                    pokemonAfectado.Visible = true;
                if (!parpadeo)
                    timerAnimacion.Enabled = false;
            }
            else if (ticks < 23)
            {
                pokemonAfectado.Visible = false;
            }
            else if (ticks < 26)
            {
                pokemonAfectado.Visible = true;
            }
            else if (ticks < 29)
            {
                pokemonAfectado.Visible = false;
            }
            else if (ticks < 32)
            {
                if (!acabaDesaparecido)
                    pokemonAfectado.Visible = true;
                timerAnimacion.Enabled = false;
            }
            ticks++;
        }

        #endregion

    }
}
