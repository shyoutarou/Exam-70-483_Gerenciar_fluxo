using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWait_Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            myTask_Wait();
            myTask_WaitMili();
            myTask_WaitAll();
            myTask_WaitAllMili();
            myTask_WaitAny();
            myTask_WaitAnyMili();

            Console.ReadKey();
        }

        private static void myTask_Wait()
        {

            Task myTask = Task.Run(() => { Thread.Sleep(1000); }); //1 Sec

            Task myTask = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task.Wait() completed after 1 Sec");
            });

            Console.WriteLine("Hello Task.Wait() From Main Thread");
            myTask.Wait();// wait until myTask get completed
            Console.WriteLine("Bye Task.Wait() From Main Thread");
        }

        private static void myTask_WaitMili()
        {
            Task myTask = Task.Run(() =>
            {
                //Wait for 2 Sec
                Thread.Sleep(2000);
                Console.WriteLine("Task.Wait(1000) wait 1 sec, completed after 2 Sec");
            });
            Task myTask2 = Task.Run(() =>
            {
                //Wait for half sec
                Thread.Sleep(500);
                Console.WriteLine("Task.Wait(1000) wait 1 sec, completed after half Sec");
            });
            myTask.Wait(1000);// wait for 1 sec
            Console.WriteLine("Hello Task.Wait(1000) from Main Thread");
            myTask2.Wait(1000);// wait for 1 sec
            Console.WriteLine("Hello Task.Wait(1000) from Main Thread, again");
            Console.WriteLine("By Task.Wait(1000) From Main Thread");
        }

        private static void myTask_WaitAll()
        {
            Task tsk1 = Task.Run(() =>
            {
                Thread.Sleep(100);
                Console.WriteLine("Task1.WaitAll() completed after 1/10 Sec");
            });
            Task tsk2 = Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine("Task2.WaitAll() completed after 1/2 Sec");
            });
            Task tsk3 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task3.WaitAll() completed after 1 Sec");
            });

            //Store reference of all tasks in an array of Task
            Task[] allTasks = { tsk1, tsk2, tsk3 };
            //Wait for all tasks to complete
            Task.WaitAll(allTasks);
            Console.WriteLine("By Task.WaitAll() from main thread");
        }

        private static void myTask_WaitAllMili()
        {
            Task tsk1 = Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine("Task1.WaitAll(1200) wait 1.2 sec, completed after 1/2 Sec");
            });
            Task tsk2 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Task2.WaitAll(1200) wait 1.2 sec, completed after 2 Sec");
            });
            Task tsk3 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task3.WaitAll(1200) wait 1.2 sec, completed after 1 Sec");
            });
            //Store reference of all task in an array of Task
            Task[] allTasks = { tsk1, tsk2, tsk3 };
            //Wait for all tasks to complete
            Task.WaitAll(allTasks, 1200);
            Console.WriteLine("By Task.WaitAll(1200) from main thread");
        }

        private static void myTask_WaitAny()
        {
            Task tsk1 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task1.WaitAny() completed after 1 Sec");
            });
            Task tsk2 = Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine("Task2.WaitAny() completed after 1/2 Sec");
            });
            Task tsk3 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Task3.WaitAny() completed after 2 Sec");
            });
            //Store reference of all task in an array of Task
            Task[] allTasks = { tsk1, tsk2, tsk3 };
            //Wait for all tasks to complete
            Task.WaitAny(allTasks);
            Console.WriteLine("By Task.WaitAny() from main thread");
        }

        private static void myTask_WaitAnyMili()
        {
            Task tsk1 = Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine("Task1.WaitAny(1200) wait 1.2 sec, completed after 1/2 Sec");
            });
            Task tsk2 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Task2.WaitAny(1200) wait 1.2 sec, completed after 2 Sec");
            });
            Task tsk3 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task3.WaitAny(1200) wait 1.2 sec, completed after 1 Sec");
            });
            //Store reference of all task in an array of Task
            Task[] allTasks = { tsk1, tsk2, tsk3 };
            //Wait for all tasks to complete
            Task.WaitAny(allTasks, 1200);
            Console.WriteLine("By Task.WaitAny(1200) from main thread");
        }
    }
}
