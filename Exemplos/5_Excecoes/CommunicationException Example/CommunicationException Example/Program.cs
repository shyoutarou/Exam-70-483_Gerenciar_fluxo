using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace CommunicationException_Example
{
    internal class SampleServiceClient : System.ServiceModel.ClientBase<ISampleService>, ISampleService
    {
        public string SampleMethod(string msg)
        {
            return "The service greets you: " + msg;
        }
    }

    [ServiceContract(Name = "SampleServiceClient", Namespace = "http://CommunicationException_Example")]
    internal interface ISampleService
    {
        [OperationContract]
        string SampleMethod(string msg);
    }

    [DataContractAttribute]
    internal class GreetingFault
    {
        private string report;

        public GreetingFault(string message)
        {
            this.report = message;
        }

        [DataMemberAttribute]
        public string Message
        {
            get { return this.report; }
            set { this.report = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Picks up configuration from the config file.
            SampleServiceClient wcfClient = new SampleServiceClient();
            try
            {
                // Making calls.
                Console.WriteLine("Enter the greeting to send: ");
                string greeting = Console.ReadLine();
                Console.WriteLine("The service responded: " + wcfClient.SampleMethod(greeting));

                Console.WriteLine("Press ENTER to exit:");
                Console.ReadLine();

                // Done with service.
                wcfClient.Close();
                Console.WriteLine("Done!");
                Console.ReadKey();
            }
            catch (TimeoutException timeProblem)
            {
                Console.WriteLine("The service operation timed out. " + timeProblem.Message);
                Console.ReadLine();
                wcfClient.Abort();
            }
            catch (FaultException<GreetingFault> greetingFault)
            {
                Console.WriteLine(greetingFault.Detail.Message);
                Console.ReadLine();
                wcfClient.Abort();
            }
            catch (FaultException unknownFault)
            {
                Console.WriteLine("An unknown exception was received. " + unknownFault.Message);
                Console.ReadLine();
                wcfClient.Abort();
            }
            catch (CommunicationException commProblem)
            {
                Console.WriteLine("There was a communication problem. " + commProblem.Message + commProblem.StackTrace);
                Console.ReadLine();
                wcfClient.Abort();
            }
        }
    }
}
