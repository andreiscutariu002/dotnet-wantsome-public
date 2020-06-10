namespace _12Tasks
{
    using System;
    using System.Threading.Tasks;

    internal class TaskContinuation3
    {
        internal static void Run()
        {
            // throws exception on Wait() 
            Task task1 = Task.Factory.StartNew(() =>
            {
                //throw null;

                return 10;
            }).ContinueWith(ant =>
            {
                if (ant.Exception != null)
                {
                    Console.WriteLine(ant.Exception.Message);
                }
                else
                {
                    Console.WriteLine(ant.Result);
                }
            });

            // throws exception on Wait()
            Task task2 = Task.Factory.StartNew(() =>
            {
                throw null;

                //return 10;
            });

            task1.Wait();

            //task2.Wait();

            Console.WriteLine("Press Enter to terminate!");
            Console.ReadLine();
        }
    }
}