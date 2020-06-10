namespace _10DemoUISolved
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using _10DemoUI.Core;

    public partial class Form1 : Form
    {
        public delegate void AppendText(string myString);

        private readonly AppendText updateContentTxbDelegate;
        private readonly AppendText updateLogTxbDelegate;

        private const string Path = "..\\..\\..\\00Data\\cars.csv";

        public Form1()
        {
            this.InitializeComponent();

            this.updateContentTxbDelegate = this.AppendToContent;
            this.updateLogTxbDelegate = this.AppendToLog;
        }

        private void GetDataBtn_Click(object sender, EventArgs e)
        {
            this.AppendToLog("start to process file");

            ThreadPool.QueueUserWorkItem(this.ReadAndDisplayCars);
        }

        private void ReadAndDisplayCars(object state)
        {
            var cars = this.ProcessCarsFile(Path).ToList();

            this.DisplayCars(cars);

            // this.AppendToLog($"finish to process file. {cars.Count()} cars downloaded");  replace with 

            this.logTbx.Invoke(this.updateLogTxbDelegate, $"finish to process file. {cars.Count()} cars downloaded");
        }

        private void DisplayCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                // this.AppendToContent(car.ToString()); replace with 

                this.contentTxb.Invoke(this.updateContentTxbDelegate, car.ToString());

                Thread.Sleep(TimeSpan.FromMilliseconds(100));
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

        public void AppendToLog(string s)
        {
            this.logTbx.AppendText($"{DateTime.Now} - {s}{Environment.NewLine}");
        }

        public void AppendToContent(string s)
        {
            this.contentTxb.AppendText($"{s}{Environment.NewLine}");
        }
    }
}
