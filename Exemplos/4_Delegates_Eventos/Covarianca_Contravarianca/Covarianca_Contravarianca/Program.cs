using System;
using System.Collections.Generic;

namespace Covarianca_Contravarianca
{
    class Parent { }
    class Child : Parent { }
    delegate Parent CovarianceHandle();
    delegate void ContravarianceHandle(Child c);

    // Hierarquia simples de classes.
    public class Person { }
    public class Employee : Person { }


    class Program
    {

        static Employee FindByTitle(String title)
        {
            // This is a stub for a method that returns  
            // an employee that has the specified title.  
            return new Employee();
        }

        static void AddToContacts(Person person)
        {
            // This method adds a Person object  
            // to a contact list.  
        }

        static void Test_Contravarianca()
        {
            // Crie uma instância do delegado sem usar variação.
            Action<Person> addPersonToContacts = AddToContacts;

            // O delegado da ação espera
            // um método que possui um parâmetro Employee,
            // mas você pode atribuir a ele um método que possui um parâmetro Person
            // porque Employee deriva de Person.
            Action<Employee> addEmployeeToContacts = AddToContacts;

            // Você também pode atribuir um delegado
            // que aceita um parâmetro menos derivado para um delegado
            // que aceita um parâmetro mais derivado.
            addEmployeeToContacts = addPersonToContacts;
        }

        static void Test_Covarianca()
        {
            // Crie uma instância do delegado sem usar variação.
            Func<String, Employee> findEmployee = FindByTitle;

            // O delegado espera que um método retorne Person,
            // mas você pode atribuir a ele um método que retorne Employee.
            Func<String, Person> findPerson = FindByTitle;

            // Você também pode atribuir um delegado
            // que retorna um tipo mais derivado
            // para um delegado que retorna um tipo menos derivado.
            findPerson = findEmployee;
        }


        static Child CovarianceMethod()
        {
            Console.WriteLine("Covariance Method");
            return new Child();
        }

        static void ContravarianceMethod(Parent p)
        {
            Child ch = p as Child;
            Console.WriteLine("Contravariance Method");
        }

        static object GetObject() { return null; }
        static void SetObject(object obj) { }

        static string GetString() { return ""; }
        static void SetString(string str) { }

        static void Main(string[] args)
        {
            Console.WriteLine("=========Contra-Covariance:Variaveis_List============");

            // Compatibilidade da atribuição.
            string str = "teste";
            // Um objeto de um tipo mais derivado é atribuído a um objeto de um tipo menos derivado.
            object obj = str;
            Console.WriteLine("obj=" + obj);
            Console.WriteLine("str=" + str);

            // Covariância.
            IEnumerable<string> strings = new List<string>();
            Console.WriteLine("strings=" + strings.GetType());
            // Um objeto que é instanciado com um argumento de tipo mais derivado
            // é atribuído a um objeto instanciado com um argumento de tipo menos derivado.
            // A compatibilidade da atribuição é preservada.
            IEnumerable<object> objetos = strings;
            Console.WriteLine("strings=" + strings.GetType());
            Console.WriteLine("objetos=" + objetos.GetType());

            // Covariância. Um delegado especifica um tipo de retorno como objeto,
            // mas você pode atribuir um método que retorna uma string.
            Func<object> del = GetString;

            Console.WriteLine("=========Contra-Covariance:Action============");

            // Contravariância.
            // Suponha que o seguinte método esteja na classe:
            // static void SetObject(object o) { }
            // Um delegado especifica um tipo de parâmetro como sequência,
            // mas você pode atribuir um método que pega um objeto.
            Action<string> del2 = SetObject;

            Action<object> actObject = SetObject;
            Console.WriteLine("actObject=" + actObject.GetType());
            // Um objeto que é instanciado com um argumento de tipo menos derivado
            // é atribuído a um objeto instanciado com um argumento de tipo mais derivado.
            // A compatibilidade da atribuição é revertida.
            Action<string> actString = actObject;
            Console.WriteLine("actObject=" + actObject.GetType());
            Console.WriteLine("actString=" + actString.GetType());

            Console.WriteLine("=========Contra-Covariance:Delegates============");

            //Covariance
            CovarianceHandle covarianca = CovarianceMethod;
            covarianca();

            ContravarianceHandle contra_varianca = ContravarianceMethod;
            Child child = new Child();
            //Contravariance
            contra_varianca(child);


            Console.WriteLine("=========Contra-Covariance:Func============");
            Test_Contravarianca();

            Test_Covarianca();

            Console.WriteLine("=========A covariância para matrizes ============");

            object[] array = new String[10];
            // The following statement produces a run-time exception.  
            //System.ArrayTypeMismatchException: 'Tentativa de acesso a 
            //um elemento como um tipo incompatível com a matriz.'
            //array[0] = 10;

            Console.ReadKey();
        }
    }
}
