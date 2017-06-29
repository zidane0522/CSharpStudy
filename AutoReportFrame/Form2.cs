using AutoReportFrame.Models;
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
    public partial class Form2 : Form
    {
        public Form2(Form loginForm,string pin)
        {
            InitializeComponent();
            currentUrl = localUrl;
            _pin = pin;
            f = loginForm;
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
            this.FormClosing += Form2_FormClosing;
            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            tm_loc_info_View.OnSelectTmLocInfo += Tm_loc_info_View_OnSelectTmLocInfo;
            this.FormClosed += Form2_FormClosed;
        }



        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f.Close();
        }

        private Form f;

        private string _pin = "";

        private int _waitSpan = 2000;
        public Models.Tm_loc_info Tm_loc_info { get; set; }

        public List<GroupInfo> GroupInfoList { get; set; }

        private string currentUrl = "";

        private string localUrl = "http://localhost:3153/";

        private string webUrl = "http://api.alibiaobiao.cn/";

        public int ItemCount { get; set; }

        public int AutoSelectItemCount { get; set; }

        private void Tm_loc_info_View_OnSelectTmLocInfo(Models.Tm_loc_info tm_loc_info)
        {
            this.Tm_loc_info = tm_loc_info;
            this.label2_applicant.Text = tm_loc_info.Applicant;
            this.label3_tmName.Text = tm_loc_info.TmName;
            this.label5_ictm.Text = tm_loc_info.TmIctm.ToString();
            this.label3_tmNum.Text = tm_loc_info.TmNum;
            AutoWriteTmNum();
            GetGroupInfoList();
            AutoSelectItem();
        }

        private void AutoWriteTmNum()
        {
            HtmlElement ele = doc.GetElementById("agentFilenum");
            ele.SetAttribute("value",this.Tm_loc_info.TmNum);
        }

        private void GetGroupInfoList()
        {
            int itemCount = 0;
            GroupInfoList = new List<Models.GroupInfo>();
            var res = JObject.Parse(CommonLibrary.CommonTool.GetResult(currentUrl+string.Format("api/AutoReport/GroupInfoList?tmId={0}&ictm={1}",this.Tm_loc_info.TmId,this.Tm_loc_info.TmIctm.ToString())));
            if (res["error"].ToString()=="")
            {
                foreach (var groupInfo in res["groupList"])
                {
                    GroupInfo gi = new Models.GroupInfo();
                    gi.GroupId = groupInfo["GroupId"].ToString();
                    gi.ItemList = new List<string>();
                    foreach (var item_zh in groupInfo["ItemList"])
                    {
                        itemCount++;
                        gi.ItemList.Add(item_zh.ToString());
                    }
                    GroupInfoList.Add(gi);
                }
            }
            this.label5_itemCount.Text = itemCount.ToString();
            this.ItemCount = itemCount;
        }

        private void AutoSelectItem()
        {
            this.AutoSelectItemCount = 0;
            for (int i = 0; i < GroupInfoList.Count; i++)
            {
                PopupItemWin(GroupInfoList[i].GroupId);
                Thread.Sleep(_waitSpan);
                if (GroupInfoList.Count>0)
                {
                    if (i == (GroupInfoList.Count - 1))
                    {
                        SelectItem(GroupInfoList[i].ItemList.ToArray(), true);
                    }
                    else
                    {
                        SelectItem(GroupInfoList[i].ItemList.ToArray(), false);
                    }
                }
         
            }    
        }

        private void SelectItem(string[] pararray,bool isLastItem)
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
                IHTMLElement table_ele = null;

                foreach (var item in elelist)
                {
                    table_ele = item as IHTMLElement;
                }

                if (table_ele != null)
                {
                    //获取td列表
                    IHTMLElementCollection trlist = (table_ele.all as IHTMLElementCollection).tags("tr") as IHTMLElementCollection;
                    IHTMLElement ele1 = null, ele2 = null;
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
                                this.AutoSelectItemCount++;
                            }
                        }
                    }
                    IHTMLElement submitlist = popupdoc.all.item("b1", 0) as IHTMLElement;
                    submitlist.click();
                }
                if (isLastItem)
                {
                    dddd.close();
                    if (AutoSelectItemCount!=ItemCount)
                    {
                        MessageBox.Show("系统自动抓取的小项个数小于实际小项总数，请人工核对抓取，或者删除现有全部小项，并再次启动自动抓取");
                    }
                    else
                    {
                        MessageBox.Show("OK");
                    }
                }
            }
            catch (Exception ex)
            {

            }
      
        }

        private void PopupItemWin(string groupId)
        {
            doc = webBrowser1.Document;
            object obj = "/tmsve/commonGoods_getIntCls.xhtml";
            doc.InvokeScript("popUpWindow", new object[] { string.Format("/tmsve/commonGoods_getGoods.xhtml?code={0}&id=2CA2DB43AF7DC0A2E0537F000001C0A2", groupId) });
                   
        }

        private bool unloginneed = true;
        // bool selectingItem = false;
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (unloginneed)
            {
                doc = webBrowser1.Document;
                AutoLogin();
                unloginneed = false;
            }
        }

        #region 属性

        public HtmlDocument doc { get; set; }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("http://wssq.saic.gov.cn:9080/tmsve/sbzcsq_getSbzcMain.xhtml");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tm_loc_info_View tliv = new tm_loc_info_View(currentUrl);
            tliv.Show();
        }

        private void AutoLogin()
        {
            try
            {
                HtmlElement pinele = doc.GetElementById("pin");
                HtmlElement loginele = doc.GetElementById("pinword");
                pinele.SetAttribute("value", _pin);
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
    }
}
