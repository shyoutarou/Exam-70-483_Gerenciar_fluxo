using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemBarriers_Volatility
{
    class Foo_Partial
    {
        int _answer1, _answer2, _answer3;
        bool _complete;

        void A()
        {
            _answer1 = 1; _answer2 = 2; _answer3 = 3;
            Thread.MemoryBarrier();
            _complete = true;
            Thread.MemoryBarrier();
        }

        void B()
        {
            Thread.MemoryBarrier();
            if (_complete)
            {
                Thread.MemoryBarrier();
                Console.WriteLine(_answer1 + _answer2 + _answer3);
            }
        }
    }

    class Foo
    {
        public int _answer;
        public bool _complete;

        public void A()
        {
            _answer = 123;
            //Thread.Sleep(200);
            _complete = true;
        }

        public void B()
        {
            if (_complete)
            {
                //Thread.Sleep(500);
                Console.WriteLine(_answer);
            }
        }


        public void MemoryBarrier_A()
        {
            _answer = 123;
            Thread.MemoryBarrier();    // Barrier 1
            _complete = true;
            Thread.MemoryBarrier();    // Barrier 2
        }

        public void MemoryBarrier_B()
        {
            Thread.MemoryBarrier();    // Barrier 3
            if (_complete)
            {
                Thread.MemoryBarrier();       // Barrier 4
                Console.WriteLine(_answer);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Exemplo_Erro();
        }

        static void Exemplo_Erro()
        {
            var count = 0;
            bool complete = false;
            var t = new Thread(() =>
            {
                bool toggle = false;
                while (!complete)
                {
                    Console.WriteLine(count++);
                    toggle = !toggle;
                }
            });
            t.Start();
            Thread.Sleep(3000);
            complete = true;
            t.Join();        // Blocks indefinitely

            Console.WriteLine("Fim do programa");
            Console.ReadKey();
        }

        static void Exemplo_Barreira()
        {

            Foo teste = new Foo();

            var phoneOrders = new Queue<string>();
            Task t1 = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    //teste._answer = 0;
                    //teste._complete = false;
                    //Thread.Sleep(200);
                    teste.A();
                }
            });

            Task t2 = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    //Thread.Sleep(100);
                    teste.B();
                }
            });
            Task.WaitAll(t2, t1);

            Console.ReadKey();
        }
    }
}
