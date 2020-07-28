using System;

namespace Pub_Event
{
    public class Publisher
    {
        public Action OnChange { get; set; }
        public Action<string> OnChangeParam;
        public Func<string, bool> OnChangeFunc;

        public void Raise(string str)
        {
            if (OnChangeParam != null)
            {
                OnChangeParam(str);
            }
        }
    }

    public class Publisher_Event
    {
        public event Action ActionEventHandler = delegate { };
        public event Action<string> ActionParEventHandler;
        public event Func<string, bool> FuncEventHandler = delegate (string str) { return str == "2"; };

        public void Raise()
        {
            ActionEventHandler();
        }

        public void Raise(string str)
        {
            ActionParEventHandler(str);
            var result = FuncEventHandler(str);
            Console.WriteLine("result:" + result);
        }
    }

    public class Subscriber
    {
        //Metodo ao ser adicionado ao evento/delegate
        public void OnMethodName(string a)
        {
            Console.WriteLine("Event raised to method " + a);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CreateAndRaise();
            CreateAndRaise_Event();

            Console.ReadKey();
        }

        public static void CreateAndRaise()
        {
            Publisher publisher = new Publisher();
            Subscriber subscriber = new Subscriber();

            publisher.OnChangeParam += subscriber.OnMethodName;
            publisher.OnChangeParam += subscriber.OnMethodName;
            // Sobrescreveu os outros métodos atribuídos
            //publisher.OnChangeParam = (str) => Console.WriteLine("Event raised to method 3" + str);

            //// Atribuiu null e chamou diretamente o metodo/delegate
            //publisher.OnChangeParam = null;
            //publisher.OnChangeParam(null);

            publisher.Raise("CreateAndRaise");
        }

        public static void CreateAndRaise_Event()
        {
            // Instancia um objeto publicador de evento e de assinante de evento
            Publisher_Event publisher = new Publisher_Event();
            Subscriber subscriber = new Subscriber();

            publisher.ActionParEventHandler += subscriber.OnMethodName;
            publisher.ActionParEventHandler += subscriber.OnMethodName;

            //Não deixa sobrescrever os métodos
            //publisher.ActionParEventHandler = subscriber.OnMethodName;

            //Não deixa chamar diretamente o delegate E
            //publisher.ActionParEventHandler("4");

            publisher.Raise("CreateAndRaise_Event");
        }

    }
}
