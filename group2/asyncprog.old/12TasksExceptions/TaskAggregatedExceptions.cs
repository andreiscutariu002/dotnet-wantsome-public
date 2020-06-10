namespace _12TasksExceptions
{
    using System;
    using System.Threading.Tasks;

    internal class TaskAggregatedExceptions
    {
        internal static void Run()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                // We’ll throw 3 exceptions at once using 3 child tasks:

                int[] numbers = { 0 };

                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);

                try
                {
                    childFactory.StartNew(() => 5 / numbers[0]); // Division by zero
                    childFactory.StartNew(() => numbers[1]); // Index out of range
                    childFactory.StartNew(() => { throw new InvalidOperationException(); }); // Null reference
                }
                catch (AggregateException aex2)
                {
                    aex2.Flatten().Handle(ex => // Note that we still need to call Flatten
                    {
                        if (ex is DivideByZeroException)
                        {
                            Console.WriteLine(ex.GetType().Name);
                            return true; // This exception is "handled"
                        }

                        if (ex is IndexOutOfRangeException)
                        {
                            Console.WriteLine(ex.GetType().Name);
                            return true; // This exception is "handled"   
                        }

                        if (ex is InvalidOperationException)
                        {
                            Console.WriteLine(ex.GetType().Name);
                            return true; // This exception is "handled"   
                        }

                        return false; // All other exceptions will get rethrown
                    });
                }
            });

            try
            {
                RunMethod(parent);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.Flatten().InnerExceptions)
                {
                    Console.WriteLine("Not handled" + ex.GetType().Name);
                }
            }
        }

        private static void RunMethod(Task parent)
        {
            try
            {
                parent.Wait();
            }
            catch (AggregateException aex2)
            {
                aex2.Flatten().Handle(ex => // Note that we still need to call Flatten
                {
                    if (ex is DivideByZeroException)
                    {
                        Console.WriteLine("Handled" + ex.GetType().Name);
                        return true; // This exception is "handled"
                    }

                    if (ex is IndexOutOfRangeException)
                    {
                        Console.WriteLine("Handled" + ex.GetType().Name);
                        return true; // This exception is "handled"   
                    }

                    //if (ex is InvalidOperationException)
                    //{
                    //    Console.WriteLine("Handled" + ex.GetType().Name);
                    //    return true; // This exception is "handled"   
                    //}

                    return false; // All other exceptions will get rethrown
                });
            }
        }
    }
}