using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogWriter
{
    public class LogFile
    {
        private string _path = "system/log";

        public string path
        {
            get { return _path; }
            set { _path = value; }
        }
        // Save the log to a file
        public void SaveLog(string log)
        {
            try
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);
                // Create a new file
                string file = Path.Combine(_path, DateTime.Now.ToString("dd-MM-yyyy-hh") + "-log.txt");
                if (!File.Exists(file))
                {
                    File.Create(file);
                }
                // Write the log to the file
                using (StreamWriter writer = File.AppendText(file))
                {
                    writer.WriteLine("[ " + DateTime.Now.ToString("dd-MM-yy hh:mm:ss") + " ] => " + log);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void RemoveFile()
        {
            // File all
            string[] files = Directory.GetFiles(_path);
            foreach (string file in files)
            {
                // Remove file 30 day
                if (File.GetCreationTime(file) < DateTime.Now.AddDays(-30))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
