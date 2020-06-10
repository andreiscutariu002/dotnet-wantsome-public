namespace _082SynchronizationAttribute
{
    using System.Threading;

    public class ContextBoundObjectEx
    {
        public static void Run()
        {
            var safeInstance = new AutoLock();
            new Thread(safeInstance.Demo).Start();    
            new Thread(safeInstance.Demo).Start();    
            safeInstance.Demo();
        }
    }
}
