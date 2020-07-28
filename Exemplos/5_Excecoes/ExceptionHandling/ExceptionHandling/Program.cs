using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //string s = null;
                //try
                //{
                //    int i = int.Parse(s);
                //}
                //catch
                //{
                //    Console.WriteLine("The quantity must be an integer.");
                //}


                string s = null;

                try
                {
                    int i = int.Parse(s);
                }
                catch (ArgumentNullException ae)
                {
                    Console.WriteLine("You need to enter a value" + ae.Message);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("{0} is not a valid number. {1}", s, fe.Message);
                }
                finally
                {
                    Console.WriteLine("Program complete.");
                    Console.ReadLine();
                }

                //string s = Console.ReadLine();
                //try
                //{
                //    int i = int.Parse(s);
                //    //Lança o erro no event Log do sistema em 
                //    //"Iniciar" >>“Administrative Tools” >> Aplicativo >> "Event Viewer"
                //    if (i == 42) Environment.FailFast("Special number entered");
                //}
                //finally
                //{
                //    Console.WriteLine("Program complete finally.");
                //}

                try
                {
                    string s1 = Console.ReadLine();
                    int i = int.Parse(s1);
                    Console.WriteLine("Parsed: {0}", i);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Message: {0}", e.Message);
                    Console.WriteLine("StackTrace: {0}", e.StackTrace);
                    Console.WriteLine("HelpLink: {0}", e.HelpLink);
                    Console.WriteLine("InnerException: {0}", e.InnerException);
                    Console.WriteLine("TargetSite: {0}", e.TargetSite);
                    Console.WriteLine("Source: {0}", e.Source);
                }
                finally
                {
                    Console.WriteLine("Program complete.");
                    Console.ReadLine();
                }


                using (Pen pen = new Pen(Color.Red, 10))
                {
                    // Use a caneta para desenhar ...
                }

                Pen pen2 = new Pen(Color.Red, 10);
                try
                {
                    pen2 = new Pen(Color.Red, 10);
                    // Use a pen para desenhar ...
                }
                finally
                {
                    if (pen2 != null) pen2.Dispose();
                }


            }
        }
    }

    internal class Pen : IDisposable
    {
        private Color color;
        private int valor;

        public Pen(Color cpr, int value)
        {
            this.color = cpr;
            this.valor = value;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
