using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;

namespace ExceptionsThrowing
{
    class Program
    {
        public static void Main(string[] args)
        {
            string fileName = Console.ReadLine();

            try
            {
                //Rethrowing_JustThrow(fileName);
                //Rethrowing_OriginalException(fileName);

                //ICanThrowException();
                //Rethrowing_NewException(fileName);
                Rethrowing_CaptureThrow(fileName);
            }
            //catch (ArgumentNullException e)
            //{
            //    Console.WriteLine($"Erro {e.Message}");
            //}
            catch (Exception e)
            {
                Console.WriteLine("Message: {0}", e.Message);
                Console.WriteLine("StackTrace: {0}", e.StackTrace);
            }
            finally
            {
                Console.WriteLine("Program complete.");
                Console.ReadLine();
            }
        }


        public static void Rethrowing_JustThrow(string fileName)
        {
            try
            {
                OpenAndParse(fileName);
            }
            catch (Exception logEx)
            {
                Log(logEx);
                throw; // rethrow the original exception
            }
        }

        private static void Rethrowing_OriginalException(string fileName)
        {
            try
            {
                OpenAndParse(fileName);
            }
            catch (Exception logEx)
            {
                Log(logEx);
                throw logEx;
            }
        }


        private static void ICanThrowException()
        {
            try
            {

                try
                {
                    throw new Exception("Out of cheese error");    // Caught in inner catch
                }
                catch (Exception)
                {
                    throw new Exception("418: I'm a teapot");     // Caught in outer catch
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);    // "418: I'm a teapot"
            }
        }

        private static void Rethrowing_NewException(string fileName)
        {
            try
            {
                OpenAndParse(fileName);
            }
            catch (Exception logEx)
            {
                Log(logEx);
                throw new Exception("Rethrown", logEx);
            }
        }

private static void Rethrowing_CaptureThrow(string fileName)
{
    try
    {
        OpenAndParse(fileName);
    }
    catch (Exception ex)
    {
        Log(ex);
        ExceptionDispatchInfo.Capture(ex).Throw();
    }
}

        [Conditional("DEBUG")]
        private static void Log(Exception logEx)
        {
            Debug.WriteLine("Debug Log Message: {0}", logEx.Message);
            Debug.WriteLine("Debug Log StackTrace: {0}", logEx.StackTrace);
        }

        public static string OpenAndParse(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName", "Filename is required");
            return File.ReadAllText(fileName);
        }
    }
}
