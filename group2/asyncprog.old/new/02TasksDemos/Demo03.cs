namespace _02TasksDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo03
    {
        public static void Run()
        {
            var x = 0;

            var calc = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Hello from thread {Thread.CurrentThread.ManagedThreadId}");
                return 7 / x;
            });

            Console.WriteLine($"Hello from thread {Thread.CurrentThread.ManagedThreadId}");

            try
            {
                Console.WriteLine(calc.Result);
            }
            catch (DivideByZeroException aex)
            {
                Console.Write(aex.InnerException.Message); // Attempted to divide by 0
            }
            catch (AggregateException aex)
            {
                Console.Write(aex.InnerException.Message); // Attempted to divide by 0
            }
            catch (Exception aex)
            {
                Console.Write(aex.InnerException.Message); // Attempted to divide by 0
            }
        }
    }
}