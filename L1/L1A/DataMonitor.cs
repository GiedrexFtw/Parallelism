using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace L1A
{
    class DataMonitor
    {
        private readonly Car[] data;
        public int Counter { get; private set; }

        public DataMonitor(int length)
        {
            data = new Car[length];
            Counter = 0;
        }

        public void AddItem(Car car)
        {
            lock (this)
            {
                while (this.Counter == data.Length)
                {
                    Monitor.Wait(this);
                }
                data[Counter] = car;
                this.SetCount(Counter+1);
                Console.WriteLine($"added car {car.Brand}");
            }
        }

        private void SetCount(int v)
        {
            Counter = v;
            Monitor.PulseAll(this);
        }

        public void RemoveItem(int index)
        {

            lock (this)
            {
                while(this.Counter == 0)
                {
                    Monitor.Wait(this);
                }
                if(data[index].Year > 2000)
                {
                    Car car = data[index];
                    string hash = this.CalculateHash(car);
                    CarResult carResult = new CarResult(car, hash);
                    Program.resultMonitor.AddItemSorted(carResult);
                }
                
                Console.WriteLine($"removed car {data[index].Brand}");
                for (int i = index; i < Counter-1; i++)
                {
                    data[i] = data[i + 1];
                }
                this.SetCount(Counter - 1);
                
            }
        }
        public Car[] GetCars()
        {
            return data;
        }
        public Car GetCar(int position)
        {
            return data[position];
        }
        private string CalculateHash(Car car)
        {
            SHA256 mySha256 = SHA256.Create();
            byte[] bytes = mySha256.ComputeHash(Encoding.UTF8.GetBytes($"{car.Brand}{car.Mileage}{car.Year}"));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
