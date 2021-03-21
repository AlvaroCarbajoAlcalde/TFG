using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Pokemon.TFG
{
    public static class TCP
    {

        #region Variables

        private static readonly int TAM_BUFFER = 1024;
        private static readonly int PUERTO_RECIBIR = 6060;
        private static readonly int PUERTO_ENVIAR = 6061;
        private static readonly string IP_MOVIL = "192.168.1.128";

        #endregion

        #region Cliente

        public static void EnviarTCP(string rutaArchivo)
        {
            try
            {
                byte[] sendingBuffer;
                TcpClient client = new TcpClient(IP_MOVIL, PUERTO_ENVIAR);
                NetworkStream netStream = client.GetStream();
                FileStream fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
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
                    netStream.Write(sendingBuffer, 0, sendingBuffer.Length);
                }
                fileStream.Close();
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

        public static void Servidor(Form_Combate form_Combate)
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Any, PUERTO_RECIBIR);

                server.Start();

                // Buffer for reading data
                byte[] bytes = new byte[256];
                string data = null;

                // Enter the listening loop.
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        data = data.ToUpper();

                        byte[] msg = Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        form_Combate.ataqueSeleccionadoMultiplayer = int.Parse(data.Substring(0,1));
                        Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ServerException: {0}", e);
            }
        }

        #endregion

    }
}
