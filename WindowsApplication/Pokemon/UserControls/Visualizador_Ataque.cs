using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Visualizador_Ataque : UserControl
    {

        #region Propiedades

        public int numAtaque, tipo, potencia, precision, pp;
        public string categoria, descripcion, nombre;
        public UC_ModificarPokemon selectorEquipo;
        public Pokedex_Movimiento pokedexMovimiento;

        #endregion

        #region Constructor

        public Visualizador_Ataque(int numAtaque, string nombre, int tipo, string categoria, string descripcion, int potencia, int precision, int pp, UC_ModificarPokemon selectorEquipo)
        {
            InitializeComponent();
            this.numAtaque = numAtaque;
            this.tipo = tipo;
            this.categoria = categoria;
            this.descripcion = descripcion;
            this.selectorEquipo = selectorEquipo;
            this.potencia = potencia;
            this.precision = precision;
            this.nombre = nombre;
            this.pp = pp;

            //Cambiar datos
            labelNombre.Text = nombre;
            picBoxTipo.BackgroundImage = Image.FromFile($@"Img\Tipes\{tipo}.gif");
            picBoxCategoria.BackgroundImage = Image.FromFile($@"Img\Categoria\{categoria}.gif");

            if (selectorEquipo != null && numAtaque == 1)
            {
                selectorEquipo.numMov1 = selectorEquipo.numMov2 = selectorEquipo.numMov3 = selectorEquipo.numMov4 = numAtaque;
                selectorEquipo.labelAtaque1.Text = selectorEquipo.labelAtaque2.Text = selectorEquipo.labelAtaque3.Text = selectorEquipo.labelAtaque4.Text = nombre;
                selectorEquipo.picBoxCategoria1.BackgroundImage = selectorEquipo.picBoxCategoria2.BackgroundImage = selectorEquipo.picBoxCategoria3.BackgroundImage = selectorEquipo.picBoxCategoria4.BackgroundImage = Image.FromFile($@"Img\Categoria\{categoria}.gif");
                selectorEquipo.picBoxTipo1.BackgroundImage = selectorEquipo.picBoxTipo2.BackgroundImage = selectorEquipo.picBoxTipo3.BackgroundImage = selectorEquipo.picBoxTipo4.BackgroundImage = Image.FromFile($@"Img\Tipes\{tipo}.gif");
            }
        }

        #endregion

        #region Metodos

        private void On_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.White;
        }

        private void OnMouseHover(object sender, EventArgs e)
        {
            BackColor = Color.LightGray;
            if (selectorEquipo == null)
            {
                pokedexMovimiento.labelPot.Text = $"POT: {potencia}";
                pokedexMovimiento.labelPre.Text = $"PRE: {precision}";
                pokedexMovimiento.labelPP.Text = $"PP: {pp}";
                pokedexMovimiento.labelNombreAtaque.Text = nombre;
                pokedexMovimiento.labelDescripcion.Text = descripcion;
            }
            else
            {
                selectorEquipo.labelDescripcionAtaque.Text = descripcion;
                selectorEquipo.labelPotencia.Text = $"Pot: {potencia}";
                selectorEquipo.labelPrecision.Text = $"Pre: {precision}";
                selectorEquipo.labelAttackName.Text = labelNombre.Text;
            }
        }
        
        private void ONClick(object sender, EventArgs e)
        {
            if (selectorEquipo == null)
                return;

            switch (selectorEquipo.comboBoxMovimiento.SelectedIndex)
            {
                case 0:
                    selectorEquipo.numMov1 = numAtaque;
                    selectorEquipo.labelAtaque1.Text = nombre;
                    selectorEquipo.picBoxCategoria1.BackgroundImage = Image.FromFile($@"Img\Categoria\{categoria}.gif");
                    selectorEquipo.picBoxTipo1.BackgroundImage = Image.FromFile($@"Img\Tipes\{tipo}.gif");
                    break;
                case 1:
                    selectorEquipo.numMov2 = numAtaque;
                    selectorEquipo.labelAtaque2.Text = nombre;
                    selectorEquipo.picBoxCategoria2.BackgroundImage = Image.FromFile($@"Img\Categoria\{categoria}.gif");
                    selectorEquipo.picBoxTipo2.BackgroundImage = Image.FromFile($@"Img\Tipes\{tipo}.gif");
                    break;
                case 2:
                    selectorEquipo.numMov3 = numAtaque;
                    selectorEquipo.labelAtaque3.Text = nombre;
                    selectorEquipo.picBoxCategoria3.BackgroundImage = Image.FromFile($@"Img\Categoria\{categoria}.gif");
                    selectorEquipo.picBoxTipo3.BackgroundImage = Image.FromFile($@"Img\Tipes\{tipo}.gif");
                    break;
                case 3:
                    selectorEquipo.numMov4 = numAtaque;
                    selectorEquipo.labelAtaque4.Text = nombre;
                    selectorEquipo.picBoxCategoria4.BackgroundImage = Image.FromFile($@"Img\Categoria\{categoria}.gif");
                    selectorEquipo.picBoxTipo4.BackgroundImage = Image.FromFile($@"Img\Tipes\{tipo}.gif");
                    break;
            }
        }

        #endregion

    }
}
