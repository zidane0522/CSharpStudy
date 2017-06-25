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
            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;

        }
        private bool loginneed = true;
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (loginneed)
            {
                doc = webBrowser1.Document;
                AutoLogin();
                loginneed = false;
            }        
        }

        private void AutoLogin()
        {
            try
            {
                HtmlElement pinele = doc.GetElementById("pin");
                HtmlElement loginele = doc.GetElementById("pinword");
                pinele.SetAttribute("value", "112233");
                loginele.InvokeMember("click");
            }
            catch (Exception ex)
            {
            }             
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.webBrowser1.Dispose();
        }

        public HtmlDocument doc { get; set; }

        public HtmlElement ele { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //获取弹出窗口对象信息
                IHTMLDocument2 hdoc = doc.DomDocument as IHTMLDocument2;
                IHTMLWindow2 win = hdoc.parentWindow as mshtml.IHTMLWindow2;
                var d = win.execScript(@"function sucdd(){ return popUpWin;}", "javascript");
                
                HTMLWindow2Class dddd = doc.InvokeScript("sucdd") as HTMLWindow2Class;
        
                IHTMLDocument2 popupdoc = dddd.document;
                win = popupdoc.parentWindow as IHTMLWindow2;
                string s = @"function confirm() {";
                s += @"return true;";
                s += @"}";
                s += @"function alert() {}";
                win.execScript(s, "javascript");
                IHTMLElementCollection elelist = popupdoc.all.tags("table") as IHTMLElementCollection;
                IHTMLElement table_ele=null;

                foreach (var item in elelist)
                {
                    table_ele = item as IHTMLElement;
                }

                string[] pararray =new string[]{ "外科胶水","药枕","产包","药棉" };

                if (table_ele!=null)
                {
                    //获取td列表

                    IHTMLElementCollection trlist = (table_ele.all as IHTMLElementCollection).tags("tr") as IHTMLElementCollection;

                    //外科胶水，药枕，产包，药棉
                    IHTMLElement ele1=null, ele2=null;
                    //匹配小项节点
                    foreach (IHTMLElement item in trlist)
                    {
                        IHTMLElementCollection tdlist = (item.all as IHTMLElementCollection).tags("td") as IHTMLElementCollection;
                        if (tdlist.length == 3)
                        {

                            ele1 = tdlist.item(null, 0) as IHTMLElement;
                            int i = 0;
                            foreach (IHTMLElement trele in tdlist)
                            {
                                if (i == 0)
                                {
                                    ele1 = trele;
                                }
                                if (i == 2)
                                {
                                    ele2 = trele;
                                }
                                i++;
                            }
                            if (pararray.Contains(ele2.innerText.ToString().Trim()))
                            {
                                IHTMLElement inputele = (ele1.children as IHTMLElementCollection).item(null, 0) as IHTMLElement;
                                inputele.setAttribute("checked", true);
                            }
                        }
                    }
                    IHTMLElement submitlist = popupdoc.all.item("b1", 0) as IHTMLElement;
                    submitlist.click();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tm_loc_info_View tliv = new tm_loc_info_View();
            tliv.ShowDialog();
        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
