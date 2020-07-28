using System;
using System.Collections.Generic;
using System.IO;

namespace covariantes_Exemplo
{
    class Program
    {
        public delegate TextWriter CovarianceDel();

        static void Main(string[] args)
        {
            Console.WriteLine("=========Contra-Covariance:Variaveis_List============");

            // Compatibilidade da atribuição.
            string str = "teste";
            // Um objeto de um tipo mais derivado (str) é atribuído a um objeto de um tipo menos derivado (obj).
            object obj = str;
            Console.WriteLine("obj=" + obj); //obj = teste
            Console.WriteLine("str=" + str); //str = teste

            IEnumerable<string> strings = new List<string>();
            IEnumerable<object> objetos = strings;
            // strings=System.Collections.Generic.List`1[System.String]
            Console.WriteLine("strings=" + strings.GetType());
            // objetos = System.Collections.Generic.List`1[System.String].
            Console.WriteLine("objetos=" + objetos.GetType());

            // Um delegado especifica um tipo de retorno como objeto,
            // mas você pode atribuir um método que retorna uma string.
            Func<object> funcdel = GetString;

            Console.WriteLine("=========A covariância para matrizes ============");

            object[] array = new String[10];
            // The following statement produces a run-time exception.  
            //System.ArrayTypeMismatchException: 'Tentativa de acesso a 
            //um elemento como um tipo incompatível com a matriz.'
            //array[0] = 10;


            CovarianceDel del;
            del = MethodStream;
            del = MethodString;

            Console.ReadKey();
        }

        static string GetString() { return ""; }

        public static StreamWriter MethodStream() { return null; }
        public static StringWriter MethodString() { return null; }
    }
}
