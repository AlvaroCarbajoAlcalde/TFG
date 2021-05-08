using System;
using System.Data.OleDb;
using System.Drawing;
using System.Media;

namespace Pokemon
{
    public class Pokemon
    {

        #region Propiedades

        public Random random;
        public string nombre, nombrePokedex;
        public int vidaActual, vidaMax, nivel, ataque, defensa, especial, velocidad, fkPokedex, numAlmacenamiento;
        public Tipo tipo1, tipo2;
        public Ataque mov1, mov2, mov3, mov4;
        public Image icono, imagenFront, imagenBack;
        public EstadisticasActuales estadisticasActuales;
        private int auxiliarM1 = 0, auxiliarM2 = 0, auxiliarM3 = 0, auxiliarM4 = 0;
        public bool esShiny;
        public string idLog;

        #endregion

        #region Constructores

        public Pokemon(int numAlmacenamiento)
        {
            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            OleDbCommand command = new OleDbCommand
            {
                Connection = con,
                CommandText = $"select * from ALMACENAMIENTO where ID={numAlmacenamiento}"
            };
            OleDbDataReader reader = command.ExecuteReader();

            //Establecer datos
            if (reader.Read())
            {
                this.numAlmacenamiento = numAlmacenamiento;
                nombre = reader[1].ToString();
                nivel = int.Parse(reader[2].ToString());
                vidaMax = vidaActual = int.Parse(reader[3].ToString());
                ataque = int.Parse(reader[4].ToString());
                defensa = int.Parse(reader[5].ToString());
                especial = int.Parse(reader[6].ToString());
                velocidad = int.Parse(reader[7].ToString());
                fkPokedex = int.Parse(reader[8].ToString());
                random = new Random((int)DateTime.Now.Ticks / fkPokedex);

                //Movimientos aux
                mov1 = mov2 = mov3 = mov4 = null;
                auxiliarM1 = int.Parse(reader[9].ToString());
                auxiliarM2 = int.Parse(reader[10].ToString());
                auxiliarM3 = int.Parse(reader[11].ToString());
                auxiliarM4 = int.Parse(reader[12].ToString());

                esShiny = (bool)reader[13];
            }
            reader.Close();

            OleDbCommand command2 = new OleDbCommand
            {
                Connection = con,
                CommandText = $"select NOMBRE, FK_TIPO1, FK_TIPO2 from POKEMON where ID={fkPokedex}"
            };
            OleDbDataReader reader2 = command2.ExecuteReader();
            //Tipos
            if (reader2.Read())
            {
                nombrePokedex = reader2[0].ToString();
                tipo1 = (Tipo)Enum.ToObject(typeof(Tipo), int.Parse(reader2[1].ToString()));
                tipo2 = (Tipo)Enum.ToObject(typeof(Tipo), int.Parse(reader2[2].ToString()));
            }
            reader2.Close();
            con.Close();

            //Establecer Movimientos
            if (auxiliarM1 != 0)
                mov1 = new Ataque(auxiliarM1);
            if (auxiliarM2 != 0)
                mov2 = new Ataque(auxiliarM2);
            if (auxiliarM3 != 0)
                mov3 = new Ataque(auxiliarM3);
            if (auxiliarM4 != 0)
                mov4 = new Ataque(auxiliarM4);

            if (esShiny || random != null && random.NextDouble() < 0.05) //Random para hacerlo shiny
            {
                esShiny = true;
                //Establecer Imagenes
                icono = Image.FromFile($@"Img\PkmIcons\{fkPokedex}.png");
                imagenFront = Image.FromFile($@"Img\Sprites\Shiny\Front\{nombrePokedex}.gif");
                imagenBack = Image.FromFile($@"Img\Sprites\Shiny\Back\{nombrePokedex}.gif");
            }
            else
            {
                //Establecer Imagenes
                icono = Image.FromFile($@"Img\PkmIcons\{fkPokedex}.png");
                imagenFront = Image.FromFile($@"Img\Sprites\Front\{nombrePokedex}.gif");
                imagenBack = Image.FromFile($@"Img\Sprites\Back\{nombrePokedex}.gif");
            }

            //Estadisticas actuales
            estadisticasActuales = new EstadisticasActuales(this);
        }

        #endregion

        #region Rugido

        public void PlayRugido()
        {
            new System.Threading.Thread(() =>
            {
                new SoundPlayer($@"Sonido\Grito\{fkPokedex}.wav").PlaySync();
            }).Start();
        }

        #endregion

        #region Modificadores de stats durante el combate

