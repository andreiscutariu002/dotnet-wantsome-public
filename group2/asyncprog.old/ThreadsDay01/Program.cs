namespace ThreadsDay01
{
    using System;
    using System.Threading;

    internal class Program
    {
        //private static void Main(string[] args)
        //{
        //    try
        //    {
        //        ForegroundBackground.Run();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.InnerException != null
        //            ? $"Exception {e.InnerException.Message}"
        //            : $"Exception {e.Message}");
        //    }
        //    finally
        //    {
        //        //Console.ReadKey();
        //    }
        //}

        public static void Main()
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

        static void Go() { throw null; }
    }
}
