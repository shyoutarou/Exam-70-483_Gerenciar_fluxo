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

namespace WF_TaskScheduler_UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Metodo_01(listBox1);
        }

        static void Metodo_01(ListBox listBox1)
        {
            TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Task.Factory.StartNew(() =>
                    {
                        listBox1.Items.Add("Number cities in problem = " + i.ToString());
                    }, CancellationToken.None, TaskCreationOptions.None, uiScheduler);

                    System.Threading.Thread.Sleep(1000);
                }
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }
    }
}
