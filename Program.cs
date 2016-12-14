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
                        RunJobs();
                        break;
                    case "2":
                        StopServer();
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

            var options = new Shift.Options();
            options.AssemblyListPath = baseDir + @"\client-assemblies\assemblylist.txt";
            options.AssemblyBaseDir = baseDir + @"\client-assemblies\"; //base dir for DLLs
            options.MaxRunnableJobs = 5;
            options.ProcessID = -999; //negative number for demo/testing
            options.DBConnectionString = "Data Source=localhost\\SQL2014;Initial Catalog=BGProcess;user=bguser; password=bguser"; //should be in config or DB
            options.CacheConfigurationString = "localhost:6379,password=LZLxuFbuPCdxcizNuuDJ0EdoXit1YHoiln8lsTVzPgGTeNB1DkoETMeCZI3FNjvQ"; //should be in config
            //options.EncryptionKey = "[OPTIONAL_ENCRYPTIONKEY]"; //optional, will encrypt parameters in DB if filled

            jobServer = new JobServer(options);
            jobServer.Start();

            addedJobIDs = new List<int>();
        }

        private static Timer _timer = null;
        private static Timer _timer2 = null;

        private static void RunJobs()
        {
            //Close any existing timers if not too many timers running.
            if (_timer != null && _timer2 != null)
            {
                _timer.Close();
                _timer2.Close();
            }

            _timer = new Timer();
            _timer.Enabled = true;
            _timer.Interval = 5000;
            _timer.Elapsed += (sender, e) => {
                jobServer.StopJobs();
                jobServer.StopDeleteJobs();
                jobServer.StartJobs();
            };

            _timer2 = new Timer();
            _timer2.Enabled = true;
            _timer2.Interval = 10000;
            _timer2.Elapsed += (sender, e) => {
                jobServer.CleanUp();
            };

            Console.WriteLine();
            Console.WriteLine("==> Server starts running jobs.");
        }

        private static void StopServer()
        {
            if (_timer != null && _timer2 != null)
            {
                _timer.Close();
                _timer2.Close();
            }
            Console.WriteLine();
            Console.WriteLine("==> Server stopped.");
        }
    }
}
