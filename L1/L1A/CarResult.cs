using System;
using System.Collections.Generic;
using System.Text;

namespace L1A
{
    class CarResult
    {
        public Car Car { get; set; }
        public string Result { get; set; }

        public CarResult(Car car, string result)
        {
            Car = car;
            Result = result;
        }
    }  
}
