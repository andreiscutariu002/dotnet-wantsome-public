namespace TasksDay02
{
    using System;

    internal class Program
    {
        private static void Main()
        {
            try
            {
                DemoEx9.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException != null
                    ? $"Exception {e.InnerException.Message}"
                    : $"Exception {e.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
