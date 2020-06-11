namespace Threads
{
    using System;
    using System.Threading;

    internal class ThreadExOk
    {
        public static void Run()
        {
            new Thread(Go).Start();
        }

        private static void Go()
        {
            try
            {
                // ...
                throw null; // The NullReferenceException will get caught below
                // ...
            }
            catch (Exception ex)
            {
                // Typically log the exception, and/or signal another thread
                // that we've come unstuck
                // ...
            }
        }
    }
}