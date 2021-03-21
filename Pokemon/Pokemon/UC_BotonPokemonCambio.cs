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
    public partial class UC_BotonPokemonCambio : UserControl
    {
        public Pokemon pokemon;
        public Form_Combate combate;

        public UC_BotonPokemonCambio(Pokemon pokemon, Form_Combate combate)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            this.combate = combate;
            labelNombre.Text = pokemon.nombre;
            labelVida.Text = pokemon.vidaActual + "/" + pokemon.vidaMax;
            picBoxIcono.BackgroundImage = pokemon.icono;
            barraDeVida.Size = new Size((int)(((double)pokemon.vidaActual / (double)pokemon.vidaMax) * 62), 5);

            if (pokemon.vidaActual < 0.3 * pokemon.vidaMax) barraDeVida.BackgroundImage = Image.FromFile(@"Img\Recursos\VidaAmarilla.png");
            else barraDeVida.BackgroundImage = Image.FromFile(@"Img\Recursos\VidaVerde.png");

            if (pokemon.vidaActual <= 0)
                BackgroundImage = Image.FromFile(@"Img\Recursos\PokemonChangeRIP.png");

            picBoxEstado.BackgroundImage = Image.FromFile(@"Img\Estado\" + (int)pokemon.estadisticasActuales.estadoActual + ".png");
        }

        private void SelectorCambio_MouseEnter(object sender, EventArgs e)
        {
            if (pokemon.vidaActual > 0)
                BackgroundImage = Image.FromFile(@"Img\Recursos\PokemonChangeSelected.png");
        }

        private void SelectorCambio_MouseLeave(object sender, EventArgs e)
        {
            if (pokemon.vidaActual > 0)
                BackgroundImage = Image.FromFile(@"Img\Recursos\PokemonChange.png");
        }

        private void SelectorCambio_Click(object sender, EventArgs e)
        {
            if (combate.pokemonBack != pokemon && pokemon.vidaActual > 0)
            {
                //Si el turno esta activo. Es decir tu pokemon se debilita.
                if (combate.pokemonBack.estadisticasActuales.debilitado)
                {
                    combate.CambiarPokemon(pokemon, "Back");
                    combate.BtnCancelar_Click(null, null);
                }
                else
                {
                    pokemon.OnCambiarPokemon();
                    combate.accionRealizadaPorBack = new Accion(pokemon);
                    combate.BtnCancelar_Click(null, null);
                    combate.EmpezarTurno();
                }
            }
        }
    }
}
