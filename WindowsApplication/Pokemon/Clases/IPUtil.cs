using System.Net;

namespace Pokemon.Clases
{
    class IPUtil
    {
        public static string ObtenerIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }

    }
}
