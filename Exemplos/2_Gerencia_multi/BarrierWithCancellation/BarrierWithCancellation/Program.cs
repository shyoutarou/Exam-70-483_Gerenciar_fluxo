﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierWithCancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            var participants = 5;
            // We create a CancellationTokenSource to be able to initiate the cancellation
            var tokenSource = new CancellationTokenSource();
            // We create a barrier object to use it for the rendez-vous points
            var barrier = new Barrier(participants,
            b =>
            {
                Console.WriteLine("{0} paricipants are at rendez-vous point {1}.",
                                    b.ParticipantCount,
                                    b.CurrentPhaseNumber);
            });
            for (int i = 0; i < participants; i++)
            {
                var localCopy = i;
                Task.Run(() =>
                {
                    Console.WriteLine("Task {0} left point A!", localCopy);
                    Thread.Sleep(1000 * localCopy + 1); // Do some "work"
        if (localCopy % 2 == 0)
                    {
                        Console.WriteLine("Task {0} arrived at point B!", localCopy);
                        barrier.SignalAndWait(tokenSource.Token);
                    }
                    else
                    {
                        Console.WriteLine("Task {0} changed its mind and went back!",
                        localCopy);
                        barrier.RemoveParticipant();
                        return;
                    }
                    Thread.Sleep(1000 * localCopy + 1);
                    Console.WriteLine("Task {0} arrived at point C!", localCopy);
                    barrier.SignalAndWait(tokenSource.Token);
                });
            }
            Console.WriteLine("Main thread is waiting for {0} tasks!",
            barrier.ParticipantsRemaining - 1);
            Console.WriteLine("Press enter to cancel!");
            Console.ReadLine();
            if (barrier.CurrentPhaseNumber < 2)
            {
                tokenSource.Cancel();
                Console.WriteLine("We canceled the operation!");
            }
            else
            {
                Console.WriteLine("Too late to cancel!");
            }
            Console.WriteLine("Main thread is done!");

            Console.ReadKey();
        }
    }
}
