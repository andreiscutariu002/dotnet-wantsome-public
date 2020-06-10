namespace _01ThreadingDemos
{
    using System;
    using System.Threading;

    public class Demo05
    {
        public static void Run()
        {
            // Naming a Mutex makes it available computer-wide. Use a name that's
            // unique to your company and application (e.g., include your URL).

            using (var mutex = new Mutex(false, "07Mutex"))
            {
                // Wait a few seconds if contended, in case another instance
                // of the program is still in the process of shutting down.

                Console.WriteLine("Check Mutex");

                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    Console.WriteLine("Already running. Bye!");
                    return;
                }

                RunProgram();
            }

            static void RunProgram()
            {
                Console.WriteLine("Running. Press Enter to exit");

                Console.ReadLine();
            }
        }
    }
}
