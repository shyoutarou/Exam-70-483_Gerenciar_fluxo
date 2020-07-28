using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_ContinueWith
{
    class Program
    {
        static void Main(string[] args)
        {
            //TarefaFactory_ContinueWith();
            Tarefa_ContinueWith();
            Console.ReadKey();
        }

        static void Tarefa_ContinueWith()
        {
            // A tarefa antecedente. Pode tambem ser criada com Task.Factory.StartNew ou Task.Run
            Task<DayOfWeek> tarefaA = new Task<DayOfWeek>(() => DateTime.Today.DayOfWeek);
            Task<DayOfWeek> tarefaB = Task<DayOfWeek>.Run(() => DateTime.Today.AddDays(1).DayOfWeek);

            // A continuacao. Seu delegate toma a tarefa antecedente
            // como um argumento e pode retornar um tipo diferente
            Task<string> continuacao = tarefaA.ContinueWith((antecedent) =>
            {
                return String.Format("Hoje é {0} ", antecedent.Result);
            });

            // Iniciar a antecedente
            tarefaA.Start();
            //Task.WhenAll(tarefaA, tarefaB);
            Task.WaitAll(tarefaA, tarefaB);
            
            // Usar o resultada da continuacao
            Console.WriteLine(continuacao.Result + " e Amanhã é " + tarefaB.Result);
        }

        static void TarefaFactory_ContinueWith()
        {
            Task tarefaFactory = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                Console.WriteLine("tsk1");
            });

            // A continuacao. Seu delegate toma a tarefa antecedente
            // como um argumento e pode retornar um tipo diferente
            Task continuacao = tarefaFactory.ContinueWith((antecedent) =>
            {
                Thread.Sleep(500);
                Console.WriteLine("tsk2");
            });

            continuacao.Wait();
        }
    }
}
