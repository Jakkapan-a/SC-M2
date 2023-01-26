using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2_V2._00.Utilities
{
    internal class LogWriter
    {
        // Save the log to a file
        public static void SaveLog(string log)
        {
            try
            {
                if (!Directory.Exists(SC_M2_V2._00.Properties.Resources.Path_log))
                    Directory.CreateDirectory(SC_M2_V2._00.Properties.Resources.Path_log);
                // Create a new file
                string file = Path.Combine(SC_M2_V2._00.Properties.Resources.Path_log, DateTime.Now.ToString("dd-MM-yyyy-hh")+"-log.txt");
                if(!File.Exists(file))
                {
                    File.Create(file);
                }
                // Write the log to the file
                using (StreamWriter writer = File.AppendText(file))
                {
                    writer.WriteLine("[ "+DateTime.Now.ToString("dd-MM-yy hh:mm:ss") + " ] => "+ log);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
       
        public static void RemoveFile()
        {
            // File all
            string[] files = Directory.GetFiles(SC_M2_V2._00.Properties.Resources.Path_log);
            foreach(string file in files)
            {
                // Remove file 30 day
                if(File.GetCreationTime(file) < DateTime.Now.AddDays(-30))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
