using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Custom_Exception
{
    [Serializable]
    class InvalidProjectionException : Exception
    {
        public int OrderId { get; }

        public InvalidProjectionException()
        : base() { }
        public InvalidProjectionException(string message)
        : base(message)
        {
            this.HelpLink = "http://www.mydomain.com/infoaboutexception\u201d";
        }

        public InvalidProjectionException(string message, Exception innerException)
    : base(message, innerException)
        {
            this.HelpLink = "http://www.mydomain.com/infoaboutexception\u201d";
        }
        protected InvalidProjectionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
            OrderId = (int)info.GetValue("OrderId", typeof(int));
            this.HelpLink = "http://www.mydomain.com/infoaboutexception\u201d";
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("OrderId", OrderId, typeof(int));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Show();
            }
            catch (InvalidProjectionException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
        private static void Show()
        {
            throw new InvalidProjectionException("It's a custom exception!");
        }
    }
}
