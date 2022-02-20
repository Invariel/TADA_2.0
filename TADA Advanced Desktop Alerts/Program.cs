using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(WriteLogFile);

            if (System.Windows.Application.Current == null)
            {
                new System.Windows.Application();
            }

            Application.Run(new MainTADAForm());
        }

        static void WriteLogFile (object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            int depth = 0;

            using (StreamWriter w = new StreamWriter("error.log", true))
            {
                w.WriteLine(e.Message);
                w.WriteLine(e.StackTrace);
                while (e.InnerException != null && depth < 15)
                {
                    w.WriteLine("Inner exception: ");
                    ++depth;
                    e = e.InnerException;
                    w.WriteLine(e.Message);
                    w.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
