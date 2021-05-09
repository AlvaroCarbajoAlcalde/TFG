using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class UC_PokemonRestantes : UserControl
    {

        #region Propiedades

        private readonly Entrenador entrenador;
        private readonly List<PictureBox> listaPokeballs;

        #endregion

        #region Constructor

        public UC_PokemonRestantes(Entrenador entrenador)
        {
            InitializeComponent();
            this.entrenador = entrenador;
            listaPokeballs = new List<PictureBox>
            {
                pokeball1,
                pokeball2,
                pokeball3,
                pokeball4,
                pokeball5,
                pokeball6
            };
        }

        #endregion

        #region Metodos

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
            if (entrenador.equipo[0].estadisticasActuales.debilitado)
                toolTip.SetToolTip(pokeball1, entrenador.equipo[0].nombre);
        }

        private void Pokeball2_MouseHover(object sender, EventArgs e)
        {
            if (entrenador.equipo[1].estadisticasActuales.debilitado)
                toolTip.SetToolTip(pokeball2, entrenador.equipo[1].nombre);
        }

        private void Pokeball3_MouseHover(object sender, EventArgs e)
        {
            if (entrenador.equipo[2].estadisticasActuales.debilitado)
                toolTip.SetToolTip(pokeball3, entrenador.equipo[2].nombre);
        }

        private void Pokeball4_MouseHover(object sender, EventArgs e)
        {
            if (entrenador.equipo[3].estadisticasActuales.debilitado)
                toolTip.SetToolTip(pokeball4, entrenador.equipo[3].nombre);
        }

        private void Pokeball5_MouseHover(object sender, EventArgs e)
        {
            if (entrenador.equipo[4].estadisticasActuales.debilitado)
                toolTip.SetToolTip(pokeball5, entrenador.equipo[4].nombre);
        }

        private void Pokeball6_MouseHover(object sender, EventArgs e)
        {
            if (entrenador.equipo[5].estadisticasActuales.debilitado)
                toolTip.SetToolTip(pokeball6, entrenador.equipo[5].nombre);
        }

        #endregion

    }
}
