using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountdownEvent_Example
{
    class Data
    {
        public int Num { get; set; }
        public Data(int i) { Num = i; }
        public Data() { }
    }
    class DataWithToken
    {
        public CancellationToken Token { get; set; }
        public Data Data { get; private set; }
        public DataWithToken(Data data, CancellationToken ct)
        {
            this.Data = data;
            this.Token = ct;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Data> source = GetData();
            using (CountdownEvent e = new CountdownEvent(1))
            {
                // fork work:
                foreach (Data element in source)
                {
                    // Dynamically increment signal count.
                    e.AddCount();
                    ThreadPool.QueueUserWorkItem(delegate (object state)
                    {
                        try
                        {
                            ProcessData(state);
                        }
                        finally
                        {
                            e.Signal();
                        }
                    },
                     element);
                }
                e.Signal();

                // The first element could be run on this thread.

                // Join with work.
                e.Wait();
            }
        }

        static IEnumerable<Data> GetData()
        {
            return new List<Data>() { new Data(1), new Data(2), new Data(3), new Data(4), new Data(5) };
        }
        static void ProcessData(object obj)
        {
            DataWithToken dataWithToken = (DataWithToken)obj;
            if (dataWithToken.Token.IsCancellationRequested)
            {
                Console.WriteLine("Canceled before starting {0}", dataWithToken.Data.Num);
                return;
            }

            for (int i = 0; i < 10000; i++)
            {
                if (dataWithToken.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Cancelling while executing {0}", dataWithToken.Data.Num);
                    return;
                }
                // Increase this value to slow down the program.
                Thread.SpinWait(100000);
            }
            Console.WriteLine("Processed {0}", dataWithToken.Data.Num);
        }
    }
}