        //Al cambiar el pokemon
        public void OnCambiarPokemon()
        {
            if (estadisticasActuales.vecesTransformado > 0)
            {
                int auxPP = 10 - estadisticasActuales.vecesTransformado;

                //Establecer Imagenes
                icono = Image.FromFile($@"Img\PkmIcons\{fkPokedex}.png");
                imagenFront = Image.FromFile($@"Img\Pokemon\{nombrePokedex}Front.gif");
                imagenBack = Image.FromFile($@"Img\Pokemon\{nombrePokedex}Back.gif");

                //Establecer Movimientos
                mov1 = mov2 = mov3 = mov4 = null;
                if (auxiliarM1 != 0)
                    mov1 = new Ataque(auxiliarM1);
                if (auxiliarM2 != 0)
                    mov2 = new Ataque(auxiliarM2);
                if (auxiliarM3 != 0)
                    mov3 = new Ataque(auxiliarM3);
                if (auxiliarM4 != 0)
                    mov4 = new Ataque(auxiliarM4);

                if (mov1 != null && mov1.ataqueID == AtaqueID.TRANSFORMACION)
                    mov1.ppActuales = auxPP;
                if (mov2 != null && mov2.ataqueID == AtaqueID.TRANSFORMACION)
                    mov2.ppActuales = auxPP;
                if (mov3 != null && mov3.ataqueID == AtaqueID.TRANSFORMACION)
                    mov3.ppActuales = auxPP;
                if (mov4 != null && mov4.ataqueID == AtaqueID.TRANSFORMACION)
                    mov4.ppActuales = auxPP;
            }

            //Reseteamos valores de las estadisticas.
            estadisticasActuales.ataqueActual = ataque;
            estadisticasActuales.defensaActual = defensa;
            estadisticasActuales.especialActual = especial;
            estadisticasActuales.velocidadActual = velocidad;
            if (estadisticasActuales.estadoActual == Estado.PARALIZADO)
                estadisticasActuales.velocidadActual /= 4;
            if (estadisticasActuales.estadoActual == Estado.QUEMADO)
                estadisticasActuales.ataqueActual /= 2;

            //Flags estadistica cambiada a 0
            estadisticasActuales.haCambiadoAtaque = estadisticasActuales.haCambiadoDefensa = estadisticasActuales.haCambiadoEspecial =
            estadisticasActuales.haCambiadoEvasion = estadisticasActuales.haCambiadoPrecision =
            estadisticasActuales.haCambiadoVelocidad = estadisticasActuales.haCambiadoCritico = 0;

            //Modificadores a 0
            estadisticasActuales.modificadorAtaque = estadisticasActuales.modificadorDefensa =
                estadisticasActuales.modificadorEspecial = estadisticasActuales.modificadorVelocidad =
                estadisticasActuales.modificadorCritico = estadisticasActuales.modificadorEvasion =
                estadisticasActuales.modificadorPrecision = 0;

            //Reseteamos Flags
            estadisticasActuales.turnosInmovilizado = estadisticasActuales.turnosEspera =
                estadisticasActuales.turnosContinuarMovimiento = estadisticasActuales.dagnoUltimoGolpeRecibido = 0;
            estadisticasActuales.ultimoAtaqueRecibido = estadisticasActuales.movLanzadoTrasEspera = estadisticasActuales.movQueContinua = null;
            estadisticasActuales.haRetrocedido = estadisticasActuales.evitarCambiosEstadistica = estadisticasActuales.pantallaLuz =
                estadisticasActuales.reflejo = false;

            //Sub Estados
            estadisticasActuales.drenadoras = estadisticasActuales.volando = estadisticasActuales.escavando = false;
            estadisticasActuales.confuso = -1;

            //Estado gravemente envenenado pasa a ser envenenado normal
            if (estadisticasActuales.estadoActual == Estado.GRAVEMENTEENVENENADO)
            {
                estadisticasActuales.turnosGravementeEnvenenado = 0;
                estadisticasActuales.estadoActual = Estado.ENVENENADO;
            }
        }

        public void OnNiebla()
        {
            //Modificadores a 0
            estadisticasActuales.modificadorAtaque = estadisticasActuales.modificadorDefensa =
                estadisticasActuales.modificadorEspecial = estadisticasActuales.modificadorVelocidad =
                estadisticasActuales.modificadorCritico = estadisticasActuales.modificadorEvasion =
                estadisticasActuales.modificadorPrecision = 0;

            estadisticasActuales.ataqueActual = ataque;
            estadisticasActuales.defensaActual = defensa;
            estadisticasActuales.especialActual = especial;
            estadisticasActuales.velocidadActual = velocidad;

            estadisticasActuales.pantallaLuz = false;
            estadisticasActuales.reflejo = false;

            estadisticasActuales.evitarCambiosEstadistica = false;
            if (estadisticasActuales.estadoActual == Estado.GRAVEMENTEENVENENADO)
                estadisticasActuales.estadoActual = Estado.ENVENENADO;
        }

