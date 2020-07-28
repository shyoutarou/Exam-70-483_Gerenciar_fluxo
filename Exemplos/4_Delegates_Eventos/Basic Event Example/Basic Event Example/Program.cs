using System;

namespace Basic_Event_Example
{
    class Program
    {
        static void Alarm(int temp)
        {
            Console.WriteLine("Delegate. Só imprimir se Temperatura > 60. Temperatura: {0}", temp);
        }

        static void Display()
        {
            Console.WriteLine("Display");
        }

        static void Show()
        {
            Console.WriteLine("Show");
        }

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


            //O EVENTO trata esses 2 problemas dos delegates
            //1.	Qualquer um pode usar um operador de atribuição 
            //      que pode sobrescrever as referências de métodos.
            Console.WriteLine();
            Console.WriteLine("======Problema delegate: Sobrescreve metodo associados======");
            Action act = Display;
            act += Show;
            act += Display;
            act();

            Action act_erro = Display;
            act_erro += Show;

            Console.WriteLine("Aqui não tem o +");
            act_erro = Display;
            act_erro();

            //2.	O delegado pode ser chamado em qualquer lugar do código, 
            //      o que pode violar a regra do encapsulamento.
            Console.WriteLine();
            Console.WriteLine("======Problema delegate: Pode ser chamado em qualquer lugar do código======");

            Room room = new Room();
            room.OnHeatAlert = Alarm;

            // Instancia um objeto de assinante de evento
            Room_Subscriber room_subscriber = new Room_Subscriber();
            room.EventName += room_subscriber.OnMethodName;
            room.EventName += room_subscriber.OnShow;
            room.EventName += room_subscriber.OnDisplay;
            room.EventName += room_subscriber.OnShow;

            //NÃO é possível sobrescrever as referências de métodos.
            //room.EventName = room_subscriber.OnShow;

            // OnHeatAlert será chamado
            room.Temperature = 90;
            room.Temperature = 15;
            // OnHeatAlert será chamado novamente o que não deveria ocorrer
            // porque o quarto não está quente (temp> 60) no set do valor Temperature
            // Temperature é propriedade de Room. Delegado é chamado fora da classe Room
            room.OnHeatAlert(room.Temperature);

            Console.WriteLine("======RESOLVIDO Problemas de delegate COM EVENTOS======");

            Console.ReadKey();
        }

        private class Room
        {
            public Action<int> OnHeatAlert;

            public event Action<object> EventName;

            private int temp;
            public int Temperature
            {
                get { return this.temp; }
                set
                {
                    temp = value;
                    if (temp > 60)
                    {
                        if (OnHeatAlert != null)
                        {
                            OnHeatAlert(temp);
                        }

                        if (EventName != null)
                        {
                            EventName(this);
                        }
                    }
                }
            }
        }

        class Room_Subscriber
        {
            //Metodo ao ser adicionado ao evento/delegate
            public void OnMethodName(object o)
            {
                Room room = (Room)o;
                Console.WriteLine("Evento. Só imprimir se Temperatura > 60. Temperatura: {0}", room.Temperature);
            }

            public void OnDisplay(object o)
            {
                Room room = (Room)o;
                Console.WriteLine("Display Temperatura: {0}", room.Temperature);
            }

            public void OnShow(object o)
            {
                Room room = (Room)o;
                Console.WriteLine("Show Temperatura: {0}", room.Temperature);
            }
        }
    }

    // Cria um delegado que nosso evento usará para
    // aponte para o método que manipulará nosso evento
    delegate void del_evt_handler(string x);
    class Classe_Publisher
    {
        // Cria um evento que usa o del delegate que acabamos de criar
        // Observe que nosso evento pode apontar para qualquer método
        // que retorna nulo e espera um parâmetro de string
        public event del_evt_handler EventName;

        public void ChecarAlgo(int x)
        {
            if (x > 250)
                // Vamos levantar o evento chamando o delegado
                // executará todos os métodos que lhe foram inscritos
                //Repare que a assinatura do delegate del_evt(string x)
                //tem os mesmos parametros de  ControlaEvento(string a)
                EventName("ATENÇÃO! O valor digitado é superior a 250 ...");
        }
    }

    class Classe_Subscriber
    {
        //Metodo ao ser adicionado ao evento/delegate
        public void OnMethodName(string a)
        {
            Console.WriteLine(a);
        }
    }

}
