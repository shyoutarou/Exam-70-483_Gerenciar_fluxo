using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Async_and_Await_Example
{

    class Program
    {


        static void Main(string[] args)
        {
            DoStuff();

            for (int i = 0; i < 100; i++)
                Console.WriteLine("Working on the Main Thread....");


            string result = DownloadContent().Result;
            Console.WriteLine(result);

            Console.ReadKey();
        }

        public static double ReadDataFromIO()
        {
            // We are simulating an I/O by putting the current thread to sleep. 
            Thread.Sleep(2000);
            return 10d;
        }

        //Método Assincrono pois está voltando um Task
        //Como não está utilizando nenhum método interno que 
        //necessite o await não precisa também da definição sync no método
        public static Task<double> ReadDataFromIOAsync()
        {
            return Task.Run(new Func<double>(ReadDataFromIO));
        }


        private Object thisLock = new Object();

        static async Task DoStuff()
        {

            // Await não pode estar dentro de instruções lock
            //lock (thisLock)
            //{
            // If we comment out the await Task.Run instructions and
            // everything happens synchronously... 
            await Task.Run(() =>
            {
                var t = CountToFifty();
            });
            //}

            // This code will not run until the CountToFifty call has completed
            Console.WriteLine("Counting to 50 completed...");
        }

        private static async Task<string> CountToFifty()
        {
            int counter;

            for (counter = 0; counter < 51; counter++)
                Console.WriteLine("BG thread: " + counter);

            return "Counter = " + counter;
        }

        public static async Task<string> DownloadContent()
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                string result = await client.GetStringAsync("http://www.microsoft.com");
                return result;
            }
        }
    }
}
