using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_TaskScheduler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ui = TaskScheduler.FromCurrentSynchronizationContext();
            var tf = Task<int>.Factory;

            // Load the three images asynchronously
            var imageOne = tf.StartNew(() => { Thread.Sleep(100); return 10; });
            var imageTwo = tf.StartNew(() => { Thread.Sleep(1000); return 50; });
            var imageThree = tf.StartNew(() => { Thread.Sleep(10000); return 30; });

            //Congela a Interface do Usuraio
            //Task.WhenAll(imageOne, imageTwo, imageThree);
            //label1.Text = (int)imageOne + (int)imageTwo + (int)imageThree;

            // When they’ve been loaded, blend them
            var blendedImage = tf.ContinueWhenAll(
                new[] { imageOne, imageTwo, imageThree }, _ =>
                Soma(imageOne.Result, imageTwo.Result, imageThree.Result));

            // When we’re done blending, display the blended image
            blendedImage.ContinueWith(_ =>
            {
                UpdateLabel(blendedImage.Result.ToString());
                //label1.Text = blendedImage.Result.ToString();
            }, ui);

            //Task.Factory.StartNew(() => { UpdateLabel(blendedImage.Result.ToString()) ;  }    , CancellationToken.None, TaskCreationOptions.None, ui); 
        }

        static int Soma(object imageOne, object imageTwo, object imageThree)
        {
            return (int)imageOne + (int)imageTwo + (int)imageThree;
        }

        private void UpdateLabel(string text)
        {
            label1.Text = text;
        }
    }
}
