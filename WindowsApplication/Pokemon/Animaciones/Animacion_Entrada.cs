using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    class AnimacionEntrada
    {
        private Timer timerAnimacion, timerPokeball;
        private String posicion;
        private PictureBox picBoxPokemon;
        private Image imagenPokemon;
        private Size sizeFinal;
        private Point posicionFinal;
        private int ticksAnimacionTotales, ticks;
        private Rectangle recPicBoxPokemon;
        private Form_Combate combate;

        public AnimacionEntrada(PictureBox picBoxPokemon, Rectangle recPicBoxPokemon, String posicion, Form_Combate combate)
        {
            this.combate = combate;
            this.recPicBoxPokemon = recPicBoxPokemon;
            this.picBoxPokemon = picBoxPokemon;
            imagenPokemon = picBoxPokemon.Image;
            sizeFinal = recPicBoxPokemon.Size;
            posicionFinal = recPicBoxPokemon.Location;
            this.posicion = posicion;
            ticksAnimacionTotales = 15;
            ticks = 0;

            //Timers
            timerAnimacion = new Timer();
            timerAnimacion.Interval = 55;
            timerAnimacion.Tick += new System.EventHandler(this.TimerAnimacion_Tick);

            timerPokeball = new Timer();
            timerPokeball.Interval = 85;
            timerPokeball.Tick += new System.EventHandler(this.TimerPokeball_Tick);

            //Imagen pokemon
            Image imgPokeball = GenerarPokebalRandom();
            picBoxPokemon.Image = imgPokeball;
            this.recPicBoxPokemon.Size = new Size(20, 20);
            if (posicion == "Front") this.recPicBoxPokemon.Location = new Point(330 - this.recPicBoxPokemon.Width / 2, 149 - this.recPicBoxPokemon.Height);
            else this.recPicBoxPokemon.Location = new Point(100 - this.recPicBoxPokemon.Width / 2, 243 - this.recPicBoxPokemon.Height);
            combate.ResizeControl(this.recPicBoxPokemon, picBoxPokemon);

            timerPokeball.Enabled = true;
        }

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            //Parada
            if (recPicBoxPokemon.Width > sizeFinal.Width || recPicBoxPokemon.Height > sizeFinal.Height)
            {
                //Posicionamos al final
                recPicBoxPokemon.Size = sizeFinal;
                recPicBoxPokemon.Location = posicionFinal;
                combate.ResizeControl(recPicBoxPokemon, picBoxPokemon);
                timerAnimacion.Enabled = false;
            }
            //Animacion
            else
            {
                //Vamos aumentando el tamagno de la imagen mientras cambiamos su localizacion para mantener el mismo centro
                recPicBoxPokemon.Size = new Size(recPicBoxPokemon.Width + sizeFinal.Width / ticksAnimacionTotales, recPicBoxPokemon.Height + sizeFinal.Height / ticksAnimacionTotales);
                if (posicion == "Front")
                    recPicBoxPokemon.Location = new Point(330 - recPicBoxPokemon.Width / 2, 149 - recPicBoxPokemon.Height);
                else
                    recPicBoxPokemon.Location = new Point(100 - recPicBoxPokemon.Size.Width / 2, 243 - recPicBoxPokemon.Height);
                combate.ResizeControl(recPicBoxPokemon, picBoxPokemon);
            }
        }

        private void TimerPokeball_Tick(object sender, EventArgs e)
        {
            //Parada
            if (ticks > 10)
            {
                //Tamagno inicial
                recPicBoxPokemon.Size = new Size(1, 1);

                //Imagen pokemon
                picBoxPokemon.Image = imagenPokemon;
                combate.ResizeControl(recPicBoxPokemon, picBoxPokemon);

                //Sonido
                if (posicion == "Front")
                    combate.pokemonFront.PlayRugido();
                else
                    combate.pokemonBack.PlayRugido();
                
                //Cambiamos de animacion a la del pokemon
                timerAnimacion.Enabled = true;
                timerPokeball.Enabled = false;
            }
            ticks++;
        }

        private Image GenerarPokebalRandom()
        {
            //Generamos una imagen de pokeball
            Random random = new Random((int)System.DateTime.Now.Ticks);
            String[] pokeballs = { "Acopio", "Buceo", "Honor", "Lujo", "Malla", "Master", "Nido", "Ocaso",
                                 "Poke", "Safari", "Sana", "Super", "Turno", "Ultra", "Veloz",};
            String salida = "Img/Pokeballs/" + pokeballs[random.Next(0, pokeballs.Length)] + "Ball.png";
            return Image.FromFile(@salida);
        }
    }
}