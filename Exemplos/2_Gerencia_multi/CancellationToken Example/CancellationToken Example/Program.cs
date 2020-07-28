using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationToken_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tarefa_Cancelada_Inicial();
            //Tarefa_Cancelada_AvisoInterno();
            //Tarefa_Cancelada_ContinueWith();
            //Tarefa_Cancelada_Timer();
            Tarefa_OnlyOnCanceled();
            Console.ReadKey();
        }


        static void Tarefa_Cancelada_Inicial()
        {
            //1 - Instantiate a cancellation token source
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                //2 - Get token from CancellationTokenSource.Token property
                CancellationToken token = cts.Token;

                //3 - Pass token to Task
                Task task = Task.Run(() =>
                {
                    //4 - Mecanismo para cada tarefa ou thread para responder ao cancel
                    while (!token.IsCancellationRequested)
                    {
                        Console.Write("*");
                        Thread.Sleep(1000);
                    }
                }, token);

                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();
                //5 - notify for cancellation
                cts.Cancel();
            }

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }


        static void Tarefa_Cancelada_AvisoInterno()
        {
            //1 - Instantiate a cancellation token source
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                //2 - Get token from CancellationTokenSource.Token property
                CancellationToken token = cts.Token;

                //3 - Pass token to Task
                Task task = Task.Run(() =>
                {
                    //4 - Mecanismo para cada tarefa ou thread para responder ao cancel
                    while (!token.IsCancellationRequested)
                    {
                        Console.Write("*");
                        Thread.Sleep(1000);

                        token.ThrowIfCancellationRequested();
                    }
                }, token);

                try
                {
                    Console.WriteLine("Press enter to stop the task");
                    Console.ReadLine();
                    //5 - notify for cancellation
                    cts.Cancel();
                    task.Wait();
                }
                catch (AggregateException e)
                {
                    Console.WriteLine("From AggregateException: " + task.Status);
                    Console.WriteLine(e.InnerExceptions[0].Message);
                }
            };

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }

        static void Tarefa_Cancelada_ContinueWith()
        {
            //1 - Instantiate a cancellation token source
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                //2 - Get token from CancellationTokenSource.Token property
                CancellationToken token = cts.Token;

                //3 - Pass token to Task
                Task task = Task.Run(() =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        Console.Write("*");
                        Thread.Sleep(1000);
                    }
                }, token).ContinueWith((t) =>
                {
                    Console.WriteLine("From Continuation: " + t.Status);
                    Console.WriteLine("You have canceled the task");
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();
                //5 - notify for cancellation
                cts.Cancel();
            };

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }

        static void Tarefa_Cancelada_Timer()
        {
            //1 - Instantiate a cancellation token source
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                //2 - Get token from CancellationTokenSource.Token property
                CancellationToken token = cts.Token;

                //3 - Pass token to Task
                Task task = Task.Run(() =>
                {
                    //4 - Mecanismo para cada tarefa ou thread para responder ao cancel
                    while (!token.IsCancellationRequested)
                    {
                        Console.Write("*");
                        Thread.Sleep(1000);
                    }
                }, token);

                //5 - notify for cancellation
                cts.CancelAfter(5000);
                Thread.Sleep(5000);
                Console.WriteLine();
                Console.WriteLine("Task timed out after 5s: " + task.Status);
            };

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }

        static void Tarefa_OnlyOnCanceled()
        {
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //CancellationToken token = cancellationTokenSource.Token;

            //Task longRunning = Task.Run(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        token.ThrowIfCancellationRequested();
            //        Console.Write("*");
            //        Thread.Sleep(1000);
            //    }
            //}, token);

            //longRunning.ContinueWith((t) =>
            //{
            //    Console.WriteLine("Task was canceled");
            //}, TaskContinuationOptions.OnlyOnCanceled);
            //longRunning.ContinueWith((t) =>
            //{
            //    Console.WriteLine("Task ran successfully");
            //}, TaskContinuationOptions.OnlyOnRanToCompletion);
            //longRunning.ContinueWith((t) =>
            //{
            //    Console.WriteLine("Task faulted");
            //}, TaskContinuationOptions.OnlyOnFaulted);

            //cancellationTokenSource.CancelAfter(1000);


            //1 - Instantiate a cancellation token source
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                //2 - Get token from CancellationTokenSource.Token property
                CancellationToken token = cts.Token;
                //3 - Pass token to Task
                Task task = Task.Run(() =>
                        {
            //4 - Mecanismo para cada tarefa ou thread para responder ao cancel
            while (!token.IsCancellationRequested)
                            {
                                Console.Write("*");
                                Thread.Sleep(1000);

                                token.ThrowIfCancellationRequested();
                            }
                        }, token);

                task.ContinueWith((t) =>
                {
                    Console.WriteLine("From Continuation: " + t.Status);
                    Console.WriteLine("You have canceled the task");
                }, TaskContinuationOptions.OnlyOnCanceled);

                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();
                //5 - notify for cancellation
                cts.Cancel();
            }
        }


        static void Tarefa_Cancelada()
        {
            //1 - Instantiate a cancellation token source
            CancellationTokenSource source = new CancellationTokenSource();
            //2 - Get token from CancellationTokenSource.Token property
            CancellationToken token = source.Token;

            //3 - Pass token to Task
            Task tsk = Task.Run(() =>
            {
                Console.WriteLine("Hello from tsk");
                while (true)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("*");
                    if (token.IsCancellationRequested == true)
                    {
                        Console.WriteLine("Bye from tsk");
                        return;
                    }
                }
            }, token);
            Console.WriteLine("Hello from main thread");
            //Wait
            Thread.Sleep(4000);
            //4 - notify for cancellation
            source.Cancel();
            //IsCancellationRequested = true;
            //Wait
            Thread.Sleep(1000);
            Console.WriteLine("Bye from main thread");

            Console.ReadKey();
        }
    }
}
