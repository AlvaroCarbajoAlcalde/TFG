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
    public partial class UC_BotonAtaque : UserControl
    {
        public Ataque ataque;
        public Form_Combate combate;

        public UC_BotonAtaque(Ataque ataque, Form_Combate combate)
        {
            InitializeComponent();
            this.ataque = ataque;
            this.combate = combate;

            if (ataque != null)
            {
                if (ataque.ppActuales < 0)
                    ataque.ppActuales = 0;

                panelMov.BackgroundImage = Image.FromFile(@"Img\\PanelesMovimiento\\" + (int)ataque.tipo + ".png");
                labelPP.Text = "PP " + ataque.ppActuales + "/" + ataque.ppMax;
                labelNombreAtaque.Text = ataque.nombre;

                if (ataque.turnosAnulado > 0)
                    panelMov.BackgroundImage = Image.FromFile(@"Img\\PanelesMovimiento\\Bloqueado.png");
            }
            else
            {
                panelMov.BackgroundImage = Image.FromFile(@"Img\\PanelesMovimiento\\0.png");
                labelPP.Text = "";
                labelNombreAtaque.Text = "";
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (ataque == null || ataque.ppActuales <= 0 || ataque.turnosAnulado > 0)
                return;

            combate.accionRealizadaPorBack = new Accion(ataque);

            combate.BtnCancelar_Click(sender, e);
            combate.EmpezarTurno();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (ataque != null && ataque.turnosAnulado <= 0 && ataque.ppActuales > 0)
                panelMov.BackgroundImage = Image.FromFile(@"Img\\PanelesMovimiento\\" + (int)ataque.tipo + ".png");
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (ataque != null && ataque.turnosAnulado <= 0 && ataque.ppActuales > 0)
                panelMov.BackgroundImage = Image.FromFile(@"Img\\PanelesMovimientoHover\\" + (int)ataque.tipo + ".png");
        }

        private void OnHover(object sender, EventArgs e)
        {
            if (ataque != null && combate.tm.texto != ataque.descripcion)
                combate.tm.MostrarTexto(ataque.descripcion);
        }
    }
}
