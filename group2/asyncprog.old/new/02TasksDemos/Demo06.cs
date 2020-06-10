namespace _02TasksDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo06
    {
        public static void Run()
        {
            Task<int> task1 = Task.Factory.StartNew(() =>
            {
                //throw null;
                return 10;
            });

            task1.ContinueWith(ant =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ant.Exception.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }, TaskContinuationOptions.OnlyOnFaulted);

            task1.ContinueWith(ant =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ant.Result);
                Console.ForegroundColor = ConsoleColor.White;
            }, TaskContinuationOptions.NotOnFaulted);

            //task1.Wait();

            Console.WriteLine("Press Enter to terminate!");
            Console.ReadLine();
        }
    }
}