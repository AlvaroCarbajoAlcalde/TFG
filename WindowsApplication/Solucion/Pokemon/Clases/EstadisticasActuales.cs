namespace Pokemon
{
    public class EstadisticasActuales
    {

        #region Propiedades

        //Flag de debilitado no cambia al cambiar pokemon.
        public bool debilitado; //Si el pokemon esta muerto.

        //Stats no varian hasta ser cambiado el pokemon.
        public int ataqueActual; //Ataque actual del pokemon.
        public int defensaActual; //Defensa actual del pokemon.
        public int especialActual; //Especial actual del pokemon.
        public int velocidadActual; //Velocidad actual del pokemon.

        //Modificadores stats no varian hasta ser cambiado el pokemon.
        public int modificadorAtaque; //[-6, 6] Empieza en 0.
        public int modificadorDefensa; //[-6, 6] Empieza en 0.
        public int modificadorEspecial; //[-6, 6] Empieza en 0.
        public int modificadorVelocidad; //[-6, 6] Empieza en 0.
        public int modificadorPrecision; //[-6, 6] Empieza en 0.
        public int modificadorEvasion; //[-6, 6] Empieza en 0.
        public int modificadorCritico; //[0, 4] Empieza en 0.

        //Flags Han cambiado estadistica Para mostrar por pantalla tras el movimiento lo que le haya sucedido al pokemon.
        //Se resetean al final del turno. 
        public int haCambiadoAtaque; //[-2, 2]
        public int haCambiadoDefensa; //[-2, 2] 
        public int haCambiadoEspecial; //[-2, 2] 
        public int haCambiadoVelocidad; //[-2, 2] 
        public int haCambiadoPrecision; //[-2, 2] 
        public int haCambiadoEvasion; //[-2, 2] 
        public int haCambiadoCritico; //[-1, 1] 

        //Flags se resetean al cambiar el pokemon.
        public int turnosInmovilizado; //Num turnos que permanece inmobilizado 0 si ninguno.
        public bool haRetrocedido; //Si te hacen retroceder (solo durante el mismo turno).

        public int turnosContinuarMovimiento; //num turnos que le quedan al movimiento para acabar.
        public Ataque movQueContinua; //Movimiento que se realizara continuadamente cada turno.

        public int turnosEspera; //Num turnos que espera hasta lanzar el siguiente movimiento;
        public Ataque movLanzadoTrasEspera; //Mov que se activará cuando acabe la espera

        public bool evitarCambiosEstadistica; //Solo movimientos de estado.

        public int dagnoUltimoGolpeRecibido; //Cantidad de daño recibido por ultima vez.
        public Ataque ultimoAtaqueRecibido; //Ultimo golpe que se ha recibido.

        public int vecesTransformado; //Para ttransformacion
        public bool pantallaLuz; //Para pantalla luz
        public bool reflejo; //Para reflejo
        public bool usandoFuria; //Para furia

        public Estado estadoActual; //Estado actual del pokemon con un enum porque solo puede tener 1.
        public int turnosDormido; //Num de turnos que le quedan de estar dormido.
        public int turnosGravementeEnvenenado; //Num turnos que esta gravemente envenenado (para calcular el daño).

        //SubEstado
        public bool drenadoras; //El pokemon sufre drenadoras.
        public bool volando; //El pokemon esta volando.
        public bool escavando; //El pokemon esta bajo tierra.
        public int confuso; //Numero de turnos de confusion [-1, 4]

        #endregion

        #region Constructor

        //Constructor
        public EstadisticasActuales(Pokemon pokemon)
        {
            //Debilitado
            debilitado = false;

            //Stats del pokemon iguales a los del pokemon al iniciar el combate
            ataqueActual = pokemon.ataque;
            defensaActual = pokemon.defensa;
            especialActual = pokemon.especial;
            velocidadActual = pokemon.velocidad;

            //Modificadores a 0
            modificadorAtaque = modificadorDefensa = modificadorEspecial = modificadorVelocidad
                = modificadorCritico = modificadorEvasion = modificadorPrecision = 0;

            //Flags estadistica cambiada a 0
            haCambiadoAtaque = haCambiadoDefensa = haCambiadoEspecial = haCambiadoEvasion =
                haCambiadoPrecision = haCambiadoVelocidad = haCambiadoCritico = 0;

            //Flags
            turnosInmovilizado = turnosEspera = turnosContinuarMovimiento = dagnoUltimoGolpeRecibido = 0;
            ultimoAtaqueRecibido = movLanzadoTrasEspera = movQueContinua = null;
            haRetrocedido = evitarCambiosEstadistica = false;
            vecesTransformado = 0;
            pantallaLuz = false;
            reflejo = false;

            //Estado a sin estado
            estadoActual = Estado.NINGUNO;
            turnosDormido = -1; //Turnos dormido a -1 para no mandar mensaje de se ha despertado
            turnosGravementeEnvenenado = 0; //Turnos gravemente envenenado a 0

            //Sub Estados
            drenadoras = volando = escavando = false;
            confuso = -1; //Turnos confuso a -1 para no mandar mensaje de ya no esta confuso
        }

        #endregion

    }
}
