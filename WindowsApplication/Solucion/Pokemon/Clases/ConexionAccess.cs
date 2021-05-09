using System.Data.OleDb;

namespace Pokemon
{
    class ConexionAccess
    {

        #region Propiedades

        private static OleDbConnection con;

        #endregion

        #region Constructor

        private ConexionAccess()
        {
            con = new OleDbConnection
            {
                ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PokemonDatabase.accdb"
            };
        }

        #endregion

        #region Metodos

        public static OleDbConnection GetConexion()
        {
            if (con == null)
                new ConexionAccess();
            return con;
        }

        #endregion

    }
}
