using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;

namespace Pokemon
{
    public class Entrenador
    {

        #region Propiedades

        private Random random;
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
            random = new Random((int)System.DateTime.Now.Ticks);

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            command.CommandText = "select * from ENTRENADOR where ID=" + numEntrenador;
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
            random = new Random((int)DateTime.Now.Ticks);

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"select * from RIVAL where ID={numEntrenador}"
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
            int[] pokemons = {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13,
            14,
            15,
            16,
            17,
            18,
            19,
            20,
            21,
            22,
            23,
            24,
            25,
            26,
            27,
            28,
            29,
            30,
            31,
            32,
            33,
            34,
            35,
            36,
            37,
            38,
            39,
            40,
            41,
            42,
            43,
            44,
            45,
            46,
            47,
            48,
            49,
            50,
            51,
            52,
            53,
            54,
            55,
            56,
            57,
            58,
            59,
            60,
            61,
            62,
            63,
            64,
            65,
            66,
            67,
            68,
            69,
            70,
            71,
            72,
            73,
            74,
            75,
            76,
            77,
            78,
            79,
            80,
            81,
            82,
            83,
            84,
            85,
            86,
            87,
            88,
            89,
            90,
            91,
            92,
            93,
            94,
            95,
            96,
            97,
            98,
            99,
            100,
            101,
            102,
            103,
            104,
            105,
            106,
            107,
            108,
            109,
            110,
            111,
            112,
            113,
            114,
            115,
            116,
            117,
            118,
            119,
            120,
            121,
            122,
            123,
            124,
            125,
            126,
            127,
            128,
            129,
            130,
            131,
            132,
            133,
            134,
            135,
            136,
            137,
            138,
            139,
            140,
            141,
            142,
            143,
            144,
            145,
            146,
            147,
            148,
            149,
            150,
            151,
            152,
            153,
            154,
            155,
            156,
            157,
            158,
            159,
            160,
            161,
            601,
            602,
            603,
            604,
            605,
            606,
            611,
            612,
            613,
            614,
            615,
            616,
            621,
            622,
            623,
            624,
            625,
            626,
            631,
            632,
            633,
            634,
            635,
            636,
            641,
            642,
            643,
            644,
            645,
            646,
            651,
            652,
            653,
            654,
            655,
            656,
            661,
            662,
            663,
            664,
            665,
            666,
            701,
            702,
            703,
            704,
            705,
            706,
            711,
            712,
            713,
            714,
            715,
            716,
            721,
            722,
            723,
            724,
            725,
            726,
            731,
            732,
            733,
            734,
            735,
            736,
            741,
            742,
            743,
            744,
            745,
            746,
            751,
            752,
            753,
            754,
            755,
            756,
            761,
            762,
            763,
            764,
            765,
            766,
            771,
            772,
            773,
            774,
            775,
            776,
            781,
            782,
            783,
            784,
            785,
            786,
            791,
            792,
            793,
            794,
            795,
            796,
            801,
            802,
            803,
            804,
            805,
            806,
            811,
            812,
            813,
            814,
            815,
            816,
            821,
            822,
            823,
            824,
            825,
            826,
            831,
            832,
            833,
            834,
            835,
            836,
            841,
            842,
            843,
            844,
            845,
            846,
            851,
            852,
            853,
            854,
            855,
            856,
            861,
            862,
            863,
            864,
            865,
            866,
            871,
            872,
            873,
            874,
            875,
            876,
            881,
            882,
            883,
            884,
            885,
            886,
            891,
            892,
            893,
            894,
            895,
            896,
            901,
            902,
            903,
            904,
            905,
            906,
            911,
            912,
            913,
            914,
            915,
            916,
            921,
            922,
            923,
            924,
            925,
            926,
            931,
            932,
            933,
            934,
            935,
            936,
            941,
            942,
            943,
            944,
            945,
            946,
            951,
            952,
            953,
            954,
            955,
            956,
            961,
            962,
            963,
            964,
            965,
            966,
            971,
            972,
            973,
            974,
            975,
            976,
            981,
            982,
            983,
            984,
            985,
            986,
            991,
            992,
            993,
            994,
            995,
            996,
            1001,
            1002,
            1003,
            1004,
            1005,
            1006,
            1011,
            1012,
            1013,
            1014,
            1015,
            1016,
            1021,
            1022,
            1023,
            1024,
            1025,
            1026,
            1031,
            1032,
            1033,
            1034,
            1035,
            1036,
            1041,
            1042,
            1043,
            1044,
            1045,
            1046,
            1051,
            1052,
            1053,
            1054,
            1055,
            1056,
            1061,
            1062,
            1063,
            1064,
            1065,
            1066,
            1071,
            1072,
            1073,
            1074,
            1075,
            1076,
            1081,
            1082,
            1083,
            1084,
            1085,
            1086,
            1091,
            1092,
            1093,
            1094,
            1095,
            1096,
            1101,
            1102,
            1103,
            1104,
            1105,
            1106,
            1111,
            1112,
            1113,
            1114,
            1115,
            1116,
            1121,
            1122,
            1123,
            1124,
            1125,
            1126,
            1131,
            1132,
            1133,
            1134,
            1135,
            1136,
            671,
            672,
            673,
            674,
            675,
            676,
            681,
            682,
            683,
            684,
            685,
            686,
            691,
            692,
            693,
            694,
            695,
            696,
            501,
            502,
            503,
            504,
            505,
            506,
            511,
            512,
            513,
            514,
            515,
            516,
            521,
            522,
            523,
            524,
            525,
            526,
            531,
            532,
            533,
            534,
            535,
            536,
            541,
            542,
            543,
            544,
            545,
            546
        };
            return pokemons[random.Next(0, pokemons.Length)];
        }

        #endregion

    }
}


