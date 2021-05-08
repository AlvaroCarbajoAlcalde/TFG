using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_SeleccionEquipo : Form
    {

        #region Propiedades

        public Entrenador entrenador;
        public UC_ModificarPokemon selectorEquipo;
        public Form_Inicio inicio;
        public string[] arrayImages = { "Bulgur", "Lance", "Malta", "Semola", "Maiza", "Eco", "LeonAzul", "Lira", "Maya", "MayaWinter" };
        private int imgActual;

        #endregion

        #region Constructor

        public Form_SeleccionEquipo(Entrenador entrenador, Form_Inicio inicio)
        {
            InitializeComponent();
            imgActual = GetPositionOf(entrenador.auxImagen);
            this.inicio = inicio;
            this.entrenador = entrenador;
            selectorEquipo = new UC_ModificarPokemon(0, entrenador, this);
            panelSelectorEquipo.Controls.Add(selectorEquipo);
            PicBoxPkmn1_Click(null, null);
            picBoxPkmn1.BackgroundImage = entrenador.equipo[0].icono;
            picBoxPkmn2.BackgroundImage = entrenador.equipo[1].icono;
            picBoxPkmn3.BackgroundImage = entrenador.equipo[2].icono;
            picBoxPkmn4.BackgroundImage = entrenador.equipo[3].icono;
            picBoxPkmn5.BackgroundImage = entrenador.equipo[4].icono;
            picBoxPkmn6.BackgroundImage = entrenador.equipo[5].icono;
            textBoxNombreEntrenador.Text = entrenador.nombre;
            picBoxImgEntrenador.Image = entrenador.imageFront;
        }

        #endregion

        #region Metodos

        private void PicBoxPkmn1_Click(object sender, EventArgs e)
        {
            indicador1.Visible = indicador2.Visible = indicador3.Visible = indicador4.Visible = indicador5.Visible = indicador6.Visible = false;
            indicador1.Visible = true;
            selectorEquipo.CargarPokemon(entrenador.equipo[0], 0);
        }

        private void PicBoxPkmn2_Click(object sender, EventArgs e)
        {
            indicador1.Visible = indicador2.Visible = indicador3.Visible = indicador4.Visible = indicador5.Visible = indicador6.Visible = false;
            indicador2.Visible = true;
            selectorEquipo.CargarPokemon(entrenador.equipo[1], 1);
        }

        private void PicBoxPkmn3_Click(object sender, EventArgs e)
        {
            indicador1.Visible = indicador2.Visible = indicador3.Visible = indicador4.Visible = indicador5.Visible = indicador6.Visible = false;
            indicador3.Visible = true;
            selectorEquipo.CargarPokemon(entrenador.equipo[2], 2);
        }

        private void PicBoxPkmn4_Click(object sender, EventArgs e)
        {
            indicador1.Visible = indicador2.Visible = indicador3.Visible = indicador4.Visible = indicador5.Visible = indicador6.Visible = false;
            indicador4.Visible = true;
            selectorEquipo.CargarPokemon(entrenador.equipo[3], 3);
        }

        private void PicBoxPkmn5_Click(object sender, EventArgs e)
        {
            indicador1.Visible = indicador2.Visible = indicador3.Visible = indicador4.Visible = indicador5.Visible = indicador6.Visible = false;
            indicador5.Visible = true;
            selectorEquipo.CargarPokemon(entrenador.equipo[4], 4);
        }

        private void PicBoxPkmn6_Click(object sender, EventArgs e)
        {
            indicador1.Visible = indicador2.Visible = indicador3.Visible = indicador4.Visible = indicador5.Visible = indicador6.Visible = false;
            indicador6.Visible = true;
            selectorEquipo.CargarPokemon(entrenador.equipo[5], 5);
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            //Insertamos valores en la tabla
            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();
            string query = "UPDATE entrenador SET nombre=@nombre, imagen=@imagen WHERE id = @id";
            OleDbCommand update = new OleDbCommand(query, con);
            update.Parameters.AddWithValue("@nombre", textBoxNombreEntrenador.Text);
            update.Parameters.AddWithValue("@imagen", arrayImages[imgActual]);
            update.Parameters.AddWithValue("@id", entrenador.numEntrenador);
            update.ExecuteNonQuery();
            con.Close();
            entrenador.nombre = textBoxNombreEntrenador.Text;
            entrenador.imageFront = Image.FromFile($@"Img\Entrenadores\Elegibles\{arrayImages[imgActual]}Front.gif");
            entrenador.imageBack = Image.FromFile($@"Img\Entrenadores\Elegibles\{arrayImages[imgActual]}Back.png");
            inicio.InsertarDatos(entrenador);
            Close();
        }

        private void RightImgArrow_Click(object sender, EventArgs e)
        {
            imgActual++;
            if (imgActual >= arrayImages.Length)
                imgActual = 0;
            picBoxImgEntrenador.Image = Image.FromFile($@"Img\Entrenadores\Elegibles\{arrayImages[imgActual]}Front.gif");
        }

        private void LeftImgArrow_Click(object sender, EventArgs e)
        {
            imgActual--;
            if (imgActual < 0)
                imgActual = arrayImages.Length - 1;
            picBoxImgEntrenador.Image = Image.FromFile($@"Img\Entrenadores\Elegibles\{arrayImages[imgActual]}Front.gif");
        }

        private int GetPositionOf(string value)
        {
            for (int i = 0; i < arrayImages.Length; i++)
                if (arrayImages[i] == value)
                    return i;
            return 0;
        }

        #endregion

    }
}
