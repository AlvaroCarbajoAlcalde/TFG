using System;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_LogCombate : Form
    {

        #region Variables

        private static readonly string deleteLogEvento = "DELETE FROM LOG_COMBATE_EVENTO";
        private static readonly string deleteLogEquipo = "DELETE FROM LOG_COMBATE_EQUIPO";
        private static readonly string deleteLogCombate = "DELETE FROM LOG_COMBATE";

        private static readonly string selectLogCombate = "SELECT * FROM LOG_COMBATE ORDER BY ID";

        private static readonly Label labelVacio = new Label { Text = "No hay partidas." };

        #endregion

        #region Constructor

        public Form_LogCombate()
        {
            InitializeComponent();

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = selectLogCombate
            };

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
                panelPartidas.Controls.Add(new Visualizador_Partida(reader[0].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(),
                    (int)reader[7], (int)reader[8], (int)reader[9], (int)reader[10], (int)reader[11], (int)reader[12],
                    (int)reader[13], (int)reader[14], (int)reader[15], (int)reader[16], (int)reader[17], (int)reader[18]));

            reader.Close();
            con.Close();

            if (panelPartidas.Controls.Count == 0)
                panelPartidas.Controls.Add(labelVacio);
        }

        #endregion

        #region Metodos

        private void BorrarTodo_Click(object sender, EventArgs e)
        {
            //Condiciones para no borrar
            if (panelPartidas.Controls.Count == 0 || panelPartidas.Controls.Contains(labelVacio))
                return;

            if (System.Windows.MessageBox.Show("Borrar todos los datos.", "Borrar datos.", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            panelPartidas.Controls.Clear();

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            //Borrar tabla LOG_COMBATE_EVENTO
            new OleDbCommand
            {
                Connection = con,
                CommandText = deleteLogEvento
            }.ExecuteNonQuery();

            //Borrar tabla LOG_COMBATE_EQUIPO
            new OleDbCommand
            {
                Connection = con,
                CommandText = deleteLogEquipo
            }.ExecuteNonQuery();

            //Borrar tabla LOG_COMBATE
            new OleDbCommand
            {
                Connection = con,
                CommandText = deleteLogCombate
            }.ExecuteNonQuery();

            con.Close();
        }

        #endregion

    }
}
