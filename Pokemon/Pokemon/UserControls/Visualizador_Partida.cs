using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Visualizador_Partida : UserControl
    {
        public String id;

        public Visualizador_Partida(String id, String entrenador1, String entrenador2, String estado, String ganador,
            String imagenEntrenador1, String imagenEntrenador2,
            int poktu1, int poktu2, int poktu3, int poktu4, int poktu5, int poktu6,
            int pokrival1, int pokrival2, int pokrival3, int pokrival4, int pokrival5, int pokrival6)
        {
            InitializeComponent();
            this.id = id;
            labelEstado.Text = "Estado: " + estado;
            labelGanador.Text = "Ganador: " + ganador;
            labelNombreRival.Text = entrenador2;
            labelNombreTu.Text = entrenador1;

            picBoxEntrenadorTu.Image = Image.FromFile(imagenEntrenador1);
            picBoxEntrenadorRival.Image = Image.FromFile(imagenEntrenador2);

            tuPok1.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + poktu1 + ".png");
            tuPok2.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + poktu2 + ".png");
            tuPok3.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + poktu3 + ".png");
            tuPok4.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + poktu4 + ".png");
            tuPok5.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + poktu5 + ".png");
            tuPok6.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + poktu6 + ".png");

            rivalPok1.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + pokrival1 + ".png");
            rivalPok2.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + pokrival2 + ".png");
            rivalPok3.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + pokrival3 + ".png");
            rivalPok4.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + pokrival4 + ".png");
            rivalPok5.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + pokrival5 + ".png");
            rivalPok6.BackgroundImage = Image.FromFile(@"Img\PkmIcons\" + pokrival6 + ".png");
        }

        private void OnClick(object sender, EventArgs e)
        {
            new Form_LogCombateDetalles(id).Show();
        }
    }
}
