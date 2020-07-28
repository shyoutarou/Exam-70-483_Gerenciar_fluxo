using System;
using System.Linq;

namespace PLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(0, 50);

            try
            {

                var parallelResult = numbers.AsParallel().WithDegreeOfParallelism(10)
                .Where(i => IsEven(i));
                parallelResult.ForAll(e => Console.Write(e + ", "));

            }
            catch (AggregateException e)
            {
                Console.WriteLine("There where {0} exceptions",
                                    e.InnerExceptions.Count);
            }

            var parallelOrdered = numbers.AsParallel().AsOrdered()
                .Where(i => i % 2 == 0).AsSequential();

            foreach (int i in parallelOrdered.Take(5))
                Console.WriteLine(i);

            Console.ReadKey();
        }

        public static bool IsEven(int i)
        {
            //Descomente essa linha para ver o funcionamento de AggregateException
            //if (i % 10 == 0) throw new ArgumentException("i");
            return i % 2 == 0;
        }
    }
}
