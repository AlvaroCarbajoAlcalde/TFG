using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Visualizador_Objeto : UserControl
    {

        #region Propiedades

        public Objeto objeto;
        private readonly Form_Combate combate;
        public Pokedex_Objeto pokedexObjeto;

        #endregion

        #region Constructor

        public Visualizador_Objeto(Objeto objeto, Form_Combate combate)
        {
            InitializeComponent();
            this.combate = combate;
            this.objeto = objeto;
            picBoxObjeto.BackgroundImage = Image.FromFile($@"Img\Bolsa\{objeto.id}.png");
            labelNombre.Text = objeto.nombre;
        }

        #endregion

        #region Metodos

        private void On_Click(object sender, EventArgs e)
        {
            if (combate != null)
            {
                combate.accionRealizadaPorBack = new Accion(objeto);

                combate.BtnCancelar_Click(sender, e);
                combate.EmpezarTurno();
            }
        }

        private void On_MouseHover(object sender, EventArgs e)
        {
            BackColor = Color.LightGray;
            if (combate != null)
            {
                if (combate.tm.texto != objeto.descripcion)
                    combate.tm.MostrarTexto(objeto.descripcion);
            }
            else
                pokedexObjeto.labelMostrarDescripcion.Text = objeto.descripcion;
        }

        private void On_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;
        }

        public void SetPokedexObjeto(Pokedex_Objeto pokedexObjeto)
        {
            this.pokedexObjeto = pokedexObjeto;
        }

        #endregion

    }
}
