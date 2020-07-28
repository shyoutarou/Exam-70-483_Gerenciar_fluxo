using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApp
{

    public partial class Form1 : Form
    {

        private BackgroundWorker _worker;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;

            if (!_worker.IsBusy)
            {
                //_worker.RunWorkerAsync();

                //ERRO de race conditions
                new Thread(() => _worker.RunWorkerAsync()) { Name = "RunWorkThread" }.Start();
            }
        }


        //void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //ERRO de race conditions
        //    //lblResult.Text = e.Result.ToString();

        //    ////Aqui poderia ser utilizado também
        //    ///this.BeginInvoke OU this.Invoke criam um bloqueio de thread
        //    ///para atulizar a interface do usuário
        //    //Task task = Task.Run(() =>
        //    //{
        //    //    this.BeginInvoke(new Action(() => UpdateLabel(e.Result.ToString())));
        //    //});

        //    ////OU Aqui poderia ser utilizado também
        //    //Task task2 = Task.Run(() =>
        //    //{
        //    //    this.BeginInvoke(new Action(() =>
        //    //    {
        //    //        lblResult.Text = e.Result.ToString();
        //    //    }));
        //    //});
        //}

        void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<string>(UpdateLabel), e.Result.ToString());
            else
                UpdateLabel(e.Result.ToString());
        }

        private void UpdateLabel(string text)
        {
            lblResult.Text = text;
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // ERRO de race conditions pois está tentando atribuir o valor de lblResult.Text
            // Que esta no thread da interface (primeiro plano) que chamou o BackgroundWorker
            // para realizar a tarefa que está em outro thread em segundo plano
            //lblResult.Text = DoIntensiveCalculations().ToString();
            e.Result = DoIntensiveCalculations();
        }

        static double ReadDataFromIO()
        {
            // We are simulating an I/O by putting the current thread to sleep. 
            Thread.Sleep(5000);
            return 10d;
        }

        static double DoIntensiveCalculations()
        {
            // We are simulating intensive calculations 
            // by doing nonsens divisions 
            double result = 100000000d;
            var maxValue = Int32.MaxValue;
            for (int i = 1; i < maxValue; i++)
            {
                result /= i;
            }
            return result + 10d;
        }


    }
}


