using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_WhenAllAny
{
    class Program
    {
        static void Main(string[] args)
        {
            Tarefa_ContinueWhenAll();
            Tarefa_ContinueWhenAny();
            Console.ReadKey();
        }

        static void Tarefa_ContinueWhenAll()
        {
            Task<int>[] tarefas = new Task<int>[2];
            tarefas[0] = new Task<int>(() =>
            {
                Thread.Sleep(500); // faz alguma coisa...  
                return 34;
            });

            tarefas[1] = new Task<int>(() =>
            {
                Thread.Sleep(200); // faz alguma coisa...  
                return 8;
            });

            var continuation = Task.Factory.ContinueWhenAll(
                            tarefas,
                            (antecedents) =>
                            {
                                int resposta = tarefas[0].Result + tarefas[1].Result;
                                Console.WriteLine("A resposta é {0}", resposta);
                            });

            tarefas[0].Start();
            tarefas[1].Start();

            //Lança Erro: InvalidOperationException
            //Estado é WaitingForActivation e só ser iniciada
            //pela tarefa antecedente
            //continuation.Start();

            //Task.WhenAll(tarefas[0], tarefas[1]);
            continuation.Wait();
        }

        static void Tarefa_ContinueWhenAny()
        {
            Task step1Task = Task.Run(() => Step1());
            Task step2Task = Task.Run(() => Step2());
            Task step3Task = Task.Factory.ContinueWhenAny(
                new Task[] { step1Task, step2Task },
                (previousTask) => Step3());

            step3Task.Wait();
        }

        static void Step1() { Thread.Sleep(1000); Console.WriteLine("Step1"); }
        static void Step2() { Thread.Sleep(200); Console.WriteLine("Step2"); }
        static void Step3() { Thread.Sleep(800); Console.WriteLine("Step3"); }
    }
}
