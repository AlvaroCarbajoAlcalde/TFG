using System.Windows.Forms;

namespace Pokemon
{
    public partial class Visualizador_Turno : UserControl
    {

        #region Constructor

        public Visualizador_Turno(int turno, int subturno, string texto)
        {
            InitializeComponent();
            labelTurno.Text = $"Turno: {turno}";
            labelSubturno.Text = $"Subturno: {subturno}";
            labelTexto.Text = texto;
        }

        #endregion

    }
}
