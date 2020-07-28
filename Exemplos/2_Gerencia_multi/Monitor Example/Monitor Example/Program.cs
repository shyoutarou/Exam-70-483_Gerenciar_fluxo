using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor_Example
{



    class PingPong
    {
        public void ping(bool running)
        {
            lock (this)
            {
                if (!running)
                {
                    //ball halts.
                    Monitor.Pulse(this); // notify any waiting threads
                    return;
                }
                Console.Write("Ping ");
                Monitor.Pulse(this); // let pong() run
                Monitor.Wait(this); // wait for pong() to complete
            }
        }
        public void pong(bool running)
        {
            lock (this)
            {
                if (!running)
                {
                    //ball halts.
                    Monitor.Pulse(this); // notify any waiting threads
                    return;
                }
                Console.WriteLine("Pong ");
                Monitor.Pulse(this); // let ping() run
                Monitor.Wait(this); // wait for ping() to complete
            }
        }
    }

    class MyThread
    {
        public Thread thread;
        PingPong pingpongObject;
        //construct a new thread.
        public MyThread(string name, PingPong pp)
        {
            thread = new Thread(new ThreadStart(this.run));
            pingpongObject = pp;
            thread.Name = name;
            thread.Start();
        }
        //Begin execution of new thread.
        void run()
        {
            if (thread.Name == "Ping")
            {
                for (int i = 0; i < 5; i++)
                    pingpongObject.ping(true);
                pingpongObject.ping(false);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                    pingpongObject.pong(true);
                pingpongObject.pong(false);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PingPong pp = new PingPong();
            Console.WriteLine("The Ball is dropped... \n");
            MyThread mythread1 = new MyThread("Ping", pp);
            MyThread mythread2 = new MyThread("Pong", pp);
            mythread1.thread.Join();
            mythread2.thread.Join();
            Console.WriteLine("\nThe Ball Stops Bouncing.");



            Nonatomic_Erro();
            Atomic_Monitor();

            Console.Read();
        }

        static void Nonatomic_Erro()
        {
            int num = 0;
            int length = 500000;
            //Run on separate thread of threadpool
            Task tsk = Task.Run(() =>
            {
                for (int i = 0; i < length; i++)
                {
                    num = num + 1;
                }
            });
            //Run on Main Thread
            for (int i = 0; i < length; i++)
            {
                num = num - 1;
            }
            tsk.Wait();
            Console.WriteLine(num); //-6674 OU o OU 655
        }

        static void Atomic_Monitor()
        {
            int num = 0;
            int length = 500000;
            object _lock = new object();
            //Run on separate thread of threadpool
            Task tsk = Task.Run(() =>
            {
                for (int i = 0; i < length; i++)
                {
                    //lock the block of code
                    Monitor.Enter(_lock);

                    try
                    {
                        num = num + 1;
                    }
                    finally
                    {
                        //unlock the locked code
                        Monitor.Exit(_lock);
                    }
                }
            });
            //Run on Main Thread
            for (int i = 0; i < length; i++)
            {
                //lock the block of code
                Monitor.Enter(_lock);
                num = num - 1;
                //unlock the locked code
                Monitor.Exit(_lock);
            }
            tsk.Wait();
            Console.WriteLine(num); //0
        }

    }
}
