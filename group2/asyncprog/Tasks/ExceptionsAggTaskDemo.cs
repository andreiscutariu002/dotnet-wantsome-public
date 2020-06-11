namespace Tasks
{
    using System;
    using System.Threading.Tasks;

    public static class ExceptionsAggTaskDemo
    {
        public static void Run()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                int[] numbers = { 0 };

                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);

                childFactory.StartNew(() => 5 / numbers[0]); // Division by zero
                childFactory.StartNew(() => numbers[1]); // Index out of range
                childFactory.StartNew(() => { throw new InvalidOperationException(); }); // Null reference
            });

            try
            {
                WaitParent(parent);
            }
            catch (AggregateException e)
            {
                foreach (var ex in e.Flatten().InnerExceptions)
                {
                    Console.WriteLine("Exception on main: " + ex.GetType().Name);
                }
            }
        }

        private static void WaitParent(Task parent)
        {
            try
            {
                parent.Wait();
            }
            catch (AggregateException e)
            {
                e.Flatten().Handle(ex =>
                {
                    if (ex is DivideByZeroException)
                    {
                        return true;
                    }

                    if (ex is InvalidOperationException)
                    {
                        return true;
                    }

                    return false;
                });
            }
        }
    }
}
