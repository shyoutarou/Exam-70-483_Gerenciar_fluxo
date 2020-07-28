using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problemas_Delegates
{
    public class Room
    {
        public Action<int> OnHeatAlert;
        int temp;
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
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

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
            // OnHeatAlert será chamado
            room.Temperature = 90;
            room.Temperature = 15;
            // OnHeatAlert será chamado novamente o que não deveria ocorrer
            // porque o quarto não está quente (temp> 60) no set do valor Temperature
            // Temperature é propriedade de Room. Delegado é chamado fora da classe Room
            room.OnHeatAlert(room.Temperature);

            Console.ReadKey();
        }

        static void Alarm(int temp)
        {
            Console.WriteLine("Turn On AC, Its hot. Room temp is {0}", temp);
        }

        static void Display()
        {
            Console.WriteLine("Display");
        }

        static void Show()
        {
            Console.WriteLine("Show");
        }
    }
}
