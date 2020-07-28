using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeoutException_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            try
            {
                // Set the COM1 serial port to speed = 4800 baud, parity = odd,
                // data bits = 8, stop bits = 1.
                SerialPort sp = new SerialPort("COM1", 4800, Parity.Odd, 8, StopBits.One);
                // Timeout after 2 seconds.
                sp.ReadTimeout = 2000;
                sp.Open();

                // Read until either the default newline termination string
                // is detected or the read operation times out.
                input = sp.ReadLine();

                sp.Close();

                // Echo the input.
                Console.WriteLine(input);
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
    }
}
