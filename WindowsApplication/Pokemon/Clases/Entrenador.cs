using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;

namespace Pokemon
{
    public class Entrenador
    {

        #region Propiedades

        private readonly Random random;
        public int auxPok1 = 0, auxPok2 = 0, auxPok3 = 0, auxPok4 = 0, auxPok5 = 0, auxPok6 = 0;
        public int numEntrenador, numPartidas, numVictorias;
        public Image imageFront, imageBack, imageMini, iconP1, iconP2, iconP3, iconP4, iconP5, iconP6;
        public string nombre, auxImagen, rutaImagenFront;
        public Pokemon[] equipo;
        public List<Objeto> objetos;

        #endregion

        #region Constructores

        public Entrenador(int numEntrenador)
        {
            random = new Random((int)DateTime.Now.Ticks);

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"SELECT * FROM ENTRENADOR WHERE ID = {numEntrenador}"
            };
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                equipo = new Pokemon[6];
                this.numEntrenador = numEntrenador;
                nombre = reader[1].ToString();

                auxPok1 = int.Parse(reader[2].ToString());
                auxPok2 = int.Parse(reader[3].ToString());
                auxPok3 = int.Parse(reader[4].ToString());
                auxPok4 = int.Parse(reader[5].ToString());
                auxPok5 = int.Parse(reader[6].ToString());
                auxPok6 = int.Parse(reader[7].ToString());
                numPartidas = int.Parse(reader[8].ToString());
                numVictorias = int.Parse(reader[9].ToString());
                auxImagen = reader[10].ToString();

                imageFront = Image.FromFile($@"Img\Entrenadores\Elegibles\{auxImagen}Front.gif");
                imageBack = Image.FromFile($@"Img\Entrenadores\Elegibles\{auxImagen}Back.png");
                imageMini = Image.FromFile($@"Img\Entrenadores\Elegibles\{auxImagen }Mini.png");

                rutaImagenFront = $@"Img\Entrenadores\Elegibles\{auxImagen}Front.gif";
            }
            reader.Close();
            con.Close();

