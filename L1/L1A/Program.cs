using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L1A
{
    class Program
    {
        public static bool stop = false;
        static DataMonitor dataMonitor = new DataMonitor(10);
        public static ResultMonitor resultMonitor = new ResultMonitor(50);
        static void Main(string[] args)
        {
            string f1 = @"C:\Users\giedr\OneDrive\Stalinis kompiuteris\Parallelism\L1\L1A\IFF-7_5_BačkaitisG_L1_dat_1.json";
            string f2 = @"C:\Users\giedr\OneDrive\Stalinis kompiuteris\Parallelism\L1\L1A\IFF-7_5_BačkaitisG_L1_dat_2.json";
            string f3 = @"C:\Users\giedr\OneDrive\Stalinis kompiuteris\Parallelism\L1\L1A\IFF-7_5_BačkaitisG_L1_dat_3.json";
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CarContainer));
            var stream = new FileStream(f1, FileMode.Open);
            CarContainer cars = (CarContainer)serializer.ReadObject(stream);
            var threads = new List<Thread> {
                new Thread(() => RetrieveAndCalculate()),
                new Thread(() => RetrieveAndCalculate()),
                new Thread(() => RetrieveAndCalculate()),
                new Thread(() => RetrieveAndCalculate())
            };
            threads.ForEach(thread => thread.Start());
            foreach (var car in cars.Cars)
            {
                dataMonitor.AddItem(car);
            }
            stop = true;
            //Patikrint kodel nesustoja(uzsisleepina ir neatsibunda threadai, 
            //sudet sortintus(dabar deda paprastai), output to file
            threads.ForEach(thread => thread.Join());
            PrintToFile();

        }

        private static void RetrieveAndCalculate()
        {
            while (true)
            {
                if(stop && dataMonitor.Counter == 0)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < dataMonitor.Counter; i++)
                    {
                        dataMonitor.RemoveItem(i);
                    }
                }
            }
        }

        private static void PrintToFile()
        {
            //formats to table and prints to result file results
        }
    }
}
