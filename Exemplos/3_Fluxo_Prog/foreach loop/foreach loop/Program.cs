using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foreach_loop
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            loop_through_array();

            CannotChangeForeachIterationVariable();
            ChangeForeachIterationVariable();

            Console.ReadLine();
        }

        static void loop_through_array()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            foreach (int i in values)
            {
                Console.Write(i);
            }
        }

        class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        static void CannotChangeForeachIterationVariable()
        {
            var people = new List<Person>
                {
                new Person() { FirstName ="John", LastName ="Doe"},
                new Person() { FirstName ="Jane", LastName = "Doe"},
                };
            foreach (Person p in people)
            {
                p.LastName = "Changed"; // This is allowed
                                        //p = new Person(); // This gives a compile error
            }
        }

        static void ChangeForeachIterationVariable()
        {
            var people = new List<Person>{
            new Person() { FirstName ="John", LastName ="Doe"},
            new Person() { FirstName ="Jane", LastName = "Doe"}};

            List<Person>.Enumerator e = people.GetEnumerator();
            try
            {
                Person v;
                while (e.MoveNext())
                {
                    v = e.Current;
                }
            }
            finally
            {
                System.IDisposable d = e as System.IDisposable;
                if (d != null) d.Dispose();
            }
        }
    }
}
