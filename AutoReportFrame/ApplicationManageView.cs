using mshtml;
using Newtonsoft.Json.Linq;
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
    public partial class ApplicationManageView : Form
    {
        public ApplicationManageView()
        {
            InitializeComponent();
            //this.menu = menu;
            this.pin = "112233";
            this.currentUrl = localUrl;
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            this.FormClosing += ApplicationManageView_FormClosing;
        }

        public ApplicationManageView(Form menu,string pin)
        {
            InitializeComponent();
            this.button3.Visible = false;
            this.menu = menu;
            this.pin = pin;
            this.currentUrl = webUrl;
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            this.FormClosing += ApplicationManageView_FormClosing;
        }

        private bool unlogineed = true;
        private HtmlDocument doc;
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (unlogineed)
            {
                doc = webBrowser1.Document;
                AutoLogin();
                unlogineed = false;
            }
            else
            {
                doc = webBrowser1.Document;
            }
        }

        private string currentUrl="";

        private string localUrl = "http://localhost:3153/";

        private string webUrl = "http://api.alibiaobiao.cn/";

        private int pageCount = 0;
        /// <summary>
        /// 自动写入信息并登陆
        /// </summary>
        private void AutoLogin()
        {
            try
            {
                HtmlElement pinele = doc.GetElementById("pin");
                HtmlElement loginele = doc.GetElementById("pinword");
                pinele.SetAttribute("value", this.pin);
                loginele.InvokeMember("click");
            }
            catch (Exception ex)
            {

            }
        }

        private void ApplicationManageView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.webBrowser1.Dispose();
            this.menu.Show();
        }

        private Form menu;
        private string pin;

        /// <summary>
        /// 打开申请管理页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("http://wssq.saic.gov.cn:9080/tmsve/wdsqgl_getappType.xhtml");
        }

        private Dictionary<string, string> dic;
      

        /// <summary>
        /// 开始抓取申请号码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            

            HtmlElementCollection tablelist = doc.GetElementsByTagName("table");
            HtmlElement tableNode = tablelist[2];

            HtmlElementCollection trlist = tableNode.GetElementsByTagName("tr");

            if (dic==null)
            {
                dic = new Dictionary<string, string>();
            }
            else
            {
                dic.Clear();
            }

            for (int i = 1; i < trlist.Count; i++)
            {
                HtmlElementCollection tdlist = trlist[i].GetElementsByTagName("td");
                if (tdlist[3].InnerText.Trim()=="")
                {
                    continue;
                }
                dic.Add(tdlist[3].InnerText.Trim(),tdlist[5].InnerText.Trim());
            }
            foreach (var item in dic)
            {
                string str = CommonLibrary.CommonTool.GetResult(currentUrl + string.Format("api/AutoReport/GetTmNum?reportNum={0}", item.Key.Insert(2, "-")));
                JObject obj = JObject.Parse(str);
                if (obj["error"].ToString()=="")
                {
                    string tmNum = obj["tmNum"].ToString();
                    str = CommonLibrary.CommonTool.GetResult(currentUrl+string.Format("api/AutoReport/FillInRegNum?tmNum={0}&regNum={1}", tmNum,item.Value));
                   
                }
                else
                {
                    MessageBox.Show(item.Key+"***" +obj["error"].ToString());
                    continue;
                }

                obj = JObject.Parse(str);
                if (obj["error"].ToString() != "")
                {
                    MessageBox.Show(item.Key + "*update**" + obj["error"].ToString());
                }
            }
            if (dic.Count==0)
            {
                MessageBox.Show("没有数据可供抓取");
            }
            else
            {
                MessageBox.Show("注册号抓取完成");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(@"file:///C:/Users/zidanepc/Desktop/suck.html");
        }

        /// <summary>
        /// 抓取所有信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            pageCount = int.Parse(doc.GetElementById("countpage").GetAttribute("value"));

            for (int i = 0; i < pageCount; i++)
            {
                GetInfoHtml();
                doc.InvokeScript("dopage",new object[]{3});
                Thread.Sleep(2000);
            }          
        }

        private void GetInfoHtml()
        {
            try
            {
                HtmlElementCollection tablelist = doc.GetElementsByTagName("table");
                HtmlElement tableNode = tablelist[2];
    
                HtmlElementCollection trlist = tableNode.GetElementsByTagName("tr");
         
                if (dic == null)
                {
                    dic = new Dictionary<string, string>();
                }
                else
                {
                    dic.Clear();
                }

                for (int i = 1; i < trlist.Count; i++)
                {
 
                    HtmlElementCollection tdlist = trlist[i].GetElementsByTagName("td");
                    if (tdlist[6].InnerText.Trim() != "注册申请" || tdlist[8].InnerText.Trim() != "申请完成")
                    {
                        continue;
                    }
                    string id = trlist[i].FirstChild.FirstChild.GetAttribute("id");
                    if (id == "")
                    {
                        continue;
                    }
       
                    PopupItemWin(id);
         
                    Thread.Sleep(2 * 1000);
                    GetInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        private void GetInfo()
        {
            IHTMLDocument2 hdoc = doc.DomDocument as IHTMLDocument2;
            IHTMLWindow2 win = hdoc.parentWindow as mshtml.IHTMLWindow2;
            var d = win.execScript(@"function sucdd(){ return popUpWinQm;}", "javascript");
            if (hdoc == null)
            {
                MessageBox.Show("hdoc is null");
            }
            if (win == null)
            {
                MessageBox.Show("win is null");
            }
            //HTMLWindow2Class dddd = doc.InvokeScript("sucdd") as HTMLWindow2Class;
            IHTMLWindow2 dddd = doc.InvokeScript("sucdd") as IHTMLWindow2;
            IHTMLDocument2 popupdoc = dddd.document;
            IHTMLElementCollection elelist = popupdoc.all.tags("tr") as IHTMLElementCollection;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            int i = 0;//申请号，申请人类型（法人或其他组织，自然人），书式类型（中国大陆），申请人名称，身份证明文件号码
            foreach (IHTMLElement item in elelist)
            {
                if (i < 3)
                {
                    continue;
                }
                if (i >= elelist.length - 15)
                {
                    break;
                }
                dic.Add(((item.children as IHTMLElementCollection).item(null, 0) as IHTMLElement).innerText, ((item.children as IHTMLElementCollection).item(null, 1) as IHTMLElement).innerText);
            }
            if (dic.Values.Contains("中国大陆"))
            {
                string regNum = "";
                string applicantCategory = "";
                string applicant = "";
                string idCard = "";
                dic.TryGetValue("申请号",out regNum);
                dic.TryGetValue("申请人类型",out applicantCategory);
                dic.TryGetValue("申请人名称", out applicant);
                dic.TryGetValue("身份证明文件号码", out idCard);
                CommonLibrary.CommonTool.GetResult(currentUrl+ string.Format("api/AutoReport/GetAddApplicantRegNum?regNum={0}&category={1}&applicant={2}&idCard={3}",regNum,applicantCategory,applicant,idCard));
            }
            dddd.close();
        }



        /// <summary>
        /// 弹出注册申请详细信息页面
        /// </summary>
        /// <param name="id"></param>
        private void PopupItemWin(string id)
        {
            doc = webBrowser1.Document;
            doc.InvokeScript("popUpWindowQm", new object[] { string.Format("/tmsve/sbzcsq_getSbzcDetail.xhtml?appid={0}&tablename=TTmoasAppTmzcAppform", id) });
        }
    }
}
