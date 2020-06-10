namespace _10DemoUISolved02
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using _10DemoUI.Core;

    public partial class Form1 : Form
    {
        private const string Path = "..\\..\\..\\00Data\\cars.csv";

        public Form1()
        {
            this.InitializeComponent();
        }

        private async void GetDataBtn_Click(object sender, EventArgs e)
        {
            this.AppendToLog("start to process file");

            try
            {
                var cars2 = await this.ReadCarsAsync();

                this.DisplayCarsToContentBox(cars2);
                this.AppendToLog($"finish to process file. {cars2.Count()} cars downloaded");
            }
            catch (Exception exception)
            {
                this.AppendToLog($"ERROR: {exception?.InnerException?.Message}");
            }
        }

        private Task<IList<Car>> ReadCarsAsync()
        {
            var task = new Task<IList<Car>>(() =>
            {
                var cars = this.ReadCarsFromFile(Path).ToList();

                return cars;
            });
            task.Start();
            return task;
        }

        private IEnumerable<Car> ReadCarsFromFile(string filePath)
        {
            var cars = new List<Car>(600);

            var lines = File.ReadAllLines(filePath).Skip(2);

            foreach (var line in lines)
            {
                cars.Add(Car.Parse(line));

                Thread.Sleep(TimeSpan.FromMilliseconds(10)); // simulate some work
            }

            return cars;
        }

        private void AppendToLog(string s)
        {
            this.logTbx.AppendText($"{DateTime.Now} - {s}{Environment.NewLine}");
        }

        private void AppendToContent(string s)
        {
            this.contentTxb.AppendText($"{s}{Environment.NewLine}");
        }

        private void DisplayCarsToContentBox(IList<Car> cars)
        {
            foreach (var car in cars) this.AppendToContent(car.ToString());
        }
    }
}
