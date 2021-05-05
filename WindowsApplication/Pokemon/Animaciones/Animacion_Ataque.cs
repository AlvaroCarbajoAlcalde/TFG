using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    class AnimacionAtaque
    {
        private PictureBox pokemonAfectado;
        private Timer timerAnimacion;
        private PictureBox imagenAtaque;
        private int ticks;
        private Boolean parpadeo, acabaDesaparecido;

        public AnimacionAtaque(PictureBox pokemonAfectado, PictureBox imagenAtaque)
        {
            timerAnimacion = new Timer();
            this.pokemonAfectado = pokemonAfectado;
            this.imagenAtaque = imagenAtaque;
            timerAnimacion.Interval = 75;
            ticks = 0;
            timerAnimacion.Tick += TimerAnimacionTick;
            parpadeo = true;
            this.acabaDesaparecido = false;
            timerAnimacion.Enabled = true;
        }

        public AnimacionAtaque(PictureBox pokemonAfectado, PictureBox imagenAtaque, Boolean parpadeo)
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

        public AnimacionAtaque(PictureBox pokemonAfectado, PictureBox imagenAtaque, Boolean parpadeo, Boolean acabaDesaparecido)
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
    }
}
