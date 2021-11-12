using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBall
{
    class log
    {
      public  void WriteLog(string message)
        {
            if (!Directory.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, "log")))
            {
                Directory.CreateDirectory(Path.Combine(System.Windows.Forms.Application.StartupPath, "log"));
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(System.Windows.Forms.Application.StartupPath, "log", DateTime.Now.ToString("yyyyMMdd") + ".txt"), true))
            {
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "   " + message);
            }


        }
    }
}
