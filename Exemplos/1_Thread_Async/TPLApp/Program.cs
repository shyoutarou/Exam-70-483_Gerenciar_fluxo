using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TPLApp
{
    class Program
    {

        const int NUMBER_OF_ITERATIONS = 32;

        static void Main(string[] args)
        {


            // We are using Stopwatch to time the code
            Stopwatch sw = Stopwatch.StartNew();

            // Run the method
            RunParallelForCorrected();

            // Print the time it took to run the application.
            Console.WriteLine("We're done in {0}ms!", sw.ElapsedMilliseconds);

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to continue . . .");
                Console.ReadKey(true);
            }
        }

        static void RunSequencial()
        {

            double result = 0d;

            // Here we call same method several times. 
            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {
                result += DoIntensiveCalculations();
            }

            // Print the result
            Console.WriteLine("The result is {0}", result);

        }

        static void RunParallelFor()
        {
            double result = 0d;

            // Here we call same method several times in parallel. 
            Parallel.For(0, NUMBER_OF_ITERATIONS, i =>
            {
                result += DoIntensiveCalculations();
            });

            // Print the result
            Console.WriteLine("The result is {0}", result);
        }


        static void RunTasks()
        {

            double result = 0d;

            Task[] tasks = new Task[NUMBER_OF_ITERATIONS];
            // We create one task per iteration. 
            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {
                tasks[i] = Task.Run(() => result += DoIntensiveCalculations());
            }

            // We wait for the tasks to finish
            Task.WaitAll(tasks);

            //// We collect the results
            //foreach (var task in tasks) {
            //    result += task.Result;
            //}

            // Print the result
            Console.WriteLine("The result is {0}", result);
        }

        static void RunTasksCorrected()
        {

            double result = 0d;

            Task<double>[] tasks = new Task<double>[NUMBER_OF_ITERATIONS];

            // We create one task per iteration. 
            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {
                tasks[i] = Task.Run(() => DoIntensiveCalculations());
            }

            // We wait for the tasks to finish
            //Task.WaitAll(tasks);

            // We collect the results
            foreach (var task in tasks)
            {
                result += task.Result;
            }

            // Print the result
            Console.WriteLine("The result is {0}", result);
        }

        //Sobrecarga de Parallel.For
        //public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)

        static void RunParallelForCorrected()
        {

            ParallelLoopResult loopparalelo = Parallel.
            For(0, 1000, (int i, ParallelLoopState loopState) =>
            {
                if (i == 500)
                {
                    Console.WriteLine("Breaking loop");
                    loopState.Break();
                }
                return;
            });

            Console.WriteLine("IsCompleted: {0}", loopparalelo.IsCompleted);
            Console.WriteLine("LowestBreakIteration: {0}", loopparalelo.LowestBreakIteration);



            double result = 0d;

            // Here we call same method several times. 
            //for (int i = 0; i < NUMBER_OF_ITERATIONS; i++) 
            Parallel.For(0, NUMBER_OF_ITERATIONS,
                // Interim resul = 0d
                () => 0d,

                //    result += DoIntensiveCalculations();
                (i, state, interimResult) => interimResult + DoIntensiveCalculations(),

                // Final step after the calculations 
                // we add the result to the final result
                (lastInterimResult) => result += lastInterimResult
            );
            // Print the result
            Console.WriteLine("The result is {0}", result);


            int[] data = { 1, 2, 3, 4, 5 };
            Parallel.ForEach<int>(data, (d) =>
            {
                Console.WriteLine(d);
            });
        }

        static double DoIntensiveCalculations()
        {
            // We are simulating intensive calculations 
            // by doing nonsens divisions 
            double result = 100000000d;
            var maxValue = Int32.MaxValue;
            for (int i = 1; i < maxValue; i++)
            {
                result /= i;
            }

            return result + 10d;
        }
    }
}
