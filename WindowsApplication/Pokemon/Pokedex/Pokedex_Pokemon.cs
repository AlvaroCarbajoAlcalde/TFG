using System;
using System.Data.OleDb;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Pokedex_Pokemon : Form
    {

        #region Propiedades

        private bool front;
        private Image img;
        private string nombrePokemon;
        private int idActual, numDatos;
        private Size originalSize;
        private Rectangle recBtnAnt, recBtnSig, recBtnBuscar, recCuadroBusqueda, recLabelId, recImgPokemon, recLabelNombre, recTxtPeso, recLabelPeso,
            recTxtAltura, recLabelAltura, recTipo1, recTipo2, recDescripcion, recHuella, recBtnGrito, recLabelCategoria, recFloatLayout,
            recBtnLista, recFondoLista;


        #endregion

        #region Constructor

        public Pokedex_Pokemon()
        {
            InitializeComponent();
            originalSize = this.Size;
            recBtnAnt = btnAnterior.Bounds;
            recBtnSig = btnSiguiente.Bounds;
            recBtnBuscar = btnBuscar.Bounds;
            recCuadroBusqueda = cuadroBusqueda.Bounds;
            recLabelId = labelId.Bounds;
            recImgPokemon = imagenPkmn.Bounds;
            recLabelNombre = labelNombrePokemon.Bounds;
            recTxtPeso = labelTxtPeso.Bounds;
            recLabelPeso = labelPeso.Bounds;
            recTxtAltura = labelTxtAltura.Bounds;
            recLabelAltura = labelAltura.Bounds;
            recTipo1 = tipo1.Bounds;
            recTipo2 = tipo2.Bounds;
            recDescripcion = labelDescripcion.Bounds;
            recHuella = imagenHuella.Bounds;
            recBtnGrito = btnGrito.Bounds;
            recLabelCategoria = labelCategoria.Bounds;
            recFloatLayout = flowPanel.Bounds;
            recBtnLista = btnAbrirLista.Bounds;
            recFondoLista = fondoLista.Bounds;

            numDatos = CalcularNumDatos();
            idActual = 1;
            InsertarDatos(idActual);
        }

        #endregion

        #region Metodos

        public void BtnAbrirLista_Click(object sender, EventArgs e)
        {
            if (flowPanel.Visible == true)
            {
                flowPanel.Visible = fondoLista.Visible = false;
                imagenPkmn.Visible = true;
                btnAbrirLista.BackgroundImage = Image.FromFile(@"Img\\Botones\\FlechaDcha.png");
            }
            else
            {
                imagenPkmn.Visible = false;
                flowPanel.Visible = fondoLista.Visible = true;
                btnAbrirLista.BackgroundImage = Image.FromFile(@"Img\\Botones\\FlechaAbajo.png");
            }
        }

        private void ImagenPkmn_Click(object sender, EventArgs e)
        {
            if (front)
            {
                img = Image.FromFile(@"Img\Sprites\Front\" + nombrePokemon + ".gif");
                front = false;
            }
            else
            {
                img = Image.FromFile(@"Img\Sprites\Back\" + nombrePokemon + ".gif");
                front = true;
            }

            imagenPkmn.Image = img;
            recImgPokemon.Size = img.Size;
            recImgPokemon.Location = new Point(93 - img.Width / 2, 160 - img.Height / 2);
            ResizeControl(recImgPokemon, imagenPkmn);
        }

        public void InsertarDatos(int pokemonId)
        {
            //Insertamos los datos desde access
            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            command.CommandText = "select id, nombre, categoria, descripcion, peso, altura, FK_TIPO1, FK_TIPO2 from Pokemon where Id=" + pokemonId;
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    front = true;
                    idActual = int.Parse(reader[0].ToString());
                    //Establecer Datos
                    labelId.Text = "";
                    if (idActual < 10) labelId.Text += "0";
                    if (idActual < 100) labelId.Text += "0";
                    labelId.Text += idActual;
                    labelNombrePokemon.Text = reader[1].ToString();
                    labelCategoria.Text = "Pokémon " + reader[2].ToString();
                    labelDescripcion.Text = reader[3].ToString();
                    labelPeso.Text = reader[4].ToString();
                    labelAltura.Text = reader[5].ToString();
                    string tipo_1 = reader[6].ToString();
                    string tipo_2 = reader[7].ToString();

                    //Establecer imagen
                    nombrePokemon = reader[1].ToString();
                    ImagenPkmn_Click(this, null);

                    //Establecer huella
                    Image img = Image.FromFile(@"Img\\Huellas\\" + idActual + ".png");
                    imagenHuella.BackgroundImage = img;
                    recHuella.Size = img.Size;
                    recHuella.Location = new Point(238 - img.Width / 2, 166 - img.Height / 2);
                    ResizeControl(recHuella, imagenHuella);

                    //Invisible todo
                    tipo1.Visible = false;
                    tipo2.Visible = false;

                    tipo1.BackgroundImage = Image.FromFile(@"Img\\Tipes\\" + tipo_1 + ".gif");
                    tipo1.Visible = true;
                    //Si tiene dos tipos
                    if (tipo_2 != "0")
                    {
                        tipo2.BackgroundImage = Image.FromFile(@"Img\\Tipes\\" + tipo_2 + ".gif");
                        tipo2.Visible = true;
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            con.Close();
        }

        private int CalcularNumDatos()
        {
            //Contamos el numero de pokemon de la base de datos
            int contador = 0;
            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            command.CommandText = "select nombre from Pokemon order by id";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                contador++;
                flowPanel.Controls.Add(new PokemonPokedex(contador, reader[0].ToString(), this));
            }
            reader.Close();
            con.Close();
            return contador;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string entrada = cuadroBusqueda.Text;
            int newid;
            //Si buscamos por id
            if (int.TryParse(entrada, out newid))
            {
                if (newid > 0 && newid <= numDatos)
                {
                    InsertarDatos(newid);
                    idActual = newid;
                }
                else MessageBox.Show("El número no corresponde a ningún Pokemon.");
            }
            //Si buscamos por nombre
            else
            {
                OleDbConnection con = ConexionAccess.GetConexion();
                con.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = con;
                command.CommandText = "select Id from Pokemon where Nombre='" + entrada + "'";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    idActual = int.Parse(reader[0].ToString());
                    con.Close();
                    InsertarDatos(idActual);
                }
                else
                {
                    con.Close();
                    MessageBox.Show("El nombre no corresponde a ningún Pokemon.");
                }
            }
            cuadroBusqueda.Text = "";
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (idActual == 1) idActual = numDatos;
            else idActual--;
            InsertarDatos(idActual);
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (idActual == numDatos) idActual = 1;
            else idActual++;
            InsertarDatos(idActual);
        }

        private void ButtonGrito_Click(object sender, EventArgs e)
        {
            //Reproducimos audio pokemon
            SoundPlayer soundPlayer = new SoundPlayer($@"Sonido\Grito\{idActual}.wav");
            soundPlayer.Play();
        }

        #endregion

        #region MustafaResize

        private void Pokedex_Resize(object sender, EventArgs e)
        {
            ResizeControl(recLabelCategoria, labelCategoria);
            ResizeControl(recImgPokemon, imagenPkmn);
            ResizeControl(recLabelNombre, labelNombrePokemon);
            ResizeControl(recLabelId, labelId);
            ResizeControl(recBtnGrito, btnGrito);
            ResizeControl(recBtnAnt, btnAnterior);
            ResizeControl(recBtnSig, btnSiguiente);
            ResizeControl(recHuella, imagenHuella);
            ResizeControl(recDescripcion, labelDescripcion);
            ResizeControl(recTxtAltura, labelTxtAltura);
            ResizeControl(recTxtPeso, labelTxtPeso);
            ResizeControl(recLabelAltura, labelAltura);
            ResizeControl(recLabelPeso, labelPeso);
            ResizeControl(recBtnBuscar, btnBuscar);
            ResizeControl(recCuadroBusqueda, cuadroBusqueda);
            ResizeControl(recTipo1, tipo1);
            ResizeControl(recTipo2, tipo2);
            ResizeControl(recBtnLista, btnAbrirLista);
            ResizeControl(recFloatLayout, flowPanel, true);
            ResizeControl(recFondoLista, fondoLista);
        }

        private void ResizeControl(Rectangle originalControl, Control control, bool sinFont)
        {
            float xRatio = (float)(this.Width) / (float)(originalSize.Width);
            float yRatio = (float)(this.Height) / (float)(originalSize.Height);

            int newX = (int)(originalControl.Location.X * xRatio);
            int newY = (int)(originalControl.Location.Y * yRatio);
            int newWidth = (int)(originalControl.Size.Width * xRatio);
            int newHeight = (int)(originalControl.Size.Height * yRatio);

            control.Size = new Size(newWidth, newHeight);
            control.Location = new Point(newX, newY);

            if (!sinFont)
                control.Font = new Font(control.Font.FontFamily, 11 * yRatio, control.Font.Style);
        }

        private void ResizeControl(Rectangle originalControl, Control control)
        {
            ResizeControl(originalControl, control, false);
        }

        #endregion

    }
}
