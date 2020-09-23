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
                data[Counter++] = carResult;
            }
        }

        public CarResult[] GetItems()
        {
            return data;
        }
    }
}
