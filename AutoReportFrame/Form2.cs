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
        public Form2()
        {
            InitializeComponent();
            currentUrl = localUrl;//选择网络链接或本地链接
            _pin = "112233";
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
            this.FormClosing += Form2_FormClosing;
            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            tm_loc_info_View.OnSelectTmLocInfo += Tm_loc_info_View_OnSelectTmLocInfo;
            tm_loc_info_View.OnLoadTmLocInfoOver += Tm_loc_info_View_OnLoadTmLocInfoOver;
            //this.FormClosed += Form2_FormClosed;
        }

        private void Tm_loc_info_View_OnLoadTmLocInfoOver(tm_loc_info_View tliv)
        {
            Action d = () => { tliv.Show(); };
            this.Invoke(d);          
        }

        public Form2(Form loginForm,string pin)
        {
            InitializeComponent();
            currentUrl = webUrl;//选择网络链接或本地链接
            _pin = pin;
            f = loginForm;
            this.webBrowser1.Url = new Uri("http://wssq.saic.gov.cn:9080/tmsve/");
            this.FormClosing += Form2_FormClosing;
            this.webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            tm_loc_info_View.OnSelectTmLocInfo += Tm_loc_info_View_OnSelectTmLocInfo;
            tm_loc_info_View.OnLoadTmLocInfoOver += Tm_loc_info_View_OnLoadTmLocInfoOver;
            this.FormClosed += Form2_FormClosed;
        }



        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

            f.Show();
        }


        #region 字段与属性

        private Form f;

        private string _pin = "";

        public Models.Tm_loc_info Tm_loc_info { get; set; }

        public List<GroupInfo> GroupInfoList { get; set; }

        private string currentUrl = "";

        private string localUrl = "http://localhost:3153/";

        private string webUrl = "http://api.alibiaobiao.cn/";

        public int ItemCount { get; set; }

        public int AutoSelectItemCount { get; set; }

        #endregion

        /// <summary>
        /// 选择一标一类后，开始具体信息解析处理。
        /// </summary>
        /// <param name="tm_loc_info"></param>
        private void Tm_loc_info_View_OnSelectTmLocInfo(Models.Tm_loc_info tm_loc_info)
        {
            this.Tm_loc_info = tm_loc_info;
            this.label2_applicant.Text = tm_loc_info.Applicant;
            this.label3_tmName.Text = tm_loc_info.TmName;
            this.label5_ictm.Text = tm_loc_info.TmIctm.ToString();
            this.label3_tmNum.Text = tm_loc_info.TmNum.Replace("-","");
            AutoWriteTmNum();
            AutoWriteApplicantInfo();
            GetGroupInfoList();
            AutoSelectItem();
        }


        public Applicant CurrentApplicant { get; set; }

        /// <summary>
        /// 自动写入申请人信息
        /// </summary>
        private void AutoWriteApplicantInfo()
        {
            string restr = CommonLibrary.CommonTool.GetResult(currentUrl + string.Format("api/AutoReport/GetApplicant?tmId={0}", this.Tm_loc_info.TmId));
            var res = JObject.Parse(restr);
            if (res["error"].ToString()=="")
            {
                if (CurrentApplicant == null)
                {
                    CurrentApplicant = new Models.Applicant();                 
                }
                CurrentApplicant.Name = res["data"]["name"].ToString();
                CurrentApplicant.Category = res["data"]["category"].ToString();
                CurrentApplicant.Address = res["data"]["address"].ToString();
                CurrentApplicant.City = res["data"]["city"].ToString();
                CurrentApplicant.Contact = res["data"]["contact"].ToString();
                CurrentApplicant.Country = res["data"]["county"].ToString();
                CurrentApplicant.IDCardNum = res["data"]["idCardNum"].ToString();
                CurrentApplicant.Province = res["data"]["province"].ToString();
                string region =CurrentApplicant.Province+ CurrentApplicant.City + CurrentApplicant.Country;
                string regionIndex = "1";
                string address = CurrentApplicant.Province + CurrentApplicant.City + CurrentApplicant.Country + CurrentApplicant.Address;
                if (CurrentApplicant.Province.Contains("北京市")|| CurrentApplicant.Province.Contains("天津市")|| CurrentApplicant.Province.Contains("重庆市")|| CurrentApplicant.Province.Contains("上海市"))
                {
                    region = CurrentApplicant.City + CurrentApplicant.Country;
                    address = CurrentApplicant.City + CurrentApplicant.Country + CurrentApplicant.Address;
                }
                if (CurrentApplicant.Province.Contains("香港"))
                {
                    region = "香港";
                    regionIndex = "4";
                    address = CurrentApplicant.City + CurrentApplicant.Country + CurrentApplicant.Address;

                }
                else if(CurrentApplicant.Province.Contains("澳门"))
                {
                    region = "澳门";
                    regionIndex = "5";
                    address = CurrentApplicant.City + CurrentApplicant.Country + CurrentApplicant.Address;

                }
                else if (CurrentApplicant.Province.Contains("台湾"))
                {
                    region = "台湾";
                    regionIndex = "3";
                }
                //opetate_sqType(2);opetate_appGjdq_sqType(2,1)
                //opetate_appGjdq(2);opetate_appGjdq_2(2);opetate_appGjdq_sqType(2,2)
                if (CurrentApplicant.Category=="法人或其他组织")
                {
                    doc.GetElementById("appTypeId").SetAttribute("selectedIndex", "1");

                    doc.InvokeScript("opetate_sqType",new object[] { 2});
                    doc.InvokeScript("opetate_appGjdq_sqType", new object[] { 2,1 });

                    doc.GetElementById("appGjdq").SetAttribute("selectedIndex", regionIndex);

                    doc.InvokeScript("opetate_appGjdq", new object[] { 2 });
                    doc.InvokeScript("opetate_appGjdq_2", new object[] { 2 });
                    doc.InvokeScript("opetate_appGjdq_sqType", new object[] { 2, 2 });
                }
                else
                {
                    doc.GetElementById("appTypeId").SetAttribute("selectedIndex", "2");
                    doc.InvokeScript("opetate_sqType", new object[] { 2 });
                    doc.InvokeScript("opetate_appGjdq_sqType", new object[] { 2, 1 });

                    doc.GetElementById("appGjdq").SetAttribute("selectedIndex", regionIndex);

                    doc.InvokeScript("opetate_appGjdq", new object[] { 2 });
                    doc.InvokeScript("opetate_appGjdq_2", new object[] { 2 });
                    doc.InvokeScript("opetate_appGjdq_sqType", new object[] { 2, 2 });


                    doc.GetElementById("appCertificateId").SetAttribute("selectedIndex", "1");
                    doc.GetElementById("appCertificateNum").SetAttribute("value",CurrentApplicant.IDCardNum);                    

                }
        
                doc.GetElementById("agentPerson").SetAttribute("value", "武奇鹏");
                doc.GetElementById("appCnName").SetAttribute("value", CurrentApplicant.Name);
                doc.GetElementById("appContactPerson").SetAttribute("value", "武奇鹏");

                //if (regionIndex=="1")
                //{
                //    doc.GetElementById("appRegionalismId").SetAttribute("value", region);
                //}
                //else
                //{

                //}
                doc.GetElementById("appCnAddr").SetAttribute("value", address);

                doc.GetElementById("appContactTel").SetAttribute("value", "010-57743079");
                doc.GetElementById("appContactZip").SetAttribute("value", "100012");

            }
            else
            {
                MessageBox.Show(res["error"].ToString());
            }
        }

        /// <summary>
        /// 将一标一类号码作为代理文号写入商标局系统
        /// </summary>
        private void AutoWriteTmNum()
        {
            try
            {
                HtmlElement ele = doc.GetElementById("agentFilenum");
                ele.SetAttribute("value", this.label3_tmNum.Text);
            }
            catch (Exception ex)
            {

            }       
        }

        /// <summary>
        /// 获取一标一类的群组信息列表
        /// </summary>
        private void GetGroupInfoList()
        {
            int itemCount = 0;
            if (GroupInfoList==null)
            {
                GroupInfoList = new List<Models.GroupInfo>();
            }
            else
            {
                GroupInfoList.Clear();
            }     
            string restr = CommonLibrary.CommonTool.GetResult(currentUrl + string.Format("api/AutoReport/GroupInfoList?tmId={0}&ictm={1}", this.Tm_loc_info.TmId, this.Tm_loc_info.TmIctm.ToString()));
            var res = JObject.Parse(restr);
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


        /// <summary>
        /// 遍历群组，开始选择小项
        /// </summary>
        private void AutoSelectItem()
        {
            this.AutoSelectItemCount = 0;
            for (int i = 0; i < GroupInfoList.Count; i++)
            {
                PopupItemWin(GroupInfoList[i].GroupId);
                Thread.Sleep(_span*1000);
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

        /// <summary>
        /// 自动选择小项具体处理
        /// </summary>
        /// <param name="pararray">小项中文说明列表</param>
        /// <param name="isLastItem">是否是最后一组要选择的小项</param>
        private void SelectItem(string[] pararray,bool isLastItem)
        {
            try
            {
                //获取弹出窗口对象信息
                IHTMLDocument2 hdoc = doc.DomDocument as IHTMLDocument2;
                IHTMLWindow2 win = hdoc.parentWindow as mshtml.IHTMLWindow2;
                var d = win.execScript(@"function sucdd(){ return popUpWin;}", "javascript");
                if (hdoc==null)
                {
                    MessageBox.Show("hdoc is null");
                }
                if (win==null)
                {
                    MessageBox.Show("win is null");
                }
                //HTMLWindow2Class dddd = doc.InvokeScript("sucdd") as HTMLWindow2Class;
                IHTMLWindow2 dddd = doc.InvokeScript("sucdd") as IHTMLWindow2;
                
                if (dddd==null)
                {
                    MessageBox.Show("DDDDDD");
                }     
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
                MessageBox.Show(ex.Message);
            }
      
        }

        /// <summary>
        /// 弹出小项选择网页
        /// </summary>
        /// <param name="groupId"></param>
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
            if (unloginneed)//自动登陆商标局系统
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("http://wssq.saic.gov.cn:9080/tmsve/wdsqgl_getappType.xhtml");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowTMLocView_async();

        }

        private async void ShowTMLocView_async()
        {
            await Task.Run(()=> {
                tm_loc_info_View tliv = new tm_loc_info_View(currentUrl);
            });
        }

        /// <summary>
        /// 自动写入信息并登陆
        /// </summary>
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

        private int _span = 3;

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this._span = int.Parse(this.textBox1.Text.Trim());
            }
            catch (Exception ex)
            {
                this._span = 3;
            }
        }

        private void button3_reLoadItem_Click(object sender, EventArgs e)
        {
            if (this.Tm_loc_info!=null)
            {
                doc = webBrowser1.Document;
                doc.InvokeScript("removeAll");
                AutoSelectItem();
            }
            else
            {
                MessageBox.Show("请先选择一标一类");
            }
        }

  
    }
}
