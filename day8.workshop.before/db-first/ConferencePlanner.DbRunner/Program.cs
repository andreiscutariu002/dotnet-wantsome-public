using DbUp;
using System;
using System.Linq;
using System.Reflection;

namespace ConferencePlanner.DbRunner
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString =
                    args.FirstOrDefault()
                    ?? "Data Source=EN1210001; Integrated Security=True; Initial Catalog=ConferencePlanner_DbFirst";

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
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
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
