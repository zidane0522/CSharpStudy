using mshtml;
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
    public partial class Form1 : Form
    {
        public Form1()
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
            object ddd = ele.InvokeMember("onclick");
            //object obj = "/tmsve/commonGoods_getIntCls.xhtml";
            //doc.InvokeScript("popUpWindow",new object[] { "/tmsve/commonGoods_getIntCls.xhtml" });
            ////popUpWindow('/tmsve/commonGoods_getIntCls.xhtml')
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
