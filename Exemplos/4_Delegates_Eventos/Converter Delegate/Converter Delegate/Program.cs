using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter_Delegate
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
            Console.WriteLine("=========FloatOriginal============");

            // Crie uma matriz de objetos PointF.
            PointF[] apf = { new PointF (27.8F, 32.62F),
                new PointF (99.3F, 147.273F), new PointF (7.5F, 1412.2F)};

            // Exibe cada elemento na matriz PointF.
            foreach (PointF p in apf)
                Console.WriteLine(p);

            // Converte cada elemento PointF em um objeto Point.
            // Repare que é utilizado a classe Array com o ConvertAll
            Point[] ap = Array.ConvertAll(apf,
                    new Converter<PointF, Point>(Array_PointFToPoint));

            // Exibe cada elemento na matriz Point.
            Console.WriteLine();
            Console.WriteLine("======Converter Float Array to int======");
            foreach (Point p in ap)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine();
            Console.WriteLine("======Converter Usuarios List to string======");

            List<Usuario> users = new List<Usuario>()
            {
                new Usuario("Romeu", 10),
                new Usuario("Julieta", 90),
                new Usuario("Hamlet", 55)
            };

            // Repare que é utilizado o objeto users com o ConvertAll
            List<string> nomes = users.ConvertAll<string>(new Converter<Usuario, string>(ConvertUsuario_ToString));

            foreach (var p in nomes)
            {
                Console.WriteLine(p);
            }

            Console.ReadKey();
        }

        public static Point Array_PointFToPoint(PointF pf)
        {
            return new Point(((int)pf.X), ((int)pf.Y));
        }

        public static string ConvertUsuario_ToString(Usuario user)
        {
            return user.Nome;
        }
    }
}
