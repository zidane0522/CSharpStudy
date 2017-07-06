using Newtonsoft.Json.Linq;
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
    public partial class ApplicationManageView : Form
    {
        public ApplicationManageView()
        {
            InitializeComponent();
            //this.menu = menu;
            this.pin = "112233";
            this.currentUrl = webUrl;
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            this.FormClosing += ApplicationManageView_FormClosing;
        }

        public ApplicationManageView(Form menu,string pin)
        {
            InitializeComponent();
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

        private string localUrl = "http://localhost:3135/";

        private string webUrl = "http://api.alibiaobiao.cn/";


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
        private List<string> errorList = new List<string>();
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
    }
}
