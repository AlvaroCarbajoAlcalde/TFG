using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_Inicio : Form
    {

        #region Propiedades

        private readonly Entrenador[] listaEntrenadores;
        private int idActual;
        private readonly int numDatos;
        private bool front;
        private Size originalSize;
        private Rectangle recBtnCombate, recBtnOnline, recBtnSig, recBtnAnt, recLabNombre, recImg, recNom, recPanelDatos, recTxtVictorias, recTxtPartidas,
            recTxtId, recTxtEquipo, recLabelVictorias, recLabelPartidas, recLabelId, recTxtCombate, recTxtOnline, recIcono1, recIcono2, recIcono3,
            recIcono4, recIcono5, recIcono6, recEstrellas, recTituloCard, recTitulo, recAlvaro, recBtnCentro, recLabelCentro, recLabelMultiplayer, recBtnMultiplayer;

        #endregion

        #region Constructor

        public Form_Inicio()
        {
            InitializeComponent();

            //Cargar entrenadores
            idActual = 1;
            numDatos = 6;
            listaEntrenadores = new Entrenador[numDatos];
            for (int i = 1; i <= numDatos; i++)
                listaEntrenadores[i - 1] = new Entrenador(i);

            InsertarDatos(listaEntrenadores[0]);

            #region Rectangles
            originalSize = this.Size;
            recEstrellas = picBoxStars.Bounds;
            recTituloCard = picBoxTitle.Bounds;
            recPanelDatos = panelDatosEntrenador.Bounds;
            recBtnAnt = btnAnterior.Bounds;
            recBtnSig = btnSiguiente.Bounds;
            recLabNombre = labelNombreEntrenador.Bounds;
            recNom = labelTxtNombre.Bounds;
            recImg = imagenEntrenador.Bounds;
            recBtnCombate = btnCombate.Bounds;
            recBtnOnline = btnRogueLike.Bounds;
            recTxtEquipo = labelTxtEquipo.Bounds;
            recTxtId = labelTxtId.Bounds;
            recTxtPartidas = labelTxtPartidas.Bounds;
            recLabelId = labelId.Bounds;
            recLabelPartidas = labelPartidas.Bounds;
            recLabelVictorias = labelVictorias.Bounds;
            recTxtVictorias = labelTxtVictorias.Bounds;
            recTxtCombate = labelTxtCombate.Bounds;
            recTxtOnline = labelTxtRogueLike.Bounds;
            recIcono1 = icono1.Bounds;
            recIcono2 = icono2.Bounds;
            recIcono3 = icono3.Bounds;
            recIcono4 = icono4.Bounds;
            recIcono5 = icono5.Bounds;
            recIcono6 = icono6.Bounds;
            recAlvaro = picBoxTituloAlvaro.Bounds;
            recTitulo = picBoxTituloPokemon.Bounds;
            recLabelCentro = labelTxtVsRojo.Bounds;
            recBtnCentro = btnVsRojo.Bounds;
            recBtnMultiplayer = btnMultiplayer.Bounds;
            recLabelMultiplayer = labelTxtMultiplayer.Bounds;
            #endregion

        }

        #endregion

        #region Metodos

        public void InsertarDatos(Entrenador aux)
        {
            front = true;

            //Establecer Datos
            labelId.Text = aux.numEntrenador + "";
            labelNombreEntrenador.Text = aux.nombre;
            labelPartidas.Text = aux.numPartidas + "";
            labelVictorias.Text = aux.numVictorias + "";

            //Imagen entrenador
            ImagenEntrenador_Click(null, null);

            //Iconos del equipo
            icono1.BackgroundImage = aux.iconP1;
            icono2.BackgroundImage = aux.iconP2;
            icono3.BackgroundImage = aux.iconP3;
            icono4.BackgroundImage = aux.iconP4;
            icono5.BackgroundImage = aux.iconP5;
            icono6.BackgroundImage = aux.iconP6;
        }

        #endregion

        #region ButtonsClicks

        private void BtnMultiplayer_Click(object sender, EventArgs e)
        {
            Entrenador entrenadorRandom = new Entrenador();
            listaEntrenadores[idActual - 1].GenerarEquipo();
            listaEntrenadores[idActual - 1].GenerarBolsa();
            new Form_Combate(this, listaEntrenadores[idActual - 1], entrenadorRandom, null, true).Show();
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (idActual == numDatos) idActual = 1;
            else idActual++;
            InsertarDatos(listaEntrenadores[idActual - 1]);
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (idActual == 1) idActual = numDatos;
            else idActual--;
            InsertarDatos(listaEntrenadores[idActual - 1]);
        }

        private void BtnCombate_Click(object sender, EventArgs e)
        {
            Entrenador entrenadorRandom = new Entrenador();
            listaEntrenadores[idActual - 1].GenerarEquipo();
            listaEntrenadores[idActual - 1].GenerarBolsa();
            new Form_Combate(this, listaEntrenadores[idActual - 1], entrenadorRandom).Show();
        }

        private void ImagenEntrenador_Click(object sender, EventArgs e)
        {
            if (front)
            {
                imagenEntrenador.Image = listaEntrenadores[idActual - 1].imageFront;
                front = false;
            }
            else
            {
                imagenEntrenador.Image = listaEntrenadores[idActual - 1].imageBack;
                front = true;
            }
        }

        private void RogueLike_Click(object sender, EventArgs e)
        {
            Hide();
            new Form_RogueLike(this, idActual).Show();
        }

        private void MenuLogCombate_Click(object sender, EventArgs e)
        {
            new Form_LogCombate().Show();
        }

        private void MenuPokedexMovimiento_Click(object sender, EventArgs e)
        {
            new Pokedex_Movimiento().Show();
        }

        private void MenuPokedexObjeto_Click(object sender, EventArgs e)
        {
            new Pokedex_Objeto().Show();
        }

        private void MenuPokedexPokemon_Click(object sender, EventArgs e)
        {
            new Pokedex_Pokemon().Show();
        }

        private void EquipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listaEntrenadores[idActual - 1].GenerarEquipo();
            new Form_SeleccionEquipo(listaEntrenadores[idActual - 1], this).Show();
        }

        private void ElegirRivalClick(object sender, EventArgs e)
        {
            new Form_MenuCombate(this, listaEntrenadores[idActual - 1]).Show();
        }

        #endregion

        #region MustafaResize

        private void Inicio_Resize(object sender, EventArgs e)
        {
            ResizeControl(recPanelDatos, panelDatosEntrenador);
            ResizeControl(recImg, imagenEntrenador);
            ResizeControl(recLabNombre, labelNombreEntrenador);
            ResizeControl(recNom, labelTxtNombre);
            ResizeControl(recBtnOnline, btnRogueLike);
            ResizeControl(recBtnAnt, btnAnterior);
            ResizeControl(recBtnSig, btnSiguiente);
            ResizeControl(recBtnCombate, btnCombate);
            ResizeControl(recTxtEquipo, labelTxtEquipo);
            ResizeControl(recTxtPartidas, labelTxtPartidas);
            ResizeControl(recTxtVictorias, labelTxtVictorias);
            ResizeControl(recLabelId, labelId);
            ResizeControl(recLabelPartidas, labelPartidas);
            ResizeControl(recLabelVictorias, labelVictorias);
            ResizeControl(recTxtId, labelTxtId);
            ResizeControl(recTxtCombate, labelTxtCombate);
            ResizeControl(recTxtOnline, labelTxtRogueLike);
            ResizeControl(recIcono1, icono1);
            ResizeControl(recIcono2, icono2);
            ResizeControl(recIcono3, icono3);
            ResizeControl(recIcono4, icono4);
            ResizeControl(recIcono5, icono5);
            ResizeControl(recIcono6, icono6);
            ResizeControl(recTituloCard, picBoxTitle);
            ResizeControl(recEstrellas, picBoxStars);
            ResizeControl(recTitulo, picBoxTituloPokemon);
            ResizeControl(recAlvaro, picBoxTituloAlvaro);
            ResizeControl(recBtnCentro, btnVsRojo);
            ResizeControl(recLabelCentro, labelTxtVsRojo);
            ResizeControl(recLabelMultiplayer, labelTxtMultiplayer);
            ResizeControl(recBtnMultiplayer, btnMultiplayer);
        }

        private void ResizeControl(Rectangle originalControl, Control control)
        {
            float xRatio = Width / (float)originalSize.Width;
            float yRatio = Height / (float)originalSize.Height;

            int newX = (int)(originalControl.Location.X * xRatio);
            int newY = (int)(originalControl.Location.Y * yRatio);
            int newWidth = (int)(originalControl.Size.Width * xRatio);
            int newHeight = (int)(originalControl.Size.Height * yRatio);

            control.Size = new Size(newWidth, newHeight);
            control.Location = new Point(newX, newY);

            control.Font = new Font(control.Font.FontFamily, 11 * yRatio, control.Font.Style);
        }

        #endregion

    }
}
