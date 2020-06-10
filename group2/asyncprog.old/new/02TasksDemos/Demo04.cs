namespace _02TasksDemos
{
    using System;
    using System.Threading.Tasks;

    public class Demo04
    {
        public static void Run()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                // We’ll throw 3 exceptions at once using 3 child tasks:

                int[] numbers = { 0 };

                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);

                childFactory.StartNew(() => 5 / numbers[0]); // Division by zero
                childFactory.StartNew(() => numbers[1]); // Index out of range
                childFactory.StartNew(() => { throw new InvalidOperationException(); }); // Null reference
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
                aex2.Flatten().Handle(ex =>
                {
                    if (ex is DivideByZeroException)
                    {
                        return true;
                    }
                    
                    if (ex is IndexOutOfRangeException)
                    {
                        return true;
                    }

                    return false;
                });
            }
        }
    }
}