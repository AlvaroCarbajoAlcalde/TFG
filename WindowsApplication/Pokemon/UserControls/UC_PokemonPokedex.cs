using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class PokemonPokedex : UserControl
    {

        #region Propiedades

        private readonly Pokedex_Pokemon pokedex;
        private readonly int id;

        #endregion

        #region Constructor

        public PokemonPokedex(int idPokemon, string nombre, Pokedex_Pokemon pokedex)
        {
            InitializeComponent();
            this.pokedex = pokedex;
            this.id = idPokemon;

            //Establecer Datos 
            string id = "";
            if (idPokemon < 100) id += "0";
            if (idPokemon < 10) id += "0";
            nombrePokemon.Text = $"{id}{idPokemon}  {nombre}";

            //Establecer imagen
            iconoPokemon.BackgroundImage = Image.FromFile($@"Img\PkmIcons\{idPokemon}.png");
        }

        #endregion

        #region Metodos

        private void OnClick(object sender, EventArgs e)
        {
            pokedex.InsertarDatos(id);
            pokedex.BtnAbrirLista_Click(sender, e);
        }

        #endregion

    }
}
