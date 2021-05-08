using System.Data.OleDb;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_LogCombateDetalles : Form
    {
        public Form_LogCombateDetalles(string id)
        {
            InitializeComponent();

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();
            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"select * from log_combate_evento where fk_id_log_combate_equipo like '{id}%' order by id"
            };
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
                panelTurnos.Controls.Add(new Visualizador_Turno((int)reader[3], (int)reader[4], reader[1].ToString()));

            reader.Close();
            con.Close();
        }
    }
}
