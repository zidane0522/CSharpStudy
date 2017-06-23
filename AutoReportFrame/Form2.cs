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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
        }

        public HtmlDocument doc { get; set; }

        public HtmlElement ele { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            doc = webBrowser1.Document;
            ele = doc.GetElementById("codefans_net").FirstChild ;
            object ddd= ele.InvokeMember("onclick");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ele.
        }
    }
}
