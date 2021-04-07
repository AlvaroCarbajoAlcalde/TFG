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
    public partial class Visualizador_Turno : UserControl
    {
        public Visualizador_Turno(int turno, int subturno, String texto)
        {
            InitializeComponent();
            labelTurno.Text = "Turno: " + turno;
            labelSubturno.Text = "Subturno: " + subturno;
            labelTexto.Text = texto;
        }
    }
}
