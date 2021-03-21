using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class UC_PokemonRestantes : UserControl
    {
        private Entrenador entrenador;
        private List<PictureBox> listaPokeballs;

        public UC_PokemonRestantes(Entrenador entrenador)
        {
            InitializeComponent();
            this.entrenador = entrenador;
            listaPokeballs = new List<PictureBox>();
            listaPokeballs.Add(pokeball1);
            listaPokeballs.Add(pokeball2);
            listaPokeballs.Add(pokeball3);
            listaPokeballs.Add(pokeball4);
            listaPokeballs.Add(pokeball5);
            listaPokeballs.Add(pokeball6);
        }

        public void ActualizarIndicador()
        {
            for (int i = 0; i < entrenador.equipo.Length; i++)
            {
                if (entrenador.equipo[i].estadisticasActuales.debilitado)
                    listaPokeballs[i].BackgroundImage = Image.FromFile(@"Img\Recursos\PkbllMuerto.png");
                else
                    listaPokeballs[i].BackgroundImage = Image.FromFile(@"Img\Recursos\PkbllVivo.png");
            }
        }

        private void Pokeball1_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pokeball1, entrenador.equipo[0].nombre);
        }

        private void Pokeball2_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pokeball2, entrenador.equipo[1].nombre);
        }

        private void Pokeball3_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pokeball3, entrenador.equipo[2].nombre);
        }

        private void Pokeball4_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pokeball4, entrenador.equipo[3].nombre);
        }

        private void Pokeball5_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pokeball5, entrenador.equipo[4].nombre);
        }

        private void Pokeball6_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pokeball6, entrenador.equipo[5].nombre);
        }
    }
}
