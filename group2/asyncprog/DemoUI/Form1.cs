namespace DemoUI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using DemoUI.Core;

    // 1
    public partial class Form1 : Form
    {
        private const string Path =
            @"..\..\..\cars.csv";

        public Form1()
        {
            this.InitializeComponent();
        }

        private async void GetDataBtn_Click(object sender, EventArgs e)
        {
            this.Log("start to process file");  // main thread

            try
            {
                var task = this.ReadCarsAsync(); // main thread -> worker thread
                var cars = await task; // worker thread -> main thread

                this.DisplayCars(cars); // -> main thread
                this.Log($"finish to process file. {cars.Count()} cars downloaded"); // -> main thread
                
                Task<IList<Car>> task2 = this.ReadCarsAsync(); // main thread -> worker thread
                var cars2 = await task2; // worker thread -> main thread

                // main thread
            }
            catch (Exception ex)
            {
                this.Log($"error. {ex.Message}");
            }
        }

        private void GetDataBtn_ClickOnTasks(object sender, EventArgs e)
        {
            this.Log("start to process file");

            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            //2
            Task<IList<Car>> task = new Task<IList<Car>>(() =>
            {
                var cars = this.ProcessCarsFile(Path).ToList();
                return cars;
            });

            task.ContinueWith(prev =>
            {
                this.DisplayCars(prev.Result);
                this.Log($"finish to process file. {prev.Result.Count()} cars downloaded");
            }, CancellationToken.None, TaskContinuationOptions.NotOnFaulted, uiScheduler);

            task.ContinueWith(prev =>
            {
                this.Log($"error. {prev.Exception}");
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, uiScheduler);

            task.Start();
        }

        private Task<IList<Car>> ReadCarsAsync()
        {
            var task = new Task<IList<Car>>(() =>
            {
                var cars = this.ProcessCarsFile(Path).ToList();
                return cars;
            });
            task.Start();
            return task;
        }

        private void GetDataBtn_ClickOnThreads(object sender, EventArgs e)
        {
            this.Log("start to process file");

            var cars = new List<Car>();

            var thread = new Thread(() =>
            {
                cars = this.ProcessCarsFile(Path).ToList();

                Action action = () =>
                {
                    this.DisplayCars(cars);

                    this.Log($"finish to process file. {cars.Count()} cars downloaded");
                };

                this.Invoke(action);
            });
            thread.Start();
        }

        private void DisplayCars(IList<Car> cars)
        {
            foreach (var car in cars)
            {
                this.AppendToContent(car.ToString());
            }
        }

        private IEnumerable<Car> ProcessCarsFile(string filePath)
        {
            var cars = new List<Car>(600);
            var lines = File.ReadAllLines(filePath).Skip(2);

            foreach (var line in lines)
            {
                cars.Add(Car.Parse(line));
            }

            Thread.Sleep(TimeSpan.FromSeconds(3)); // simulate some work

            return cars;
        }

        public void Log(string s)
        {
            this.logTbx.AppendText($"{DateTime.Now} - {s}{Environment.NewLine}");
        }

        public void AppendToContent(string s)
        {
            this.contentTxb.AppendText($"{s}{Environment.NewLine}");

            //Thread.Sleep(TimeSpan.FromSeconds(3)); // simulate other work
        }
    }
}
