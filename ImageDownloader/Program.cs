using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ImageDownloader
{
    class Program
    {
        static string remoteUri = @"";
        static string downloadRepository = @"";
        static List<string> fileNames;
        static List<string> errorLog = new List<string>();
        static string csvPath;
        static string errorLogPath = @"" + "errors.csv";

        static void Main(string[] args)
        {
            BuildImageDirectory();
        }

        private static void BuildImageDirectory()
        {
            fileNames = GetFileNames();
            DownloadFiles(fileNames);
            if(errorLog.Count > 0)
            {
                WriteErrorLog();
                Console.WriteLine("Finished with errors");
            } else
            {
                Console.WriteLine("Finished without errors");
            }
        }

        private static void DownloadFiles(List<string> fileNames)
        {
            using (WebClient client = new WebClient())
            {
                foreach(var fileName in fileNames)
                {
                    try
                    {
                        client.DownloadFile($"{remoteUri}{fileName}.jpg", $"{downloadRepository}{fileName}.jpg");
                    } catch
                    {
                        errorLog.Add(fileName);
                        Console.WriteLine(fileName + " had an error");
                    }
                }
            }
        }

        private static List<string> GetFileNames()
        {
            List<string> fileNames = new List<string>();

            using(StreamReader sr = File.OpenText(csvPath))
            {
                string fileName;
                while((fileName = sr.ReadLine()) != null)
                {
                    fileNames.Add(fileName);
                }
            }

            return fileNames;
        }
        private static void WriteErrorLog()
        {
            if (!File.Exists(errorLogPath))
            {
                using (StreamWriter sw = File.CreateText(errorLogPath))
                {
                    foreach(var error in errorLog)
                    {
                        sw.WriteLine(error);
                    }
                }
            }
        }

    }
}
