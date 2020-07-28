using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Async_CafedaManha
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("comecou");

            RodarMetodos().Wait();

            Console.ReadKey();
        }

        static async Task RodarMetodos()
        {
            await MainAsync();
            MainSync();
        }

        static void MainSync()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            EncherXicara(stopwatch);

            Console.WriteLine();
            Console.WriteLine("XICARA CHEIA!" + stopwatch.ElapsedMilliseconds);

            FritarOvos(stopwatch);

            Console.WriteLine();
            Console.WriteLine("OVOS PRONTOS!" + stopwatch.ElapsedMilliseconds);

            FritarBacon(stopwatch);

            Console.WriteLine();
            Console.WriteLine("BACON PRONTO!" + stopwatch.ElapsedMilliseconds);

            TorrarPao(stopwatch);

            PassarManteiga("tarefas[0]TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds);
            PassarGeleia("tarefas[1]TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds);

            Console.WriteLine();
            Console.WriteLine("finishedTORRADA PRONTA!" + stopwatch.ElapsedMilliseconds);

            stopwatch.Stop();

            Console.WriteLine("Cafe da manha PRONTO:" + stopwatch.ElapsedMilliseconds);
        }

        private static void EncherXicara(Stopwatch stopwatch)
        {
            //Thread.Sleep(TimeSpan.FromSeconds(3));
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("EncherXicara:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Start();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.Write("Encher Xicara...");
            }
        }

        private static void FritarOvos(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("FritarOvos:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(500);
                Console.Write("Mexe no Ovo... ");
            }
        }


        private static void FritarBacon(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("FritarBacon:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(200);
                Console.Write("Fritar Bacon... ");
            }
        }

        private static void TorrarPao(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("TorrarPao:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Start();

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(700);
                Console.Write("Torrar Pao... ");
            }
        }

        private static void PassarManteiga(string msg)
        {
            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(1000);
                Console.Write("Passar Manteiga... ");
            }
        }

        private static void PassarGeleia(string msg)
        {
            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(500);
                Console.Write("Passar Geleia... ");
            }
        }


        static async Task MainAsync()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var Cafe = EncherXicaraAsync(stopwatch);
            var Ovos = FritarOvosAsync(stopwatch);
            var Bacons = FritarBaconAsync(stopwatch);
            var Torrada = TorrarPaoAsync(stopwatch);

            var allTasks = new List<Task> { Cafe, Ovos, Bacons, Torrada };

            using (var tokenSource = new CancellationTokenSource())
            {
                while (allTasks.Any())
                {
                    Task finished = await Task.WhenAny(allTasks);
                    if (finished == Cafe)
                    {
                        Console.WriteLine();
                        Console.WriteLine("XICARA CHEIA!" + stopwatch.ElapsedMilliseconds);
                        Console.WriteLine();
                    }
                    else if (finished == Ovos)
                    {
                        Console.WriteLine();
                        Console.WriteLine("OVOS PRONTOS!" + stopwatch.ElapsedMilliseconds);
                        Console.WriteLine();
                    }
                    else if (finished == Bacons)
                    {
                        Console.WriteLine();
                        Console.WriteLine("BACON PRONTO!" + stopwatch.ElapsedMilliseconds);
                        Console.WriteLine();
                    }
                    else if (finished == Torrada)
                    {
                        try
                        {
                            CancellationToken token = tokenSource.Token;

                            Task[] tarefas = new Task[2];

                            tarefas[0] = PassarManteigaAsync("tarefas[0]TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds, token);
                            tarefas[1] = PassarGeleiaAsync("tarefas[1]TORRADA PRONTA!" + stopwatch.ElapsedMilliseconds, token);
                        }
                        catch (TaskCanceledException)
                        {
                            Console.WriteLine("Task was cancelled");
                        }

                        Console.WriteLine();
                        Console.WriteLine("finishedTORRADA PRONTA!" + stopwatch.ElapsedMilliseconds);
                    }

                    allTasks.Remove(finished);

                }

                if (allTasks.Count == 0)
                {
                    // Cancel the task
                    tokenSource.Cancel();
                }
            }

            stopwatch.Stop();

            Console.WriteLine("Cafe da manha PRONTO:" + stopwatch.ElapsedMilliseconds);
        }

        private static async Task<Stopwatch> EncherXicaraAsync(Stopwatch stopwatch)
        {
            //await Task.Delay(TimeSpan.FromSeconds(3));
            stopwatch.Stop();
            Console.WriteLine("EncherXicara:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Start();

            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(500);
                    Console.Write("Encher Xicara...");
                }
            });

            return stopwatch;
        }

        private static async Task<Stopwatch> FritarOvosAsync(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("FritarOvos:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Start();

            await Task.Run(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    Thread.Sleep(500);
                    Console.Write("Mexe no Ovo... ");
                }
            });

            return stopwatch;
        }

        private static async Task<Stopwatch> FritarBaconAsync(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("FritarBacon:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Start();

            await Task.Run(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    Thread.Sleep(200);
                    Console.Write("Fritar Bacon... ");
                }
            });

            return stopwatch;
        }

        private static async Task<Stopwatch> TorrarPaoAsync(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("TorrarPao:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Start();

            await Task.Run(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    Thread.Sleep(700);
                    Console.Write("Torrar Pao... ");
                }
            });

            return stopwatch;
        }

        private static async Task PassarManteigaAsync(string msg, CancellationToken token)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("In iteration {0}, cancellation has been requested...",
                                          i + 1);
                        break;
                    }

                    Thread.Sleep(1000);
                    Console.Write("Passar Manteiga... ");
                }
            });
        }

        private static async Task PassarGeleiaAsync(string msg, CancellationToken token)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("In iteration {0}, cancellation has been requested...",
                                          i + 1);
                        break;

                    }

                    Thread.Sleep(500);
                    Console.Write("Passar Geleia... ");
                }
            });
        }
    }
}
