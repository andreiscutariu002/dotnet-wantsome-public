namespace _081SignalingConstructs
{
    using System;
    using System.Threading;

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