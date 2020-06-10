namespace _02TasksDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo05
    {
        public static void Run()
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

            var t1 = Task.Run(() =>
            {
                var capturedToken = token;

                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"Step {i}.");

                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();

                    //if (capturedToken.IsCancellationRequested)
                    //{
                    //    Console.WriteLine($"Cancelled at step {i}.");

                    //    break;

                    //    // // throw an OperationCanceledException.
                    //}

                    capturedToken.ThrowIfCancellationRequested();
                }
            }, token);

            //var t2 = Task.Factory.StartNew(stateObject =>
            //{
            //    var castedToken = (CancellationToken)stateObject;

            //    //...
            //}, token);

            //Task.Delay(TimeSpan.FromSeconds(5)).Wait();

            tokenSource.CancelAfter(TimeSpan.FromSeconds(4));

            Console.ReadKey();
        }
    }
}