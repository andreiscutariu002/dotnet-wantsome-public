namespace _12Tasks
{
    using System;
    using System.Threading.Tasks;

    internal class TaskContinuation4
    {
        internal static void Run()
        {
            // throws exception on Wait() 
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

            Console.WriteLine("Press Enter to terminate!");
            Console.ReadLine();
        }
    }
}