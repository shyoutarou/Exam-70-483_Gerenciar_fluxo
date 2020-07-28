using System;
using System.Diagnostics;
using System.IO;
using System.Messaging;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

namespace rethrow_correctly
{
    [Serializable]
    public class OrderProcessingException : Exception, ISerializable
    {
        //public OrderProcessingException(int orderId)
        //{
        //    OrderId = orderId;
        //    this.HelpLink = "http://www.mydomain.com/infoaboutexception\u201d";
        //}
        //public OrderProcessingException(int orderId, string message)
        //    : base(message)
        //{
        //    OrderId = orderId;
        //    this.HelpLink = "http://www.mydomain.com/infoaboutexception\u201d";
        //}

        public OrderProcessingException(string message, Exception innerException, int orderId)
            : base(message, innerException)
        {
            OrderId = orderId;
            this.HelpLink = "http://www.mydomain.com/infoaboutexception\u201d";
        }

        public int OrderId { get; }

        //protected OrderProcessingException(SerializationInfo info, StreamingContext context)
        //{
        //    OrderId = (int)info.GetValue("OrderId", typeof(int));
        //}

        //public int OrderId { get; private set; }

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("OrderId", OrderId, typeof(int));
        //}
    }

    class Program
    {
        public static void Main(string[] args)
        {
            string fileName = Console.ReadLine();

            try
            {
                //Rethrowing_JustThrow(fileName);
                //Rethrowing_OriginalException(fileName);

                //Rethrowing_CaptureThrow(fileName);

                Rethrowing_NewException(fileName);
                //Rethrowing_AnotherNewException(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: {0}", ex.Message);
                Console.WriteLine("StackTrace: {0}", ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Program complete.");
                Console.ReadLine();
            }
        }

        private static void Rethrowing_JustThrow(string fileName)
        {
            try
            {
                //ICanThrowException();
                //ICanThrowException();
                OpenAndParse(fileName);
            }
            catch (Exception ex)
            {
                Log(ex);
                throw;
            }
        }

        private static void Rethrowing_OriginalException(string fileName)
        {
            try
            {
                //ICanThrowException();
                //ICanThrowException();
                OpenAndParse(fileName);
            }
            catch (Exception ex)
            {
                Log(ex);
                throw ex;
            }
        }

        private static void Rethrowing_NewException(string fileName)
        {
            try
            {
                //ICanThrowException();
                //ICanThrowException();
                OpenAndParse(fileName);
            }
            catch (Exception ex)
            {
                Log(ex);
                throw new Exception("Rethrown", ex);
            }
        }

        private static void Rethrowing_AnotherNewException(string fileName)
        {
            try
            {
                //ICanThrowException();
                //ICanThrowException();
                OpenAndParse(fileName);
            }
            catch (MessageQueueException ex)
            {
                throw new OrderProcessingException("Error while processing order", ex, ex.ErrorCode);
            }
        }

        private static void Rethrowing_CaptureThrow(string fileName)
        {
            try
            {
                //ICanThrowException();
                //ICanThrowException();
                OpenAndParse(fileName);
            }
            catch (Exception ex)
            {
                Log(ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }

        private static void ICanThrowException()
        {
            throw new Exception("Bad thing happened");
        }

        public static string OpenAndParse(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName", "Filename is required");
            return File.ReadAllText(fileName);
        }

        [Conditional("DEBUG")]
        private static void Log(Exception logEx)
        {
            Debug.WriteLine("Debug Log Message: {0}", logEx.Message);
            Debug.WriteLine("Debug Log StackTrace: {0}", logEx.StackTrace);
        }
    }
}
