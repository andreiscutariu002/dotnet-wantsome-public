namespace _21FileProcessor
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var orchestrator = new Orchestrator();
            orchestrator.Run("C:\\Users\\ascutariu\\OneDrive - ENDAVA\\Desktop\\data");

            Console.WriteLine("Any key to exit!");
            Console.ReadKey();
        }
    }
}
