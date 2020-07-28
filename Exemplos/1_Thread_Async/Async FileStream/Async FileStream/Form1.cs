using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async_FileStream
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string content = await httpClient
                .GetStringAsync("http://www.microsoft.com")
                .ConfigureAwait(false);

            //using (FileStream sourceStream = new FileStream("temp.html",
            //        FileMode.Create, FileAccess.Write, FileShare.None,
            //        4096, useAsync: true))
            //{
            //    byte[] encodedText = Encoding.Unicode.GetBytes(content);
            //    await sourceStream.WriteAsync(encodedText, 0, encodedText.Length)
            //    .ConfigureAwait(false);
            //};


            Task task = Task.Run(() =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    textBox1.Text = content;
                }));
            });

            ////Funicona somente com ConfigureAwait(true), SynchronizationContext = true
            textBox1.Text = content;
        }
    }
}
