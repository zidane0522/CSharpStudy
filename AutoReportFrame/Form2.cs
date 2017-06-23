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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
            this.FormClosing += Form2_FormClosing;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.webBrowser1.Dispose();
        }

        public HtmlDocument doc { get; set; }

        public HtmlElement ele { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            //doc = webBrowser1.Document;
            ////ele = doc.GetElementById("codefans_net").FirstChild ;
            ////object ddd= ele.InvokeMember("onclick");
            //object obj = "/tmsve/commonGoods_getIntCls.xhtml";
            //doc.InvokeScript("popUpWindow",new object[] { "/tmsve/commonGoods_getIntCls.xhtml" });
            ////popUpWindow('/tmsve/commonGoods_getIntCls.xhtml')

            this.webBrowser1.Navigate("http://wssq.saic.gov.cn:9080/tmsve/sbzcsq_getSbzcMain.xhtml");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (doc!=null)
            //{
            //    IHTMLDocument2 hdoc =doc.DomDocument as IHTMLDocument2;
            //    IHTMLWindow2 win = hdoc.parentWindow as mshtml.IHTMLWindow2;
            //    var d= win.execScript(@"function sucdd(){ return popUpWin;}", "javascript");
            //}
            //var dddd = doc.InvokeScript("sucdd");
            ////mshtml.htmlWindow2class
            try
            {
                doc = webBrowser1.Document;
                //HtmlElementCollection dd = doc.GetElementsByTagName("a");
                //foreach (HtmlElement item in dd)
                //{
                //    if (item.InnerText.Contains("【点击添加商品"))
                //    {
                //        item.InvokeMember("click");
                //        break;
                //    }
                //}

                object obj = "/tmsve/commonGoods_getIntCls.xhtml";
                doc.InvokeScript("popUpWindow", new object[] { "/tmsve/commonGoods_getGoods.xhtml?code=0506&id=2CA2DB43AF7DC0A2E0537F000001C0A2" });
            }
            catch (Exception ex)
            {

            }
    

        }
    }
}
