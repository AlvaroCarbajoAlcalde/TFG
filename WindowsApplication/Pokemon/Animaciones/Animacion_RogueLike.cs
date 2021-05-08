using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Pokemon.Animaciones
{
    class Animacion_RogueLike
    {

        #region Propiedades

        private readonly Panel playerPicture;
        private static readonly int velocidad = 500;
        private static int tics = 0;
        private static Point localizacionFinal, localizacionInicial;
        private readonly System.Windows.Forms.Timer timer;
        private readonly Point[] rutaActual;
        private int puntoDeRutaActual;

        #endregion

        #region Points

        //private static readonly Point[] startPoint = { new Point(36, 213) };
        private static readonly Point[] t1 = { new Point(135, 232) };
        private static readonly Point[] t2 = { new Point(243, 234), new Point(242, 190) };
        private static readonly Point[] t3 = { new Point(254, 148), new Point(301, 143) };
        private static readonly Point[] t4 = { new Point(365, 146) };
        private static readonly Point[] t5 = { new Point(364, 237), new Point(397, 240) };
        private static readonly Point[] t6 = { new Point(455, 212), new Point(484, 219) };
        private static readonly Point[] t7 = { new Point(582, 281) };
        private static readonly Point[] t8 = { new Point(612, 249), new Point(615, 175), new Point(640, 175) };
        private static readonly Point[] t9 = { new Point(770, 174) };
        private static readonly Point[] t10 = { new Point(771, 225) };
        private static readonly Point[] t11 = { new Point(821, 229) };
        private static readonly Point[] t12 = { new Point(904, 214) };
        private static readonly Point[] t13 = { new Point(1034, 208) };

        #endregion

        #region Constructor

        public Animacion_RogueLike(Panel playerPicture, int numCombate)
        {
            this.playerPicture = playerPicture;

            timer = new System.Windows.Forms.Timer
            {
                Interval = 100
            };
            timer.Tick += Timer_Tick;

            rutaActual = GetRuta(numCombate);
            localizacionFinal = rutaActual[0];
            puntoDeRutaActual = 0;
            timer.Enabled = true;
        }

        private Point[] GetRuta(int combate)
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
            if (tics > (velocidad / timer.Interval) || localizacionFinal == localizacionInicial)
            {
                timer.Enabled = false;
                puntoDeRutaActual++;
                playerPicture.Location = localizacionFinal;
                tics = 0;

                //Si la ruta sigue
                if(rutaActual.Length >= puntoDeRutaActual)
                {
                    localizacionInicial = playerPicture.Location;
                    localizacionFinal = new Point(rutaActual[puntoDeRutaActual - 1].X, rutaActual[puntoDeRutaActual - 1].Y);
                    timer.Enabled = true;
                }

            }
            //Animacion
            else
            {
                //Se mueve de izqda a derecha
                if (localizacionFinal.X >= localizacionInicial.X)
                    playerPicture.Location = new Point(playerPicture.Location.X + (localizacionFinal.X - localizacionInicial.X) / (velocidad / timer.Interval), playerPicture.Location.Y);
                else
                    //Se mueve de dcha a izqda
                    playerPicture.Location = new Point(playerPicture.Location.X - (localizacionInicial.X - localizacionFinal.X) / (velocidad / timer.Interval), playerPicture.Location.Y);
                //Se mueve de arriba a abajo
                if (localizacionFinal.Y >= localizacionInicial.Y)
                    playerPicture.Location = new Point(playerPicture.Location.X, playerPicture.Location.Y + (localizacionFinal.Y - localizacionInicial.Y) / (velocidad / timer.Interval));
                else
                    //Se mueve de abajo a arriba
                    playerPicture.Location = new Point(playerPicture.Location.X, playerPicture.Location.Y - (localizacionInicial.Y - localizacionFinal.Y) / (velocidad / timer.Interval));
            }
        }

        #endregion

    }
}
