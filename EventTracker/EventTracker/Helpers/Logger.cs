using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventTracker.Helpers
{
    public class Logger
    {
        public static void Log(string message)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\log.txt", true))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
