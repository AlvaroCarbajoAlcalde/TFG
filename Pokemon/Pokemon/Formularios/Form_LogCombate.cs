using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_LogCombate : Form
    {
        public Form_LogCombate()
        {
            InitializeComponent();

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            command.CommandText = "select * from log_combate order by id";
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
                panelPartidas.Controls.Add(new Visualizador_Partida(reader[0].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(),
                    (int)reader[7], (int)reader[8], (int)reader[9], (int)reader[10], (int)reader[11], (int)reader[12],
                     (int)reader[13], (int)reader[14], (int)reader[15], (int)reader[16], (int)reader[17], (int)reader[18]));

            reader.Close();
            con.Close();
        }

        private void BorrarTodo_Click(object sender, EventArgs e)
        {

        }

    }
}
