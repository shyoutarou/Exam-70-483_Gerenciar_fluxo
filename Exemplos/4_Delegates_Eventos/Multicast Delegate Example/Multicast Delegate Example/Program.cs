using System;

namespace Multicast_Delegate_Example
{
    public class MethodInstance
    {
        public string Name;

        public void Method_01(string message)
        {
            Console.WriteLine("Instance Método 01: " + message);
        }
        public void Method_02(string message)
        {
            Console.WriteLine("Instance Método 02: " + message);
        }


        // A method that returns a string.
        public delegate void GetStringDelegate();
        public static void StaticMethod_01()
        {
            Console.WriteLine("Static Método");
        }
        public void InstanceMethod_02()
        {
            Console.WriteLine("Instance Método Name:" + Name);
        }

        // Variables to hold GetStringDelegates.
        public GetStringDelegate StaticMethod;
        public GetStringDelegate InstanceMethod;
        public GetStringDelegate PrintMethods;
    }


    class Program
    {
        delegate void del_assign(string str);

        delegate void del(int x, int y);
        delegate void del_operacoes(int x, int y);
        delegate int del_operacoes_retorno(int x, int y);

        static void Main(string[] args)
        {

            del d;

            d = AddNumbers;

            Console.WriteLine("Invoking delegate d with one target: ");
            d(6, 5);
            Console.WriteLine();

            d += MultiplyNumbers;
            Console.WriteLine("Invoking delegate d with 2 targets: ");
            d(6, 5);
            Console.WriteLine();

            d += SubtractNumbers;
            Console.WriteLine("Invoking delegate d with 3 targets: ");
            d(6, 5);
            Console.WriteLine();

            d -= MultiplyNumbers;
            Console.WriteLine("Removed MultiplyNumbers: ");
            d(6, 5);
            Console.WriteLine();



            Console.WriteLine("======MultiCast======");

            MethodInstance alice = new MethodInstance() { Name = "Alice" };
            MethodInstance bob = new MethodInstance() { Name = "Bob" };


            //Static e Intance methods
            alice.StaticMethod = MethodInstance.StaticMethod_01;
            alice.InstanceMethod = alice.InstanceMethod_02;
            bob.StaticMethod = MethodInstance.StaticMethod_01;
            bob.InstanceMethod = alice.InstanceMethod_02;

            bob.PrintMethods = alice.StaticMethod + alice.InstanceMethod;
            bob.PrintMethods += bob.StaticMethod + bob.InstanceMethod;
            bob.PrintMethods += bob.InstanceMethod;

            bob.PrintMethods();

            var met_instance = new MethodInstance();

            //Both types of assignment are valid.
            del_assign del_01 = met_instance.Method_01;
            del_assign del_02 = met_instance.Method_02;
            del_assign del_03 = StaticMethod;

            del_assign allMethodsDelegate = del_01 + del_02;
            allMethodsDelegate += del_03;

            del_assign del_04 = StaticMethod;

            allMethodsDelegate += del_04;
            //remove Method1
            allMethodsDelegate -= del_04;

            // copy AllMethodsDelegate while removing d2
            del_assign oneMethodDelegate = allMethodsDelegate - del_04;



            allMethodsDelegate("MultiCast");

            int invocationCount = del_01.GetInvocationList().GetLength(0);
            Console.WriteLine("Métodos em del_01: " + invocationCount);
            int allinvocationCount = allMethodsDelegate.GetInvocationList().GetLength(0);
            Console.WriteLine("Métodos em allinvocationCount: " + allinvocationCount);

            Console.WriteLine("======MultiCast+Parametros======");

            del_operacoes sum_mult = AddNumbers;

            sum_mult(1, 2);

            //Adiciona outro metodo a ser processado junto
            sum_mult += MultiplyNumbers;
            sum_mult(3, 4);

            //Poderia voltar um retorno; delegate int 
            del_operacoes_retorno subt = SubtractRetorno;

            var resultado = subt(4, 5);
            Console.WriteLine("SubtraiNumbers: " + resultado); // SubtraiNumbers: -1

            resultado = subt(10, 5);
            resultado = subt(1, 100);
            Console.WriteLine("SubtraiNumbers: " + resultado); // SubtraiNumbers: -99

            Console.ReadKey();
        }

        public static void StaticMethod(string message)
        {
            Console.WriteLine("Método estático: " + message);
        }

        public static void AddNumbers(int a, int b)
        {
            Console.WriteLine("AddNumbers: a + b = " + (a + b));
        }

        public static void MultiplyNumbers(int a, int b)
        {
            Console.WriteLine("MultiplyNumbers: a * b = " + (a * b));
        }

        public static void SubtractNumbers(int a, int b)
        {
            Console.WriteLine("SubtractNumbers: a - b = " + (a - b));
        }

        public static int SubtractRetorno(int a, int b)
        {
            var subtrai = a - b;
            return subtrai;
        }
    }

}
