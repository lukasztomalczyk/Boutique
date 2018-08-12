using System;
using System.Linq;
using Microsoft.SqlServer.Dac;

namespace DatabaseDeploy
{
    public class Program
    {
        private static string DatabaseName = "Boutique";
        private static string DacpacFileName = @"..\..\..\FileStore\Boutique.Database.dacpac";
        private static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Boutique;Integrated Security=True;Connect Timeout=0;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static bool IsErrorAtDeyploment;


        public static void Main(string[] args)
        {
            DacpacFileName = args.Any() ? args[0] : DacpacFileName;

            var dacServices = new DacServices(ConnectionString);
            dacServices.Message += DacServices_Message;
            var options = new DacDeployOptions
            {
                CreateNewDatabase = true,
                BlockOnPossibleDataLoss = false,
                GenerateSmartDefaults = true,
                VerifyDeployment = true,
                DropObjectsNotInSource = true
            };

            Console.WriteLine();
            Console.WriteLine("Start process...");
            Console.WriteLine();

            //TODO na pszyłość można to wykorzystać
            //dacServices.GenerateDeployReport(DacPackage.Load(DacpacFileName), DatabaseName, options);


            dacServices.Deploy(DacPackage.Load(DacpacFileName), DatabaseName, true, options);

            Console.ReadKey();
        }

        private static void DacServices_Message(object sender, DacMessageEventArgs e)
        {
            IsErrorAtDeyploment = false;
            if (e.Message.MessageType == DacMessageType.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message.Message);
                IsErrorAtDeyploment = true;
            }
            if (e.Message.MessageType == DacMessageType.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(e.Message.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(e.Message.Message);
            }


        }
    }
}
