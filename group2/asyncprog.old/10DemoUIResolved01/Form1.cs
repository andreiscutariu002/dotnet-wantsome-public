namespace _10DemoUISolved01
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using _10DemoUI.Core;

    public partial class Form1 : Form
    {
        private const string Path = "..\\..\\..\\00Data\\cars.csv";

        private static BackgroundWorker bw;

        public Form1()
        {
            bw = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            bw.DoWork += (s, e) =>
            {
                var cars = this.ProcessCarsFile(Path).ToList();

                this.DisplayCars(cars);

                e.Result = cars.Count;
            };

            bw.ProgressChanged += (s, e) => { this.Log($"progress {e.ProgressPercentage} %"); };

            bw.RunWorkerCompleted += (s, e) => { this.Log($"finish to process file. {e.Result} cars downloaded"); };

            this.InitializeComponent();
        }

        private void GetDataBtn_Click(object sender, EventArgs e)
        {
            this.Log("start to process file");

            bw.RunWorkerAsync();
        }

        private void DisplayCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                this.AppendToContent(car.ToString());

                //Thread.Sleep(TimeSpan.FromMilliseconds(50)); // simulate other work
            }
        }

        private IEnumerable<Car> ProcessCarsFile(string filePath)
        {
            var cars = new List<Car>(600);

            var lines = File.ReadAllLines(filePath).Skip(2).ToList();

            var carsCount = lines.Count;
            var progress = 0;
            var prevPercent = -1;

            foreach (var line in lines)
            {
                cars.Add(Car.Parse(line));

                progress++;
                var percentProgress = ((double) progress / carsCount) * 100;
                var percent = (int) (percentProgress * 100) / 100;

                if (percent != prevPercent && percent % 10 == 0)
                {
                    bw.ReportProgress(percent);
                    prevPercent = percent;
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(5)); // simulate some work
            }

            return cars;
        }

        public void Log(string s)
        {
            this.logTbx.AppendText($"{DateTime.Now} - {s}{Environment.NewLine}");
        }

        public void AppendToContent(string s)
        {
            this.contentTxb.AppendText($"{s}{Environment.NewLine}");
        }
    }
}
