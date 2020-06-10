namespace _10DemoUI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Core;

    public partial class Form1 : Form
    {
        private const string Path = "..\\..\\..\\00Data\\cars.csv";

        public Form1()
        {
            this.InitializeComponent();
        }

        private void GetDataBtn_Click(object sender, EventArgs e)
        {
            this.logTbx.AppendText($"{DateTime.Now} - {"start to process file"}{Environment.NewLine}");

            var t = new Thread(() =>
            {
                var cars = this.ProcessCarsFile(Path).ToList();
                this.DisplayCars(cars);
                
                string s = $"finish to process file. {cars.Count()} cars downloaded";

                var formattableString = $"{DateTime.Now} - {s}{Environment.NewLine}";
            });

            t.Start();
        }

        private void DisplayCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                this.contentTxb.AppendText($"{car.ToString()}{Environment.NewLine}");
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

            Thread.Sleep(TimeSpan.FromSeconds(5)); // simulate some work

            return cars;
        }
    }
}
