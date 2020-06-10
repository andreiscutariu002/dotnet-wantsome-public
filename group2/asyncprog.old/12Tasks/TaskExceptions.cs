namespace _12Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TaskExceptions
    {
        internal static void Run()
        {
            var x = 0;

            var calc = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Hello from child thread {Thread.CurrentThread.ManagedThreadId}");
                
                return 7 / x;
            });

            Console.WriteLine($"Hello from main thread {Thread.CurrentThread.ManagedThreadId}");

            try
            {
                calc.Wait();
            }
            catch (AggregateException aex)
            {
                Console.Write(aex.Message); // Attempted to divide by 0
            }

            //var parent = Task.Factory.StartNew(() =>
            //{
            //    // We’ll throw 3 exceptions at once using 3 child tasks:

            //    int[] numbers = {0};

            //    var childFactory = new TaskFactory (TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);

            //    childFactory.StartNew(() => 5 / numbers[0]); // Division by zero
            //    childFactory.StartNew(() => numbers[1]); // Index out of range
            //    childFactory.StartNew(() => { throw new InvalidOperationException(); }); // Null reference
            //});

            //try
            //{
            //    parent.Wait();
            //}
            //catch (AggregateException aex)
            //{
            //    aex.Flatten().Handle(ex => // Note that we still need to call Flatten
            //    {
            //        if (ex is DivideByZeroException)
            //        {
            //            Console.WriteLine("Divide by zero handled");
            //            return true; // This exception is "handled"
            //        }

            //        if (ex is IndexOutOfRangeException)
            //        {
            //            Console.WriteLine("Index out of range handled");
            //            return true; // This exception is "handled"   
            //        }

            //        return false; // All other exceptions will get rethrown
            //    });
            //}
        }
    }
}
