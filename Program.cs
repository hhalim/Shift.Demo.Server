using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Shift.Demo.Server
{
    class Program
    {
        private static JobServer jobServer;

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

            jobServer.StopServer(); //always stop server before terminating app
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
            var config = new Shift.ServerConfig();
            config.AssemblyFolder = ConfigurationManager.AppSettings["AssemblyFolder"];
            //config.AssemblyListPath = ConfigurationManager.AppSettings["AssemblyListPath"];
            config.MaxRunnableJobs = Convert.ToInt32(ConfigurationManager.AppSettings["MaxRunnableJobs"]);
            config.ProcessID = ConfigurationManager.AppSettings["ShiftPID"]; //demo/testing ID
            config.DBConnectionString = ConfigurationManager.ConnectionStrings["ShiftDBConnection"].ConnectionString;

            var autoDeletePeriod = ConfigurationManager.AppSettings["AutoDeletePeriod"];
            config.AutoDeletePeriod = string.IsNullOrWhiteSpace(autoDeletePeriod) ? null : (int?)Convert.ToInt32(autoDeletePeriod);
            //config.AutoDeleteStatus = new List<JobStatus?> { JobStatus.Completed, JobStatus.Error }; //Auto delete only the jobs that had Stopped or with Error

            config.StorageMode = ConfigurationManager.AppSettings["StorageMode"];
            var progressDBInterval = ConfigurationManager.AppSettings["ProgressDBInterval"];
            if (!string.IsNullOrWhiteSpace(progressDBInterval))
                config.ProgressDBInterval = TimeSpan.Parse(progressDBInterval); //Interval when progress is updated in main DB

            //config.UseCache = Convert.ToBoolean(ConfigurationManager.AppSettings["UseCache"]);
            //config.CacheConfigurationString = ConfigurationManager.AppSettings["CacheConfigurationString"];
            //options.EncryptionKey = "[OPTIONAL_ENCRYPTIONKEY]"; //optional, will encrypt parameters in DB if filled

            jobServer = new JobServer(config);

        }

    }
}
