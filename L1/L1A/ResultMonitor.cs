using System;
using System.Collections.Generic;
using System.Text;

namespace L1A
{
    class ResultMonitor
    {

        private CarResult[] data;
        public int Counter { get; private set; }

        public ResultMonitor(int length)
        {
            data = new CarResult[length];
            Counter = 0;
        }

        public void AddItemSorted(CarResult carResult)
        {
            lock (this)
            {
                if (Counter == 0)
                {
                    data[Counter++] = carResult;
                    return;
                }
                int position = Counter;
                for (int i = 0; i < Counter; i++)
                {
                    if (carResult.Car.Year < data[i].Car.Year)
                    {
                        position = i;
                        break;
                    }
                }
                
                for (int i = Counter; i >= position; i--)
                {
                    data[i+1] = data[i];
                }
                data[position] = carResult;
                Counter++;
                
            }
        }

        public CarResult[] GetItems()
        {
            return data;
        }
    }
}
