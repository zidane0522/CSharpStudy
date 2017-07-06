using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoReportFrame
{
    public partial class Menu : Form
    {
        public Menu(Form f,string pin)
        {
            InitializeComponent();
            this.f = f;
            this.pin = pin;
            this.FormClosing += Menu_FormClosing;
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.f.Close();
        }

        private Form f;

        private string pin;

        /// <summary>
        /// 进入自动上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2(this,pin);
            form2.Show();
        }

        /// <summary>
        /// 进入申请管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ApplicationManageView amv = new AutoReportFrame.ApplicationManageView(this,pin);            
            amv.Show();
        }
    }
}
