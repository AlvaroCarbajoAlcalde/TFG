using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Pokemon
{
    public static class TCP
    {

        #region Variables

        private static readonly int TAM_BUFFER = 1024;
        public static readonly int PUERTO_RECIBIR = 6060;
        private static readonly int PUERTO_ENVIAR = 6061;
        private static readonly int PUERTO_ENVIAR_MENSAJE = 6062;

        #endregion

        #region Cliente

        public static void EnviarArchivoTCP(string rutaArchivo, string ipMovil)
        {
            try
            {
                byte[] sendingBuffer;

                //Abrimos el cliente
                TcpClient client = new TcpClient(ipMovil, PUERTO_ENVIAR);
                NetworkStream netStream = client.GetStream();

                //Abrimkos el archivo a enviar
                FileStream fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);

                //Vamos leyeendo el archivo para enviarlo
                int numPaquetes = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(fileStream.Length) / Convert.ToDouble(TAM_BUFFER)));
                int tamagnoTotal = (int)fileStream.Length, tamPaquete;
                for (int i = 0; i < numPaquetes; i++)
                {
                    if (tamagnoTotal > TAM_BUFFER)
                    {
                        tamPaquete = TAM_BUFFER;
                        tamagnoTotal -= tamPaquete;
                    }
                    else
                        tamPaquete = tamagnoTotal;

                    sendingBuffer = new byte[tamPaquete];
                    fileStream.Read(sendingBuffer, 0, tamPaquete);

                    //Enviamos datos leidos
                    netStream.Write(sendingBuffer, 0, sendingBuffer.Length);
                }

                //Cerramos todo 
                fileStream.Close();
                netStream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR TCP 1: {ex.Message}");
            }
        }

        public static void EnviarMensajeTCP(string mensaje, string ipMovil)
        {
            try
            {
                byte[] sendingBuffer;

                //Abrimos el cliente
                TcpClient client = new TcpClient(ipMovil, PUERTO_ENVIAR_MENSAJE);
                NetworkStream netStream = client.GetStream();

                //Enviamos el mensaje
                sendingBuffer = Encoding.UTF8.GetBytes(mensaje);
                netStream.Write(sendingBuffer, 0, sendingBuffer.Length);

                netStream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR TCP 2: {ex.Message}");
            }
        }

        #endregion

        #region Servidor

        public static void Servidor(Form_Combate form_Combate, TcpListener server)
        {
            try
            {
                server.Start();

                //Buffer
                byte[] bytes = new byte[256];
                string data = null;

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;

                    //Recibir datos
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.UTF8.GetString(bytes, 0, i);
                        data = data.ToUpper();

                        byte[] msg = Encoding.UTF8.GetBytes(data);

                        //Respuesta.
                        stream.Write(msg, 0, msg.Length);

                        //Seleccionamos el tipo de accion dependiendo del dato recibido
                        form_Combate.multiplayer = true;

                        //Al mandar START se guarda la IP
                        if (data.ToUpper().Contains("START"))
                        {
                            //Para quitar la basura que envia detras del dato que me interesa obtener.
                            char[] values = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                            string ipMovil = data.ToUpper().Substring("START".Length);
                            ipMovil = ipMovil.Substring(0, ipMovil.LastIndexOfAny(values) + 1);
                            form_Combate.ipMovil = ipMovil;
                            Console.WriteLine($"IP DEL MOVIL CONECTADO::::   {form_Combate.ipMovil}");
                        }

                        //Ataque
                        if (int.TryParse(data.Substring(0, 1), out int ataque))
                            form_Combate.ataqueSeleccionadoMultiplayer = ataque;

                        //Salir multijugador
                        if (data.ToUpper().Contains("STOP"))
                            form_Combate.multiplayer = false;
                    }

                    //Cerrar cliente
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion Servidor: {0}", e);
            }
        }

        #endregion

    }
}
