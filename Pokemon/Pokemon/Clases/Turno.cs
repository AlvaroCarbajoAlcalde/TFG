using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace Pokemon
{
    class Turno
    {

        #region Propiedades

        private SoundPlayer player;
        private Form_Combate combate;
        private Random random;
        private Timer timer;
        private int ticks;
        private String primeroEnAtacar;

        #endregion

        #region Constructor

        public Turno(Form_Combate combate)
        {
            ticks = 0;
            timer = new Timer();
            timer.Interval = 5300;
            timer.Tick += new System.EventHandler(this.Timer_Tick);
            random = new Random((int)System.DateTime.Now.Ticks);
            this.combate = combate;
            SeleccionarPrimeroEnAtacar();

            Timer_Tick(null, null);
            timer.Enabled = true;
        }

        #endregion

        #region Log

        public void InsertLogEvento(Pokemon pokemon, int subturno, String descripcion)
        {
            //Insertar datos base datos
            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();
            String sql = "insert into log_combate_evento (descripcion, FK_ID_LOG_COMBATE_EQUIPO, turno, subturno) values (@des, @log, @tur, @sub)";
            OleDbCommand insert = new OleDbCommand(sql, con);
            insert.Parameters.Add("@des", descripcion);
            insert.Parameters.Add("@log", pokemon.idLog);
            insert.Parameters.Add("@tur", combate.numTurnos);
            insert.Parameters.Add("@sub", subturno);
            insert.ExecuteNonQuery();
            con.Close();
        }

        #endregion

        #region Timer controlador de subturnos

        public void Timer_Tick(object sender, EventArgs e)
        {
            String msgMostrar = "";
            switch (ticks)
            {
                //Primer subturno
                case 0:
                    if (primeroEnAtacar == "Back")
                    {
                        switch (combate.accionRealizadaPorBack.tipoAccion)
                        {
                            case Accion.TipoAccion.ATAQUE:
                                msgMostrar = Ataque(combate.pokemonBack, combate.accionRealizadaPorBack.ataqueUsado, combate.pokemonFront);
                                break;
                            case Accion.TipoAccion.CAMBIOPOKEMON:
                                msgMostrar = "¡" + combate.pokemonBack.nombre + ", cambio!\n" + "¡Adelante, " +
                                    combate.accionRealizadaPorBack.pokemonACambiar.nombre + "!";
                                combate.CambiarPokemon(combate.accionRealizadaPorBack.pokemonACambiar, "Back");
                                break;
                            case Accion.TipoAccion.OBJETO:
                                msgMostrar = UsarObjeto(combate.entrenadorTu, combate.pokemonBack, combate.accionRealizadaPorBack.objetoUsado);
                                break;
                        }
                        InsertLogEvento(combate.pokemonBack, 1, msgMostrar);
                    }
                    else
                    {
                        switch (combate.accionRealizadaPorFront.tipoAccion)
                        {
                            case Accion.TipoAccion.ATAQUE:
                                msgMostrar = Ataque(combate.pokemonFront, combate.accionRealizadaPorFront.ataqueUsado, combate.pokemonBack);
                                break;
                            case Accion.TipoAccion.CAMBIOPOKEMON:
                                break;
                            case Accion.TipoAccion.OBJETO:
                                break;
                        }
                        InsertLogEvento(combate.pokemonFront, 1, msgMostrar);
                    }

                    ActualizarParteGrafica();
                    combate.tm.MostrarTexto(msgMostrar);
                    break;

                //Segundo subturno
                case 1:
                    if (primeroEnAtacar == "Back")
                    {
                        if (combate.pokemonFront.estadisticasActuales.debilitado)
                        {
                            ticks++;
                            Timer_Tick(null, null);
                            break;
                        }
                        switch (combate.accionRealizadaPorFront.tipoAccion)
                        {
                            case Accion.TipoAccion.ATAQUE:
                                msgMostrar = Ataque(combate.pokemonFront, combate.accionRealizadaPorFront.ataqueUsado, combate.pokemonBack);
                                break;
                            case Accion.TipoAccion.CAMBIOPOKEMON:
                                break;
                            case Accion.TipoAccion.OBJETO:
                                break;
                        }
                        InsertLogEvento(combate.pokemonFront, 2, msgMostrar);
                    }
                    else
                    {
                        if (combate.pokemonBack.estadisticasActuales.debilitado)
                        {
                            ticks++;
                            Timer_Tick(null, null);
                            break;
                        }
                        switch (combate.accionRealizadaPorBack.tipoAccion)
                        {
                            case Accion.TipoAccion.ATAQUE:
                                msgMostrar = Ataque(combate.pokemonBack, combate.accionRealizadaPorBack.ataqueUsado, combate.pokemonFront);
                                break;
                            case Accion.TipoAccion.CAMBIOPOKEMON:
                                msgMostrar = "¡" + combate.pokemonBack.nombre + ", cambio!\n" + "¡Adelante, " +
                                    combate.accionRealizadaPorBack.pokemonACambiar.nombre + "!";
                                combate.CambiarPokemon(combate.accionRealizadaPorBack.pokemonACambiar, "Back");
                                break;
                            case Accion.TipoAccion.OBJETO:
                                msgMostrar = UsarObjeto(combate.entrenadorTu, combate.pokemonBack, combate.accionRealizadaPorBack.objetoUsado);
                                break;
                        }
                        InsertLogEvento(combate.pokemonBack, 2, msgMostrar);
                    }

                    ActualizarParteGrafica();
                    combate.tm.MostrarTexto(msgMostrar);
                    break;

                //Tercer subturno
                case 2:
                    if (!SufreDOT(combate.pokemonFront) || combate.pokemonFront.estadisticasActuales.debilitado)
                    {
                        ticks++;
                        Timer_Tick(null, null);
                        break;
                    }
                    msgMostrar = DOT(combate.pokemonFront, combate.pokemonBack);
                    InsertLogEvento(combate.pokemonFront, 3, msgMostrar);
                    ActualizarParteGrafica();
                    combate.tm.MostrarTexto(msgMostrar);
                    break;

                //Cuarto subturno
                case 3:
                    if (!SufreDOT(combate.pokemonBack) || combate.pokemonBack.estadisticasActuales.debilitado)
                    {
                        ticks++;
                        Timer_Tick(null, null);
                        break;
                    }
                    msgMostrar = DOT(combate.pokemonBack, combate.pokemonFront);
                    InsertLogEvento(combate.pokemonBack, 4, msgMostrar);
                    ActualizarParteGrafica();
                    combate.tm.MostrarTexto(msgMostrar);
                    break;

                default:
                    timer.Enabled = false;
                    combate.TerminarTurno();
                    break;
            }
            ticks++;
        }

        #endregion

        #region Objetos

        public string UsarObjeto(Entrenador entrenador, Pokemon pokemon, Objeto objetoUsado)
        {
            //Si está debilitado.
            if (pokemon.estadisticasActuales.debilitado)
                return "";

            string textoSalida = entrenador.nombre + " ha usado " + objetoUsado.nombre + ".\n";

            switch (objetoUsado.idObjeto)
            {
                case Objeto.ObjetcID.Aguafresca:
                    textoSalida += EfectoObjeto(50, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.Antídoto:
                    textoSalida += EfectoObjeto(0, Estado.ENVENENADO, pokemon);
                    break;

                case Objeto.ObjetcID.Antihielo:
                    textoSalida += EfectoObjeto(0, Estado.CONGELADO, pokemon);
                    break;

                case Objeto.ObjetcID.Antiparalizador:
                    textoSalida += EfectoObjeto(0, Estado.PARALIZADO, pokemon);
                    break;

                case Objeto.ObjetcID.Antiquemar:
                    textoSalida += EfectoObjeto(0, Estado.QUEMADO, pokemon);
                    break;

                case Objeto.ObjetcID.Curatotal:
                    textoSalida += EfectoObjeto(0, Estado.TODOS, pokemon);
                    break;

                case Objeto.ObjetcID.Despertar:
                    textoSalida += EfectoObjeto(0, Estado.DORMIDO, pokemon);
                    break;

                case Objeto.ObjetcID.Hiperpoción:
                    textoSalida += EfectoObjeto(200, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.Limonada:
                    textoSalida += EfectoObjeto(80, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.Poción:
                    textoSalida += EfectoObjeto(40, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.Pociónmáxima:
                    textoSalida += EfectoObjeto(pokemon.vidaMax, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.Pokéflauta:
                    textoSalida += EfectoObjeto(0, Estado.DORMIDO, pokemon);
                    break;

                case Objeto.ObjetcID.Refresco:
                    textoSalida += EfectoObjeto(60, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.Restaurartodo:
                    textoSalida += EfectoObjeto(pokemon.vidaMax, Estado.TODOS, pokemon);
                    break;

                case Objeto.ObjetcID.GalletaLava:
                    textoSalida += EfectoObjeto(0, Estado.TODOS, pokemon);
                    break;

                case Objeto.ObjetcID.LecheMumu:
                    textoSalida += EfectoObjeto(100, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.RaizEnergia:
                    textoSalida += EfectoObjeto(200, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.SuperPoción:
                    textoSalida += EfectoObjeto(80, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.PolvoCuración:
                    textoSalida += EfectoObjeto(0, Estado.TODOS, pokemon);
                    break;

                case Objeto.ObjetcID.PolvoEnergía:
                    textoSalida += EfectoObjeto(50, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaPerasi:
                    textoSalida += EfectoObjeto(10, Estado.CONGELADO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaMeloc:
                    textoSalida += EfectoObjeto(10, Estado.ENVENENADO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaSafre:
                    textoSalida += EfectoObjeto(10, Estado.QUEMADO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaZreza:
                    textoSalida += EfectoObjeto(10, Estado.PARALIZADO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaAtania:
                    textoSalida += EfectoObjeto(10, Estado.DORMIDO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaZiuela:
                    textoSalida += EfectoObjeto(10, Estado.TODOS, pokemon);
                    break;

                case Objeto.ObjetcID.BayaZidra:
                    textoSalida += EfectoObjeto(30, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaAranja:
                    textoSalida += EfectoObjeto(20, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaPabaya:
                    textoSalida += EfectoObjeto(40, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.Elixir:
                    if (pokemon.mov1 != null)
                    {
                        pokemon.mov1.ppActuales = pokemon.mov1.ppActuales + 5;
                        if (pokemon.mov1.ppActuales > pokemon.mov1.ppMax)
                            pokemon.mov1.ppActuales = pokemon.mov1.ppMax;
                    }
                    if (pokemon.mov2 != null)
                    {
                        pokemon.mov2.ppActuales = pokemon.mov2.ppActuales + 5;
                        if (pokemon.mov2.ppActuales > pokemon.mov2.ppMax)
                            pokemon.mov2.ppActuales = pokemon.mov2.ppMax;
                    }
                    if (pokemon.mov3 != null)
                    {
                        pokemon.mov3.ppActuales = pokemon.mov3.ppActuales + 5;
                        if (pokemon.mov3.ppActuales > pokemon.mov3.ppMax)
                            pokemon.mov3.ppActuales = pokemon.mov3.ppMax;
                    }
                    if (pokemon.mov4 != null)
                    {
                        pokemon.mov4.ppActuales = pokemon.mov4.ppActuales + 5;
                        if (pokemon.mov4.ppActuales > pokemon.mov4.ppMax)
                            pokemon.mov4.ppActuales = pokemon.mov4.ppMax;
                    }
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    break;

                case Objeto.ObjetcID.ElixirMáximo:
                    if (pokemon.mov1 != null)
                        pokemon.mov1.ppActuales = pokemon.mov1.ppMax;
                    if (pokemon.mov2 != null)
                        pokemon.mov2.ppActuales = pokemon.mov2.ppMax;
                    if (pokemon.mov3 != null)
                        pokemon.mov3.ppActuales = pokemon.mov3.ppMax;
                    if (pokemon.mov4 != null)
                        pokemon.mov4.ppActuales = pokemon.mov4.ppMax;
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    break;

                case Objeto.ObjetcID.AtaqueX:
                    if (pokemon.estadisticasActuales.modificadorAtaque < 6)
                    {
                        textoSalida += "El ataque de " + pokemon.nombre + " subió enormemente.\n";
                        pokemon.estadisticasActuales.modificadorAtaque += 2;
                        if (pokemon.estadisticasActuales.modificadorAtaque > 6)
                            pokemon.estadisticasActuales.modificadorAtaque = 6;
                        pokemon.CambiarEstadistica(Estadistica.ATAQUE);
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "El ataque de " + pokemon.nombre + " no puede subir más.\n";
                    break;

                case Objeto.ObjetcID.DefensaX:
                    if (pokemon.estadisticasActuales.modificadorDefensa < 6)
                    {
                        textoSalida += "La defensa de " + pokemon.nombre + " subió enormemente.\n";
                        pokemon.estadisticasActuales.modificadorDefensa += 2;
                        if (pokemon.estadisticasActuales.modificadorDefensa > 6)
                            pokemon.estadisticasActuales.modificadorDefensa = 6;
                        pokemon.CambiarEstadistica(Estadistica.DEFENSA);
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "La defensa de " + pokemon.nombre + " no puede subir más.\n";
                    break;

                case Objeto.ObjetcID.VelocidadX:
                    if (pokemon.estadisticasActuales.modificadorVelocidad < 6)
                    {
                        textoSalida += "La velocidad de " + pokemon.nombre + " subió enormemente.\n";
                        pokemon.estadisticasActuales.modificadorVelocidad += 2;
                        if (pokemon.estadisticasActuales.modificadorVelocidad > 6)
                            pokemon.estadisticasActuales.modificadorVelocidad = 6;
                        pokemon.CambiarEstadistica(Estadistica.VELOCIDAD);
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "La velocidad de " + pokemon.nombre + " no puede subir más.\n";
                    break;

                case Objeto.ObjetcID.EspecialX:
                    if (pokemon.estadisticasActuales.modificadorEspecial < 6)
                    {
                        textoSalida += "El especial de " + pokemon.nombre + " subió enormemente.\n";
                        pokemon.estadisticasActuales.modificadorEspecial += 2;
                        if (pokemon.estadisticasActuales.modificadorEspecial > 6)
                            pokemon.estadisticasActuales.modificadorEspecial = 6;
                        pokemon.CambiarEstadistica(Estadistica.ESPECIAL);
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "El especial de " + pokemon.nombre + " no puede subir más.\n";
                    break;

                case Objeto.ObjetcID.PrecisionX:
                    if (pokemon.estadisticasActuales.modificadorPrecision < 6)
                    {
                        textoSalida += "La precision de " + pokemon.nombre + " subió enormemente.\n";
                        pokemon.estadisticasActuales.modificadorPrecision += 2;
                        if (pokemon.estadisticasActuales.modificadorPrecision > 6)
                            pokemon.estadisticasActuales.modificadorPrecision = 6;
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "La precision de " + pokemon.nombre + " no puede subir más. \n";
                    break;

                case Objeto.ObjetcID.EvasionX:
                    if (pokemon.estadisticasActuales.modificadorEvasion < 6)
                    {
                        textoSalida += "La evasion de " + pokemon.nombre + " subió enormemente. \n";
                        pokemon.estadisticasActuales.modificadorEvasion += 2;
                        if (pokemon.estadisticasActuales.modificadorEvasion > 6)
                            pokemon.estadisticasActuales.modificadorEvasion = 6;
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "La evasion de " + pokemon.nombre + " no puede subir más. \n";
                    break;

                case Objeto.ObjetcID.CriticoX:
                    if (pokemon.estadisticasActuales.modificadorCritico < 4)
                    {
                        textoSalida += "El critico de " + pokemon.nombre + " subió enormemente. \n";
                        pokemon.estadisticasActuales.modificadorCritico += 2;
                        if (pokemon.estadisticasActuales.modificadorCritico > 4)
                            pokemon.estadisticasActuales.modificadorCritico = 4;
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "El critico de " + pokemon.nombre + " no puede subir más. \n";
                    break;

                case Objeto.ObjetcID.CaracteristicasX:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    textoSalida += "Todas las estadísticas de " + pokemon.nombre + " subieron. \n";
                    if (pokemon.estadisticasActuales.modificadorCritico < 4)
                        pokemon.estadisticasActuales.modificadorCritico++;
                    if (pokemon.estadisticasActuales.modificadorAtaque < 6)
                        pokemon.estadisticasActuales.modificadorAtaque++;
                    if (pokemon.estadisticasActuales.modificadorDefensa < 6)
                        pokemon.estadisticasActuales.modificadorDefensa++;
                    if (pokemon.estadisticasActuales.modificadorEspecial < 6)
                        pokemon.estadisticasActuales.modificadorEspecial++;
                    if (pokemon.estadisticasActuales.modificadorVelocidad < 6)
                        pokemon.estadisticasActuales.modificadorVelocidad++;
                    if (pokemon.estadisticasActuales.modificadorPrecision < 6)
                        pokemon.estadisticasActuales.modificadorPrecision++;
                    if (pokemon.estadisticasActuales.modificadorEvasion < 6)
                        pokemon.estadisticasActuales.modificadorEvasion++;
                    pokemon.CambiarEstadistica(Estadistica.ATAQUE);
                    pokemon.CambiarEstadistica(Estadistica.DEFENSA);
                    pokemon.CambiarEstadistica(Estadistica.ESPECIAL);
                    pokemon.CambiarEstadistica(Estadistica.VELOCIDAD);
                    break;

                case Objeto.ObjetcID.CarameloRaro:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    pokemon.vidaMax += 25;
                    pokemon.vidaActual += 25;
                    pokemon.ataque += 15;
                    pokemon.defensa += 15;
                    pokemon.velocidad += 15;
                    pokemon.especial += 15;
                    pokemon.nivel++;
                    pokemon.CambiarEstadistica(Estadistica.ATAQUE);
                    pokemon.CambiarEstadistica(Estadistica.DEFENSA);
                    pokemon.CambiarEstadistica(Estadistica.ESPECIAL);
                    pokemon.CambiarEstadistica(Estadistica.VELOCIDAD);
                    textoSalida += pokemon.nombre + " ha subido de nivel. \n";
                    combate.vidaTu = new BarraDeVida("Back", pokemon, combate);
                    break;

                case Objeto.ObjetcID.Proteina:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    pokemon.ataque += 15;
                    pokemon.CambiarEstadistica(Estadistica.ATAQUE);
                    textoSalida += "El ataque de " + pokemon.nombre + " ha aumentado.\n";
                    break;

                case Objeto.ObjetcID.Hierro:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    pokemon.defensa += 15;
                    pokemon.CambiarEstadistica(Estadistica.DEFENSA);
                    textoSalida += "La defensa de " + pokemon.nombre + " ha aumentado.\n";
                    break;

                case Objeto.ObjetcID.Calcio:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    pokemon.especial += 15;
                    pokemon.CambiarEstadistica(Estadistica.ESPECIAL);
                    textoSalida += "El especial de " + pokemon.nombre + " ha aumentado.\n";
                    break;

                case Objeto.ObjetcID.Carburante:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    pokemon.velocidad += 15;
                    pokemon.CambiarEstadistica(Estadistica.VELOCIDAD);
                    textoSalida += "La velocidad de " + pokemon.nombre + " ha aumentado.\n";
                    break;

                case Objeto.ObjetcID.MasPS:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    pokemon.vidaMax += 30;
                    pokemon.vidaActual += 30;
                    textoSalida += "Los PV máximos de " + pokemon.nombre + " han aumentado.\n";
                    combate.vidaTu = new BarraDeVida("Back", pokemon, combate);
                    break;

                case Objeto.ObjetcID.CarameloFuria:
                    textoSalida += EfectoObjeto(20, Estado.TODOS, pokemon);
                    break;

                case Objeto.ObjetcID.BayaGuaya:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 6, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaPeragu:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 6, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaUvav:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 6, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaMais:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 7, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaZanama:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 7, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaPinia:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 7, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaOram:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 8, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaAlgama:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 8, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.BayaCaquic:
                    textoSalida += EfectoObjeto(10, Estado.NINGUNO, pokemon);
                    if (pokemon.estadisticasActuales.confuso > 0)
                    {
                        pokemon.estadisticasActuales.confuso = 0;
                        textoSalida += pokemon.nombre + " ya no está confuso. \n";
                    }
                    break;

                case Objeto.ObjetcID.BarritaPlus:
                    textoSalida += EfectoObjeto(0, Estado.TODOS, pokemon);
                    break;

                case Objeto.ObjetcID.ZumodeBaya:
                    textoSalida += EfectoObjeto(pokemon.vidaMax / 5, Estado.NINGUNO, pokemon);
                    break;

                case Objeto.ObjetcID.HierbaMental:
                    if (pokemon.estadisticasActuales.modificadorEspecial < 6)
                    {
                        textoSalida += "El especial de " + pokemon.nombre + " subió.\n";
                        pokemon.estadisticasActuales.modificadorEspecial++;
                        pokemon.CambiarEstadistica(Estadistica.ESPECIAL);
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "El especial de " + pokemon.nombre + " no puede subir más.\n";
                    break;

                case Objeto.ObjetcID.FlautaAmarilla:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    if (pokemon.estadisticasActuales.confuso > 0)
                    {
                        pokemon.estadisticasActuales.confuso = 0;
                        textoSalida += pokemon.nombre + " ya no está confuso. \n";
                    }
                    break;

                case Objeto.ObjetcID.FlautaAzul:
                    textoSalida += EfectoObjeto(0, Estado.DORMIDO, pokemon);
                    break;

                case Objeto.ObjetcID.FlautaRoja:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();
                    if (pokemon.estadisticasActuales.drenadoras)
                    {
                        pokemon.estadisticasActuales.drenadoras = false;
                        textoSalida += pokemon.nombre + " ya no está infectado. \n";
                    }
                    break;

                case Objeto.ObjetcID.HierbaMedicinal:
                    textoSalida += EfectoObjeto(pokemon.vidaMax, Estado.TODOS, pokemon);
                    break;

                case Objeto.ObjetcID.CenizaSagrada:
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    textoSalida += "Todas las estadísticas de " + pokemon.nombre + " se restablecieron. \n";
                    pokemon.estadisticasActuales.modificadorCritico = 0;
                    pokemon.estadisticasActuales.modificadorAtaque = 0;
                    pokemon.estadisticasActuales.modificadorDefensa = 0;
                    pokemon.estadisticasActuales.modificadorEspecial = 0;
                    pokemon.estadisticasActuales.modificadorVelocidad = 0;
                    pokemon.estadisticasActuales.modificadorPrecision = 0;
                    pokemon.estadisticasActuales.modificadorEvasion = 0;
                    pokemon.CambiarEstadistica(Estadistica.ATAQUE);
                    pokemon.CambiarEstadistica(Estadistica.DEFENSA);
                    pokemon.CambiarEstadistica(Estadistica.ESPECIAL);
                    pokemon.CambiarEstadistica(Estadistica.VELOCIDAD);
                    break;

                case Objeto.ObjetcID.HierbaAguante:
                    if (pokemon.estadisticasActuales.modificadorDefensa < 6)
                    {
                        textoSalida += "La defensa de " + pokemon.nombre + " subió.\n";
                        pokemon.estadisticasActuales.modificadorDefensa++;
                        pokemon.CambiarEstadistica(Estadistica.DEFENSA);
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "La defensa de " + pokemon.nombre + " no puede subir más.\n";
                    break;

                case Objeto.ObjetcID.HierbaLucha:
                    if (pokemon.estadisticasActuales.modificadorAtaque < 6)
                    {
                        textoSalida += "El ataque de " + pokemon.nombre + " subió.\n";
                        pokemon.estadisticasActuales.modificadorAtaque++;
                        pokemon.CambiarEstadistica(Estadistica.ATAQUE);
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
                    }
                    else
                        textoSalida += "El ataque de " + pokemon.nombre + " no puede subir más.\n";
                    break;
            }

            if (objetoUsado.idObjeto != Objeto.ObjetcID.Pokéflauta && objetoUsado.idObjeto != Objeto.ObjetcID.FlautaAmarilla && objetoUsado.idObjeto != Objeto.ObjetcID.FlautaAzul && objetoUsado.idObjeto != Objeto.ObjetcID.FlautaRoja)
                entrenador.objetos.Remove(objetoUsado);

            return textoSalida;
        }

        public string EfectoObjeto(int cantidadCura, Estado estadoQueCura, Pokemon pokemon)
        {
            string textoSalida = "";

            if (cantidadCura > 0)
            {
                textoSalida += pokemon.nombre + " ha recuperado salud. \n";
                pokemon.vidaActual += cantidadCura;
                if (pokemon.vidaActual > pokemon.vidaMax)
                    pokemon.vidaActual = pokemon.vidaMax;
            }

            if (estadoQueCura != Estado.NINGUNO && (pokemon.estadisticasActuales.estadoActual == estadoQueCura || estadoQueCura == Estado.TODOS ||
                (pokemon.estadisticasActuales.estadoActual == Estado.GRAVEMENTEENVENENADO && estadoQueCura == Estado.ENVENENADO)))
            {
                textoSalida += pokemon.nombre + " se ha recuperado del problema de estado. \n";
                pokemon.estadisticasActuales.estadoActual = Estado.NINGUNO;
            }

            //Sonido
            new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\objectused.wav").PlaySync(); }).Start();

            return textoSalida;
        }

        #endregion

        #region Parte grafica

        public void ActualizarParteGrafica()
        {
            combate.picBoxEstadoTu.BackgroundImage = Image.FromFile(@"Img\Estado\" + (int)combate.pokemonBack.estadisticasActuales.estadoActual + ".png");
            combate.picBoxEstadoRival.BackgroundImage = Image.FromFile(@"Img\Estado\" + (int)combate.pokemonFront.estadisticasActuales.estadoActual + ".png");

            if (combate.pokemonBack.vidaActual <= 0)
            {
                combate.pokemonBack.vidaActual = 0;
                combate.pokemonBack.estadisticasActuales.debilitado = true;
            }
            if (combate.pokemonFront.vidaActual <= 0)
            {
                combate.pokemonFront.vidaActual = 0;
                combate.pokemonFront.estadisticasActuales.debilitado = true;
            }

            combate.vidaRival.SetVida(combate.pokemonFront.vidaActual);
            combate.vidaTu.SetVida(combate.pokemonBack.vidaActual);

            combate.labelNivelTu.Text = "Nv" + combate.pokemonBack.nivel;
            combate.labelNivelRival.Text = "Nv" + combate.pokemonFront.nivel;
        }

        #endregion

        #region Seleccion orden

        public void SeleccionarPrimeroEnAtacar()
        {
            //Se mueve primero si cambia.
            if (combate.accionRealizadaPorBack.tipoAccion == Accion.TipoAccion.CAMBIOPOKEMON)
            {
                primeroEnAtacar = "Back";
                return;
            }
            if (combate.accionRealizadaPorFront.tipoAccion == Accion.TipoAccion.CAMBIOPOKEMON)
            {
                primeroEnAtacar = "Front";
                return;
            }

            //Se mueve primero si usa objeto.
            if (combate.accionRealizadaPorBack.tipoAccion == Accion.TipoAccion.OBJETO)
            {
                primeroEnAtacar = "Back";
                return;
            }
            if (combate.accionRealizadaPorFront.tipoAccion == Accion.TipoAccion.OBJETO)
            {
                primeroEnAtacar = "Front";
                return;
            }

            if (combate.pokemonBack.estadisticasActuales.velocidadActual >= combate.pokemonFront.estadisticasActuales.velocidadActual)
                primeroEnAtacar = "Back";
            else
                primeroEnAtacar = "Front";

            //Contraataque
            if (combate.accionRealizadaPorFront.ataqueUsado != null && combate.accionRealizadaPorFront.ataqueUsado.idMovimiento == 34)
                primeroEnAtacar = "Back";
            if (combate.accionRealizadaPorBack.ataqueUsado != null && combate.accionRealizadaPorBack.ataqueUsado.idMovimiento == 34)
                primeroEnAtacar = "Front";

            //Espejo
            if (combate.accionRealizadaPorFront.ataqueUsado != null && combate.accionRealizadaPorFront.ataqueUsado.idMovimiento == 159)
                primeroEnAtacar = "Back";
            if (combate.accionRealizadaPorBack.ataqueUsado != null && combate.accionRealizadaPorBack.ataqueUsado.idMovimiento == 159)
                primeroEnAtacar = "Front";

            //Venganza
            if (combate.accionRealizadaPorFront.ataqueUsado != null && combate.accionRealizadaPorFront.ataqueUsado.idMovimiento == 113)
                primeroEnAtacar = "Back";
            if (combate.accionRealizadaPorBack.ataqueUsado != null && combate.accionRealizadaPorBack.ataqueUsado.idMovimiento == 113)
                primeroEnAtacar = "Front";

            //At rapido
            if (combate.accionRealizadaPorBack.ataqueUsado != null && combate.accionRealizadaPorBack.ataqueUsado.idMovimiento == 50)
                primeroEnAtacar = "Back";
            if (combate.accionRealizadaPorFront.ataqueUsado != null && combate.accionRealizadaPorFront.ataqueUsado.idMovimiento == 50)
                primeroEnAtacar = "Front";

            //Velocidad Extrema
            if (combate.accionRealizadaPorBack.ataqueUsado != null && combate.accionRealizadaPorBack.ataqueUsado.idMovimiento == 193)
                primeroEnAtacar = "Back";
            if (combate.accionRealizadaPorFront.ataqueUsado != null && combate.accionRealizadaPorFront.ataqueUsado.idMovimiento == 193)
                primeroEnAtacar = "Front";

            //Ultrapuño
            if (combate.accionRealizadaPorBack.ataqueUsado != null && combate.accionRealizadaPorBack.ataqueUsado.idMovimiento == 186)
                primeroEnAtacar = "Back";
            if (combate.accionRealizadaPorFront.ataqueUsado != null && combate.accionRealizadaPorFront.ataqueUsado.idMovimiento == 186)
                primeroEnAtacar = "Front";

            //Puño sombra
            if (combate.accionRealizadaPorBack.ataqueUsado != null && combate.accionRealizadaPorBack.ataqueUsado.idMovimiento == 221)
                primeroEnAtacar = "Back";
            if (combate.accionRealizadaPorFront.ataqueUsado != null && combate.accionRealizadaPorFront.ataqueUsado.idMovimiento == 221)
                primeroEnAtacar = "Front";

            //Acua Jet
            if (combate.accionRealizadaPorBack.ataqueUsado != null && combate.accionRealizadaPorBack.ataqueUsado.idMovimiento == 251)
                primeroEnAtacar = "Back";
            if (combate.accionRealizadaPorFront.ataqueUsado != null && combate.accionRealizadaPorFront.ataqueUsado.idMovimiento == 251)
                primeroEnAtacar = "Front";
        }

        #endregion

        #region DOT

        public Boolean SufreDOT(Pokemon pokemon)
        {
            if (pokemon.estadisticasActuales.drenadoras == false &&
                pokemon.estadisticasActuales.estadoActual != Estado.QUEMADO &&
                pokemon.estadisticasActuales.estadoActual != Estado.GRAVEMENTEENVENENADO &&
                pokemon.estadisticasActuales.estadoActual != Estado.ENVENENADO
            )
            {
                return false;
            }
            return true;
        }

        public String DOT(Pokemon pokemonAfectado, Pokemon pokemonRival)
        {
            if (pokemonAfectado.estadisticasActuales.debilitado)
                return "";

            String textoSalida = "";
            //Switch del estado del pokemon.
            switch (pokemonAfectado.estadisticasActuales.estadoActual)
            {
                //El pokemon esta envenenado.
                case Estado.ENVENENADO:
                    textoSalida += "El veneno resta ps a " + pokemonAfectado.nombre + "\n";
                    pokemonAfectado.vidaActual -= pokemonAfectado.vidaMax / 16;
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Status Poisoned.wav").PlaySync(); }).Start();
                    break;

                //El pokemon esta gravemente envenenado.
                case Estado.GRAVEMENTEENVENENADO:
                    textoSalida += "El veneno resta ps a " + pokemonAfectado.nombre + "\n";
                    double dagnoSufrido = pokemonAfectado.estadisticasActuales.turnosGravementeEnvenenado / (double)16;
                    pokemonAfectado.vidaActual -= (int)(pokemonAfectado.vidaMax * dagnoSufrido);
                    //Aumentamos los turnos de gravemente envenenado.
                    pokemonAfectado.estadisticasActuales.turnosGravementeEnvenenado++;
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Status Poisoned.wav").PlaySync(); }).Start();
                    break;

                //El pokemon esta quemado
                case Estado.QUEMADO:
                    textoSalida += pokemonAfectado.nombre + " se resiente de sus quemaduras.\n";
                    pokemonAfectado.vidaActual -= pokemonAfectado.vidaMax / 16;
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Status Burned.wav").PlaySync(); }).Start();
                    break;
            }

            //Si el pokemon se debilita tras sufrir el DOT
            if (pokemonAfectado.vidaActual <= 0)
            {
                //El pokemon se debilita.
                pokemonAfectado.vidaActual = 0;
                pokemonAfectado.estadisticasActuales.debilitado = true;
                switch (pokemonAfectado.estadisticasActuales.estadoActual)
                {
                    case Estado.ENVENENADO:
                        textoSalida += pokemonAfectado.nombre + " no fue capaz de resistir el veneno.\n";
                        break;
                    case Estado.GRAVEMENTEENVENENADO:
                        textoSalida += pokemonAfectado.nombre + " no fue capaz de resistir el veneno.\n";
                        break;
                    case Estado.QUEMADO:
                        textoSalida += pokemonAfectado.nombre + " no fue capaz de resistir las quemaduras.\n";
                        break;
                }
                return textoSalida;
            }

            //Si sufre drenadoras.
            if (pokemonAfectado.estadisticasActuales.drenadoras)
            {
                //A los tipo planta no les afecta
                if (pokemonAfectado.tipo1 != Tipo.PLANTA && pokemonAfectado.tipo2 != Tipo.PLANTA)
                {
                    int vidaRobada = pokemonAfectado.vidaMax / 16;
                    pokemonAfectado.vidaActual -= vidaRobada;

                    textoSalida += "Drenadoras afecta a " + pokemonAfectado.nombre + ".\n";

                    if (!pokemonRival.estadisticasActuales.debilitado)
                    {
                        textoSalida += pokemonRival.nombre + " ha recuperado algunos Ps.\n";
                        new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\healing.wav").PlaySync(); }).Start();
                        pokemonRival.vidaActual += vidaRobada;

                        //Si el pokemon rival excede sus ps maximos.
                        if (pokemonRival.vidaActual > pokemonRival.vidaMax)
                            pokemonRival.vidaActual = pokemonRival.vidaMax;
                    }
                }
                else
                    textoSalida += "Drenadoras no a afecta a " + pokemonAfectado.nombre + ".\n";
            }

            //Si el pokemon se debilita tras las drenadoras.
            if (pokemonAfectado.vidaActual <= 0)
            {
                textoSalida += "Drenadoras acabó con " + pokemonAfectado.nombre + "\n";
                pokemonAfectado.vidaActual = 0;
                pokemonAfectado.estadisticasActuales.debilitado = true;
            }
            return textoSalida;
        }

        #endregion

        #region Ataque

        public String Ataque(Pokemon origen, Ataque ataqueRealizado, Pokemon destino)
        {
            String textoMostrar = "";

            ataqueRealizado.ppActuales--;

            //Si está debilitado.
            if (origen.estadisticasActuales.debilitado)
                return "";

            //Si tiene el ataque anulado
            if (ataqueRealizado.turnosAnulado > 0)
                return ataqueRealizado.nombre + " está anulado.\n";

            //Si esta inmovilizado (giro fuego y similares)
            if (ataqueRealizado.ataqueID == AtaqueID.GIRORAPIDO)
                origen.estadisticasActuales.turnosInmovilizado = 0;
            if (origen.estadisticasActuales.turnosInmovilizado > 0) //Comprobamos si esta inmovilizado
                return origen.nombre + " está inmovilizado, no se puede mover.\n";

            //Si tiene que descansar por hiperrayo
            if (origen.estadisticasActuales.movLanzadoTrasEspera != null && origen.estadisticasActuales.movLanzadoTrasEspera.ataqueID == AtaqueID.HIPERRAYO)
            {
                origen.estadisticasActuales.movLanzadoTrasEspera = null;
                ataqueRealizado.ppActuales++;
                return origen.nombre + " está descansando.\n";
            }

            //Si retrocede.
            if (origen.estadisticasActuales.haRetrocedido)
            {
                ataqueRealizado.ppActuales++;
                return origen.nombre + " ha retrocedido.\n";
            }

            //Si se acaban los pp
            if (ataqueRealizado.ppActuales < 0)
            {
                origen.estadisticasActuales.movLanzadoTrasEspera = origen.estadisticasActuales.movQueContinua = null;
                return origen.nombre + " trató de usar " + ataqueRealizado.nombre + " pero no quedaban pp.";
            }

            //Si esta dormido
            if (origen.estadisticasActuales.estadoActual == Estado.DORMIDO)
            {
                if (origen.estadisticasActuales.turnosDormido > 0) //Comprobamos los turnos de dormido
                {
                    origen.estadisticasActuales.usandoFuria = false;
                    origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    ataqueRealizado.ppActuales++;
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Status Sleeping.wav").PlaySync(); }).Start();
                    return origen.nombre + " está dormido como un tronco.\n";
                }
                else
                {
                    textoMostrar += origen.nombre + " se ha despertado.\n";
                    origen.estadisticasActuales.estadoActual = Estado.NINGUNO;
                }
            }

            //Si esta congelado
            if (origen.estadisticasActuales.estadoActual == Estado.CONGELADO)
            {
                ataqueRealizado.ppActuales++;
                origen.estadisticasActuales.movLanzadoTrasEspera = origen.estadisticasActuales.movQueContinua = null;
                new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Status Frozen.wav").PlaySync(); }).Start();
                return origen.nombre + " está congelado no se puede mover.\n";
            }

            //Si esta confuso
            if (origen.estadisticasActuales.confuso > 0)
            {
                textoMostrar += origen.nombre + " está confuso.\n";
                new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Status Confused.wav").PlaySync(); }).Start();
                if (random.Next(1, 100) <= 50)
                {
                    textoMostrar += "Está tan confuso que se hirió a si mismo.\n";
                    int A = origen.estadisticasActuales.ataqueActual;
                    int D = origen.estadisticasActuales.defensaActual;
                    double dagno = 0.01 * 92 * (((0.2 * origen.nivel + 1) * A * 40) / (25 * D) + 2);
                    origen.vidaActual -= (int)dagno;

                    origen.estadisticasActuales.volando = false;
                    origen.estadisticasActuales.escavando = false;
                    origen.estadisticasActuales.movLanzadoTrasEspera = origen.estadisticasActuales.movQueContinua = null;

                    if (destino == combate.pokemonFront)
                        combate.picBoxPkmnBack.Visible = true;
                    else
                        combate.picBoxPkmnFront.Visible = true;

                    if (origen.vidaActual <= 0)
                    {
                        origen.vidaActual = 0;
                        origen.estadisticasActuales.debilitado = true;
                    }
                    origen.estadisticasActuales.confuso--;
                    return textoMostrar;
                }
            }
            if (origen.estadisticasActuales.confuso == 0)
            {
                textoMostrar += origen.nombre + " ya no está confuso.\n";
                origen.estadisticasActuales.confuso = -1;
            }

            //Si esta paralizado
            if (origen.estadisticasActuales.estadoActual == Estado.PARALIZADO)
                if (random.Next(1, 100) <= 25)
                {
                    new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Status Paralyzed.wav").PlaySync(); }).Start();
                    textoMostrar += origen.nombre + " está paralizado, no se puede mover.\n";
                    origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    origen.estadisticasActuales.volando = false;
                    origen.estadisticasActuales.escavando = false;

                    if (destino == combate.pokemonFront)
                        combate.picBoxPkmnBack.Visible = true;
                    else
                        combate.picBoxPkmnFront.Visible = true;

                    return textoMostrar;
                }
                else
                    textoMostrar += origen.nombre + " sufre paralisis, quizá no se pueda mover.\n";

            //Aqui va el ya no tan infierno de switch. :)
            switch (ataqueRealizado.ataqueID)
            {
                case AtaqueID.COMBATE:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 50);
                    break;

                case AtaqueID.BURBUJA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 10);
                    break;

                case AtaqueID.CASCADA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.HIDROBOMBA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.MARTILLAZO:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.PISTOLAAGUA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.RAYOBURBUJA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 33.3);
                    break;

                case AtaqueID.REFUGIO:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.DEFENSA, 1, 0);
                    break;

                case AtaqueID.SURF:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.TENAZA:
                    if (origen.estadisticasActuales.movQueContinua == null)
                        textoMostrar += EmpezarAtaqueDeVariosTurnos(origen, ataqueRealizado, destino);
                    else
                    {
                        if (origen.estadisticasActuales.turnosContinuarMovimiento <= 0)
                            origen.estadisticasActuales.movQueContinua = null;

                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    }
                    break;

                case AtaqueID.CHUPAVIDAS:
                    textoMostrar += AtaqueConCuracion(origen, ataqueRealizado, destino, 50);
                    break;

                case AtaqueID.DISPARODEMORA:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 1, false);
                    break;

                case AtaqueID.DOBLEATAQUE:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, 2);
                    if (AciertaProbabilidad(20) && destino.estadisticasActuales.estadoActual == Estado.NINGUNO)
                    {
                        destino.estadisticasActuales.estadoActual = Estado.ENVENENADO;
                        textoMostrar += destino.nombre + " ha sido envenenado.\n";
                    }
                    break;

                case AtaqueID.PINMISIL:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.FURIADRAGON:
                    textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, 40);
                    break;

                case AtaqueID.IMPACTRUENO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ONDATRUENO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.PUGNOTRUENO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.RAYO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.TRUENO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.LENGUETAZO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.RAYOCONFUSO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, true);
                    break;

                case AtaqueID.TINIEBLA:
                    textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, origen.nivel);
                    break;

                case AtaqueID.ASCUAS:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.GIROFUEGO:
                    if (origen.estadisticasActuales.movQueContinua == null)
                        textoMostrar += EmpezarAtaqueDeVariosTurnos(origen, ataqueRealizado, destino);
                    else
                    {
                        if (origen.estadisticasActuales.turnosContinuarMovimiento <= 0)
                            origen.estadisticasActuales.movQueContinua = null;

                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    }
                    break;

                case AtaqueID.LANZALLAMAS:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.LLAMARADA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PUGNOFUEGO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.NEBLINA:
                    AnimacionEstadoTu(origen, ataqueRealizado);
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    origen.estadisticasActuales.evitarCambiosEstadistica = true;
                    textoMostrar += origen.nombre + " se ha protegido de cambios de stat.\n";
                    break;

                case AtaqueID.NIEBLA:
                    AnimacionEstadoTu(origen, ataqueRealizado);
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    origen.OnNiebla();
                    destino.OnNiebla();
                    textoMostrar += "Los cambios de stat de ambos pokemon se eliminaron.\n";
                    break;

                case AtaqueID.PUGNOHIELO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.RAYOAURORA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.RAYOHIELO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.VENTISCA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CONTRAATAQUE:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    //Solo funciona si es de tipo lucha o normal y ha recibido ataque.
                    if (origen.estadisticasActuales.ultimoAtaqueRecibido != null && (origen.estadisticasActuales.ultimoAtaqueRecibido.tipo == Tipo.NORMAL || origen.estadisticasActuales.ultimoAtaqueRecibido.tipo == Tipo.LUCHA))
                        textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, origen.estadisticasActuales.dagnoUltimoGolpeRecibido * 2);
                    else
                        textoMostrar += "Pero ha fallado.\n";
                    break;

                case AtaqueID.DOBLEPATADA:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, 2);
                    break;

                case AtaqueID.GOLPEKARATE:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.PATADABAJA:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.PATADAGIRO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.PATADASALTO:
                    textoMostrar += AtaqueConDagnoSiSeFalla(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PATADASALTOALTA:
                    textoMostrar += AtaqueConDagnoSiSeFalla(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.SISMICO:
                    textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, origen.nivel);
                    break;

                case AtaqueID.SUMISION:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 25);
                    break;

                case AtaqueID.AFILAR:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ATAQUE, 1, 0);
                    break;

                case AtaqueID.AGARRE:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.AMORTIGUADOR:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.NINGUNA, 0, 50);
                    break;

                case AtaqueID.ANULACION:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    if (AciertaAtaque(origen, ataqueRealizado, destino))
                    {
                        AnimacionEstadoRival(destino, ataqueRealizado);
                        Ataque aux = GetAtaqueRandom(destino);
                        if (destino.mov1 != null)
                            destino.mov1.turnosAnulado = 0;
                        if (destino.mov2 != null)
                            destino.mov2.turnosAnulado = 0;
                        if (destino.mov3 != null)
                            destino.mov3.turnosAnulado = 0;
                        if (destino.mov4 != null)
                            destino.mov4.turnosAnulado = 0;
                        textoMostrar += origen.nombre + " anuló " + aux.nombre + " del rival.\n";
                        aux.turnosAnulado = random.Next(2, 8);
                    }
                    else
                        textoMostrar += "Pero falló.\n";
                    break;

                case AtaqueID.ARAGNAZO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ATAQUEFURIA:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.ATADURA:
                    if (origen.estadisticasActuales.movQueContinua == null)
                        textoMostrar += EmpezarAtaqueDeVariosTurnos(origen, ataqueRealizado, destino);
                    else
                    {
                        if (origen.estadisticasActuales.turnosContinuarMovimiento <= 0)
                            origen.estadisticasActuales.movQueContinua = null;

                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    }
                    break;

                case AtaqueID.ATAQUERAPIDO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.AUTODESTRUCCION:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 999999);
                    break;

                case AtaqueID.BESOAMOROSO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.BOMBAHUEVO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.BOMBASONICA:
                    textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.PRESA:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.CABEZAZO:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " ha cargado energía.\n";
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                    }
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    }
                    break;

                case AtaqueID.CANTO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.CHIRRIDO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 2, false);
                    break;

                case AtaqueID.CLAVOCAGNON:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.CONSTRICCION:
                    if (origen.estadisticasActuales.movQueContinua == null)
                        textoMostrar += EmpezarAtaqueDeVariosTurnos(origen, ataqueRealizado, destino);
                    else
                    {
                        if (origen.estadisticasActuales.turnosContinuarMovimiento <= 0)
                            origen.estadisticasActuales.movQueContinua = null;

                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    }
                    break;

                case AtaqueID.CONVERSION:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    AnimacionEstadoRival(destino, ataqueRealizado);
                    origen.tipo1 = destino.tipo1;
                    textoMostrar += "Conversión cambia el tipo de " + origen.nombre + ".\n";
                    break;

                case AtaqueID.CORNADA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CORTE:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CUCHILLADA:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.DANZAESPADA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ATAQUE, 2, 0);
                    break;

                case AtaqueID.DERRIBO:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 25);
                    break;

                case AtaqueID.DESARROLLO:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ESPECIAL, 1, 0);
                    break;

                case AtaqueID.DESLUMBRAR:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.DESTELLO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.PRECISION, 1, false);
                    break;

                case AtaqueID.DESTRUCTOR:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.DIADEPAGO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    textoMostrar += "Se esparcieron monedas.\n";
                    break;

                case AtaqueID.DOBLEBOFETON:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, 2);
                    break;

                case AtaqueID.DOBLEEQUIPO:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.EVASION, 1, 0);
                    break;

                case AtaqueID.DOBLEFILO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 25);
                    break;

                case AtaqueID.EXPLOSION:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 99999);
                    break;

                case AtaqueID.FOCOENERGIA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.CRITICO, 1, 0);
                    break;

                case AtaqueID.FORTALEZA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.DEFENSA, 1, 0);
                    break;

                case AtaqueID.FUERZA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.FURIA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    origen.estadisticasActuales.usandoFuria = true;
                    break;

                case AtaqueID.GOLPE:
                    if (origen.estadisticasActuales.movQueContinua == null)
                        textoMostrar += EmpezarAtaqueDeVariosTurnos(origen, ataqueRealizado, destino);
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        ataqueRealizado.ppActuales++;
                        if (origen.estadisticasActuales.turnosContinuarMovimiento <= 0)
                        {
                            origen.estadisticasActuales.movQueContinua = null;
                            origen.estadisticasActuales.confuso = random.Next(1, 4);
                            textoMostrar += origen.nombre + " se ha confundido.\n";
                        }
                    }
                    break;

                case AtaqueID.GOLPECABEZA:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.GOLPECUERPO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.GOLPESFURIA:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.GRUGNIDO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.ATAQUE, 1, false);
                    break;

                case AtaqueID.GUILLOTINA:
                    textoMostrar += AtaqueConFulminante(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.HIPERCOLMILLO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.HIPERRAYO:
                    origen.estadisticasActuales.turnosEspera = 2;
                    origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.LATIGO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 1, false);
                    break;

                case AtaqueID.MALICIOSO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 1, false);
                    break;

                case AtaqueID.MEGAPATADA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.MEGAPUGNO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.METRONOMO:
                    return Ataque(origen, new Ataque(random.Next(1, Enum.GetValues(typeof(AtaqueID)).Length)), destino);

                case AtaqueID.MIMETICO:
                    textoMostrar += origen.nombre + " usó  " + ataqueRealizado.nombre + ".\n";
                    AnimacionEstadoRival(destino, ataqueRealizado);
                    Ataque auxAtaque = new Ataque(GetAtaqueRandom(destino).idMovimiento);
                    textoMostrar += origen.nombre + " ha copiado " + auxAtaque.nombre + ".\n";
                    if (origen.mov1 != null && origen.mov1 == ataqueRealizado)
                        origen.mov1 = auxAtaque;
                    if (origen.mov2 != null && origen.mov2 == ataqueRealizado)
                        origen.mov2 = auxAtaque;
                    if (origen.mov3 != null && origen.mov3 == ataqueRealizado)
                        origen.mov3 = auxAtaque;
                    if (origen.mov4 != null && origen.mov4 == ataqueRealizado)
                        origen.mov4 = auxAtaque;
                    break;

                case AtaqueID.PANTALLAHUMO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.PRECISION, 1, false);
                    break;

                case AtaqueID.PERFORADOR:
                    textoMostrar += AtaqueConFulminante(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PISOTON:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.PLACAJE:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PUGNOCOMETA:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.PUGNOMAREO:
                    textoMostrar += AtaqueConConfundir(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.RAPIDEZ:
                    textoMostrar += AtaqueInfalible(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.RECUPERACION:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.NINGUNA, 0, 50);
                    break;

                case AtaqueID.REDUCCION:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.EVASION, 1, 0);
                    break;

                case AtaqueID.REMOLINO:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\nPero falló.";
                    break;

                case AtaqueID.RESTRICCION:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 10);
                    break;

                case AtaqueID.RIZODEFENSA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.DEFENSA, 1, 0);
                    break;

                case AtaqueID.RUGIDO:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\nPero falló.";
                    break;

                case AtaqueID.SALPICADURA:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\nNo tuvo ningún efecto.";
                    AnimacionEstadoTu(origen, ataqueRealizado);
                    break;

                case AtaqueID.SUPERDIENTE:
                    textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, destino.vidaActual / 2);
                    break;

                case AtaqueID.SUPERSONICO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, true);
                    break;

                case AtaqueID.TRANSFORMACION:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    if (origen == combate.pokemonBack)
                    {
                        combate.pokemonBack.estadisticasActuales = new EstadisticasActuales(combate.pokemonFront);
                        combate.pokemonBack.estadisticasActuales.estadoActual = origen.estadisticasActuales.estadoActual;
                        combate.pokemonBack.estadisticasActuales.drenadoras = origen.estadisticasActuales.drenadoras;
                        combate.pokemonBack.estadisticasActuales.vecesTransformado = origen.estadisticasActuales.vecesTransformado + 1;
                        combate.pokemonBack.estadisticasActuales.confuso = origen.estadisticasActuales.confuso;
                        combate.pokemonBack.estadisticasActuales.turnosDormido = origen.estadisticasActuales.turnosDormido;
                        combate.pokemonBack.estadisticasActuales.turnosInmovilizado = origen.estadisticasActuales.turnosInmovilizado;
                        combate.pokemonBack.estadisticasActuales.turnosGravementeEnvenenado = origen.estadisticasActuales.turnosGravementeEnvenenado;
                        combate.pokemonBack.tipo1 = combate.pokemonFront.tipo1;
                        combate.pokemonBack.tipo2 = combate.pokemonFront.tipo2;
                        combate.pokemonBack.imagenBack = combate.pokemonFront.imagenBack;
                        combate.pokemonBack.imagenFront = combate.pokemonFront.imagenFront;

                        if (combate.pokemonFront.mov1 != null)
                        {
                            combate.pokemonBack.mov1 = new Ataque(combate.pokemonFront.mov1.idMovimiento);
                            combate.pokemonBack.mov1.ppActuales = 5;
                        }
                        else combate.pokemonBack.mov1 = null;
                        if (combate.pokemonFront.mov2 != null)
                        {
                            combate.pokemonBack.mov2 = new Ataque(combate.pokemonFront.mov2.idMovimiento);
                            combate.pokemonBack.mov2.ppActuales = 5;
                        }
                        else combate.pokemonBack.mov2 = null;
                        if (combate.pokemonFront.mov3 != null)
                        {
                            combate.pokemonBack.mov3 = new Ataque(combate.pokemonFront.mov3.idMovimiento);
                            combate.pokemonBack.mov3.ppActuales = 5;
                        }
                        else combate.pokemonBack.mov3 = null;
                        if (combate.pokemonFront.mov4 != null)
                        {
                            combate.pokemonBack.mov4 = new Ataque(combate.pokemonFront.mov4.idMovimiento);
                            combate.pokemonBack.mov4.ppActuales = 5;
                        }
                        else combate.pokemonBack.mov4 = null;

                        Image imgPkmnBack = combate.pokemonFront.imagenBack;
                        combate.picBoxPkmnBack.Image = imgPkmnBack;
                        combate.recPicBoxPokemonBack.Size = imgPkmnBack.Size;
                        combate.recPicBoxPokemonBack.Location = new Point(100 - combate.recPicBoxPokemonBack.Width / 2, 243 - combate.recPicBoxPokemonBack.Height);
                        combate.ResizeControl(combate.recPicBoxPokemonBack, combate.picBoxPkmnBack);
                    }
                    else
                    {
                        combate.pokemonFront.estadisticasActuales = new EstadisticasActuales(combate.pokemonBack);
                        combate.pokemonFront.estadisticasActuales.estadoActual = origen.estadisticasActuales.estadoActual;
                        combate.pokemonFront.estadisticasActuales.drenadoras = origen.estadisticasActuales.drenadoras;
                        combate.pokemonFront.estadisticasActuales.vecesTransformado = origen.estadisticasActuales.vecesTransformado + 1;
                        combate.pokemonFront.estadisticasActuales.confuso = origen.estadisticasActuales.confuso;
                        combate.pokemonFront.estadisticasActuales.turnosDormido = origen.estadisticasActuales.turnosDormido;
                        combate.pokemonFront.estadisticasActuales.turnosInmovilizado = origen.estadisticasActuales.turnosInmovilizado;
                        combate.pokemonFront.estadisticasActuales.turnosGravementeEnvenenado = origen.estadisticasActuales.turnosGravementeEnvenenado;
                        combate.pokemonFront.tipo1 = combate.pokemonBack.tipo1;
                        combate.pokemonFront.tipo2 = combate.pokemonBack.tipo2;
                        combate.pokemonFront.imagenBack = combate.pokemonBack.imagenBack;
                        combate.pokemonFront.imagenFront = combate.pokemonBack.imagenFront;

                        if (combate.pokemonBack.mov1 != null)
                        {
                            combate.pokemonFront.mov1 = new Ataque(combate.pokemonBack.mov1.idMovimiento);
                            combate.pokemonFront.mov1.ppActuales = 5;
                        }
                        else combate.pokemonFront.mov1 = null;
                        if (combate.pokemonBack.mov2 != null)
                        {
                            combate.pokemonFront.mov2 = new Ataque(combate.pokemonBack.mov2.idMovimiento);
                            combate.pokemonFront.mov2.ppActuales = 5;
                        }
                        else combate.pokemonFront.mov2 = null;
                        if (combate.pokemonBack.mov3 != null)
                        {
                            combate.pokemonFront.mov3 = new Ataque(combate.pokemonBack.mov3.idMovimiento);
                            combate.pokemonFront.mov3.ppActuales = 5;
                        }
                        else combate.pokemonFront.mov3 = null;
                        if (combate.pokemonBack.mov4 != null)
                        {
                            combate.pokemonFront.mov4 = new Ataque(combate.pokemonBack.mov4.idMovimiento);
                            combate.pokemonFront.mov4.ppActuales = 5;
                        }
                        else combate.pokemonFront.mov4 = null;

                        Image imgPkmnFront = combate.pokemonFront.imagenFront;
                        combate.picBoxPkmnFront.Image = imgPkmnFront;
                        combate.recPicBoxPokemonFront.Size = imgPkmnFront.Size;
                        combate.recPicBoxPokemonFront.Location = new Point(330 - combate.recPicBoxPokemonFront.Width / 2, 149 - combate.recPicBoxPokemonFront.Height);
                        combate.ResizeControl(combate.recPicBoxPokemonFront, combate.picBoxPkmnFront);
                    }
                    textoMostrar += origen.nombre + " se transformó.\n";
                    break;

                case AtaqueID.TRIATAQUE:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.VENGANZA:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " ha cargado energía.\n";
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                    }
                    else
                    {
                        textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, origen.estadisticasActuales.dagnoUltimoGolpeRecibido * 2);
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    }
                    break;

                case AtaqueID.VIENTOCORTANTE:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " ha cargado energía.\n";
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                    }
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    }
                    break;

                case AtaqueID.ABSORBER:
                    textoMostrar += AtaqueConCuracion(origen, ataqueRealizado, destino, 50);
                    break;

                case AtaqueID.DANZAPETALO:
                    if (origen.estadisticasActuales.movQueContinua == null)
                        textoMostrar += EmpezarAtaqueDeVariosTurnos(origen, ataqueRealizado, destino);
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        ataqueRealizado.ppActuales++;
                        if (origen.estadisticasActuales.turnosContinuarMovimiento <= 0)
                        {
                            origen.estadisticasActuales.movQueContinua = null;
                            origen.estadisticasActuales.confuso = random.Next(1, 4);
                            textoMostrar += origen.nombre + " se ha confundido.\n";
                        }
                    }
                    break;

                case AtaqueID.DRENADORAS:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". ";
                    if (AciertaAtaque(origen, ataqueRealizado, destino))
                    {
                        AnimacionEstadoRival(destino, ataqueRealizado);
                        destino.estadisticasActuales.drenadoras = true;
                        textoMostrar += destino.nombre + " fue infectado.\n";
                    }
                    else
                        textoMostrar += "Pero ha fallado.\n";
                    break;

                case AtaqueID.ESPORA:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.HOJAAFILADA:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.LATIGOCEPA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.MEGAAGOTAR:
                    textoMostrar += AtaqueConCuracion(origen, ataqueRealizado, destino, 50);
                    break;

                case AtaqueID.PARALIZADOR:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.RAYOSOLAR:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " ha cargado energía.\n";
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                    }
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    }
                    break;

                case AtaqueID.SOMNIFERO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.AGILIDAD:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.VELOCIDAD, 2, 0);
                    break;

                case AtaqueID.AMNESIA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ESPECIAL, 2, 0);
                    break;

                case AtaqueID.BARRERA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.DEFENSA, 2, 0);
                    break;

                case AtaqueID.COMESUEGNOS:
                    textoMostrar += AtaqueConCuracion(origen, ataqueRealizado, destino, 50);
                    break;

                case AtaqueID.CONFUSION:
                    textoMostrar += AtaqueConConfundir(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.DESCANSO:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.NINGUNA, 0, 100);
                    break;

                case AtaqueID.HIPNOSIS:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.KINETICO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.PRECISION, 1, false);
                    break;

                case AtaqueID.MEDITACION:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ATAQUE, 1, 0);
                    break;

                case AtaqueID.PANTALLADELUZ:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    AnimacionEstadoTu(origen, ataqueRealizado);
                    origen.estadisticasActuales.pantallaLuz = true;
                    textoMostrar += "Pantalla luz protrege a " + origen.nombre + " de ataques especiales.\n";
                    break;

                case AtaqueID.PSICOONDA:
                    textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, (int)((random.NextDouble() + 0.5) * origen.nivel));
                    break;

                case AtaqueID.PSICORRAYO:
                    textoMostrar += AtaqueConConfundir(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.PSIQUICO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 30);
                    break;

                case AtaqueID.REFLEJO:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    AnimacionEstadoTu(origen, ataqueRealizado);
                    origen.estadisticasActuales.reflejo = true;
                    textoMostrar += "Reflejo protrege a " + origen.nombre + " de ataques físicos.\n";
                    break;

                case AtaqueID.TELETRANSPORTE:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\nPero falló.";
                    break;

                case AtaqueID.AVALANCHA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.LANZARROCAS:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.MORDISCO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.ATAQUEARENA:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.PRECISION, 1, false);
                    break;

                case AtaqueID.EXCAVAR:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        ataqueRealizado.ppActuales++;
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " se ocultó bajo tierra.\n";
                        AnimacionVueloExcavar(origen, ataqueRealizado);
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                        origen.estadisticasActuales.volando = true;
                    }
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        origen.estadisticasActuales.volando = false;
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                        if (destino == combate.pokemonFront)
                            combate.picBoxPkmnBack.Visible = true;
                        else
                            combate.picBoxPkmnFront.Visible = true;
                    }
                    break;

                case AtaqueID.FISURA:
                    textoMostrar += AtaqueConFulminante(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.HUESOPALO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.HUESOMERANG:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, 2);
                    break;

                case AtaqueID.TERREMOTO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ACIDO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 10);
                    break;

                case AtaqueID.ARMADURAACIDA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.DEFENSA, 2, 0);
                    break;

                case AtaqueID.GASVENENOSO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.PICOTAZOVENENO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.POLUCION:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.POLVOVENENO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.RESIDUOS:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.TOXICO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.ATAQUEAEREO:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " ha cargado energía.\n";
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                    }
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    }
                    break;

                case AtaqueID.ATAQUEALA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ESPEJO:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    if (origen.estadisticasActuales.ultimoAtaqueRecibido != null)
                        textoMostrar += Ataque(origen, origen.estadisticasActuales.ultimoAtaqueRecibido, destino);
                    else
                        textoMostrar += "Pero ha fallado.\n";
                    break;

                case AtaqueID.PICOTALADRO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PICOTAZO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.TORNADO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.VUELO:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        ataqueRealizado.ppActuales++;
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " alzó el vuelo.\n";
                        AnimacionVueloExcavar(origen, ataqueRealizado);
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                        origen.estadisticasActuales.volando = true;
                    }
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        origen.estadisticasActuales.volando = false;
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                        if (destino == combate.pokemonFront)
                            combate.picBoxPkmnBack.Visible = true;
                        else
                            combate.picBoxPkmnFront.Visible = true;
                    }
                    break;

                case AtaqueID.ALAACERO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 10);
                    break;

                case AtaqueID.COLAFERREA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 30);
                    break;

                case AtaqueID.GARRAMETAL:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ATAQUE, 10);
                    break;

                case AtaqueID.PULPOCAGNON:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.PRECISION, 50);
                    break;

                case AtaqueID.CORTEFURIA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.MEGACUERNO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CICLON:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.DRAGOALIENTO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ENFADO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CHISPA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ELECTROCAGNON:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.BOLASOMBRA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 20);
                    break;

                case AtaqueID.FUEGOSAGRADO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.RUEDAFUEGO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ENCANTO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.ATAQUE, 2, false);
                    break;

                case AtaqueID.NIEVEPOLVO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.VIENTOHIELO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 100);
                    break;

                case AtaqueID.GOLPEROCA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 50);
                    break;

                case AtaqueID.PUGNODINAMICO:
                    textoMostrar += AtaqueConConfundir(origen, ataqueRealizado, destino, 100);
                    break;

                case AtaqueID.TAJOCRUZADO:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.TIROVITAL:
                    textoMostrar += AtaqueInfalible(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.TRIPLEPATADA:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, 3);
                    break;

                case AtaqueID.ULTRAPUGNO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CAMPANACURA:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    textoMostrar += origen.nombre + " se ha recuperado de cambios de estado.\n";
                    origen.estadisticasActuales.estadoActual = Estado.NINGUNO;
                    break;

                case AtaqueID.CARASUSTO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 2, false);
                    break;

                case AtaqueID.DULCEAROMA:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.EVASION, 1, false);
                    break;

                case AtaqueID.GIRORAPIDO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.RONQUIDO:
                    if (origen.estadisticasActuales.estadoActual == Estado.DORMIDO)
                        textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    else
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n" + "Pero falló.\n";
                    break;

                case AtaqueID.SONABMULO:
                    if (origen.estadisticasActuales.estadoActual == Estado.DORMIDO)
                        return Ataque(origen, GetAtaqueRandom(origen), destino);
                    else
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n" + "Pero falló.\n";
                    break;

                case AtaqueID.VELOCIDADEXTREMA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ESPORAGODON:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 2, false);
                    break;

                case AtaqueID.GIGADRENADO:
                    textoMostrar += AtaqueConCuracion(origen, ataqueRealizado, destino, 50);
                    break;

                case AtaqueID.SINTESIS:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.NINGUNA, 0, 60);
                    break;

                case AtaqueID.PODERPASADO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 30);
                    break;

                case AtaqueID.FINTA:
                    textoMostrar += AtaqueInfalible(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.LADRON:
                    textoMostrar += AtaqueConCuracion(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.PALIZA:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.PERSECUCION:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.TRITURAR:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 20);
                    break;

                case AtaqueID.ATAQUEOSEO:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.BOFETONLODO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.PRECISION, 100);
                    break;

                case AtaqueID.BOMBALODO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.AEROCHORRO:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.DEFENSAFERREA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.DEFENSA, 2, 0);
                    break;

                case AtaqueID.DESEOOCULTO:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " ha cargado energía.\n";
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                    }
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    }
                    break;

                case AtaqueID.ECOMETALICO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 2, false);
                    break;

                case AtaqueID.PUGNOMETEORO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.HIDROCAGNON:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.HIDROPULSO:
                    textoMostrar += AtaqueConConfundir(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.DOBLERAYO:
                    textoMostrar += AtaqueConConfundir(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.RAFAGA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ESPECIAL, 2, 0);
                    break;

                case AtaqueID.VIENTOPLATA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ATAQUE, 10);
                    break;

                case AtaqueID.DANZADRAGON:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ATAQUE, 1, 0);
                    break;

                case AtaqueID.GARRADRAGON:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ONDAVOLTIO:
                    textoMostrar += AtaqueInfalible(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PLACAJEELECTRICO:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 33);
                    break;

                case AtaqueID.IMPRESIONAR:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.PUGNOSOMBRA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.FUEGOFATUO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.ONDAIGNEA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PATADAIGNEA:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.CARAMBANO:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.FRIOPOLAR:
                    textoMostrar += AtaqueConFulminante(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.BOSTEZO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.GARRABRUTAL:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 50);
                    break;

                case AtaqueID.AROMATERAPIA:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    textoMostrar += origen.nombre + " se ha recuperado de cambios de estado.\n";
                    origen.estadisticasActuales.estadoActual = Estado.NINGUNO;
                    break;

                case AtaqueID.BRAZOPINCHO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.HOJAAGUDA:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.HOJAMAGICA:
                    textoMostrar += AtaqueInfalible(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PAZMENTAL:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ESPECIAL, 1, 0);
                    break;

                case AtaqueID.PEDRADA:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, NumGolpes());
                    break;

                case AtaqueID.TUMBARROCAS:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 100);
                    break;

                case AtaqueID.CAMELO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, true);
                    break;

                case AtaqueID.DESARME:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.LLANTOFALSO:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 2, false);
                    break;

                case AtaqueID.DISPAROLODO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 100);
                    break;

                case AtaqueID.COLMILLOVENENO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.COLAVENENO:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.AIREAFILADO:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.DANZAPLUMA:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.ATAQUE, 2, false);
                    break;

                case AtaqueID.GOLPEAEREO:
                    textoMostrar += AtaqueInfalible(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.BOMBAIMAN:
                    textoMostrar += AtaqueInfalible(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CABEZADEHIERRO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.DISPAROESPEJO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.PRECISION, 30);
                    break;

                case AtaqueID.FOCORESPLANDOR:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 10);
                    break;

                case AtaqueID.PUGNOBALA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ACUACOLA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ACUAJET:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ADEFENDER:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.DEFENSA, 1, 0);
                    break;

                case AtaqueID.AUXILIO:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.NINGUNA, 0, 50);
                    break;

                case AtaqueID.PICADURA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.TIJERAX:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ZUMBIDO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 10);
                    break;

                case AtaqueID.CARGADRAGON:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.COMETADRACO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CORTEVACIO:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.DISTORSION:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PULSODRAGON:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CHISPAZO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.COLMILLORAYO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.RAYOCARGA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.GARRAUMBRIA:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.GOLPEUMBRIO:
                    if (origen.estadisticasActuales.movLanzadoTrasEspera == null)
                    {
                        ataqueRealizado.ppActuales++;
                        textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ". \n";
                        textoMostrar += origen.nombre + " se ocultó en las sombras.\n";
                        AnimacionVueloExcavar(origen, ataqueRealizado);
                        origen.estadisticasActuales.turnosEspera = 2;
                        origen.estadisticasActuales.movLanzadoTrasEspera = ataqueRealizado;
                        origen.estadisticasActuales.volando = true;
                    }
                    else
                    {
                        textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                        origen.estadisticasActuales.volando = false;
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                        if (destino == combate.pokemonFront)
                            combate.picBoxPkmnBack.Visible = true;
                        else
                            combate.picBoxPkmnFront.Visible = true;
                    }
                    break;

                case AtaqueID.SOMBRAVIL:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.VIENTOACIAGO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ATAQUE, 10);
                    break;

                case AtaqueID.COLMILLOIGNEO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.ENVITEIGNEO:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 33);
                    break;

                case AtaqueID.HUMAREDA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ALUD:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.COLMILLOHIELO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 10);
                    break;

                case AtaqueID.ABOCAJARRO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ESFERAAURAL:
                    textoMostrar += AtaqueInfalible(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ONDACERTERA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 10);
                    break;

                case AtaqueID.ONDAVACIO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PUGNODRENAJE:
                    textoMostrar += AtaqueConCuracion(origen, ataqueRealizado, destino, 50);
                    break;

                case AtaqueID.LLUEVEHOJAS:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.LATIGAZO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.MAZAZO:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 33);
                    break;

                case AtaqueID.CABEZAZOZEN:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.PSICOCORTE:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.JOYADELUZ:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PULIMIENTO:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.VELOCIDAD, 2, 0);
                    break;

                case AtaqueID.ROCAAFILADA:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.ROMPERROCAS:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 20);
                    break;

                case AtaqueID.TESTARAZO:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 33);
                    break;

                case AtaqueID.BRECHANEGRA:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.NINGUNA, 0, false);
                    break;

                case AtaqueID.BUENABAZA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.PULSOUMBRIO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.TAJOUMBRIO:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.GOLPEBAJO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CASTIGO:
                    textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, origen.nivel);
                    break;

                case AtaqueID.MAQUINACION:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.ESPECIAL, 2, 0);
                    break;

                case AtaqueID.BOMBAFANGO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.PRECISION, 30);
                    break;

                case AtaqueID.LANZAMUGRE:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.TIERRAVIVA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 10);
                    break;

                case AtaqueID.PUYANOCIVA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.VENENOX:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.RESPIRO:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.NINGUNA, 0, 50);
                    break;

                case AtaqueID.TAJOAEREO:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.DESPEJAR:
                    textoMostrar += AtaqueDeEstadoSobreEnemigo(origen, ataqueRealizado, destino, Estadistica.EVASION, 1, false);
                    break;

                case AtaqueID.PAJAROOSADO:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 33);
                    break;

                case AtaqueID.PULSOPRIMIGENIO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.FILODELABISMO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ASCENSODRACO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ENERGIBOLA:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 10);
                    break;

                case AtaqueID.ALIGERAR:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.VELOCIDAD, 2, 0);
                    break;

                case AtaqueID.RUEDADOBLE:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, 2);
                    break;

                case AtaqueID.CONCHAFILO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.DEFENSA, 50);
                    break;

                case AtaqueID.ESCALDAR:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.ESTOICISMO:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.ESPECIAL, 100);
                    break;

                case AtaqueID.RODILLODEPUAS:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.GOLPEBIS:
                    textoMostrar += AtaqueConMultiHit(origen, ataqueRealizado, destino, 2);
                    break;

                case AtaqueID.VOLTIOCRUEL:
                    textoMostrar += AtaqueConDagnoUsuario(origen, ataqueRealizado, destino, 25);
                    break;

                case AtaqueID.INFORTUNIO:
                    int auxInfortunio = 50;
                    if (destino.estadisticasActuales.estadoActual != Estado.NINGUNO)
                        auxInfortunio = 100;
                    textoMostrar += AtaqueConDagnoFijo(origen, ataqueRealizado, destino, auxInfortunio);
                    break;

                case AtaqueID.CALCINACION:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.INFIERNO:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.LLAMAAZUL:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.CHUZOS:
                    textoMostrar += AtaqueConRetroceso(origen, ataqueRealizado, destino, 20);
                    break;

                case AtaqueID.TALADRADORA:
                    textoMostrar += AtaqueConProbCritico(origen, ataqueRealizado, destino, 12.5);
                    break;

                case AtaqueID.TERRATEMBLOR:
                    textoMostrar += AtaqueConCambioEstadistica(origen, ataqueRealizado, destino, Estadistica.VELOCIDAD, 100);
                    break;

                case AtaqueID.ONDATOXICA:
                    textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
                    break;

                case AtaqueID.VASTAGUARDIA:
                    textoMostrar += AtaqueDeEstadoSobreTi(origen, ataqueRealizado, Estadistica.DEFENSA, 2, 0);
                    break;

                case AtaqueID.VENDAVAL:
                    textoMostrar += AtaqueConConfundir(origen, ataqueRealizado, destino, 30);
                    break;

                case AtaqueID.ESCUDOREAL:
                    textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";
                    AnimacionEstadoTu(origen, ataqueRealizado);
                    origen.estadisticasActuales.reflejo = true;
                    textoMostrar += "Escudo Real protrege a " + origen.nombre + " de ataques físicos.\n";
                    break;

                case AtaqueID.ALAMORTIFERA:
                    textoMostrar += AtaqueConCuracion(origen, ataqueRealizado, destino, 75);
                    break;

                default:
                    textoMostrar += origen.nombre + " uso " + ataqueRealizado.nombre + ".\nPero no esta programado.";
                    break;
            }
            //Falta sustituto 
            if (origen.fkPokedex == (int)PokemonNombre.Meowth) 
                //Si es meawth te insulta. Es lo que tiene poder hablar. F
                textoMostrar += GetInsulto(origen);
            return textoMostrar;
        }

