using System;
using System.Data.OleDb;

namespace Pokemon
{
    public class Objeto
    {

        #region Propiedades

        public ObjetcID idObjeto;
        public int id;
        public string nombre, descripcion;

        #endregion

        #region Constructor

        public Objeto(int id)
        {
            this.id = id;
            idObjeto = (ObjetcID)Enum.ToObject(typeof(ObjetcID), id);

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            command.CommandText = "select nombre, descripcion from objeto where ID=" + id;
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                nombre = reader[0].ToString();
                descripcion = reader[1].ToString();
            }
            reader.Close();
            con.Close();
        }

        #endregion

        #region Enum ID

        public enum ObjetcID
        {
            Aguafresca = 1,
            Antídoto = 2,
            Antihielo = 3,
            Antiparalizador = 4,
            Antiquemar = 5,
            Curatotal = 6,
            Despertar = 7,
            Hiperpoción = 8,
            Limonada = 9,
            Poción = 10,
            Pociónmáxima = 11,
            Pokéflauta = 12,
            Refresco = 13,
            Restaurartodo = 14,
            GalletaLava = 15,
            LecheMumu = 16,
            RaizEnergia = 17,
            SuperPoción = 18,
            PolvoCuración = 19,
            PolvoEnergía = 20,
            BayaPerasi = 21,
            BayaMeloc = 22,
            BayaSafre = 23,
            BayaZreza = 24,
            BayaAtania = 25,
            BayaZiuela = 26,
            BayaZidra = 27,
            BayaAranja = 28,
            BayaPabaya = 29,
            ElixirMáximo = 30,
            Elixir = 31,
            AtaqueX = 32,
            DefensaX = 33,
            VelocidadX = 34,
            EspecialX = 35,
            CriticoX = 36,
            EvasionX = 37,
            PrecisionX = 38,
            CarameloRaro = 39,
            Proteina = 40,
            Hierro = 41,
            Calcio = 42,
            Carburante = 43,
            MasPS = 44,
            CarameloFuria = 45,
            CaracteristicasX = 46,
            BayaGuaya = 47,
            BayaPeragu = 48,
            BayaUvav = 49,
            BayaMais = 50,
            BayaZanama = 51,
            BayaPinia = 52,
            BayaOram = 53,
            BayaAlgama = 54,
            BayaCaquic = 55,
            BarritaPlus = 56,
            ZumodeBaya = 57,
            HierbaMental = 58,
            FlautaAmarilla = 59,
            FlautaAzul = 60,
            FlautaRoja = 61,
            HierbaMedicinal = 62,
            CenizaSagrada = 63,
            HierbaAguante = 64,
            HierbaLucha = 65
        };

        #endregion

    }
}
