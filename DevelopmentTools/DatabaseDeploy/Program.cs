using System;
using System.Linq;
using System.Reflection;
using DbUp;

namespace DatabaseDeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                args.FirstOrDefault()
                ?? "Server=172.17.0.2;user=SA;password=Kleopatra2017@@;database=shop;trusted_connection=false";

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithVariable("DatabaseName", "shop")
                    .WithScriptsFromFileSystem("Scripts/")
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif                

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

        }
    }
}