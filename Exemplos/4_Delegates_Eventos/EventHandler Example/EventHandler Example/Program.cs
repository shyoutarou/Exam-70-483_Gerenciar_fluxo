using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandler_Example
{
    public class MyArgs : EventArgs
    {
        public MyArgs(int value)
        {
            Value = value;
        }
        public int Value { get; set; }
    }

    public class Publisher
    {
        public event EventHandler<MyArgs> ChangeEventHandler = delegate { };
        public void Raise()
        {
            var exceptions = new List<Exception>();

            foreach (Delegate handler in ChangeEventHandler.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(this, new MyArgs(42));
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
                throw new AggregateException(exceptions);
        }
    }

    public class Publisher_Lock
    {
        private event EventHandler<MyArgs> _onChange = delegate { };
        public event EventHandler<MyArgs> ChangeEventHandler
        {
            add
            {
                lock (_onChange)
                {
                    _onChange += value;
                }
            }
            remove
            {
                lock (_onChange)
                {
                    _onChange -= value;
                }
            }
        }
        public void Raise()
        {
            _onChange(this, new MyArgs(42));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CreateAndRaise();
            CreateAndRaiseError();
            Console.ReadKey();
        }

        public static void CreateAndRaise()
        {
            Publisher p = new Publisher();
            p.ChangeEventHandler += (sender, e) => Console.WriteLine("Event raised: {0}", e.Value);
            p.Raise();
        }

        public static void CreateAndRaiseError()
        {
            Publisher p = new Publisher();
            p.ChangeEventHandler += (sender, e) => Console.WriteLine("Subscriber 1 called");
            p.ChangeEventHandler += (sender, e) => { throw new Exception(); };
            p.ChangeEventHandler += (sender, e) => Console.WriteLine("Subscriber 3 called");

            try
            {
                p.Raise();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions.Count);
            }
        }
    }
}
