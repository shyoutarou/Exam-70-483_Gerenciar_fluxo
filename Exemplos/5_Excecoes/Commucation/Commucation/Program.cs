using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commucation.ServiceReference1;

namespace Commucation
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (SampleServiceClient client = new SampleServiceClient())
            {
                var retorno = client.SampleMethod("Ricardo");
                Console.WriteLine(retorno); // Hell World
            };

            Console.ReadKey();
        }
    }
}
