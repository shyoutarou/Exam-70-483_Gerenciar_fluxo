using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BackgroundWorker _worker;

        public MainWindow()
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
                // Comment the previous line and uncomment the line below to kick of the worker from onather thread.
                new Thread(() => _worker.RunWorkerAsync()) { Name = "RunWorkThread" }.Start();
            }
        }

        void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Se chamar por esse metodo:
            //new Thread(() => _worker.RunWorkerAsync()) { Name = "RunWorkThread" }.Start();
            //E tentar atualizar direto o label
            //lblResult.Content = e.Result;
            //Da ERRRO: O thread de chamada não pode acessar este objeto porque ele pertence a um thread diferente.'

            // Instead of updating the UI directly we call Dispatcher.Invoke
            this.Dispatcher.Invoke(() => lblResult.Content = e.Result);

            //OU
            //Task task = Task.Run(() =>
            //{
            //    this.Dispatcher.BeginInvoke(new Action(() =>
            //    {
            //        lblResult.Content = e.Result;
            //    }));
            //});

        }

        private void UpdateLabel(object text)
        {
            lblResult.Content = text;
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {

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
