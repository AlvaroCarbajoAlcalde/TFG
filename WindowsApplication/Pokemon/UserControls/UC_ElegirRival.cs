using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class UC_ElegirRival : UserControl
    {

        #region Propiedades 

        public int id;
        private readonly Form_MenuCombate menuCombate;
        private readonly int aux1, aux2, aux3, aux4, aux5, aux6;
        private readonly string nombre, rutaImagen;

        private readonly Image imagenEntrenador;
        private readonly Panel[] listaPaneles;

        #endregion

        #region Constructor

        public UC_ElegirRival(int id, Form_MenuCombate menuCombate)
        {
            InitializeComponent();
            this.menuCombate = menuCombate;
            aux1 = aux2 = aux3 = aux4 = aux5 = aux6 = 0;
            this.id = id;
            listaPaneles = new Panel[6];
            listaPaneles[0] = panelPok1;
            listaPaneles[1] = panelPok2;
            listaPaneles[2] = panelPok3;
            listaPaneles[3] = panelPok4;
            listaPaneles[4] = panelPok5;
            listaPaneles[5] = panelPok6;

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"select * from RIVAL where id={id}"
            };
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                nombre = labelNombreEntrenador.Text = reader[1].ToString();
                rutaImagen = $@"Img\Entrenadores\Rivales\{reader[8]}";
                imagenEntrenador = picBoxEntrenador.Image = Image.FromFile(rutaImagen);
                aux1 = (int)reader[2];
                aux2 = (int)reader[3];
                aux3 = (int)reader[4];
                aux4 = (int)reader[5];
                aux5 = (int)reader[6];
                aux6 = (int)reader[7];
            }
            reader.Close();
            con.Close();

            GetIcons(aux1, aux2, aux3, aux4, aux5, aux6);
        }

        #endregion

        #region Metodos

        private void OnClick(object sender, EventArgs e)
        {
            menuCombate.rival = new Entrenador(nombre, aux1, aux2, aux3, aux4, aux5, aux6, rutaImagen);
            menuCombate.picBoxEntrenadorRival.Image = imagenEntrenador;
        }

        private Image GetIcons(int auxPok1, int auxPok2, int auxPok3, int auxPok4, int auxPok5, int auxPok6)
        {
            Image toReturn = Image.FromFile(@"Img\\PkmIcons\\0.png"); ;

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"select fk_pokedex from ALMACENAMIENTO where id={auxPok1} or id={auxPok2} or id={auxPok3} or id={auxPok4} or id={auxPok5} or id={auxPok6}"
            };
            OleDbDataReader reader = command.ExecuteReader();

            int contador = 0;
            while (reader.Read())
            {
                listaPaneles[contador].BackgroundImage = Image.FromFile($@"Img\PkmIcons\{reader[0]}.png");
                contador++;
            }

            reader.Close();
            con.Close();
            return toReturn;
        }

        #endregion

    }
}