        //Para actualizar las stats del pokemon.
        public void CambiarEstadistica(Estadistica estadisticaModificar)
        {
            switch (estadisticaModificar)
            {
                case Estadistica.ATAQUE:
                    estadisticasActuales.ataqueActual = (int)(ataque * GetCambioStat(estadisticasActuales.modificadorAtaque));
                    if (estadisticasActuales.estadoActual == Estado.QUEMADO)
                        estadisticasActuales.ataqueActual /= 2;
                    break;
                case Estadistica.DEFENSA:
                    estadisticasActuales.defensaActual = (int)(defensa * GetCambioStat(estadisticasActuales.modificadorDefensa));
                    break;
                case Estadistica.ESPECIAL:
                    estadisticasActuales.especialActual = (int)(especial * GetCambioStat(estadisticasActuales.modificadorEspecial));
                    break;
                case Estadistica.VELOCIDAD:
                    estadisticasActuales.velocidadActual = (int)(velocidad * GetCambioStat(estadisticasActuales.modificadorVelocidad));
                    if (estadisticasActuales.estadoActual == Estado.PARALIZADO)
                        estadisticasActuales.velocidadActual /= 4;
                    break;
            }
        }

        public double GetCambioStat(int modificador)
        {
            double numerador = 2, denominador = 2;
            if (modificador > 0)
                numerador += modificador;
            if (modificador < 0)
                denominador += -modificador;
            return numerador / denominador;
        }

        #endregion

        #region Mostrar Datos

        public void MostrarDatos()
        {
            Console.WriteLine($"----------  {nombre}  ----------");
            if (estadisticasActuales.debilitado)
                Console.WriteLine("-Debilitado");
            Console.WriteLine($"-Vida Actual: {vidaActual}/{vidaMax}");
            Console.WriteLine($"-Ataque: {estadisticasActuales.ataqueActual}, mod: {estadisticasActuales.modificadorAtaque}, base: {ataque}");
            Console.WriteLine($"-Defensa: {estadisticasActuales.defensaActual}, mod: {estadisticasActuales.modificadorDefensa }, base: {defensa}");
            Console.WriteLine($"-Especial: {estadisticasActuales.especialActual}, mod: {estadisticasActuales.modificadorEspecial }, base: {especial}");
            Console.WriteLine($"-Velocidad: {estadisticasActuales.velocidadActual}, mod: {estadisticasActuales.modificadorVelocidad}, base: {velocidad}");
            Console.WriteLine($"-Precision: {estadisticasActuales.modificadorPrecision}");
            Console.WriteLine($"-Evasion: {estadisticasActuales.modificadorEvasion}");
            Console.WriteLine($"-Critico: {estadisticasActuales.modificadorCritico}");
            if (estadisticasActuales.turnosDormido > 0)
                Console.WriteLine($"-Turnos Dormido: {estadisticasActuales.turnosDormido}");
            if (estadisticasActuales.confuso > 0)
                Console.WriteLine($"-Turnos Confuso: {estadisticasActuales.confuso}");
            if (estadisticasActuales.turnosInmovilizado > 0)
                Console.WriteLine($"-Turnos Inmovil: {estadisticasActuales.turnosInmovilizado}");
            if (estadisticasActuales.estadoActual == Estado.GRAVEMENTEENVENENADO)
                Console.WriteLine($"-Turnos G.Envenenado: {estadisticasActuales.turnosGravementeEnvenenado}");
            if (estadisticasActuales.ultimoAtaqueRecibido != null)
                Console.WriteLine($"-Ult. Ataque Recibido: {estadisticasActuales.ultimoAtaqueRecibido.nombre}");
            if (estadisticasActuales.dagnoUltimoGolpeRecibido > 0)
                Console.WriteLine($"-Ult daño recibido: {estadisticasActuales.dagnoUltimoGolpeRecibido}");
            if (estadisticasActuales.usandoFuria)
                Console.WriteLine("-Usando furia");
            if (estadisticasActuales.volando)
                Console.WriteLine("-Volando");
            if (estadisticasActuales.escavando)
                Console.WriteLine("-Bajo Tierra");
            if (estadisticasActuales.drenadoras)
                Console.WriteLine("-Infectado con drenadoras");
            if (estadisticasActuales.evitarCambiosEstadistica)
                Console.WriteLine("-Inmune a cambios de stat");
            if (estadisticasActuales.haRetrocedido)
                Console.WriteLine("-Ha retrocedido");
            if (estadisticasActuales.pantallaLuz)
                Console.WriteLine("-Bajo efectos de Pantalla Luz");
            if (estadisticasActuales.reflejo)
                Console.WriteLine("-Bajo efectos de Reflejo");
            if (estadisticasActuales.movLanzadoTrasEspera != null)
                Console.WriteLine($"-Mov. tras espera: {estadisticasActuales.movLanzadoTrasEspera.nombre}");
            if (estadisticasActuales.turnosEspera > 0)
                Console.WriteLine($"-Turnos espera: {estadisticasActuales.turnosEspera}");
            if (estadisticasActuales.movQueContinua != null)
                Console.WriteLine($"-Mov. tras espera: {estadisticasActuales.movQueContinua.nombre}");
            if (estadisticasActuales.turnosContinuarMovimiento > 0)
                Console.WriteLine($"-Turnos espera: {estadisticasActuales.turnosContinuarMovimiento}");
        }

        #endregion

    }
}


