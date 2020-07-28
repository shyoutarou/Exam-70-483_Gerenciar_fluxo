using System;
using System.IO;

namespace contravariantes_Ex
{
    class Program
    {
        public delegate void ContravarianceDel(StreamWriter tw);

        static void Main(string[] args)
        {
            Console.WriteLine("=========Contra-Covariance:Action============");

            Action<object> actObject = SetObject;
            // Um objeto que é instanciado com um argumento de tipo menos derivado
            // é atribuído a um objeto instanciado com um argumento de tipo mais derivado.
            // A compatibilidade da atribuição é revertida.
            Action<string> actString = actObject;
            Console.WriteLine("actObject=" + actObject.GetType()); // actObject = System.Action`1[System.Object]
            Console.WriteLine("actString=" + actString.GetType()); // actString = System.Action`1[System.Object]

            ContravarianceDel del = DoSomething;

            Console.ReadKey();
        }

        static void SetObject(object obj) { }

        static void DoSomething(TextWriter tw) { }
    }
}
