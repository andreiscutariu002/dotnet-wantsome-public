namespace _12Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class WaitAllEx
    {
        public static void Run()
        {
            var tasks = new Task[3];

            tasks[0] = Task.Run(() =>
            {
                Console.WriteLine("1 - start");
                Thread.Sleep(1000);
                Console.WriteLine("1 - finish");
                return 1;
            });

            tasks[1] = Task.Run(() =>
            {
                Console.WriteLine("2 - start");
                Thread.Sleep(1000);
                Console.WriteLine("2 - finish");
                return 2;
            });

            tasks[2] = Task.Run(() =>
            {
                Console.WriteLine("3 - start");
                Thread.Sleep(5000);
                Console.WriteLine("3 - finish");
                return 3;
            });

            Task.WaitAll(tasks);

            Console.WriteLine("Press Enter to terminate! Tasks finished!");
            Console.ReadLine();
        }
    }
}
