using System;
using System.Windows.Forms;

namespace Pokemon
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// Aplicacion realizada por Alvaro Carbajo para Desarrollo de Aplicaciones Multiplataforma
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_Inicio());
        }
    }
}