#endregion

        #region Ataques

        public String AtaqueConFulminante(Pokemon origen, Ataque ataqueRealizado, Pokemon destino)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, 0, 0, 0, Estadistica.NINGUNA, 0, false, true, 0, 1);
        }

        public String AtaqueConDagnoSiSeFalla(Pokemon origen, Ataque ataqueRealizado, Pokemon destino)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, 0, 0, 0, Estadistica.NINGUNA, 0, true, false, 0, 1);
        }

        public String AtaqueConCuracion(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, int curacion)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, 0, 0, curacion, Estadistica.NINGUNA, 0, false, false, 0, 1);
        }

        public String AtaqueConProbCritico(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, double probCritico)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, probCritico, false, 0, 0, 0, 0, Estadistica.NINGUNA, 0, false, false, 0, 1);
        }

        public String AtaqueConCambioEstadistica(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, Estadistica estadisticaCambiar, double probEstadistica)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, 0, 0, 0, estadisticaCambiar, probEstadistica, false, false, 0, 1);
        }

        public String AtaqueConDagnoUsuario(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, int cantDagno)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, 0, cantDagno, 0, Estadistica.NINGUNA, 0, false, false, 0, 1);
        }

        public String AtaqueBasico(Pokemon origen, Ataque ataqueRealizado, Pokemon destino)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, 0, 0, 0, Estadistica.NINGUNA, 0, false, false, 0, 1);
        }

        public String AtaqueConMultiHit(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, int numGolpes)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, 0, 0, 0, Estadistica.NINGUNA, 0, false, false, 0, numGolpes);
        }

        public String AtaqueInfalible(Pokemon origen, Ataque ataqueRealizado, Pokemon destino)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, true, 0, 0, 0, 0, Estadistica.NINGUNA, 0, false, false, 0, 1);
        }

        public String AtaqueConConfundir(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, int probConfundir)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, 0, 0, 0, Estadistica.NINGUNA, 0, false, false, probConfundir, 1);
        }

        public String AtaqueConRetroceso(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, double probretroceso)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, 0, probretroceso, 0, 0, Estadistica.NINGUNA, 0, false, false, 0, 1);
        }

        public String AtaqueConDagnoFijo(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, int dagnoFijo)
        {
            return AtaqueCompleto(origen, ataqueRealizado, destino, 0, false, dagnoFijo, 0, 0, 0, Estadistica.NINGUNA, 0, false, false, 0, 1);
        }

        public String AtaqueCompleto(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, double probCritico,
            Boolean infalible, int dagnoFijo, double probRetroceso, int dagnoAlUsuario, int curacionAlUsuario, Estadistica estadisticaQueCambia,
            double probCambiarEstadistica, Boolean pierdeVidaSiFalla, Boolean fulminante, int probConfusion, int numGolpes)
        {
            Boolean haCambiadoStat = false;
            String textoMostrar = origen.nombre + " usó " + ataqueRealizado.nombre + ". ";
            int vidaQuitada = 0;
            double eficacia = CalculoEficacia(ataqueRealizado, destino);
            //Si no afecta al objetivo, dagno fijo siempre afecta.
            if (eficacia == 0 && dagnoFijo == 0)
                return textoMostrar + "No afecta a " + destino.nombre + ".";

            //Si acierta el ataque o es infalible (rapidez)
            if (AciertaAtaque(origen, ataqueRealizado, destino) || infalible)
            {
                if (dagnoFijo == 0) //Si es dagno fijo da igual la eficacia.
                    textoMostrar += MuestraEficacia(ataqueRealizado, destino);

                //Animacion del ataque.
                if (!fulminante || origen.estadisticasActuales.velocidadActual > destino.estadisticasActuales.velocidadActual)
                    AnimacionAtaqueNormal(destino, ataqueRealizado, eficacia);

                //Si pegamos con fuego a un enemigo congelado, se descongela.
                if (ataqueRealizado.tipo == Tipo.FUEGO && destino.estadisticasActuales.estadoActual == Estado.CONGELADO)
                {
                    destino.estadisticasActuales.estadoActual = Estado.NINGUNO;
                    textoMostrar += destino.nombre + " se ha descongelado.\n";
                }

                //Si hace retroceder al objetivo
                if (probRetroceso != 0 && AciertaProbabilidad(probRetroceso))
                    destino.estadisticasActuales.haRetrocedido = true;

                //Si confunde al objetivo
                if (probConfusion > 0 && AciertaProbabilidad(probConfusion))
                {
                    textoMostrar += destino.nombre + " se confundió.\n";
                    destino.estadisticasActuales.confuso = random.Next(1, 4);
                }

                //Si el ataque no es de dagno fijo
                if (dagnoFijo == 0 && !fulminante)
                {
                    if (numGolpes > 1)
                        textoMostrar += "Número de golpes: " + numGolpes + "\n";
                    //Bajamos vida del pokemon afectado miramos el num de golpes.
                    for (int i = 0; i < numGolpes; i++)
                    {
                        vidaQuitada = CalculaDagnoDelGolpe(origen, ataqueRealizado, destino);
                        //Si el golpe es un crítico.
                        if (EsCritico(origen.estadisticasActuales, probCritico))
                        {
                            textoMostrar += "¡Un golpe crítico!\n";
                            vidaQuitada *= 2;
                        }
                        destino.vidaActual -= vidaQuitada;
                    }
                }
                //Si es dagno fijo
                else if (!fulminante)
                {
                    vidaQuitada = dagnoFijo;
                    destino.vidaActual -= vidaQuitada;
                }

                //Si el ataque es fulminante
                if (fulminante && origen.estadisticasActuales.velocidadActual > destino.estadisticasActuales.velocidadActual)
                {
                    vidaQuitada = destino.vidaMax + 1;
                    destino.vidaActual -= vidaQuitada;
                    textoMostrar += destino.nombre + " sufrió un golpe fulminante.\n";
                }
                else if (fulminante)
                    textoMostrar += "Pero falló.\n";

                if (destino.vidaActual <= 0) //Si se debilita.
                {
                    destino.vidaActual = 0;
                    destino.estadisticasActuales.debilitado = true;
                    if (ataqueRealizado.ataqueID == AtaqueID.HIPERRAYO) //Si lanzamos hiperrayo se cancela la espera
                    {
                        origen.estadisticasActuales.turnosEspera = 0;
                        origen.estadisticasActuales.movLanzadoTrasEspera = null;
                    }
                }

                //Si el usuario se dagna tambien
                if (dagnoAlUsuario != 0)
                {
                    textoMostrar += origen.nombre + " también se hace daño.\n";
                    origen.vidaActual -= (int)(vidaQuitada * (double)dagnoAlUsuario / (double)100);
                    if (origen.vidaActual <= 0) //Si se debilita.
                    {
                        origen.vidaActual = 0;
                        origen.estadisticasActuales.debilitado = true;
                    }
                }

                //Si se cura parte del dagno
                if (curacionAlUsuario != 0)
                {
                    textoMostrar += origen.nombre + " ha recuperado vida.\n";
                    origen.vidaActual += (int)(vidaQuitada * (double)curacionAlUsuario / (double)100);
                    if (origen.vidaActual > origen.vidaMax)
                        origen.vidaActual = origen.vidaMax;
                }

                //Si el ataque causa algun estado
                if (ataqueRealizado.estadoQueProvoca != Estado.NINGUNO && AciertaProbabilidad(ataqueRealizado.probEstado))
                {
                    switch (ataqueRealizado.estadoQueProvoca)
                    {
                        case Estado.ENVENENADO:
                            //No afecta a veneno y acero
                            if (destino.tipo1 == Tipo.VENENO || destino.tipo2 == Tipo.VENENO || destino.tipo1 == Tipo.ACERO || destino.tipo2 == Tipo.ACERO)
                                break;
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.ENVENENADO;
                                textoMostrar += destino.nombre + " se ha envenenado.\n";
                            }
                            break;
                        case Estado.QUEMADO:
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.QUEMADO;
                                destino.estadisticasActuales.ataqueActual /= 2;
                                textoMostrar += destino.nombre + " se ha quemado.\n";
                            }
                            break;
                        case Estado.CONGELADO:
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO && destino.tipo1 != Tipo.HIELO && destino.tipo2 != Tipo.HIELO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.CONGELADO;
                                textoMostrar += destino.nombre + " se ha congelado.\n";
                            }
                            break;
                        case Estado.DORMIDO:
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.DORMIDO;
                                destino.estadisticasActuales.turnosDormido = random.Next(2, 7);
                                textoMostrar += destino.nombre + " se ha dormido.\n";
                            }
                            break;
                        case Estado.GRAVEMENTEENVENENADO:
                            //No afecta a veneno y acero
                            if (destino.tipo1 == Tipo.VENENO || destino.tipo2 == Tipo.VENENO || destino.tipo1 == Tipo.ACERO || destino.tipo2 == Tipo.ACERO)
                                break;

                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO || destino.estadisticasActuales.estadoActual == Estado.ENVENENADO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.GRAVEMENTEENVENENADO;
                                destino.estadisticasActuales.turnosGravementeEnvenenado = 1;
                                textoMostrar += destino.nombre + " fue gravemente envenenado.\n";
                            }
                            break;
                        case Estado.PARALIZADO:
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.PARALIZADO;
                                destino.estadisticasActuales.velocidadActual /= 4;
                                textoMostrar += destino.nombre + " se ha paralizado.\n";
                            }
                            break;
                    }
                }

                //Si cambia alguna estadistica el ataque.
                if (estadisticaQueCambia != Estadistica.NINGUNA && AciertaProbabilidad(probCambiarEstadistica))
                {
                    switch (estadisticaQueCambia)
                    {
                        case Estadistica.ATAQUE:
                            if (destino.estadisticasActuales.modificadorAtaque > -6)
                            {
                                textoMostrar += "El ataque de " + destino.nombre + " bajó.\n";
                                destino.estadisticasActuales.modificadorAtaque--;
                                destino.CambiarEstadistica(Estadistica.ATAQUE);
                                haCambiadoStat = true;
                            }
                            break;
                        case Estadistica.DEFENSA:
                            if (destino.estadisticasActuales.modificadorDefensa > -6)
                            {
                                textoMostrar += "La defensa de " + destino.nombre + " bajó.\n";
                                destino.estadisticasActuales.modificadorDefensa--;
                                destino.CambiarEstadistica(Estadistica.DEFENSA);
                                haCambiadoStat = true;
                            }
                            break;
                        case Estadistica.VELOCIDAD:
                            if (destino.estadisticasActuales.modificadorVelocidad > -6)
                            {
                                destino.estadisticasActuales.modificadorVelocidad--;
                                destino.CambiarEstadistica(Estadistica.VELOCIDAD);
                                textoMostrar += "La velocidad de " + destino.nombre + " ha disminuido.\n";
                                haCambiadoStat = true;
                            }
                            break;
                        case Estadistica.ESPECIAL:
                            if (destino.estadisticasActuales.modificadorEspecial > -6)
                            {
                                destino.estadisticasActuales.modificadorEspecial--;
                                destino.CambiarEstadistica(Estadistica.ESPECIAL);
                                textoMostrar += "El especial de " + destino.nombre + " ha disminuido.\n";
                                haCambiadoStat = true;
                            }
                            break;
                        case Estadistica.CRITICO:
                            if (destino.estadisticasActuales.modificadorCritico > 0)
                            {
                                destino.estadisticasActuales.modificadorCritico--;
                                textoMostrar += "El crítico de " + destino.nombre + " ha disminuido.\n";
                                haCambiadoStat = true;
                            }
                            break;
                        case Estadistica.EVASION:
                            if (destino.estadisticasActuales.modificadorEvasion > -6)
                            {
                                destino.estadisticasActuales.modificadorEvasion--;
                                textoMostrar += "La evasión de " + destino.nombre + " ha disminuido.\n";
                                haCambiadoStat = true;
                            }
                            break;
                        case Estadistica.PRECISION:
                            if (destino.estadisticasActuales.modificadorPrecision > -6)
                            {
                                destino.estadisticasActuales.modificadorPrecision--;
                                textoMostrar += "La precisión de " + destino.nombre + " ha disminuido.\n";
                                haCambiadoStat = true;
                            }
                            break;
                    }

                    //Sonido estadistica
                    if (haCambiadoStat)
                    {
                        new System.Threading.Thread(() =>
                        {
                            SoundPlayer player2 = new SoundPlayer("Sonido\\Hit\\Stat Fall Down.wav");
                            player2.PlaySync();
                        }).Start();
                    }
                }
            }
            else
            {
                textoMostrar += "Pero ha fallado.\n";
                if (pierdeVidaSiFalla)
                {
                    textoMostrar += origen.nombre + " se hace daño.\n";
                    origen.vidaActual -= 1;
                    if (origen.vidaActual <= 0)
                    {
                        origen.vidaActual = 0;
                        origen.estadisticasActuales.debilitado = true;
                    }
                }
            }
            destino.estadisticasActuales.ultimoAtaqueRecibido = ataqueRealizado;
            destino.estadisticasActuales.dagnoUltimoGolpeRecibido = vidaQuitada;
            return textoMostrar;
        }

        #endregion

        #region Ataque de estado

        public String AtaqueDeEstadoSobreEnemigo(Pokemon origen, Ataque ataqueRealizado, Pokemon destino, Estadistica estadisticaCambiar, int cantidadEstadistica, Boolean confunde)
        {
            Boolean haCambiadoStat = false;
            String textoMostrar = "";
            textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";

            //Ataque
            if (AciertaAtaque(origen, ataqueRealizado, destino))
            {
                //Animacion
                AnimacionEstadoRival(destino, ataqueRealizado);

                //Si causa estado
                if (ataqueRealizado.estadoQueProvoca != Estado.NINGUNO)
                    switch (ataqueRealizado.estadoQueProvoca)
                    {
                        case Estado.ENVENENADO:
                            //No afecta a veneno y acero
                            if (destino.tipo1 == Tipo.VENENO || destino.tipo2 == Tipo.VENENO || destino.tipo1 == Tipo.ACERO || destino.tipo2 == Tipo.ACERO)
                            {
                                textoMostrar += destino.nombre + " es inmune al veneno.\n";
                                break;
                            }
                            if (destino.estadisticasActuales.estadoActual == Estado.ENVENENADO)
                                textoMostrar += destino.nombre + " ya está envenenado.\n";
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.ENVENENADO;
                                textoMostrar += destino.nombre + " se ha envenenado.\n";
                            }
                            break;
                        case Estado.QUEMADO:
                            if (destino.estadisticasActuales.estadoActual == Estado.QUEMADO)
                                textoMostrar += destino.nombre + " ya está quemado.\n";
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO && destino.tipo1 != Tipo.FUEGO && destino.tipo2 != Tipo.FUEGO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.QUEMADO;
                                destino.estadisticasActuales.ataqueActual /= 2;
                                textoMostrar += destino.nombre + " se ha quemado.\n";
                            }
                            break;
                        case Estado.CONGELADO:
                            if (destino.estadisticasActuales.estadoActual == Estado.CONGELADO)
                                textoMostrar += destino.nombre + " ya está congelado.\n";
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO && destino.tipo1 != Tipo.HIELO && destino.tipo2 != Tipo.HIELO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.CONGELADO;
                                textoMostrar += destino.nombre + " se ha congelado.\n";
                            }
                            break;
                        case Estado.DORMIDO:
                            if (destino.estadisticasActuales.estadoActual == Estado.DORMIDO)
                                textoMostrar += destino.nombre + " ya está dormido.\n";
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.DORMIDO;
                                destino.estadisticasActuales.turnosDormido = random.Next(2, 7);
                                textoMostrar += destino.nombre + " se ha dormido.\n";
                            }
                            break;
                        case Estado.GRAVEMENTEENVENENADO:
                            //No afecta a veneno y acero
                            if (destino.tipo1 == Tipo.VENENO || destino.tipo2 == Tipo.VENENO || destino.tipo1 == Tipo.ACERO || destino.tipo2 == Tipo.ACERO)
                            {
                                textoMostrar += destino.nombre + " es inmune al veneno.\n";
                                break;
                            }
                            if (destino.estadisticasActuales.estadoActual == Estado.GRAVEMENTEENVENENADO)
                                textoMostrar += destino.nombre + " ya está gravemente envenenado.\n";
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO || destino.estadisticasActuales.estadoActual == Estado.ENVENENADO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.GRAVEMENTEENVENENADO;
                                destino.estadisticasActuales.turnosGravementeEnvenenado = 1;
                                textoMostrar += destino.nombre + " fue gravemente envenenado.\n";
                            }
                            break;
                        case Estado.PARALIZADO:
                            if (destino.estadisticasActuales.estadoActual == Estado.PARALIZADO)
                                textoMostrar += destino.nombre + " ya está paralizado.\n";
                            if (destino.estadisticasActuales.estadoActual == Estado.NINGUNO)
                            {
                                destino.estadisticasActuales.estadoActual = Estado.PARALIZADO;
                                destino.estadisticasActuales.velocidadActual /= 4;
                                textoMostrar += destino.nombre + " se ha paralizado.\n";
                            }
                            break;
                    }

                //Si cambia estadisticas
                if (estadisticaCambiar != Estadistica.NINGUNA)
                    switch (estadisticaCambiar)
                    {
                        case Estadistica.ATAQUE:
                            if (destino.estadisticasActuales.modificadorAtaque > -6)
                            {
                                if (cantidadEstadistica > 1)
                                    textoMostrar += "El ataque de " + destino.nombre + " bajó enormemente.\n";
                                else
                                    textoMostrar += "El ataque de " + destino.nombre + " bajó.\n";
                                destino.estadisticasActuales.modificadorAtaque -= cantidadEstadistica;
                                if (destino.estadisticasActuales.modificadorAtaque < -6)
                                    destino.estadisticasActuales.modificadorAtaque = -6;
                                destino.CambiarEstadistica(Estadistica.ATAQUE);
                                haCambiadoStat = true;
                            }
                            else
                                textoMostrar += "El ataque de " + destino.nombre + " no puede bajar más.\n";
                            break;
                        case Estadistica.DEFENSA:
                            if (destino.estadisticasActuales.modificadorDefensa > -6)
                            {
                                if (cantidadEstadistica > 1)
                                    textoMostrar += "La defensa de " + destino.nombre + " bajó enormemente.\n";
                                else
                                    textoMostrar += "La defensa de " + destino.nombre + " bajó.\n";
                                destino.estadisticasActuales.modificadorDefensa -= cantidadEstadistica;
                                if (destino.estadisticasActuales.modificadorDefensa < -6)
                                    destino.estadisticasActuales.modificadorDefensa = -6;
                                destino.CambiarEstadistica(Estadistica.DEFENSA);
                                haCambiadoStat = true;
                            }
                            else
                                textoMostrar += "La defensa de " + destino.nombre + " no puede bajar más.\n";
                            break;
                        case Estadistica.VELOCIDAD:
                            if (destino.estadisticasActuales.modificadorVelocidad > -6)
                            {
                                if (cantidadEstadistica > 1)
                                    textoMostrar += "La velocidad de " + destino.nombre + " bajó enormemente.\n";
                                else
                                    textoMostrar += "La velocidad de " + destino.nombre + " bajó.\n";
                                destino.estadisticasActuales.modificadorVelocidad -= cantidadEstadistica;
                                if (destino.estadisticasActuales.modificadorVelocidad < -6)
                                    destino.estadisticasActuales.modificadorVelocidad = -6;
                                destino.CambiarEstadistica(Estadistica.VELOCIDAD);
                                haCambiadoStat = true;
                            }
                            else
                                textoMostrar += "La velocidad de " + destino.nombre + " no puede bajar más.\n";
                            break;
                        case Estadistica.ESPECIAL:
                            if (destino.estadisticasActuales.modificadorEspecial > -6)
                            {
                                if (cantidadEstadistica > 1)
                                    textoMostrar += "El especial de " + destino.nombre + " bajó enormemente.\n";
                                else
                                    textoMostrar += "El especial de " + destino.nombre + " bajó.\n";
                                destino.estadisticasActuales.modificadorEspecial -= cantidadEstadistica;
                                if (destino.estadisticasActuales.modificadorEspecial < -6)
                                    destino.estadisticasActuales.modificadorEspecial = -6;
                                destino.CambiarEstadistica(Estadistica.ESPECIAL);
                                haCambiadoStat = true;
                            }
                            else
                                textoMostrar += "El especial de " + destino.nombre + " no puede bajar más.\n";
                            break;
                        case Estadistica.CRITICO:
                            if (destino.estadisticasActuales.modificadorCritico > 0)
                            {
                                destino.estadisticasActuales.modificadorCritico -= cantidadEstadistica;
                                textoMostrar += "El crítico de " + destino.nombre + " ha disminuido.\n";
                                haCambiadoStat = true;
                            }
                            else
                                textoMostrar += "El crítico de " + destino.nombre + " no puede bajar más.\n";
                            break;
                        case Estadistica.EVASION:
                            if (destino.estadisticasActuales.modificadorEvasion > -6)
                            {
                                destino.estadisticasActuales.modificadorEvasion -= cantidadEstadistica;
                                textoMostrar += "La evasión de " + destino.nombre + " ha disminuido.\n";
                                haCambiadoStat = true;
                            }
                            else
                                textoMostrar += "La evasión de " + destino.nombre + " no puede bajar más.\n";
                            break;
                        case Estadistica.PRECISION:
                            if (destino.estadisticasActuales.modificadorPrecision > -6)
                            {
                                destino.estadisticasActuales.modificadorPrecision -= cantidadEstadistica;
                                textoMostrar += "La precisión de " + destino.nombre + " ha disminuido.\n";
                                haCambiadoStat = true;
                            }
                            else
                                textoMostrar += "La precisión de " + destino.nombre + " no puede bajar más.\n";
                            break;
                    }

                //Sonido estadistica
                if (haCambiadoStat)
                {
                    new System.Threading.Thread(() =>
                    {
                        player = new SoundPlayer("Sonido\\Hit\\Stat Fall Down.wav");
                        player.PlaySync();
                    }).Start();
                }

                //Si confunde
                if (confunde)
                    if (destino.estadisticasActuales.confuso <= -1)
                    {
                        destino.estadisticasActuales.confuso = random.Next(1, 4);
                        textoMostrar += destino.nombre + " se ha confundido.\n";
                    }
                    else
                        textoMostrar += destino.nombre + " ya está confuso.\n";
            }
            else
                textoMostrar += "Pero falló.\n";
            return textoMostrar;
        }

        public String AtaqueDeEstadoSobreTi(Pokemon origen, Ataque ataqueRealizado, Estadistica estadisticaCambiar, int cantidadEstadistica, int curacion)
        {
            Boolean haCambiadoStat = false;
            String textoMostrar = "";
            textoMostrar += origen.nombre + " usó " + ataqueRealizado.nombre + ".\n";

            //Animacion de combate
            AnimacionEstadoTu(origen, ataqueRealizado);

            //Si se duerme a causa del ataque
            if (ataqueRealizado.estadoQueProvoca == Estado.DORMIDO)
            {
                origen.estadisticasActuales.estadoActual = Estado.DORMIDO;
                origen.estadisticasActuales.turnosDormido = 5;
                textoMostrar += origen.nombre + " se fue a dormir.\n";
            }

            //Si cambia estadisticas
            switch (estadisticaCambiar)
            {
                case Estadistica.ATAQUE:
                    if (origen.estadisticasActuales.modificadorAtaque < 6)
                    {
                        if (cantidadEstadistica > 1)
                            textoMostrar += "El ataque de " + origen.nombre + " subió enormemente.\n";
                        else
                            textoMostrar += "El ataque de " + origen.nombre + " subió.\n";
                        origen.estadisticasActuales.modificadorAtaque += cantidadEstadistica;
                        if (origen.estadisticasActuales.modificadorAtaque > 6)
                            origen.estadisticasActuales.modificadorAtaque = 6;
                        origen.CambiarEstadistica(Estadistica.ATAQUE);
                        haCambiadoStat = true;
                    }
                    else
                        textoMostrar += "El ataque de " + origen.nombre + " no puede subir más.\n";
                    break;
                case Estadistica.DEFENSA:
                    if (origen.estadisticasActuales.modificadorDefensa < 6)
                    {
                        if (cantidadEstadistica > 1)
                            textoMostrar += "La defensa de " + origen.nombre + " subió enormemente.\n";
                        else
                            textoMostrar += "La defensa de " + origen.nombre + " subió.\n";
                        origen.estadisticasActuales.modificadorDefensa += cantidadEstadistica;
                        if (origen.estadisticasActuales.modificadorDefensa > 6)
                            origen.estadisticasActuales.modificadorDefensa = 6;
                        origen.CambiarEstadistica(Estadistica.DEFENSA);
                        haCambiadoStat = true;
                    }
                    else
                        textoMostrar += "La defensa de " + origen.nombre + " no puede subir más.\n";
                    break;
                case Estadistica.VELOCIDAD:
                    if (origen.estadisticasActuales.modificadorVelocidad < 6)
                    {
                        if (cantidadEstadistica > 1)
                            textoMostrar += "La velocidad de " + origen.nombre + " subió enormemente.\n";
                        else
                            textoMostrar += "La velocidad de " + origen.nombre + " subió.\n";
                        origen.estadisticasActuales.modificadorVelocidad += cantidadEstadistica;
                        if (origen.estadisticasActuales.modificadorVelocidad > 6)
                            origen.estadisticasActuales.modificadorVelocidad = 6;
                        origen.CambiarEstadistica(Estadistica.VELOCIDAD);
                        haCambiadoStat = true;
                    }
                    else
                        textoMostrar += "La velocidad de " + origen.nombre + " no puede subir más.\n";
                    break;
                case Estadistica.ESPECIAL:
                    if (origen.estadisticasActuales.modificadorEspecial < 6)
                    {
                        if (cantidadEstadistica > 1)
                            textoMostrar += "El especial de " + origen.nombre + " subió enormemente.\n";
                        else
                            textoMostrar += "El especial de " + origen.nombre + " subió.\n";
                        origen.estadisticasActuales.modificadorEspecial += cantidadEstadistica;
                        if (origen.estadisticasActuales.modificadorEspecial > 6)
                            origen.estadisticasActuales.modificadorEspecial = 6;
                        origen.CambiarEstadistica(Estadistica.ESPECIAL);
                        haCambiadoStat = true;
                    }
                    else
                        textoMostrar += "El especial de " + origen.nombre + " no puede subir más.\n";
                    break;
                case Estadistica.CRITICO:
                    if (origen.estadisticasActuales.modificadorCritico < 4)
                    {
                        origen.estadisticasActuales.modificadorCritico += cantidadEstadistica;
                        textoMostrar += "El crítico de " + origen.nombre + " ha aumentado.\n";
                        haCambiadoStat = true;
                    }
                    else
                        textoMostrar += "El crítico de " + origen.nombre + " no puede aumentar más.\n";
                    break;
                case Estadistica.EVASION:
                    if (origen.estadisticasActuales.modificadorEvasion < 6)
                    {
                        origen.estadisticasActuales.modificadorEvasion += cantidadEstadistica;
                        textoMostrar += "La evasión de " + origen.nombre + " ha aumentado.\n";
                        haCambiadoStat = true;
                    }
                    else
                        textoMostrar += "La evasión de " + origen.nombre + " no puede aumentar más.\n";
                    break;
                case Estadistica.PRECISION:
                    if (origen.estadisticasActuales.modificadorPrecision < 6)
                    {
                        origen.estadisticasActuales.modificadorPrecision += cantidadEstadistica;
                        textoMostrar += "La precisión de " + origen.nombre + " ha aumentado.\n";
                        haCambiadoStat = true;
                    }
                    else
                        textoMostrar += "La precisión de " + origen.nombre + " no puede aumentar más.\n";
                    break;
            }

            //Sonido estadistica
            if (haCambiadoStat)
            {
                new System.Threading.Thread(() => { new SoundPlayer("Sonido\\Hit\\Stat Rise Up.wav").PlaySync(); }).Start();
            }

            //Si se cura el pokemon
            if (curacion > 0)
            {
                player = new SoundPlayer("Sonido\\Hit\\healing.wav");
                textoMostrar += origen.nombre + " ha recuperado salud.\n";
                origen.vidaActual += (int)(origen.vidaMax * ((double)curacion / (double)100));
                if (origen.vidaActual > origen.vidaMax)
                    origen.vidaActual = origen.vidaMax;
            }
            return textoMostrar;
        }

        #endregion

        #region Ataque de varios turnos

        public String EmpezarAtaqueDeVariosTurnos(Pokemon origen, Ataque ataqueRealizado, Pokemon destino)
        {
            String textoMostrar = "";
            int nGolpes = NumGolpes();

            if (ataqueRealizado.ataqueID == AtaqueID.GOLPE || ataqueRealizado.ataqueID == AtaqueID.DANZAPETALO)
                nGolpes = random.Next(3, 4);

            origen.estadisticasActuales.turnosContinuarMovimiento = nGolpes;
            origen.estadisticasActuales.movQueContinua = ataqueRealizado;
            textoMostrar += AtaqueBasico(origen, ataqueRealizado, destino);
            if (ataqueRealizado.ataqueID != AtaqueID.GOLPE && ataqueRealizado.ataqueID != AtaqueID.DANZAPETALO)
            {
                destino.estadisticasActuales.turnosInmovilizado = nGolpes;
                textoMostrar += destino.nombre + " ha sido atrapado. \n";
            }
            return textoMostrar;
        }

        #endregion

        #region Animaciones

        public void AnimacionAtaqueNormal(Pokemon destino, Ataque ataqueRealizado, double eficacia)
        {
            //Animacion de combate
            Image img = Image.FromFile("Img\\Ataques\\" + ataqueRealizado.idMovimiento + ".png");
            combate.picBoxAtaque.Image = img;

            if (destino == combate.pokemonFront)
            {
                combate.picBoxAtaque.Size = combate.picBoxPkmnFront.Size;
                combate.picBoxAtaque.Location = combate.picBoxPkmnFront.Location;
                new AnimacionAtaque(combate.picBoxPkmnFront, combate.picBoxAtaque);
            }
            else
            {
                combate.picBoxAtaque.Size = combate.picBoxPkmnBack.Size;
                combate.picBoxAtaque.Location = combate.picBoxPkmnBack.Location;
                new AnimacionAtaque(combate.picBoxPkmnBack, combate.picBoxAtaque);
            }
            //Sonido animacion
            new System.Threading.Thread(() =>
            {
                switch (eficacia)
                {
                    case 0:
                    case 0.25:
                    case 0.5:
                        player = new SoundPlayer("Sonido\\Hit\\pocoEficaz.wav");
                        break;
                    case 2:
                    case 4:
                        player = new SoundPlayer("Sonido\\Hit\\superEficaz.wav");
                        break;
                    default:
                        player = new SoundPlayer("Sonido\\Hit\\normal.wav");
                        break;
                }
                player.PlaySync();
            }).Start();
        }

        public void AnimacionEstadoRival(Pokemon destino, Ataque ataqueRealizado)
        {
            //Animacion de combate
            Image img = Image.FromFile("Img\\Ataques\\" + ataqueRealizado.idMovimiento + ".png");
            combate.picBoxAtaque.Image = img;

            if (destino == combate.pokemonFront)
            {
                combate.picBoxAtaque.Size = combate.picBoxPkmnFront.Size;
                combate.picBoxAtaque.Location = combate.picBoxPkmnFront.Location;
                new AnimacionAtaque(combate.picBoxPkmnFront, combate.picBoxAtaque, false);
            }
            else
            {
                combate.picBoxAtaque.Size = combate.picBoxPkmnBack.Size;
                combate.picBoxAtaque.Location = combate.picBoxPkmnBack.Location;
                new AnimacionAtaque(combate.picBoxPkmnBack, combate.picBoxAtaque, false);
            }
        }

        public void AnimacionEstadoTu(Pokemon origen, Ataque ataqueRealizado)
        {
            //Animacion de combate
            Image img = Image.FromFile("Img\\Ataques\\" + ataqueRealizado.idMovimiento + ".png");
            combate.picBoxAtaque.Image = img;

            if (origen == combate.pokemonFront)
            {
                combate.picBoxAtaque.Size = combate.picBoxPkmnFront.Size;
                combate.picBoxAtaque.Location = combate.picBoxPkmnFront.Location;
                new AnimacionAtaque(combate.picBoxPkmnFront, combate.picBoxAtaque, false);
            }
            else
            {
                combate.picBoxAtaque.Size = combate.picBoxPkmnBack.Size;
                combate.picBoxAtaque.Location = combate.picBoxPkmnBack.Location;
                new AnimacionAtaque(combate.picBoxPkmnBack, combate.picBoxAtaque, false);
            }
        }

        public void AnimacionVueloExcavar(Pokemon origen, Ataque ataqueRealizado)
        {
            //Animacion de combate al empezar movimiento
            Image img = null;
            switch (ataqueRealizado.ataqueID)
            {
                case AtaqueID.VUELO:
                    img = Image.FromFile("Img\\Ataques\\vuelo.png");
                    break;
                case AtaqueID.EXCAVAR:
                    img = Image.FromFile("Img\\Ataques\\excavar.png");
                    break;
                case AtaqueID.GOLPEUMBRIO:
                    img = Image.FromFile("Img\\Ataques\\gumbrio.png");
                    break;
            }
            combate.picBoxAtaque.Image = img;
            if (origen == combate.pokemonFront)
            {
                combate.picBoxAtaque.Size = combate.picBoxPkmnFront.Size;
                combate.picBoxAtaque.Location = combate.picBoxPkmnFront.Location;
                new AnimacionAtaque(combate.picBoxPkmnFront, combate.picBoxAtaque, false, true);
            }
            else
            {
                combate.picBoxAtaque.Size = combate.picBoxPkmnBack.Size;
                combate.picBoxAtaque.Location = combate.picBoxPkmnBack.Location;
                new AnimacionAtaque(combate.picBoxPkmnBack, combate.picBoxAtaque, false, true);
            }
        }

        #endregion

        #region Formulas combate

        public Ataque GetAtaqueRandom(Pokemon pokemon)
        {
            List<Ataque> listaAtaques = new List<Ataque>();
            if (pokemon.mov1 != null && pokemon.mov1.ppActuales > 0 && pokemon.mov1.turnosAnulado <= 0)
                listaAtaques.Add(pokemon.mov1);
            if (pokemon.mov2 != null && pokemon.mov2.ppActuales > 0 && pokemon.mov2.turnosAnulado <= 0)
                listaAtaques.Add(pokemon.mov2);
            if (pokemon.mov3 != null && pokemon.mov3.ppActuales > 0 && pokemon.mov3.turnosAnulado <= 0)
                listaAtaques.Add(pokemon.mov3);
            if (pokemon.mov4 != null && pokemon.mov4.ppActuales > 0 && pokemon.mov4.turnosAnulado <= 0)
                listaAtaques.Add(pokemon.mov4);

            int ran = random.Next(0, listaAtaques.Count());
            return listaAtaques[ran];
        }

        public int NumGolpes()
        {
            double i = random.NextDouble();
            if (i < 37.5 / (double)100)
                return 2;
            if (i < 75 / (double)100)
                return 3;
            if (i < 87.5 / (double)100)
                return 4;
            return 5;
        }

        public string MuestraEficacia(Ataque ataqueRealizado, Pokemon destino)
        {
            switch (CalculoEficacia(ataqueRealizado, destino))
            {
                case 0:
                    return "No afecta al " + destino.nombre + " enemigo.\n";
                case 0.25:
                    return "Es poco eficaz.\n";
                case 0.5:
                    return "No es muy eficaz...\n";
                case 2:
                    return "¡Es muy eficaz!\n";
                case 4:
                    return "¡Es super eficaz!\n";
                default:
                    return "\n";
            }
        }

        public bool EsCritico(EstadisticasActuales estadisticasActuales, double baseAtaque)
        {
            double probabilidad = baseAtaque;
            switch (estadisticasActuales.modificadorCritico)
            {
                case 0:
                    probabilidad += 6.25;
                    break;
                case 1:
                    probabilidad += 12.5;
                    break;
                case 2:
                    probabilidad += 25;
                    break;
                case 3:
                    probabilidad += 33.33;
                    break;
                case 4:
                    probabilidad += 50;
                    break;
            }

            if (random.NextDouble() <= probabilidad / (double)100)
                return true;
            else
                return false;
        }

        public int CalculaDagnoDelGolpe(Pokemon origen, Ataque ataqueRealizado, Pokemon destino)
        {
            //Si es de estado no pega
            if (ataqueRealizado.categoria == "Estado")
                return 0;

            double B; //Bonificacion por ser del mismo tipo que el ataque.
            double E; //Eficacia. Entre los tipos del ataque y el objetivo.
            double V; //Aleatoriedad del ataque.
            double N; //Nivel del atacante.
            double A; //Ataque o especial dependiendo del tipo de golpe.
            double P; //Potencia del ataque
            double D; //Defensa o especial dependiendo del tipo de golpe.

            //Calculo de los valores
            E = CalculoEficacia(ataqueRealizado, destino);
            V = random.Next(85, 100);
            N = origen.nivel;
            P = ataqueRealizado.potencia;

            //Calculo de A y D dependiendo de la categoria
            A = D = 0;
            switch (ataqueRealizado.categoria)
            {
                case "Físico":
                    A = origen.estadisticasActuales.ataqueActual;
                    D = destino.estadisticasActuales.defensaActual;
                    if (destino.estadisticasActuales.reflejo)
                        D *= 2;
                    break;
                case "Especial":
                    A = origen.estadisticasActuales.especialActual;
                    D = destino.estadisticasActuales.especialActual;
                    if (destino.estadisticasActuales.pantallaLuz)
                        D *= 2;
                    break;
            }

            //Calculo de Bonificacion (1.5 si atacante y ataque son del mismo tipo).
            if (origen.tipo1 == ataqueRealizado.tipo || origen.tipo2 == ataqueRealizado.tipo)
                B = 1.5;
            else B = 1;

            //Formula
            double dagno = 0.01 * B * E * V * (((0.2 * N + 1) * A * P) / (25 * D) + 2);
            return (int)dagno;
        }

        public double CalculoEficacia(Ataque ataque, Pokemon destino)
        {
            double eficacia1 = 1, eficacia2 = 1;

            //Calculamos la eficacia 1
            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            command.CommandText = "select MULTIPLICADOR from TIPO_EFICACIA where TIPO_ORIGEN = " + (int)ataque.tipo + " and TIPO_DESTINO= " + (int)destino.tipo1;
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
                double.TryParse(reader[0].ToString(), out eficacia1);

            reader.Close();
            con.Close();

            //Calculamos la eficacia 2
            OleDbConnection con2 = ConexionAccess.GetConexion();
            con2.Open();
            OleDbCommand command2 = new OleDbCommand();
            command2.Connection = con2;
            command2.CommandText = "select MULTIPLICADOR from TIPO_EFICACIA where tipo_origen = " + (int)ataque.tipo + " and tipo_destino= " + (int)destino.tipo2;
            OleDbDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read())
                double.TryParse(reader2[0].ToString(), out eficacia2);

            reader2.Close();
            con2.Close();

            return eficacia1 * eficacia2;
        }

        public bool AciertaAtaque(Pokemon origen, Ataque ataqueRealizado, Pokemon destino)
        {
            //Para comesuegnos
            if (ataqueRealizado.ataqueID == AtaqueID.COMESUEGNOS && destino.estadisticasActuales.estadoActual != Estado.DORMIDO)
                return false;

            if (origen == destino)
                return true;

            if (destino.estadisticasActuales.volando == true || destino.estadisticasActuales.escavando == true)
                return false;

            double PMbase; //Precision base del movimiento entre 100
            double Patacante; //Precision del atacante.
            double Erival; //Evasion del rival.

            PMbase = ataqueRealizado.precision / (double)100;

            //Calculo de Patacante
            int numerador = 3, denominador = 3;
            if (origen.estadisticasActuales.modificadorPrecision > 0)
                numerador += origen.estadisticasActuales.modificadorPrecision;
            if (origen.estadisticasActuales.modificadorPrecision < 0)
                denominador += -origen.estadisticasActuales.modificadorPrecision;
            Patacante = (double)numerador / (double)denominador;

            //Calculo de Erival
            numerador = denominador = 3;
            if (origen.estadisticasActuales.modificadorEvasion > 0)
                denominador += destino.estadisticasActuales.modificadorEvasion;
            if (origen.estadisticasActuales.modificadorEvasion < 0)
                numerador += -destino.estadisticasActuales.modificadorEvasion;
            Erival = (double)numerador / (double)denominador;

            //Formula
            double probabilidadGolpeo = PMbase * Patacante / Erival;

            if (random.NextDouble() <= probabilidadGolpeo)
            {
                //Si te golpean con furia.
                if (destino.estadisticasActuales.usandoFuria && ataqueRealizado.categoria != "Estado")
                {
                    destino.estadisticasActuales.modificadorAtaque++;
                    if (destino.estadisticasActuales.modificadorAtaque > 6)
                        destino.estadisticasActuales.modificadorAtaque = 6;
                    destino.CambiarEstadistica(Estadistica.ATAQUE);
                }
                return true;
            }
            return false;
        }

        public bool AciertaProbabilidad(double probabilidad)
        {
            if (random.NextDouble() <= probabilidad / (double)100)
                return true;
            else
                return false;
        }

        #endregion

        #region Insulto

        public String GetInsulto(Pokemon origen)
        {
            String[] insultos = {" ha insinuado que tu madre es una llama.\n",
                                 " te enseña el culo.\n",
                                 " te llama orangután, zampabollos.\n",
                                 " te escupe en el pelo.\n",
                                 " se ha cagado encima.\n",
                                 " ha insultado a tu abuelita.\n",
                                 " dice que eres más tonto que el difunto idiota.\n"};
            return origen.nombre + insultos[random.Next(0, insultos.Length)];
        }

        #endregion

    }
}