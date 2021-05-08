using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_RogueLike : Form
    {

        #region Propiedades

        private readonly Entrenador entrenador;
        private Entrenador entrenadorRival;
        private int numCombate, auxEquipo;
        private int auxPokElegible1, auxPokElegible2, auxPokElegible3;
        private Pokemon pokElegible1, pokElegible2, pokElegible3;
        private Objeto objetoRecibido;
        private readonly Random random;

        private static readonly int NIVEL1 = 479;
        private static readonly int NIVEL2 = 559;
        private static readonly int NIVEL3 = 643;

        private readonly List<int> listaNivel1, listaNivel2, listaNivel3, listaNivel4, listaTodos;
        private readonly int[] listaRivalesNvl1, listaRivalesNvl2, listaRivalesNvl3, listaRivalesNvl4, listaRivalesLegendarios;

        private readonly Form_Inicio inicio;

        #endregion

        #region Constructor

        public Form_RogueLike(Form_Inicio inicio)
        {
            this.inicio = inicio;
            InitializeComponent();
            numCombate = 1;
            auxEquipo = 0;
            auxPokElegible1 = auxPokElegible2 = auxPokElegible3 = 0;
            objetoRecibido = new Objeto(1);
            random = new Random((int)DateTime.Now.Ticks);

            #region Listas Rivales

            listaNivel1 = new List<int>();
            listaNivel2 = new List<int>();
            listaNivel3 = new List<int>();
            listaNivel4 = new List<int>();
            listaTodos = new List<int>();
            int[] aux1 = { 29, 30, 49, 50, 51, 52, 55, 56 };
            int[] aux2 = { 9, 11, 14, 15, 20, 21, 23, 25, 26, 27, 28, 32, 33, 34, 36, 41, 54 };
            int[] aux3 = { 10, 12, 13, 16, 17, 18, 19, 22, 24, 31, 35, 37, 38, 40 };
            int[] aux4 = { 1, 2, 3, 4, 5, 6, 7, 8, 39, 53 };
            int[] aux5 = { 42, 43, 44, 45, 46, 47 };
            listaRivalesNvl1 = aux1;
            listaRivalesNvl2 = aux2;
            listaRivalesNvl3 = aux3;
            listaRivalesNvl4 = aux4;
            listaRivalesLegendarios = aux5;

            #endregion

            entrenador = new Entrenador(6);
            entrenador.GenerarEquipo();
            AlmacenarDatosListas();
            ActualizarPicBox();

            //Agnadir objetos
            entrenador.objetos = new List<Objeto>
            {
                new Objeto(1),
                new Objeto(7),
                new Objeto(10),
                new Objeto(14),
                new Objeto(24)
            };

            RerrollPremios();
        }

        #endregion

        #region Metodos
        private void ActualizarPicBox()
        {
            picBoxPkmn1.BackgroundImage = entrenador.equipo[0].icono;
            picBoxPkmn2.BackgroundImage = entrenador.equipo[1].icono;
            picBoxPkmn3.BackgroundImage = entrenador.equipo[2].icono;
            picBoxPkmn4.BackgroundImage = entrenador.equipo[3].icono;
            picBoxPkmn5.BackgroundImage = entrenador.equipo[4].icono;
            picBoxPkmn6.BackgroundImage = entrenador.equipo[5].icono;

            if (pokElegible1 != null)
                picBoxElegido1.BackgroundImage = pokElegible1.icono;
            if (pokElegible2 != null)
                picBoxElegido2.BackgroundImage = pokElegible2.icono;
            if (pokElegible3 != null)
                picBoxElegido3.BackgroundImage = pokElegible3.icono;

            picBoxObjetoObtenido.BackgroundImage = Image.FromFile($@"Img\Bolsa\{objetoRecibido.id}.png");
        }

        private void AlmacenarDatosListas()
        {
            int suma;
            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = "SELECT ID, VIDA, ATAQUE, DEFENSA, ESPECIAL, VELOCIDAD FROM ALMACENAMIENTO"
            };

            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listaTodos.Add((int)reader[0]);
                suma = (int)reader[1] + (int)reader[2] + (int)reader[3] + (int)reader[4] + (int)reader[5];
                if (suma < NIVEL1)
                    listaNivel1.Add((int)reader[0]);
                else if (suma < NIVEL2)
                    listaNivel2.Add((int)reader[0]);
                else if (suma < NIVEL3)
                    listaNivel3.Add((int)reader[0]);
                else
                    listaNivel4.Add((int)reader[0]);
            }
            reader.Close();
            con.Close();
        }

        private void RerrollPremios()
        {
            int mediaJugador = CalcularMediaJugador();

            //Combate vs BOSS
            if (numCombate % 3 == 0)
            {
                auxPokElegible1 = listaTodos[random.Next(0, listaTodos.Count)];

                auxPokElegible2 = entrenadorRival.equipo[random.Next(0, 6)].numAlmacenamiento;

                if (mediaJugador < NIVEL2)
                    auxPokElegible3 = listaNivel2[random.Next(0, listaNivel2.Count)];
                else if (mediaJugador < NIVEL3)
                    auxPokElegible3 = listaNivel3[random.Next(0, listaNivel3.Count)];
                else
                    auxPokElegible3 = listaNivel4[random.Next(0, listaNivel4.Count)];
            }
            //Combate normal
            else
            {
                if (mediaJugador < NIVEL2)
                {
                    auxPokElegible1 = listaNivel2[random.Next(0, listaNivel2.Count)];
                    auxPokElegible2 = listaNivel2[random.Next(0, listaNivel2.Count)];
                    auxPokElegible3 = listaNivel2[random.Next(0, listaNivel2.Count)];
                }
                else if (mediaJugador < NIVEL3)
                {
                    auxPokElegible1 = listaNivel3[random.Next(0, listaNivel3.Count)];
                    auxPokElegible2 = listaNivel3[random.Next(0, listaNivel3.Count)];
                    auxPokElegible3 = listaNivel3[random.Next(0, listaNivel3.Count)];
                }
                else
                {
                    auxPokElegible1 = listaNivel4[random.Next(0, listaNivel4.Count)];
                    auxPokElegible2 = listaNivel4[random.Next(0, listaNivel4.Count)];
                    auxPokElegible3 = listaNivel4[random.Next(0, listaNivel4.Count)];
                }
            }

            pokElegible1 = new Pokemon(auxPokElegible1);
            pokElegible2 = new Pokemon(auxPokElegible2);
            pokElegible3 = new Pokemon(auxPokElegible3);

            objetoRecibido = new Objeto(random.Next(1, 65));

            ActualizarPicBox();
        }

        private int CalcularMediaJugador()
        {
            int suma = 0;
            for (int i = 0; i < 6; i++)
                suma +=
                    entrenador.equipo[i].vidaMax +
                    entrenador.equipo[i].ataque +
                    entrenador.equipo[i].defensa +
                    entrenador.equipo[i].especial +
                    entrenador.equipo[i].velocidad;
            return suma / 6;
        }

        private void AceptaCambio_Click(object sender, EventArgs e)
        {
            Pokemon elegido;

            entrenador.objetos.Add(objetoRecibido);

            //Pokemon del cambio
            if (selectArrow1.Visible)
                elegido = pokElegible1;
            else if (selectArrow2.Visible)
                elegido = pokElegible2;
            else
                elegido = pokElegible3;

            switch (auxEquipo)
            {
                case 0:
                    entrenador.auxPok1 = elegido.numAlmacenamiento;
                    break;
                case 1:
                    entrenador.auxPok2 = elegido.numAlmacenamiento;
                    break;
                case 2:
                    entrenador.auxPok3 = elegido.numAlmacenamiento;
                    break;
                case 3:
                    entrenador.auxPok4 = elegido.numAlmacenamiento;
                    break;
                case 4:
                    entrenador.auxPok5 = elegido.numAlmacenamiento;
                    break;
                case 5:
                    entrenador.auxPok6 = elegido.numAlmacenamiento;
                    break;
            }

            panelPremios.Visible = false;
        }

        private void SeleccionarEquipoRival(int nivel)
        {
            Pokemon[] equipo = new Pokemon[6];
            List<int> lista;

            switch (nivel)
            {
                case 1:
                    lista = listaNivel1;
                    break;
                case 2:
                    lista = listaNivel2;
                    break;
                case 3:
                    lista = listaNivel3;
                    break;
                case 4:
                    lista = listaNivel4;
                    break;
                default:
                    lista = listaTodos;
                    break;
            }

            for (int i = 0; i < 6; i++)
                equipo[i] = new Pokemon(lista[random.Next(0, lista.Count)]);

            entrenadorRival = new Entrenador(equipo);
        }

        private void BtnCombate_Click(object sender, MouseEventArgs e)
        {
            switch (numCombate)
            {
                case 1:
                case 2:
                    SeleccionarEquipoRival(1);
                    break;
                //Primer BOSS
                case 3:
                    entrenadorRival = new Entrenador(listaRivalesNvl1[random.Next(0, listaRivalesNvl1.Length)], "Rival1");
                    break;
                case 4:
                case 5:
                    SeleccionarEquipoRival(2);
                    break;
                //Segundo BOSS
                case 6:
                    entrenadorRival = new Entrenador(listaRivalesNvl2[random.Next(0, listaRivalesNvl2.Length)], "Rival2");
                    break;
                case 7:
                case 8:
                    SeleccionarEquipoRival(3);
                    break;
                //Tercer BOSS
                case 9:
                    entrenadorRival = new Entrenador(listaRivalesNvl3[random.Next(0, listaRivalesNvl3.Length)], "Rival3");
                    break;
                case 10:
                case 11:
                    SeleccionarEquipoRival(4);
                    break;
                //Cuarto BOSS
                case 12:
                    entrenadorRival = new Entrenador(listaRivalesNvl4[random.Next(0, listaRivalesNvl4.Length)], "Rival4");
                    break;
                //Pelea vs legendarios
                case 13:
                    entrenadorRival = new Entrenador(listaRivalesLegendarios[random.Next(0, listaRivalesLegendarios.Length)], "Legendarios");
                    break;
            }

            entrenador.GenerarEquipo();
            Form_Combate combate = new Form_Combate(inicio, entrenador, entrenadorRival, this);
            combate.Show();
            Hide();
            combate.Text = $"Combate {numCombate}";
            numCombate++;
        }

        public void CombateFinalizado(bool ganas)
        {
            Show();
            if (ganas)
            {
                RerrollPremios();
                panelPremios.Visible = true;
            }
            else
            {
                Close();
            }
        }

        #endregion

        #region Arrows
        private void PicBoxPkmn1_Click(object sender, EventArgs e)
        {
            selectArrowEquipo1.Visible = selectArrowEquipo2.Visible = selectArrowEquipo3.Visible = selectArrowEquipo4.Visible = selectArrowEquipo5.Visible = selectArrowEquipo6.Visible = false;
            selectArrowEquipo1.Visible = true;
            auxEquipo = 0;
        }

        private void PicBoxPkmn2_Click(object sender, EventArgs e)
        {
            selectArrowEquipo1.Visible = selectArrowEquipo2.Visible = selectArrowEquipo3.Visible = selectArrowEquipo4.Visible = selectArrowEquipo5.Visible = selectArrowEquipo6.Visible = false;
            selectArrowEquipo2.Visible = true;
            auxEquipo = 1;
        }

        private void PicBoxPkmn3_Click(object sender, EventArgs e)
        {
            selectArrowEquipo1.Visible = selectArrowEquipo2.Visible = selectArrowEquipo3.Visible = selectArrowEquipo4.Visible = selectArrowEquipo5.Visible = selectArrowEquipo6.Visible = false;
            selectArrowEquipo3.Visible = true;
            auxEquipo = 2;
        }

        private void PicBoxPkmn4_Click(object sender, EventArgs e)
        {
            selectArrowEquipo1.Visible = selectArrowEquipo2.Visible = selectArrowEquipo3.Visible = selectArrowEquipo4.Visible = selectArrowEquipo5.Visible = selectArrowEquipo6.Visible = false;
            selectArrowEquipo4.Visible = true;
            auxEquipo = 3;
        }

        private void PicBoxPkmn5_Click(object sender, EventArgs e)
        {
            selectArrowEquipo1.Visible = selectArrowEquipo2.Visible = selectArrowEquipo3.Visible = selectArrowEquipo4.Visible = selectArrowEquipo5.Visible = selectArrowEquipo6.Visible = false;
            selectArrowEquipo5.Visible = true;
            auxEquipo = 4;
        }

        private void PicBoxPkmn6_Click(object sender, EventArgs e)
        {
            selectArrowEquipo1.Visible = selectArrowEquipo2.Visible = selectArrowEquipo3.Visible = selectArrowEquipo4.Visible = selectArrowEquipo5.Visible = selectArrowEquipo6.Visible = false;
            selectArrowEquipo6.Visible = true;
            auxEquipo = 5;
        }

        private void PicBoxElegido1_Click(object sender, EventArgs e)
        {
            selectArrow1.Visible = selectArrow2.Visible = selectArrow3.Visible = false;
            selectArrow1.Visible = true;
        }

        private void PicBoxElegido2_Click(object sender, EventArgs e)
        {
            selectArrow1.Visible = selectArrow2.Visible = selectArrow3.Visible = false;
            selectArrow2.Visible = true;
        }

        private void PicBoxElegido3_Click(object sender, EventArgs e)
        {
            selectArrow1.Visible = selectArrow2.Visible = selectArrow3.Visible = false;
            selectArrow3.Visible = true;
        }

        #endregion

    }
}
