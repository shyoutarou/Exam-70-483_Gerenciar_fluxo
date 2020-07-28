using System;
using System.ComponentModel;

namespace EventArgs_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instancia um objeto publicador de evento
            Classe_Publisher publisher = new Classe_Publisher();

            // Instancia um objeto de assinante de evento
            Classe_Subscriber subscriber = new Classe_Subscriber();

            // Inscreva no evento publisher.EventName criando o método subscriber.OnMethodName
            // um destino do delegado publisher.EventName
            publisher.EventName += subscriber.OnMethodName;

            // Chama o método ChecarAlgo no objeto publisher
            // chamará o delegado publisher.EventName se o saldo for superior a 250
            publisher.ChecarAlgo(251);


            Console.WriteLine();
            Console.WriteLine("======TESTE: classe Room=====");

            Room room = new Room();

            // Instancia um objeto de assinante de evento
            Room_Subscriber room_subscriber = new Room_Subscriber();
            room.EventName += room_subscriber.OnMethodName;
            room.EventName += room_subscriber.OnShow;
            room.EventName += room_subscriber.OnDisplay;

            room.Temperature = 90;
            room.Temperature = 15;


            Console.WriteLine();
            Console.WriteLine("======TESTE: classe Person PropertyChangedEventHandler=====");

            Person person = new Person();
            person.PropertyChanged += OnPropertyChanged;
            person.PersonName = "Ali";


            Console.ReadKey();
        }

        private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Person person = (Person)sender;
            Console.WriteLine("Property [{0}] has a new value = [{1}]",
            e.PropertyName, person.PersonName);
        }

        class HotelData : EventArgs
        {
            public string HotelName { get; set; }
            public int TotalRooms { get; set; }
        }

        private class Room
        {
            public event EventHandler EventName;

            private int temp;
            public int Temperature
            {
                get { return this.temp; }
                set
                {
                    temp = value;
                    if (temp > 60)
                    {
                        if (EventName != null)
                        {
                            HotelData data = new HotelData
                            {
                                HotelName = "5 Star Hotel",
                                TotalRooms = 450
                            };
                            //Pass event data
                            EventName(this, data);
                        }
                    }
                }
            }
        }

        class Room_Subscriber
        {
            //Metodo ao ser adicionado ao evento/delegate
            public void OnMethodName(object o, EventArgs e)
            {
                Room room = (Room)o;
                HotelData data = (HotelData)e;
                Console.WriteLine("Evento. Só imprimir se Temperatura > 60. Temperatura: {0}", room.Temperature);
                Console.WriteLine("{0} has total {1} rooms", data.HotelName, data.TotalRooms);
            }

            public void OnDisplay(object o, EventArgs e)
            {
                Room room = (Room)o;
                Console.WriteLine("Display Temperatura: {0}", room.Temperature);
            }

            public void OnShow(object o, EventArgs e)
            {
                Room room = (Room)o;
                Console.WriteLine("Show Temperatura: {0}", room.Temperature);
            }
        }
    }

    public class Classe_Publisher
    {
        //EventHandler com Generics, EvtArgsClass
        public event EventHandler<EvtArgsClass> EventName;

        public void ChecarAlgo(int x)
        {
            // Para passar EventArgs, precisamos criar uma classe que herda de EventArgs
            if (x > 250)
            {
                EvtArgsClass eac = new EvtArgsClass("Balance exceeds 250...");

                EventName(this, eac);
            }
        }
    }

    public class Classe_Subscriber
    {
        public void OnMethodName(object sender, EvtArgsClass e)
        {
            Console.WriteLine("ATTENTION! from " + sender + ": " + e.Message);
        }
    }

    public class EvtArgsClass : EventArgs
    {
        // Construtor aceita mensagem quando um
        // O objeto EvtArgsClass é instanciado
        public EvtArgsClass(string str)
        {
            msg = str;
        }

        private string msg;
        public string Message
        {
            get { return msg; }
        }
    }

    public class Person : INotifyPropertyChanged
    {
        private string name;
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        public Person()
        {
        }
        public Person(string value)
        {
            this.name = value;
        }
        public string PersonName
        {
            get { return name; }
            set
            {
                name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("PersonName");
            }
        }
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
