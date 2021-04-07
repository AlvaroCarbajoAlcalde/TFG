using System.Data.OleDb;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Pokedex_Movimiento : Form
    {
        public Pokedex_Movimiento()
        {
            InitializeComponent();

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            //Ataques
            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            command.CommandText = "select nombre, FK_TIPO, precision, potencia, descripcion, categoria, id, pp from movimiento where id<>0 order by fk_tipo, categoria, potencia, nombre";
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Visualizador_Ataque v = new Visualizador_Ataque((int)reader[6], reader[0].ToString(), (int)reader[1], reader[5].ToString(), reader[4].ToString(), (int)reader[3], (int)reader[2], (int)reader[7], null);
                v.pokedexMovimiento = this;
                panelMovimientos.Controls.Add(v);
            }

            reader.Close();
            con.Close();
        }
    }
}
