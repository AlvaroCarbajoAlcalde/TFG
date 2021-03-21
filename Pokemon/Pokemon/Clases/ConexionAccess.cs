using System.Data.OleDb;

namespace Pokemon
{
    class ConexionAccess
    {
        private static OleDbConnection con;

        private ConexionAccess()
        {
            con = new OleDbConnection();
            con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PokemonDatabase.accdb";
        }

        public static OleDbConnection GetConexion()
        {
            if (con == null)
                new ConexionAccess();
            return con;
        }
    }
}
