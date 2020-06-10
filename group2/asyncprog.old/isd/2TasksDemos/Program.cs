using System;

namespace _2TasksDemos
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            Demo04.Run();
        }
    }

    public class Demo04
    {
        internal static void Run()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            token.Register(() =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You can cancel other things in progress in your task (http call, db operation, etc..). " +
                                  $"This callback is called when token was cancelled!");
                Console.ForegroundColor = ConsoleColor.White;
            });

            var task = Task.Run(() =>
            {
                var capturedToken = token;

                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"Step {i}.");

                    Task.Delay(TimeSpan.FromSeconds(1), capturedToken).Wait(capturedToken);

                    if (capturedToken.IsCancellationRequested)
                    {
                        Console.WriteLine($"Cancelled at step {i}.");

                        break;

                        //capturedToken.ThrowIfCancellationRequested(); // throw an OperationCanceledException.
                    }
                }

                //...
            }, token);

            Thread.Sleep(TimeSpan.FromSeconds(4));

            tokenSource.Cancel();
        }
    }

    public class Demo03
    {
        internal static void Run()
        {
            var x = 0;

            var parent = Task.Factory.StartNew(() =>
            {
                int[] numbers = {0};

                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);

                try
                {
                    childFactory.StartNew(() => 5 / numbers[0]); // Division by zero
                    childFactory.StartNew(() => numbers[1]); // Index out of range
                    childFactory.StartNew(() => { throw new InvalidOperationException(); }); // Null reference
                }
                catch (AggregateException ae)
                {
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
                    Console.WriteLine("Not handled " + e.GetType().Name);
                }
            }
        }

        private static void RunMethod(Task parent)
        {
            try
            {
                parent.Wait();
            }
            catch (AggregateException ex)
            {
                ex.Flatten().Handle(e =>
                {
                    if (e is DivideByZeroException)
                    {
                        Console.WriteLine("Handled" + ex.GetType().Name);
                        return true; // This exception is "handled"
                    }

                    if (e is IndexOutOfRangeException)
                    {
                        Console.WriteLine("Handled " + ex.GetType().Name);
                        return true; // This exception is "handled"   
                    }

                    return false;
                });
            }
        }

        private static Func<int> ActionToRun(int x)
        {
            return () =>
            {
                Console.WriteLine($"Hello from thread {Thread.CurrentThread.ManagedThreadId}");
                return 7 / x;
            };
        }
    }

    // todo
    public class Demo02
    {
        public static void Run()
        {
            Task<int[]> parent = new Task<int[]>(() =>
            {
                var results = new int[3];

                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);

                tf.StartNew(() => {
                    Thread.Sleep(15000);
                    results[0] = 0;
                });

                tf.StartNew(() => results[1] = 1);
                tf.StartNew(() => results[2] = 2);

                return results;
            });

            parent.Start();

            var finalTask = parent.ContinueWith(
                parentTask => {
                    foreach (int i in parentTask.Result)
                        Console.WriteLine(i);
                });

            finalTask.Wait();
            Console.ReadLine();
        }
    }

    public class Demo
    {
        public static void Run()
        {
            Task<int> t1 = new Task<int>(PrintInfo);
            t1.Start();

            //t1.Exception;
            //t1.Result;
            
            Console.WriteLine($"[T_ID: Main Thread Completed");
            Console.ReadLine();

            Action someAction = () =>
            {
                //print task t thread id
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Task Current Thread Id:" + threadId);
            };

            var task = new Task(someAction, TaskCreationOptions.LongRunning);

            task.Start();
            task.Wait();

            Console.WriteLine("Task Main Thread Id:" + Thread.CurrentThread.ManagedThreadId);
        }

        static int PrintInfo()
        {
            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine($"[T_ID: i value: {i}");
            }

            Console.WriteLine($"[T_ID: Child Thread Completed");

            return 2;
        }
    }
}
