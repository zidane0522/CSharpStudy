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

namespace asyncWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Action blockact = () => 
            //{

            //};
            //blockact.BeginInvoke(ar=> 
            //{
            //    blockact.EndInvoke(ar);
            //    MessageBox.Show("fdsfds");
            //},null);

            beginasy();
        }

        private async void beginasy()
        {
            //Func<string, Action> threadFun = r1 => { return () => { }; };
            await Task.Run(()=> { Thread.Sleep(6000); });
            MessageBox.Show("Wake up");
        }
    }
}
