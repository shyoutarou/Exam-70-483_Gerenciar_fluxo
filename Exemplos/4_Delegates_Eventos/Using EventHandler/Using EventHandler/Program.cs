using System;

namespace Using_EventHandler
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

            Console.ReadKey();
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
                            EventName(this, EventArgs.Empty);
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
                Console.WriteLine("Evento. Só imprimir se Temperatura > 60. Temperatura: {0}", room.Temperature);
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
        // Cria um delegado que nosso evento usará para
        // aponte para o método que manipulará nosso evento
        // public delegate void del_evt_handler(string x);

        // Cria um evento que usa o del delegate que acabamos de criar
        // Observe que nosso evento pode apontar para qualquer método
        // que retorna nulo e espera um parâmetro de string
        //public event del_evt_handler EventName;

        // Let's use the standard .NET event handler
        public EventHandler EventName;  

        public void ChecarAlgo(int x)
        {
            if (x > 250)
                // Vamos levantar o evento chamando o delegado
                // executará todos os métodos que lhe foram inscritos
                // evt("ATTENION! The curren balance is over 250...");

                // O EVENTHANDLER espera um valor de EventArgs
                EventName(this, EventArgs.Empty);
        }

    }


    public class Classe_Subscriber
    {
        // Teremos que mudar isso para corresponder à nova chamada de delegado
        // public void HandleTheEvent(string a)
        public void OnMethodName(object sender, EventArgs e)
        {
            Console.WriteLine("ATTENTION! " + sender + " is advising that the balance is over 250...");
        }

    }
}
