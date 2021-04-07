using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Pokemon
{
    public partial class PokemonPokedex : UserControl
    {
        private Pokedex_Pokemon pokedex;
        private int id;

        public PokemonPokedex(int idPokemon, String nombre, Pokedex_Pokemon pokedex)
        {
            InitializeComponent();
            this.pokedex = pokedex;
            this.id = idPokemon;

            //Establecer Datos 
            String id = "";
            if (idPokemon < 100) id += "0";
            if (idPokemon < 10) id += "0";
            nombrePokemon.Text = id + idPokemon + "  " + nombre;

            //Establecer imagen
            iconoPokemon.BackgroundImage = Image.FromFile(@"Img\\PkmIcons\\" + idPokemon + ".png");
        }

        private void OnClick(object sender, EventArgs e)
        {
            pokedex.InsertarDatos(id);
            pokedex.BtnAbrirLista_Click(sender, e);
        }
    }
}
