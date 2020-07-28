using System;

namespace Publisher_Subscriber
{
    delegate void del_evt_handler(string x);

    class Classe_Publisher
    {
        //Declare an event
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

            Console.ReadKey();

        }
    }
}
