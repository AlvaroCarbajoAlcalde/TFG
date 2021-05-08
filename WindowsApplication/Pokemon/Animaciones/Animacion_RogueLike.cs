using System.Drawing;
using System.Windows.Forms;

namespace Pokemon.Animaciones
{
    class Animacion_RogueLike
    {

        #region Propiedades

        private readonly Panel playerPicture;
        private static readonly int valor = 400;
        private static int tics = 0;
        private static Point localizacionFinal, localizacionInicial;
        private readonly Timer timer;

        #endregion

        #region Points

        private static readonly Point t1 = new Point(135, 232);
        private static readonly Point t2 = new Point(217, 212);
        private static readonly Point t3 = new Point(301, 143);
        private static readonly Point t4 = new Point(381, 140);
        private static readonly Point t5 = new Point(381, 232);
        private static readonly Point t6 = new Point(464, 259);
        private static readonly Point t7 = new Point(611, 283);
        private static readonly Point t8 = new Point(613, 174);
        private static readonly Point t9 = new Point(770, 174);
        private static readonly Point t10 = new Point(771, 225);
        private static readonly Point t11 = new Point(821, 229);
        private static readonly Point t12 = new Point(904, 214);
        private static readonly Point t13 = new Point(1034, 208);

        #endregion

        #region Constructor

        public Animacion_RogueLike(Panel playerPicture, int numCombate)
        {
            this.playerPicture = playerPicture;

            timer = new Timer
            {
                Interval = 20
            };
            timer.Tick += Timer_Tick;

            localizacionInicial = playerPicture.Location;
            localizacionFinal = GetDestino(numCombate);
            timer.Enabled = true;
        }

        private Point GetDestino(int combate)
        {
            switch (combate)
            {
                case 1:
                    return t1;
                case 2:
                    return t2;
                case 3:
                    return t3;
                case 4:
                    return t4;
                case 5:
                    return t5;
                case 6:
                    return t6;
                case 7:
                    return t7;
                case 8:
                    return t8;
                case 9:
                    return t9;
                case 10:
                    return t10;
                case 11:
                    return t11;
                case 12:
                    return t12;
                default:
                    return t13;
            }
        }

        #endregion

        #region Timer

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            tics++;

            //Condción de parada Se acaban los n segundos o no hay segundos o la localizacion final es la misma que la inicial
            if (tics > (valor / timer.Interval) || localizacionFinal == localizacionInicial)
            {
                timer.Enabled = false;
                playerPicture.Location = localizacionFinal;
                tics = 0;
            }
            //Animacion
            else
            {
                //Se mueve de izqda a derecha
                if (localizacionFinal.X >= localizacionInicial.X)
                    playerPicture.Location = new Point(playerPicture.Location.X + (localizacionFinal.X - localizacionInicial.X) / (valor / timer.Interval), playerPicture.Location.Y);
                else
                    //Se mueve de dcha a izqda
                    playerPicture.Location = new Point(playerPicture.Location.X - (localizacionInicial.X - localizacionFinal.X) / (valor / timer.Interval), playerPicture.Location.Y);
                //Se mueve de arriba a abajo
                if (localizacionFinal.Y >= localizacionInicial.Y)
                    playerPicture.Location = new Point(playerPicture.Location.X, playerPicture.Location.Y + (localizacionFinal.Y - localizacionInicial.Y) / (valor / timer.Interval));
                else
                    //Se mueve de abajo a arriba
                    playerPicture.Location = new Point(playerPicture.Location.X, playerPicture.Location.Y - (localizacionInicial.Y - localizacionFinal.Y) / (valor / timer.Interval));
            }
        }

        #endregion

    }
}
