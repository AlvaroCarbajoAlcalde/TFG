using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Visualizador_Pokemon : UserControl
    {

        #region Propiedades

        private readonly UC_ModificarPokemon selectorEquipo;
        private readonly int numPokedex, hp, atq, def, esp, vel, tipo1, tipo2;
        private readonly string nombre;

        #endregion

        #region Constructor

        public Visualizador_Pokemon(int numPokedex, string nombre, int tipe1, int tipe2, UC_ModificarPokemon selectorEquipo, int hp, int atq, int def, int esp, int vel)
        {
            InitializeComponent();
            this.tipo1 = tipe1;
            this.tipo2 = tipe2;
            this.numPokedex = numPokedex;
            this.nombre = nombre;
            this.selectorEquipo = selectorEquipo;
            this.hp = hp;
            this.atq = atq;
            this.def = def;
            this.esp = esp;
            this.vel = vel;
            picBoxIcon.BackgroundImage = Image.FromFile($@"Img\PkmIcons\{numPokedex}.png");
            picBoxTipo1.BackgroundImage = Image.FromFile($@"Img\Tipes\{tipe1}.gif");
            picBoxTipo2.BackgroundImage = Image.FromFile($@"Img\Tipes\{tipe2}.gif");
            labelNombre.Text = nombre;
            if (numPokedex == 1)
                OnClick(null, null);
        }

        #endregion

        #region Metodos

        public void OnClick(object sender, EventArgs e)
        {
            selectorEquipo.SetPokemon(numPokedex, nombre);
            selectorEquipo.textBoxApodo.Text = nombre;
            selectorEquipo.textBoxAtq.Text = atq + "";
            selectorEquipo.textBoxDef.Text = def + "";
            selectorEquipo.textBoxHp.Text = hp + "";
            selectorEquipo.textBoxEsp.Text = esp + "";
            selectorEquipo.textBoxVel.Text = vel + "";
            selectorEquipo.checkBoxShiny.Checked = false;
            selectorEquipo.tipo1 = tipo1;
            selectorEquipo.tipo2 = tipo2;
        }

        #endregion

    }
}
