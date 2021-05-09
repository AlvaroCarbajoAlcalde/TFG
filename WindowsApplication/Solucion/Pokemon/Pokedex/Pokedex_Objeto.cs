using System.Windows.Forms;

namespace Pokemon
{
    public partial class Pokedex_Objeto : Form
    {

        #region Constructor

        public Pokedex_Objeto()
        {
            InitializeComponent();
            Visualizador_Objeto selector;
            for (int i = 1; i < 66; i++)
            {
                selector = new Visualizador_Objeto(new Objeto(i), null);
                selector.SetPokedexObjeto(this);
                panelBolsa.Controls.Add(selector);
            }
        }

        #endregion

    }
}
