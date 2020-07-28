using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_Delegate
{
    public class Usuario
    {
        public string Nome { get; set; }
        public int Idade { get; set; }

        public Usuario(string nome, int idade)
        {
            this.Nome = nome;
            this.Idade = idade;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======Comparison Nome Usuarios List======");

            Comparison<Usuario> UsuariosComparer = new Comparison<Usuario>(CompareUsuarios);

            List<Usuario> users = new List<Usuario>()
            {
                new Usuario("Romeu", 10),
                new Usuario("Julieta", 90),
                new Usuario("Hamlet", 55)
            };

            users.Sort(UsuariosComparer);

            foreach (var p in users)
            {
                Console.WriteLine("Nome = " + p.Nome + " e Idade = " + p.Idade);
            }

            Console.WriteLine();
            Console.WriteLine("======Comparison Anonima Idade Usuarios List======");

            Comparison<Usuario> AnomIdadeComparer = new Comparison<Usuario>(delegate (Usuario u1, Usuario u2)
            {
                return u2.Idade.CompareTo(u1.Idade);
            });

            users.Sort(AnomIdadeComparer);

            foreach (var p in users)
            {
                Console.WriteLine("Nome = " + p.Nome + " e Idade = " + p.Idade);
            }

            Console.WriteLine();
            Console.WriteLine("======Comparison Lambda Lenght Usuarios Array======");

            // Repare que é utilizado o objeto users com o ConvertAll
            List<string> nomes = users.ConvertAll<string>(new Converter<Usuario, string>(ConvertUsuario_ToString));

            var array_num = nomes.ToArray();

            // Use lambda to sort on length.
            Array.Sort(array_num, (a, b) => a.Length.CompareTo(b.Length));

            // Write result.
            Console.WriteLine("RESULT: {0}", string.Join(";", array_num));

            Console.ReadKey();
        }

        public static string ConvertUsuario_ToString(Usuario user)
        {
            return user.Nome;
        }

        private static int CompareUsuarios(Usuario e1, Usuario e2)
        {
            var comparar = e1.Nome.Length.CompareTo(e2.Nome.Length);
            Console.WriteLine(e1.Nome + " CompareTo " + e2.Nome + ":" + comparar);

            return comparar;
        }
    }
}
