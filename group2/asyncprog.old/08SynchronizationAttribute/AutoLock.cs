namespace _082SynchronizationAttribute
{
    using System;
    using System.Runtime.Remoting.Contexts;
    using System.Threading;

    [Synchronization]
    public class AutoLock : ContextBoundObject
    {
        public void Demo()
        {
            Console.Write("Start...");
            Thread.Sleep(1000);
            Console.WriteLine("end");
        }
    }
}