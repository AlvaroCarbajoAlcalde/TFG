using System;
using System.Data.OleDb;
using System.IO;

namespace Pokemon
{
    class XML
    {

        #region Variables 

        public static readonly string RUTA_FICHERO_XML_POKEMON = @"TCPFiles\XML_Pokemons.XML";
        public static readonly string RUTA_FICHERO_DATOS_POKEMON = @"TCPFiles\DatosPokemon.xml";

        private static readonly string CONSULTA_POKEMON = "SELECT * FROM POKEMON ORDER BY ID";

        private static readonly string CABECERA_XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        #endregion

        #region XML DatosCombate

        public static void CrearXMLDatosCombate(Pokemon pokUsuario, Pokemon pokRival)
        {
            //Creo el archivo para escribir el XML append false para sobreescribir el archivo si ya existe
            StreamWriter file = new StreamWriter(RUTA_FICHERO_DATOS_POKEMON, append: false);
            file.WriteLine($"{CABECERA_XML}\n");

            file.WriteLine("\n<Pokemons>");

            CrearXMLDatosPokemon(pokUsuario, file);
            CrearXMLDatosPokemon(pokRival, file);

            file.WriteLine("\n</Pokemons>");
            file.Close();
        }

        private static void CrearXMLDatosPokemon(Pokemon pokemon, StreamWriter file)
        {
            //Escribo el XML
            file.WriteLine("\n\t<Pokemon>\n");

            //Datos del pokemon
            file.WriteLine($"\t\t\t<IdPok>{pokemon.numAlmacenamiento}</IdPok>");
            file.WriteLine($"\t\t\t<NumPokedex>{pokemon.fkPokedex}</NumPokedex>");
            file.WriteLine($"\t\t\t<Nombre>{pokemon.nombre}</Nombre>");
            file.WriteLine($"\t\t\t<VidaMax>{pokemon.vidaMax}</VidaMax>");
            file.WriteLine($"\t\t\t<VidaAct>{pokemon.vidaActual}</VidaAct>\n");
            file.WriteLine($"\t\t\t<Estado>{(int)pokemon.estadisticasActuales.estadoActual}</Estado>\n");

            //Movimientos
            EscribirXMLAtaque(file, pokemon.mov1, 1);
            EscribirXMLAtaque(file, pokemon.mov2, 2);
            EscribirXMLAtaque(file, pokemon.mov3, 3);
            EscribirXMLAtaque(file, pokemon.mov4, 4);

            //Tipos del pokemon
            file.WriteLine($"\t\t\t<Tipo1>{(int)pokemon.tipo1}</Tipo1>");
            file.WriteLine($"\t\t\t<Tipo2>{(int)pokemon.tipo2}</Tipo2>");

            file.WriteLine("\n\t</Pokemon>");
        }

        private static void EscribirXMLAtaque(StreamWriter file, Ataque ataque, int numMov)
        {
            if (ataque != null)
            {
                file.WriteLine($"\t\t\t<Mov{numMov}>");
                file.WriteLine($"\t\t\t\t<MovId>{(int)ataque.ataqueID}</MovId>");
                file.WriteLine($"\t\t\t\t<MovNombre>{ataque.nombre}</MovNombre>");
                file.WriteLine($"\t\t\t\t<MovTipo>{(int)ataque.tipo}</MovTipo>");
                file.WriteLine($"\t\t\t\t<PP>{(int)ataque.ppActuales}</PP>");
                file.WriteLine($"\t\t\t\t<PPmax>{(int)ataque.ppMax}</PPmax>");
                file.WriteLine($"\t\t\t</Mov{numMov}>\n");
            }
        }

        #endregion

        #region XML Pokedex

        public static void CrearXMLPokedex()
        {
            try
            {
                //Creo el archivo para escribir el XML
                StreamWriter file = new StreamWriter(RUTA_FICHERO_XML_POKEMON, append: false);

                //Abro la conexion a la base de datos de ACCESS con un singleton
                OleDbConnection con = ConexionAccess.GetConexion();
                con.Open();

                //Hago la consulta
                OleDbCommand selectPokemon = new OleDbCommand(CONSULTA_POKEMON, con);
                OleDbDataReader reader = selectPokemon.ExecuteReader();

                //Leo la consulta
                file.WriteLine($"{CABECERA_XML}\n");
                file.WriteLine("<Pokemons>\n");
                while (reader.Read())
                {
                    file.WriteLine("\t<Pokemon>");

                    file.WriteLine($"\t\t<IdPok>{reader.GetInt32(0)}</IdPok>");
                    file.WriteLine($"\t\t<Nombre>{reader.GetString(1)}</Nombre>");
                    file.WriteLine($"\t\t<Categoria>{reader.GetString(2)}</Categoria>");
                    file.WriteLine($"\t\t<Tipo1>{reader.GetInt32(3)}</Tipo1>");
                    file.WriteLine($"\t\t<Tipo2>{reader.GetInt32(4)}</Tipo2>");
                    file.WriteLine($"\t\t<Descripcion>{reader.GetString(5)}</Descripcion>");
                    file.WriteLine($"\t\t<Peso>{reader.GetString(6)}</Peso>");
                    file.WriteLine($"\t\t<Altura>{reader.GetString(7)}</Altura>");

                    file.WriteLine("\t</Pokemon>\n");
                }
                file.WriteLine("</Pokemons>");
                file.Close();

                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR al crear XML de pokedex: {e.Message}");
            }
        }

        #endregion

    }
}
