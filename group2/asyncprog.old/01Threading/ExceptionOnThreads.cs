namespace _01Threading
{
    using System;
    using System.Threading;

    public class ExceptionOnThreads
    {
        public static void Run()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            try
            {
                var t = new Thread(Go);
                t.Start();
            }
            catch (Exception)
            {
                // We'll never get here!
                Console.WriteLine("Exception!");
            }
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception) args.ExceptionObject;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Go()
        {
            throw new InvalidOperationException();
        }
    }
}
