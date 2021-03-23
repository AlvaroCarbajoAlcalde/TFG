using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Pokemon.TFG
{
    public static class TCP
    {

        #region Variables

        private static readonly int TAM_BUFFER = 1024;
        public static readonly int PUERTO_RECIBIR = 6060;
        private static readonly int PUERTO_ENVIAR = 6061;
        private static readonly int PUERTO_ENVIAR_MENSAJE = 6062;
        private static readonly string IP_MOVIL = "192.168.1.128";

        #endregion

        #region Cliente

        public static void EnviarArchivoTCP(string rutaArchivo)
        {
            try
            {
                byte[] sendingBuffer;

                //Abrimos el cliente
                TcpClient client = new TcpClient(IP_MOVIL, PUERTO_ENVIAR);
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
                Console.WriteLine("ERROR TCP: " + ex.Message);
            }
        }

        public static void EnviarMensajeTCP(string mensaje)
        {
            try
            {
                byte[] sendingBuffer;

                //Abrimos el cliente
                TcpClient client = new TcpClient(IP_MOVIL, PUERTO_ENVIAR_MENSAJE);
                NetworkStream netStream = client.GetStream();

                //Enviamos el mensaje
                sendingBuffer = Encoding.UTF8.GetBytes(mensaje);
                netStream.Write(sendingBuffer, 0, sendingBuffer.Length);

                netStream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR TCP: " + ex.Message);
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
                        int ataque = 0;

                        //Ataque
                        if (int.TryParse(data.Substring(0, 1), out ataque))
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
