using mshtml;
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

namespace AutoReportFrame
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
        }


        public HtmlDocument doc { get; set; }

        public HtmlElement ele { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            doc = webBrowser1.Document;
            ele = doc.GetElementById("codefans_net").FirstChild;

            for (int i = 0; i < 10; i++)
            {
                doc.InvokeScript("popUpWindow1", new object[] { "tmoas/wssqsy/help/m88.html" });
                Thread.Sleep(3000);
            }
         
            popUPWinAsync(doc);

        }

        private async void popUPWinAsync(HtmlDocument doc)
        {
            await Task.Run(()=> {
                doc.InvokeScript("popUpWindow1", new object[] { "tmoas/wssqsy/help/m88.html" });
                //object ddd = ele.InvokeMember("onclick");
                Thread.Sleep(3000);
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IHTMLDocument2 hdoc = doc.DomDocument as IHTMLDocument2;
            IHTMLWindow2 win = hdoc.parentWindow as mshtml.IHTMLWindow2;

            var d = win.execScript(@"function sucdd(){ return popUpWin;}", "javascript");
            HTMLWindow2Class dddd = doc.InvokeScript("sucdd") as HTMLWindow2Class;
            IHTMLDocument2 popupdoc = dddd.document;
            IHTMLElementCollection eleclt= popupdoc.all.tags("span") as IHTMLElementCollection;
            foreach (IHTMLElement item in eleclt)
            {
                if (item.innerText!=null)
                {
                    if (item.innerText.Contains("问题十四"))
                    {
                        item.click();
                    }
                }
               
            }
            //IHTMLElement ele=eleclt.item
        }
    }
}
