using System;
using System.Data.OleDb;

namespace Pokemon
{
    public class Ataque
    {

        #region Propiedades

        public int potencia, precision, ppActuales, ppMax, probEstado, idMovimiento;
        public string categoria, nombre, descripcion;
        public int turnosAnulado;
        public Tipo tipo;
        public Estado estadoQueProvoca;
        public AtaqueID ataqueID;

        #endregion

        #region Constructor

        public Ataque(int idMovimiento)
        {
            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"select nombre, fk_tipo, potencia, precision, categoria, pp, estado, probabilidad_estado, descripcion from MOVIMIENTO where ID = {idMovimiento}"
            };
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                turnosAnulado = 0;
                this.idMovimiento = idMovimiento;
                ataqueID = (AtaqueID)Enum.ToObject(typeof(AtaqueID), idMovimiento);
                nombre = reader[0].ToString();
                tipo = (Tipo)Enum.ToObject(typeof(Tipo), int.Parse(reader[1].ToString()));
                potencia = int.Parse(reader[2].ToString());
                precision = int.Parse(reader[3].ToString());
                categoria = reader[4].ToString();
                ppMax = ppActuales = int.Parse(reader[5].ToString());
                estadoQueProvoca = (Estado)Enum.ToObject(typeof(Estado), int.Parse(reader[6].ToString()));
                probEstado = int.Parse(reader[7].ToString());
                descripcion = reader[8].ToString();
            }
            reader.Close();
            con.Close();
        }

        #endregion

    }
}
