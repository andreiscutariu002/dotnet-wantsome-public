namespace Threads
{
    using System;
    using System.Threading;

    internal class ThreadExNotOk
    {
        public static void Run()
        {
            try
            {
                new Thread(Go).Start();
            }
            catch (Exception ex)
            {
                // We'll never get here!
                Console.WriteLine("Exception!");
            }
        }

        private static void Go()
        {
            throw null;
        } // Throws a NullReferenceException
    }
}
