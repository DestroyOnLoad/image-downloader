using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ImageDownloader
{
    class Program
    {
        static string remoteUri;
        static string downloadRepository;
        static List<string> fileNames;

        static void Main(string[] args)
        {
            BuildImageDirectory();
        }

        private static void BuildImageDirectory()
        {
            fileNames = GetFileNames();
            DownloadFiles();
        }

        private static void DownloadFiles()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile($"{remoteUri}", $"{downloadRepository}");
            }
        }

        private static List<string> GetFileNames()
        {
            throw new NotImplementedException();
        }
    }
}
