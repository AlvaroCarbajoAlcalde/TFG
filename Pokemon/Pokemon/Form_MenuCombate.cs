using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_MenuCombate : Form
    {
        public Entrenador entrenador, rival;

        public Form_MenuCombate(Entrenador entrenador)
        {
            InitializeComponent();
            rival = new Entrenador();
            picBoxEntrenadorRival.Image = rival.imageFront;
            picBoxEntrenadorTu.Image = entrenador.imageFront;
            this.entrenador = entrenador;
            List<int> listadoEntrenadores = new List<int>();

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            command.CommandText = "select id from RIVAL";
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
                listadoEntrenadores.Add((int)reader[0]);
            reader.Close();
            con.Close();

            for (int i = 0; i < listadoEntrenadores.Count; i++)
                panelRivales.Controls.Add(new UC_ElegirRival(listadoEntrenadores[i], this));
        }

        private void Aceptar(object sender, EventArgs e)
        {
            entrenador.GenerarEquipo();
            entrenador.GenerarBolsa();
            new Form_Combate(entrenador, rival).Show();
            Close();
        }
    }
}
