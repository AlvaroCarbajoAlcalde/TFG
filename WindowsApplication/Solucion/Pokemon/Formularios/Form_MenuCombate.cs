﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_MenuCombate : Form
    {

        #region Propiedades

        public Entrenador entrenador, rival;
        private readonly Form_Inicio inicio;

        #endregion

        #region Constructor

        public Form_MenuCombate(Form_Inicio inicio, Entrenador entrenador)
        {
            this.inicio = inicio;
            InitializeComponent();
            rival = new Entrenador();
            picBoxEntrenadorRival.Image = rival.imageFront;
            picBoxEntrenadorTu.Image = entrenador.imageFront;
            this.entrenador = entrenador;
            List<int> listadoEntrenadores = new List<int>();

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = "SELECT ID FROM RIVAL"
            };
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
                listadoEntrenadores.Add((int)reader[0]);

            reader.Close();
            con.Close();

            for (int i = 0; i < listadoEntrenadores.Count; i++)
                panelRivales.Controls.Add(new UC_ElegirRival(listadoEntrenadores[i], this));
        }

        #endregion

        #region Metodos

        private void Aceptar(object sender, EventArgs e)
        {
            entrenador.GenerarEquipo();
            entrenador.GenerarBolsa();
            new Form_Combate(inicio, entrenador, rival).Show();
            Close();
        }

        #endregion

    }
}
