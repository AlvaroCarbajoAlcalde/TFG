using Pokemon.TFG;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form_Combate : Form
    {

        #region Propiedades

        public string ipMovil;
        private TcpListener server;
        public int ataqueSeleccionadoMultiplayer;
        public bool multiplayer;
        private Random random;
        private UC_PokemonRestantes indicadorRestantesTu, indicadorRestantesRival;
        public int numTurnos, ticks;
        public Accion accionRealizadaPorFront, accionRealizadaPorBack;
        public bool menuBloqueado;
        public Entrenador entrenadorTu, entrenadorRival;
        public Pokemon pokemonFront, pokemonBack;
        public BarraDeVida vidaTu, vidaRival;
        public TextoMostrar tm;
        private Size originalSize;
        public Rectangle recPanelCombate, recPicBoxPokemonFront, recPicBoxPokemonBack, recMarcoCombate, recPanelInfoRival,
            recPanelInfoJugador, recLabelPvTu, recLabelNombrePkmnTu, recLabelNombrePkmnRival, recLabelNvlTu,
            recLabelNvlRival, recVidaVerdeTu, recVidaRojaTu, recVidaVerdeRival, recVidaRojaRival, recEstadoTu, recEstadoRival;
        public string idLogCombate;
        private bool rendido;
        private Form_RogueLike formRogueLike;
        private string nombreGanador;
        private bool endEnvio;
        private Form_Inicio inicio;

        #endregion

        #region Constructor

        public Form_Combate(Form_Inicio inicio, Entrenador entrenadorTu, Entrenador entrenadorRival, Form_RogueLike formRogueLike = null, bool multiplayer = false)
        {
            this.inicio = inicio;
            inicio.Visible = false;
            InitializeComponent();

            //TCP SERVER
            new Thread(() =>
            {
                server = new TcpListener(IPAddress.Any, TCP.PUERTO_RECIBIR);
                TCP.Servidor(this, server);
            }).Start();

            this.multiplayer = multiplayer;
            nombreGanador = entrenadorRival.nombre;
            idLogCombate = DateTime.Now.Ticks + "";
            rendido = false;
            this.formRogueLike = formRogueLike;

            #region LogCombate
            //Insertar datos base datos
            //Log combate

            OleDbConnection con = ConexionAccess.GetConexion();
            con.Open();

            string sql = "UPDATE entrenador SET partidas=@par WHERE id=@ide";
            OleDbCommand update = new OleDbCommand(sql, con);
            update.Parameters.AddWithValue("@par", ++entrenadorTu.numPartidas);
            update.Parameters.AddWithValue("@ide", entrenadorTu.numEntrenador);
            update.ExecuteNonQuery();

            sql = "insert into log_combate (id, estado, entrenador_1, entrenador_2, img_entrenador1, img_entrenador2, " +
                "img_pok_tu_1, img_pok_tu_2, img_pok_tu_3, img_pok_tu_4, img_pok_tu_5, img_pok_tu_6, " +
                "img_pok_rival_1, img_pok_rival_2, img_pok_rival_3, img_pok_rival_4, img_pok_rival_5, img_pok_rival_6)" +
                " values (@id, @est, @en1, @en2, @imen1, @imen2, @imtupok1, @imtupok2, @imtupok3, @imtupok4, @imtupok5, @imtupok6" +
                ", @imrivalpok1, @imrivalpok2, @imrivalpok3, @imrivalpok4, @imrivalpok5, @imrivalpok6)";
            OleDbCommand insert = new OleDbCommand(sql, con);
            insert.Parameters.AddWithValue("@id", idLogCombate);
            insert.Parameters.AddWithValue("@est", "En Curso");
            insert.Parameters.AddWithValue("@en1", entrenadorTu.nombre);
            insert.Parameters.AddWithValue("@en2", entrenadorRival.nombre);
            insert.Parameters.AddWithValue("@imen1", entrenadorTu.rutaImagenFront);
            insert.Parameters.AddWithValue("@imen2", entrenadorRival.rutaImagenFront);
            insert.Parameters.AddWithValue("@imtupok1", entrenadorTu.equipo[0].fkPokedex);
            insert.Parameters.AddWithValue("@imtupok2", entrenadorTu.equipo[1].fkPokedex);
            insert.Parameters.AddWithValue("@imtupok3", entrenadorTu.equipo[2].fkPokedex);
            insert.Parameters.AddWithValue("@imtupok4", entrenadorTu.equipo[3].fkPokedex);
            insert.Parameters.AddWithValue("@imtupok5", entrenadorTu.equipo[4].fkPokedex);
            insert.Parameters.AddWithValue("@imtupok6", entrenadorTu.equipo[5].fkPokedex);
            insert.Parameters.AddWithValue("@imrivalpok1", entrenadorRival.equipo[0].fkPokedex);
            insert.Parameters.AddWithValue("@imrivalpok2", entrenadorRival.equipo[1].fkPokedex);
            insert.Parameters.AddWithValue("@imrivalpok3", entrenadorRival.equipo[2].fkPokedex);
            insert.Parameters.AddWithValue("@imrivalpok4", entrenadorRival.equipo[3].fkPokedex);
            insert.Parameters.AddWithValue("@imrivalpok5", entrenadorRival.equipo[4].fkPokedex);
            insert.Parameters.AddWithValue("@imrivalpok6", entrenadorRival.equipo[5].fkPokedex);
            insert.ExecuteNonQuery();

            //Log combate equipo
            string idBase = idLogCombate + "tu";
            for (int i = 0; i < entrenadorTu.equipo.Length; i++)
            {
                entrenadorTu.equipo[i].idLog = idBase + i;
                sql = "insert into log_combate_equipo (id, FK_ID_ALMACENAMIENTO, FK_ID_ENTRENADOR, FK_ID_LOGCOMBATE) values (@id, @idal, @iden, @idlog)";
                insert = new OleDbCommand(sql, con);
                insert.Parameters.AddWithValue("@id", entrenadorTu.equipo[i].idLog);
                insert.Parameters.AddWithValue("@idal", entrenadorTu.equipo[i].numAlmacenamiento);
                insert.Parameters.AddWithValue("@iden", entrenadorTu.numEntrenador);
                insert.Parameters.AddWithValue("@idlog", idLogCombate);
                insert.ExecuteNonQuery();
            }
            idBase = idLogCombate + "ri";
            int hola = 0;
            for (int i = 0; i < entrenadorRival.equipo.Length; i++)
            {
                entrenadorRival.equipo[i].idLog = idBase + i;
                sql = "insert into log_combate_equipo (id, FK_ID_ALMACENAMIENTO, FK_ID_ENTRENADOR, FK_ID_LOGCOMBATE) values (@id, @idal, @iden, @idlog)";
                insert = new OleDbCommand(sql, con);
                insert.Parameters.AddWithValue("@id", entrenadorRival.equipo[i].idLog);
                insert.Parameters.AddWithValue("@idal", entrenadorRival.equipo[i].numAlmacenamiento);
                insert.Parameters.AddWithValue("@iden", hola);
                insert.Parameters.AddWithValue("@idlog", idLogCombate);
                insert.ExecuteNonQuery();
            }
            con.Close();
            #endregion

            numTurnos = ticks = 0;
            menuBloqueado = true;
            random = new Random((int)DateTime.Now.Ticks);
            tm = new TextoMostrar(labelTexto);

            this.entrenadorTu = entrenadorTu;
            this.entrenadorRival = entrenadorRival;
            originalSize = panelUp.Size;

            #region Rectangles
            recPanelCombate = panelCombate.Bounds;
            recPicBoxPokemonBack = picBoxPkmnBack.Bounds;
            recPicBoxPokemonFront = picBoxPkmnFront.Bounds;
            recMarcoCombate = marcoCombate.Bounds;
            recPanelInfoJugador = panelInfoTu.Bounds;
            recPanelInfoRival = panelInfoRival.Bounds;
            recLabelNombrePkmnRival = labelRivalNombre.Bounds;
            recLabelNombrePkmnTu = labelNombreTu.Bounds;
            recLabelNvlRival = labelNivelRival.Bounds;
            recLabelNvlTu = labelNivelTu.Bounds;
            recLabelPvTu = labelPvTu.Bounds;
            recVidaRojaRival = vidaRojaRival.Bounds;
            recVidaVerdeRival = vidaVerdeRival.Bounds;
            recVidaRojaTu = vidaRojaTu.Bounds;
            recVidaVerdeTu = vidaVerdeTu.Bounds;
            recEstadoRival = picBoxEstadoRival.Bounds;
            recEstadoTu = picBoxEstadoTu.Bounds;
            #endregion

            //Indicadores pokemon restantes
            indicadorRestantesTu = new UC_PokemonRestantes(this.entrenadorTu);
            indicadorRestantesRival = new UC_PokemonRestantes(this.entrenadorRival);
            panelIndicadorTu.Controls.Add(indicadorRestantesTu);
            panelIndicadorRival.Controls.Add(indicadorRestantesRival);
            indicadorRestantesTu.ActualizarIndicador();
            indicadorRestantesRival.ActualizarIndicador();

            //Musica y fondo random
            CambiarMusicaToolStripMenuItem_Click(this, null);
            GenerarFondoCombate(this, null);

            //Imagen entrenador back
            picBoxPkmnBack.Image = entrenadorTu.imageBack;
            recPicBoxPokemonBack.Size = new Size((int)(entrenadorTu.imageBack.Width * 1.5), (int)(entrenadorTu.imageBack.Height * 1.5));
            recPicBoxPokemonBack.Location = new Point(100 - recPicBoxPokemonBack.Width / 2, 243 - recPicBoxPokemonBack.Height);
            ResizeControl(recPicBoxPokemonBack, picBoxPkmnBack);
            //Imagen entrenador front
            picBoxPkmnFront.Image = entrenadorRival.imageFront;
            recPicBoxPokemonFront.Size = new Size((int)(entrenadorRival.imageFront.Width * 1.5), (int)(entrenadorRival.imageFront.Height * 1.5));
            recPicBoxPokemonFront.Location = new Point(330 - recPicBoxPokemonFront.Width / 2, 149 - recPicBoxPokemonFront.Height);
            ResizeControl(recPicBoxPokemonFront, picBoxPkmnFront);

            timerAnimacionInicial.Enabled = true;
            TimerAnimacionInicial_Tick(null, null);
        }

        #endregion

        #region Botones Clicks

        private void BtnBolsa_Click(object sender, EventArgs e)
        {
            if (menuBloqueado == true)
                return;

            panelBolsa.Controls.Clear();
            for (int i = 0; i < entrenadorTu.objetos.Count; i++)
                panelBolsa.Controls.Add(new Visualizador_Objeto(entrenadorTu.objetos[i], this));

            btnBolsa.Visible = btnHuir.Visible = btnCambiarPokemon.Visible = false;
            btnCancelar.Visible = panelBag.Visible = true;
        }

        private void BtnLuchar_Click(object sender, EventArgs e)
        {
            if (menuBloqueado == true)
                return;

            UC_BotonAtaque m1, m2, m3, m4;

            List<Ataque> listaAtaques = new List<Ataque>();
            if (pokemonBack.mov1 != null && pokemonBack.mov1.ppActuales > 0 && pokemonBack.mov1.turnosAnulado <= 0)
                listaAtaques.Add(pokemonBack.mov1);
            if (pokemonBack.mov2 != null && pokemonBack.mov2.ppActuales > 0 && pokemonBack.mov2.turnosAnulado <= 0)
                listaAtaques.Add(pokemonBack.mov2);
            if (pokemonBack.mov3 != null && pokemonBack.mov3.ppActuales > 0 && pokemonBack.mov3.turnosAnulado <= 0)
                listaAtaques.Add(pokemonBack.mov3);
            if (pokemonBack.mov4 != null && pokemonBack.mov4.ppActuales > 0 && pokemonBack.mov4.turnosAnulado <= 0)
                listaAtaques.Add(pokemonBack.mov4);

            if (listaAtaques.Count() == 0)
            {
                m1 = new UC_BotonAtaque(new Ataque(0), this);
                m2 = new UC_BotonAtaque(new Ataque(0), this);
                m3 = new UC_BotonAtaque(new Ataque(0), this);
                m4 = new UC_BotonAtaque(new Ataque(0), this);
            }
            else
            {
                m1 = new UC_BotonAtaque(pokemonBack.mov1, this);
                m2 = new UC_BotonAtaque(pokemonBack.mov2, this);
                m3 = new UC_BotonAtaque(pokemonBack.mov3, this);
                m4 = new UC_BotonAtaque(pokemonBack.mov4, this);
            }

            tm.MostrarTexto("Qué hará " + pokemonBack.nombre + "?");

            panelMov1.Controls.Clear();
            panelMov1.Controls.Add(m1);
            panelMov2.Controls.Clear();
            panelMov2.Controls.Add(m2);
            panelMov3.Controls.Clear();
            panelMov3.Controls.Add(m3);
            panelMov4.Controls.Clear();
            panelMov4.Controls.Add(m4);

            btnBolsa.Visible = btnHuir.Visible = btnCambiarPokemon.Visible = btnLuchar.Visible = false;
            btnCancelar.Visible = panelMov1.Visible = panelMov2.Visible = panelMov3.Visible = panelMov4.Visible = true;
        }

        public void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (pokemonBack.estadisticasActuales.debilitado)
                return;

            tm.MostrarTexto(" ");
            panelTexto.Visible = btnBolsa.Visible = btnHuir.Visible = btnCambiarPokemon.Visible = btnLuchar.Visible = true;
            panelCambioDePokemons.Visible = btnCancelar.Visible = panelMov1.Visible = panelMov2.Visible = panelMov3.Visible = panelMov4.Visible = panelBag.Visible = false;
        }

        private void BtnHuir_Click(object sender, EventArgs e)
        {
            tm.MostrarTexto("Escapaste sin problemas.");
            mediaPlayer.Ctlcontrols.pause();
            ticks = 0;
            timerAnimacionFinal.Enabled = rendido = menuBloqueado = true;
        }

        private void BtnCambiarPokemon_Click(object sender, EventArgs e)
        {
            if (menuBloqueado == true)
                return;

            panelPkmnChange1.Controls.Clear();
            panelPkmnChange1.Controls.Add(new UC_BotonPokemonCambio(entrenadorTu.equipo[0], this));
            panelPkmnChange2.Controls.Clear();
            panelPkmnChange2.Controls.Add(new UC_BotonPokemonCambio(entrenadorTu.equipo[1], this));
            panelPkmnChange3.Controls.Clear();
            panelPkmnChange3.Controls.Add(new UC_BotonPokemonCambio(entrenadorTu.equipo[2], this));
            panelPkmnChange4.Controls.Clear();
            panelPkmnChange4.Controls.Add(new UC_BotonPokemonCambio(entrenadorTu.equipo[3], this));
            panelPkmnChange5.Controls.Clear();
            panelPkmnChange5.Controls.Add(new UC_BotonPokemonCambio(entrenadorTu.equipo[4], this));
            panelPkmnChange6.Controls.Clear();
            panelPkmnChange6.Controls.Add(new UC_BotonPokemonCambio(entrenadorTu.equipo[5], this));

            btnBolsa.Visible = btnHuir.Visible = btnCambiarPokemon.Visible = panelTexto.Visible = false;
            btnCancelar.Visible = panelCambioDePokemons.Visible = true;
        }

        private void TablaTiposMenu_Click(object sender, EventArgs e)
        {
            new Form_TablaTipos().Show();
        }

        private void CambiarMusicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] canciones = { "CyntiaBattle", "RojoBattle", "PalkiaDialga", "AzelfUxieMesprit", "TrainerCombat", "Suicune" };
            mediaPlayer.URL = "Sonido\\Musica\\" + canciones[random.Next(0, canciones.Length)] + ".wav";
        }

        #endregion

        #region Timers

        private void TimerAnimacionFinal_Tick(object sender, EventArgs e)
        {
            switch (ticks)
            {
                case 0:
                    //Imagen entrenador back
                    picBoxPkmnBack.Image = entrenadorTu.imageBack;
                    recPicBoxPokemonBack.Size = new Size((int)(entrenadorTu.imageBack.Width * 1.5), (int)(entrenadorTu.imageBack.Height * 1.5));
                    recPicBoxPokemonBack.Location = new Point(100 - recPicBoxPokemonBack.Width / 2, 243 - recPicBoxPokemonBack.Height);
                    ResizeControl(recPicBoxPokemonBack, picBoxPkmnBack);
                    //Imagen entrenador front
                    picBoxPkmnFront.Image = entrenadorRival.imageFront;
                    recPicBoxPokemonFront.Size = new Size((int)(entrenadorRival.imageFront.Width * 1.5), (int)(entrenadorRival.imageFront.Height * 1.5));
                    recPicBoxPokemonFront.Location = new Point(330 - recPicBoxPokemonFront.Width / 2, 149 - recPicBoxPokemonFront.Height);
                    ResizeControl(recPicBoxPokemonFront, picBoxPkmnFront);

                    panelInfoRival.Visible = panelInfoTu.Visible = false;
                    menuBloqueado = true;

                    tm.MostrarTexto("El combate entre " + entrenadorTu.nombre + " y " + entrenadorRival.nombre + " ha terminado.");
                    break;
                case 1:
                    string sql;
                    OleDbConnection con = ConexionAccess.GetConexion();
                    con.Open();
                    OleDbCommand update;

                    if (RivalSacaPokemon() == null && !rendido)
                    {
                        nombreGanador = entrenadorTu.nombre;
                        new Thread(() => { new SoundPlayer("Sonido\\Hit\\victoria.wav").PlaySync(); }).Start();

                        //Insertar datos base datos
                        sql = "UPDATE entrenador SET victorias=@vic WHERE id=@id";
                        update = new OleDbCommand(sql, con);
                        update.Parameters.AddWithValue("@vic", ++entrenadorTu.numVictorias);
                        update.Parameters.AddWithValue("@id", entrenadorTu.numEntrenador);
                        update.ExecuteNonQuery();
                    }
                    else
                        nombreGanador = entrenadorRival.nombre;
                    tm.MostrarTexto(nombreGanador + " ha ganado.");

                    //Insertar datos base datos
                    sql = "UPDATE log_combate SET estado=@est, ganador=@gan WHERE id=@id";
                    update = new OleDbCommand(sql, con);
                    update.Parameters.AddWithValue("@est", "Finalizado");
                    update.Parameters.AddWithValue("@gan", nombreGanador);
                    update.Parameters.AddWithValue("@id", idLogCombate);
                    update.ExecuteNonQuery();
                    con.Close();
                    break;
                case 2:
                    if (formRogueLike != null)
                        formRogueLike.CombateFinalizado(nombreGanador == entrenadorTu.nombre);
                    break;
                default:
                    //Matamos hilos
                    server.Stop();
                    server.Server.Close();
                    endEnvio = true;
                    inicio.Visible = true;
                    this.Close();
                    this.Dispose();
                    break;
            }
            ticks++;
        }

        private void TimerAnimacionInicial_Tick(object sender, EventArgs e)
        {
            switch (ticks)
            {
                case 0:
                    tm.MostrarTexto("El combate entre " + entrenadorTu.nombre + " y " + entrenadorRival.nombre + " va a comenzar.");
                    break;
                case 1:
                    tm.MostrarTexto(entrenadorTu.nombre + ": Adelante " + entrenadorTu.equipo[0].nombre + ".");
                    break;
                case 2:
                    CambiarPokemon(entrenadorTu.equipo[0], "Back");
                    panelInfoTu.Visible = true;
                    break;
                case 3:
                    tm.MostrarTexto(entrenadorRival.nombre + ": Adelante " + entrenadorRival.equipo[0].nombre + ".");
                    break;
                case 4:
                    CambiarPokemon(entrenadorRival.equipo[0], "Front");
                    panelInfoRival.Visible = true;
                    break;
                default:
                    tm.MostrarTexto("");
                    menuBloqueado = false;
                    timerAnimacionInicial.Enabled = false;
                    //Si es multiplayer enviamos datos combate
                    new Thread(() =>
                    {
                        while (!endEnvio)
                        {
                            if (multiplayer)
                            {
                                XML.CrearXMLDatosCombate(pokemonBack, pokemonFront);
                                TCP.EnviarArchivoTCP(XML.RUTA_FICHERO_DATOS_POKEMON, ipMovil);
                                Thread.Sleep(5000);
                            }
                        }
                    }).Start();
                    break;
            }
            ticks++;
        }

        #endregion

        #region Metodos

        public void CambiarPokemon(Pokemon pokemon, string posicion)
        {
            string mensaje = "";
            pokemon.OnCambiarPokemon();

            if (posicion == "Front")
            {
                if (pokemonFront != null)
                    mensaje += "¡" + pokemonFront.nombre + ", cambio!\n";
                pokemonFront = pokemon;
                Image imgPkmnFront = pokemonFront.imagenFront;
                picBoxPkmnFront.Image = imgPkmnFront;
                recPicBoxPokemonFront.Size = imgPkmnFront.Size;
                recPicBoxPokemonFront.Location = new Point(330 - recPicBoxPokemonFront.Width / 2, 149 - recPicBoxPokemonFront.Height);
                ResizeControl(recPicBoxPokemonFront, picBoxPkmnFront);

                new AnimacionEntrada(picBoxPkmnFront, recPicBoxPokemonFront, "Front", this);
                vidaRival = new BarraDeVida("Front", pokemonFront, this);
                vidaRival.SetVida(pokemonFront.vidaActual);
                labelNivelRival.Text = "Nv" + pokemonFront.nivel;
                labelRivalNombre.Text = pokemonFront.nombre;
                mensaje += "¡Adelante, " + pokemonFront.nombre + "!";
                picBoxEstadoRival.BackgroundImage = Image.FromFile(@"Img\Estado\" + (int)pokemon.estadisticasActuales.estadoActual + ".png");
            }
            else
            {
                if (pokemonBack != null)
                    mensaje += "¡" + pokemonBack.nombre + ", cambio!\n";
                pokemonBack = pokemon;
                Image imgPkmnBack = pokemonBack.imagenBack;
                picBoxPkmnBack.Image = imgPkmnBack;
                recPicBoxPokemonBack.Size = imgPkmnBack.Size;
                recPicBoxPokemonBack.Location = new Point(100 - recPicBoxPokemonBack.Width / 2, 243 - recPicBoxPokemonBack.Height);
                ResizeControl(recPicBoxPokemonBack, picBoxPkmnBack);

                new AnimacionEntrada(picBoxPkmnBack, recPicBoxPokemonBack, "Back", this);
                vidaTu = new BarraDeVida("Back", pokemonBack, this);
                vidaTu.SetVida(pokemonBack.vidaActual);
                labelNivelTu.Text = "Nv" + pokemonBack.nivel;
                labelNombreTu.Text = pokemonBack.nombre;
                labelPvTu.Text = "Pv " + pokemonBack.vidaActual + "/" + pokemonBack.vidaMax;
                if (ticks > 4)
                    mensaje += "¡Adelante, " + pokemonBack.nombre + "!";

                picBoxEstadoTu.BackgroundImage = Image.FromFile(@"Img\Estado\" + (int)pokemon.estadisticasActuales.estadoActual + ".png");
                tm.MostrarTexto(mensaje);
            }


        }

        public void GenerarFondoCombate(object sender, EventArgs e)
        {
            string[] fondos = { "Arena.png", "Otogno.jpg", "Cementerio.png", "Ciudad.png", "CiudadNoche.png", "Escaleras.png",
                "Espacio.png", "GimnasioAzul.png", "GimnasioHielo.png", "GimnasioMarron.png", "GimnasioMorado.png", "Montagna.png",
                "Montagna2.png", "Nieve.png", "ParqueNoche.jpg", "Playa.png", "Prado.jpg", "Templo.png", "Vidriera.png", "Casa.png",
                "Mar.png", "Prado2.png", "Bosque.png", "Ruta.png", "Prado3.png", "Granja.png", "Atardecer.png", "Playa2.png",
                "Playa3.png", "PradoNoche.png", "Mar2.png", "Cueva.png", "Cueva2.png", "CuevaD.png", "Volcan.png", "Desierto.jpg",
                "Amapolas.png", "Castillo.png", "Subida.png" };
            string fondoElegido = fondos[random.Next(0, fondos.Length)];
            string salida = "Img/Fondos/fondo" + fondoElegido;
            panelCombate.BackgroundImage = Image.FromFile(@salida);
        }

        public void EmpezarTurno()
        {
            menuBloqueado = true;
            //Rival
            if (pokemonFront.estadisticasActuales.usandoFuria)
                accionRealizadaPorFront = new Accion(new Ataque(79));
            else if (pokemonFront.estadisticasActuales.movLanzadoTrasEspera != null && pokemonFront.estadisticasActuales.movLanzadoTrasEspera.ppActuales > 0)
                accionRealizadaPorFront = new Accion(pokemonFront.estadisticasActuales.movLanzadoTrasEspera);
            else if (pokemonFront.estadisticasActuales.movQueContinua != null && pokemonFront.estadisticasActuales.turnosContinuarMovimiento > 0)
                accionRealizadaPorFront = new Accion(pokemonFront.estadisticasActuales.movQueContinua);
            else
                accionRealizadaPorFront = new Accion(SeleccionarAtaqueRival(pokemonFront));

            #region Log Consola
            //Log
            Console.WriteLine("\n********** Comienzo del turno " + (++numTurnos) + " **********\n");
            pokemonBack.MostrarDatos();
            Console.WriteLine("-Acción usada: " + accionRealizadaPorBack.Mostrar());
            Console.WriteLine("----------  FIN  ----------\n");
            pokemonFront.MostrarDatos();
            Console.WriteLine("-Acción usada: " + accionRealizadaPorFront.Mostrar());
            Console.WriteLine("----------  FIN  ----------\n");
            #endregion

            //Reseteamos eleccion usuario
            if (multiplayer)
                ataqueSeleccionadoMultiplayer = 0;

            new Turno(this);
        }

        public Ataque SeleccionarAtaqueRival(Pokemon pokemon)
        {
            //Si es multijugador se recoge el dato del ataque elegido
            if (multiplayer)
            {
                int timeout = 0;
                while (true)
                {
                    timeout++;
                    switch (ataqueSeleccionadoMultiplayer)
                    {
                        case 1:
                            return pokemon.mov1;
                        case 2:
                            return pokemon.mov2;
                        case 3:
                            return pokemon.mov3;
                        case 4:
                            return pokemon.mov4;
                        default:
                            Thread.Sleep(3000);
                            break;
                    }
                    if (timeout > 8)
                        return new Ataque(0);
                }
            }
            //Si no es multijugador se devuelve un ataque aleatorio
            else
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

                if (listaAtaques.Count() == 0)
                    return new Ataque(0);

                int ran = random.Next(0, listaAtaques.Count());
                return listaAtaques[ran];
            }
        }

        public void TerminarTurno()
        {
            string txtMostrar = "";
            StatsFinTurno(pokemonBack);
            StatsFinTurno(pokemonFront);

            indicadorRestantesTu.ActualizarIndicador();
            indicadorRestantesRival.ActualizarIndicador();

            //Si se debilita el rival
            if (pokemonFront.estadisticasActuales.debilitado)
            {
                txtMostrar += pokemonFront.nombre + " se ha debilitado.\n";
                Pokemon aSacar = RivalSacaPokemon();
                if (aSacar == null)
                {
                    menuBloqueado = true;
                    ticks = 0;
                    timerAnimacionFinal.Enabled = true;
                    return;
                }
                else
                {
                    txtMostrar += entrenadorRival.nombre + " saca a " + aSacar.nombre + ".\n";
                    CambiarPokemon(aSacar, "Front");
                }
            }
            tm.MostrarTexto(txtMostrar);

            menuBloqueado = false;

            //Si te debilitan tu pokemon
            if (pokemonBack.estadisticasActuales.debilitado == true)
                ForzarCambioTu();
            else
            {
                //Empezamos turno si es un mov que continua
                if (pokemonBack.estadisticasActuales.movQueContinua != null && pokemonBack.estadisticasActuales.turnosContinuarMovimiento > 0 && pokemonBack.estadisticasActuales.movQueContinua.ppActuales > 0)
                {
                    accionRealizadaPorBack = new Accion(pokemonBack.estadisticasActuales.movQueContinua);
                    EmpezarTurno();
                }

                //Empezamos turno si es un mov de varios turnos
                if (pokemonBack.estadisticasActuales.movLanzadoTrasEspera != null)
                {
                    accionRealizadaPorBack = new Accion(pokemonBack.estadisticasActuales.movLanzadoTrasEspera);
                    EmpezarTurno();
                }

                //Empezamos turno si furia
                if (pokemonBack.estadisticasActuales.usandoFuria)
                {
                    accionRealizadaPorBack = new Accion(new Ataque(79));
                    EmpezarTurno();
                }
            }
            Console.WriteLine("\n********** Fin del turno " + numTurnos + " **********\n");
            pokemonBack.MostrarDatos();
            Console.WriteLine("----------  FIN  ----------\n");
            pokemonFront.MostrarDatos();
            Console.WriteLine("----------  FIN  ----------\n");

            //Creamos los datos para enviar al otro jugador
            if (multiplayer)
                EnviarDatosMultijugador();
        }

        private void EnviarDatosMultijugador()
        {
            //Creamos los datos para enviar al otro jugador
            XML.CrearXMLDatosCombate(pokemonBack, pokemonFront);
            TCP.EnviarArchivoTCP(XML.RUTA_FICHERO_DATOS_POKEMON, ipMovil);
        }

        public void ForzarCambioTu()
        {
            bool pierdes = true;
            for (int i = 0; i < entrenadorTu.equipo.Length; i++)
                if (entrenadorTu.equipo[i].estadisticasActuales.debilitado == false)
                    pierdes = false;

            if (pierdes)
            {
                menuBloqueado = true;
                ticks = 0;
                timerAnimacionFinal.Enabled = true;
            }
            else
                BtnCambiarPokemon_Click(null, null);
        }

        public Pokemon RivalSacaPokemon()
        {
            for (int i = 0; i < entrenadorRival.equipo.Length; i++)
                if (entrenadorRival.equipo[i].estadisticasActuales.debilitado == false)
                    return entrenadorRival.equipo[i];

            return null;
        }

        public void StatsFinTurno(Pokemon pokemon)
        {
            pokemon.estadisticasActuales.haRetrocedido = false;
            pokemon.estadisticasActuales.ultimoAtaqueRecibido = null;
            pokemon.estadisticasActuales.dagnoUltimoGolpeRecibido = 0;

            //Turnos
            if (pokemon.estadisticasActuales.turnosInmovilizado > 0)
                pokemon.estadisticasActuales.turnosInmovilizado--;
            if (pokemon.estadisticasActuales.turnosEspera > 0)
                pokemon.estadisticasActuales.turnosEspera--;
            if (pokemon.estadisticasActuales.turnosDormido > -1)
                pokemon.estadisticasActuales.turnosDormido--;
            if (pokemon.estadisticasActuales.turnosContinuarMovimiento > 0)
                pokemon.estadisticasActuales.turnosContinuarMovimiento--;

            if (pokemon.estadisticasActuales.turnosContinuarMovimiento <= 0)
                pokemon.estadisticasActuales.movQueContinua = null;

            //Anulacion
            if (pokemon.mov1 != null && pokemon.mov1.turnosAnulado > 0)
                pokemon.mov1.turnosAnulado--;
            if (pokemon.mov2 != null && pokemon.mov2.turnosAnulado > 0)
                pokemon.mov2.turnosAnulado--;
            if (pokemon.mov3 != null && pokemon.mov3.turnosAnulado > 0)
                pokemon.mov3.turnosAnulado--;
            if (pokemon.mov4 != null && pokemon.mov4.turnosAnulado > 0)
                pokemon.mov4.turnosAnulado--;
        }

        #endregion

        #region MUSTAFA Resize

        public void ResizeControl(Rectangle originalControl, Control control)
        {
            ResizeControl(originalControl, control, 1);
        }

        public void ResizeControl(Rectangle originalControl, Control control, int originalFont)
        {
            float xRatio = (float)(panelUp.Width) / (float)(originalSize.Width);
            float yRatio = (float)(panelUp.Height) / (float)(originalSize.Height);

            int newX = (int)(originalControl.Location.X * xRatio);
            int newY = (int)(originalControl.Location.Y * yRatio);
            int newWidth = (int)(originalControl.Size.Width * xRatio);
            int newHeight = (int)(originalControl.Size.Height * yRatio);

            control.Size = new Size(newWidth, newHeight);
            control.Location = new Point(newX, newY);

            control.Font = new Font(control.Font.FontFamily, originalFont * yRatio, control.Font.Style);
        }

        private void Combate_Resize(object sender, EventArgs e)
        {
            ResizeControl(recPanelCombate, panelCombate);
            ResizeControl(recPicBoxPokemonBack, picBoxPkmnBack);
            ResizeControl(recPicBoxPokemonFront, picBoxPkmnFront);
            ResizeControl(recMarcoCombate, marcoCombate);
            ResizeControl(recPanelInfoRival, panelInfoRival);
            ResizeControl(recPanelInfoJugador, panelInfoTu);
            ResizeControl(recPanelInfoRival, panelInfoRival);
            ResizeControl(recLabelNvlTu, labelNivelTu, 8);
            ResizeControl(recLabelNvlRival, labelNivelRival, 8);
            ResizeControl(recLabelNombrePkmnRival, labelRivalNombre, 9);
            ResizeControl(recLabelNombrePkmnTu, labelNombreTu, 9);
            ResizeControl(recLabelPvTu, labelPvTu, 8);
            ResizeControl(recVidaRojaTu, vidaRojaTu);
            ResizeControl(recVidaVerdeTu, vidaVerdeTu);
            ResizeControl(recVidaRojaRival, vidaRojaRival);
            ResizeControl(recVidaVerdeRival, vidaVerdeRival);
            ResizeControl(recEstadoTu, picBoxEstadoTu);
            ResizeControl(recEstadoRival, picBoxEstadoRival);
        }

        #endregion

        # region ButtonHovers
        private void BtnCancelar_MouseDown(object sender, MouseEventArgs e)
        {
            btnCancelar.BackgroundImage = Image.FromFile(@"Img\Botones\BtnCancelarHover.png");
        }

        private void BtnCancelar_MouseUp(object sender, MouseEventArgs e)
        {
            btnCancelar.BackgroundImage = Image.FromFile(@"Img\Botones\BtnCancelar.png");
        }

        private void BtnHuir_MouseUp(object sender, MouseEventArgs e)
        {
            btnHuir.BackgroundImage = Image.FromFile(@"Img\Botones\BtnHuir.png");
        }

        private void BtnHuir_MouseDown(object sender, MouseEventArgs e)
        {
            btnHuir.BackgroundImage = Image.FromFile(@"Img\Botones\BtnHuirHover.png");
        }

        private void BtnCambiarPokemon_MouseDown(object sender, MouseEventArgs e)
        {
            btnCambiarPokemon.BackgroundImage = Image.FromFile(@"Img\Botones\BtnCambioHover.png");
        }

        private void BtnCambiarPokemon_MouseUp(object sender, MouseEventArgs e)
        {
            btnCambiarPokemon.BackgroundImage = Image.FromFile(@"Img\Botones\BtnCambio.png");
        }

        private void BtnBolsa_MouseUp(object sender, MouseEventArgs e)
        {
            btnBolsa.BackgroundImage = Image.FromFile(@"Img\Botones\BtnBolsa.png");
        }

        private void BtnBolsa_MouseDown(object sender, MouseEventArgs e)
        {
            btnBolsa.BackgroundImage = Image.FromFile(@"Img\Botones\BtnBolsaHover.png");
        }

        private void BtnLuchar_MouseDown(object sender, MouseEventArgs e)
        {
            btnLuchar.BackgroundImage = Image.FromFile(@"Img\Botones\BtnFightHover.png");
        }

        private void BtnLuchar_MouseUp(object sender, MouseEventArgs e)
        {
            btnLuchar.BackgroundImage = Image.FromFile(@"Img\Botones\BtnFight.png");
        }
        #endregion

    }
}