            //Establecer Iconos equipo
            iconP1 = GetIcon(auxPok1);
            iconP2 = GetIcon(auxPok2);
            iconP3 = GetIcon(auxPok3);
            iconP4 = GetIcon(auxPok4);
            iconP5 = GetIcon(auxPok5);
            iconP6 = GetIcon(auxPok6);
        }

        public Entrenador()
        {
            random = new Random((int)DateTime.Now.Ticks);
            equipo = new Pokemon[6];

            nombre = GenerarNombreAleatorio();
            iconP1 = iconP2 = iconP3 = iconP4 = iconP5 = iconP6 = iconP3 = Image.FromFile(@"Img\PkmIcons\0.png");
            imageFront = imageBack = Image.FromFile($@"Img\Entrenadores\NoElegibles\{nombre}.png");
            rutaImagenFront = $@"Img\Entrenadores\NoElegibles\{nombre}.png";

            //Establecemos los pokemon del equipo
            equipo[0] = new Pokemon(GetNumPokemonAleatorio());
            equipo[1] = new Pokemon(GetNumPokemonAleatorio());
            equipo[2] = new Pokemon(GetNumPokemonAleatorio());
            equipo[3] = new Pokemon(GetNumPokemonAleatorio());
            equipo[4] = new Pokemon(GetNumPokemonAleatorio());
            equipo[5] = new Pokemon(GetNumPokemonAleatorio());
        }

        public Entrenador(Pokemon[] equipo)
        {
            random = new Random((int)DateTime.Now.Ticks);
            this.equipo = equipo;

            nombre = GenerarNombreAleatorio();
            iconP1 = iconP2 = iconP3 = iconP4 = iconP5 = iconP6 = iconP3 = Image.FromFile(@"Img\PkmIcons\0.png");
            imageFront = imageBack = Image.FromFile($@"Img\Entrenadores\NoElegibles\{nombre}.png");
            rutaImagenFront = $@"Img\Entrenadores\NoElegibles\{nombre}.png";
        }

        public Entrenador(string nombre, int pok1, int pok2, int pok3, int pok4, int pok5, int pok6, string rutaImagenEntrenador)
        {
            random = new Random((int)DateTime.Now.Ticks);
            equipo = new Pokemon[6];

            this.nombre = nombre;
            rutaImagenFront = rutaImagenEntrenador;
            iconP1 = iconP2 = iconP3 = iconP4 = iconP5 = iconP6 = iconP3 = Image.FromFile(@"Img\PkmIcons\0.png");
            imageFront = imageBack = Image.FromFile(rutaImagenFront);
            //Establecemos los pokemon del equipo
            equipo[0] = new Pokemon(pok1);
            equipo[1] = new Pokemon(pok2);
            equipo[2] = new Pokemon(pok3);
            equipo[3] = new Pokemon(pok4);
            equipo[4] = new Pokemon(pok5);
            equipo[5] = new Pokemon(pok6);
        }

        public Entrenador(int numEntrenador, string cadena)
        {
            _ = cadena;
            random = new Random((int)DateTime.Now.Ticks);

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"SELECT * FROM RIVAL WHERE ID = {numEntrenador}"
            };
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                equipo = new Pokemon[6];
                this.numEntrenador = numEntrenador;
                nombre = reader[1].ToString();

                auxPok1 = int.Parse(reader[2].ToString());
                auxPok2 = int.Parse(reader[3].ToString());
                auxPok3 = int.Parse(reader[4].ToString());
                auxPok4 = int.Parse(reader[5].ToString());
                auxPok5 = int.Parse(reader[6].ToString());
                auxPok6 = int.Parse(reader[7].ToString());
                auxImagen = reader[8].ToString();

                imageFront = Image.FromFile($@"Img\Entrenadores\Rivales\{auxImagen}");
                imageBack = Image.FromFile($@"Img\Entrenadores\Rivales\{auxImagen}");

                rutaImagenFront = $@"Img\Entrenadores\Rivales\{auxImagen}";
            }
            reader.Close();
            con.Close();
            GenerarEquipo();
        }
        #endregion

        #region Metodos

        public void GenerarBolsa()
        {
            objetos = new List<Objeto>();
            Random random = new Random((int)DateTime.Now.Ticks);
            int numObjetos = random.Next(10, 25);
            for (int i = 0; i < numObjetos; i++)
                objetos.Add(new Objeto(random.Next(1, 66)));
        }

        public void GenerarEquipo()
        {
            //Establecemos los pokemon del equipo
            //Pokemon 1
            if (auxPok1 == 0)
                equipo[0] = new Pokemon(GetNumPokemonAleatorio());
            else
                equipo[0] = new Pokemon(auxPok1);

            //Pokemon 2
            if (auxPok2 == 0)
                equipo[1] = new Pokemon(GetNumPokemonAleatorio());
            else
                equipo[1] = new Pokemon(auxPok2);

            //Pokemon 3
            if (auxPok3 == 0)
                equipo[2] = new Pokemon(GetNumPokemonAleatorio());
            else
                equipo[2] = new Pokemon(auxPok3);

            //Pokemon 4
            if (auxPok4 == 0)
                equipo[3] = new Pokemon(GetNumPokemonAleatorio());
            else
                equipo[3] = new Pokemon(auxPok4);

            //Pokemon 5
            if (auxPok5 == 0)
                equipo[4] = new Pokemon(GetNumPokemonAleatorio());
            else
                equipo[4] = new Pokemon(auxPok5);

            //Pokemon 6
            if (auxPok6 == 0)
                equipo[5] = new Pokemon(GetNumPokemonAleatorio());
            else
                equipo[5] = new Pokemon(auxPok6);
        }

        private Image GetIcon(int auxPok)
        {
            Image toReturn = Image.FromFile(@"Img\PkmIcons\0.png"); ;

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"select fk_pokedex from ALMACENAMIENTO where id={auxPok}"
            };
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
                toReturn = Image.FromFile($@"Img\PkmIcons\{reader[0]}.png");

            reader.Close();
            con.Close();
            return toReturn;
        }

        private string GenerarNombreAleatorio()
        {
            string[] entrenadoresRivales = { "Anciano Ricardo", "Arlequin Roberto", "Artista Godofredo", "Modelo Anastasia",
            "Breakdancer Josefo", "Calvo Manuel", "Camara Ernesto", "Camarera Juanita", "Camarera Felicia",
            "Camarero Alejandro", "Cantautor Bono", "Cazabichos Miguel", "Chica Macarena", "Chica Angela",
            "Chica Maria", "Ciclista Juana", "Ciclista Contador", "Cientifica Ana", "Cientifico Moriarti",
            "Comefuego Hugo", "Criador Juan", "Criador Federico", "Dama Tiana", "Dama Marta", "Damisela Laura",
            "Damisela Lidia", "Domadragon Felipe", "Duque Paco", "Duque Pablo", "Entrenadora Guay Lisa",
            "Entrenador Guay Manolo", "Escolar Iker", "Esquiador Miguel", "Esquiadora Ana", "Idol Lola",
            "Karateka David", "Ladron Anonimo", "Maestra Edurne", "Malabarista Carlos", "Marinero Jaime",
            "Marquesa Alba", "Modelo Carla", "Montagnero Ignacio", "Motorista Unai", "Motorista Alex",
            "Nadador Urdangarin", "Nadadora Sara", "Nadadora Julia", "Nadadora Salma", "Nigno Bien Ignigo",
            "Oficinista Maria", "Pescador Ricardo", "Pokecolector Alberto", "Pokefan Laura", "Pokefan Javi",
            "Pokemaniaco Manuel"};
            return entrenadoresRivales[random.Next(0, entrenadoresRivales.Length)];
        }

        #endregion

        #region Get Pokemon Aleatorio

        public int GetNumPokemonAleatorio()
        {
            List<int> listadoPokemons = new List<int>();

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = "SELECT ID FROM ALMACENAMIENTO"
            };
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
                listadoPokemons.Add(reader.GetInt32(0));

            reader.Close();
            con.Close();

            return listadoPokemons[random.Next(0, listadoPokemons.Count)];
        }

        #endregion

    }
}


