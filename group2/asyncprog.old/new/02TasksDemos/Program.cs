namespace _02TasksDemos
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Demo08.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
