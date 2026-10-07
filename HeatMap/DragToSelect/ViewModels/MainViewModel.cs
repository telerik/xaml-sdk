using System;
using System.Collections.Generic;

namespace DragToSelect.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            var months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
            var r = new Random(0);
            this.Temperatures = new List<TempInfo>();
            for (int i = 2000; i < 2014; i++)
            {
                for (int k = 0; k < months.Length; k++)
                {
                    this.Temperatures.Add(new TempInfo { Year = i, Month = months[k], Temp = r.Next(0, 100) });
                }
            }

            this.Cars = new List<CarInfo>();
            this.Cars.Add(new CarInfo { Name = "Lada", Price = 7000, HP = 60, MaxSpeed = 135, Weight = 900, });
            this.Cars.Add(new CarInfo { Name = "Lada 1", Price = 8000, HP = 66, MaxSpeed = 145, Weight = 900, });
            this.Cars.Add(new CarInfo { Name = "Lada 2", Price = 9000, HP = 60, MaxSpeed = 155, Weight = 1000, });
            this.Cars.Add(new CarInfo { Name = "Lada 3", Price = 9500, HP = 70, MaxSpeed = 165, Weight = 950, });
            this.Cars.Add(new CarInfo { Name = "Lada 4", Price = 9900, HP = 50, MaxSpeed = 175, Weight = 980, });
            this.Cars.Add(new CarInfo { Name = "Lada 5", Price = 12000, HP = 80, MaxSpeed = 185, Weight = 900, });
            this.Cars.Add(new CarInfo { Name = "Lada 6", Price = 12200, HP = 88, MaxSpeed = 195, Weight = 890, });
            this.Cars.Add(new CarInfo { Name = "Lada 7", Price = 12500, HP = 80, MaxSpeed = 205, Weight = 880, });
            this.Cars.Add(new CarInfo { Name = "Lada 8", Price = 7000, HP = 60, MaxSpeed = 135, Weight = 900, });
            this.Cars.Add(new CarInfo { Name = "Lada 9", Price = 8000, HP = 66, MaxSpeed = 145, Weight = 900, });
            this.Cars.Add(new CarInfo { Name = "Lada 10", Price = 9000, HP = 60, MaxSpeed = 155, Weight = 1000, });
            this.Cars.Add(new CarInfo { Name = "Lada 11", Price = 9500, HP = 70, MaxSpeed = 165, Weight = 950, });
            this.Cars.Add(new CarInfo { Name = "Lada 12", Price = 9900, HP = 50, MaxSpeed = 175, Weight = 980, });
            this.Cars.Add(new CarInfo { Name = "Lada 13", Price = 12000, HP = 80, MaxSpeed = 185, Weight = 900, });
            this.Cars.Add(new CarInfo { Name = "Lada 14", Price = 12200, HP = 88, MaxSpeed = 195, Weight = 890, });
            this.Cars.Add(new CarInfo { Name = "Lada 15", Price = 12500, HP = 80, MaxSpeed = 205, Weight = 880, });
        }

        public List<TempInfo> Temperatures { get; set; }
        public List<CarInfo> Cars { get; set; }
    }
}
