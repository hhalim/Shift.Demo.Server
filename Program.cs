using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Shift.Demo.Server
{
    class Program
    {
        private static JobServer jobServer;
        private static IList<int> addedJobIDs;

        static void Main(string[] args)
        {
            InitShiftServer();

            ConsoleKeyInfo cki;
            do
            {

                cki = DisplayMenu();  // show the key as you read it
                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        jobServer.RunServer();
                        Console.WriteLine("==> Server started.");
                        break;
                    case "2":
                        jobServer.StopServer();
                        Console.WriteLine("==> Server stopped.");
                        break;
                }
            } while (cki.Key != ConsoleKey.Escape);

        }

        static public ConsoleKeyInfo DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Shift Server Demo");
            Console.WriteLine("1. Start Shift Server.");
            Console.WriteLine("2. Stop Shift Server.");
            Console.WriteLine("Press escape (ESC) key to exit.");
            return Console.ReadKey(false);
        }

        private static void InitShiftServer()
        {
            var baseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var config = new Shift.ServerConfig();
            config.AssemblyListPath = baseDir + @"\client-assemblies\assemblylist.txt";
            config.AssemblyBaseDir = baseDir + @"\client-assemblies\"; //base dir for DLLs
            config.MaxRunnableJobs = 10;
            config.ProcessID = "-123"; //demo/testing ID
            config.DBConnectionString = "Data Source=localhost\\SQL2014;Initial Catalog=ShiftJobsDB;Integrated Security=SSPI;"; //should be in app.config or global DB
            config.UseCache = true;
            config.CacheConfigurationString = "localhost:6379"; //should be in app.config
            //options.EncryptionKey = "[OPTIONAL_ENCRYPTIONKEY]"; //optional, will encrypt parameters in DB if filled

            jobServer = new JobServer(config);

            addedJobIDs = new List<int>();
        }

    }
}
