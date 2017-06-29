using AutoReportFrame.Models;
using CommonLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoReportFrame
{
    public delegate void SelectTmLocInfo(Tm_loc_info tm_loc_info);
    public partial class tm_loc_info_View : Form
    {
        public tm_loc_info_View(string url)
        {
            InitializeComponent();
            this.currentUrl = url;
            pageNo = 1;
            this.label5_pageNo.Text = "1";
            GetTmLocInfoList(1);
          
            this.label5_pageNo.Text = "1";
        }

        private string condition = "";

        private string currentUrl = "";

        public int pageNo { get; set; }

        /// <summary>
        /// 获取一标一类列表
        /// </summary>
        /// <param name="pageIndex"></param>
        private void GetTmLocInfoList(int pageIndex)
        {
            try
            {
                //获取未被提交的一标一类信息列表
                //http://localhost:3153/
                List<Tm_loc_info> list = new List<Models.Tm_loc_info>();
                var res = JObject.Parse(CommonTool.GetResult(currentUrl + "api/AutoReport/TmLocInfoList?pageIndex="+pageIndex.ToString() + condition));
                if (res["error"].ToString() == "")
                {
                    foreach (var item in res["list"])
                    {
                        Tm_loc_info t = new Tm_loc_info();
                        t.Applicant = item["Applicant"].ToString();
                        t.Phone = item["Phone"].ToString();
                        t.TmIctm = Convert.ToInt16(item["TmIctm"].ToString());
                        t.TmId = item["TmId"].ToString();
                        t.TmName = item["TmName"].ToString();
                        t.TmNum = item["TmNum"].ToString();
                        list.Add(t);
                    }
                }
                this.dataGridView1.DataSource = list;
                this.dataGridView1.Columns["Applicant"].HeaderText = "申请人";
                this.dataGridView1.Columns["Phone"].HeaderText = "手机号";
                this.dataGridView1.Columns["TmName"].HeaderText = "商标名字";
                this.dataGridView1.Columns["TmIctm"].HeaderText = "类别";
                this.dataGridView1.Columns["TmNum"].HeaderText = "编号";
                this.dataGridView1.Columns["TmId"].Visible = false;
            }
            catch (Exception ex)
            {
            }
           

        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static event SelectTmLocInfo OnSelectTmLocInfo;


        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            
            Tm_loc_info t = new Models.Tm_loc_info();
            t.Applicant = this.dataGridView1.SelectedRows[0].Cells["Applicant"].Value.ToString();
            t.Phone= this.dataGridView1.SelectedRows[0].Cells["Phone"].Value.ToString();
            t.TmIctm = (int)this.dataGridView1.SelectedRows[0].Cells["TmIctm"].Value;
            t.TmId = this.dataGridView1.SelectedRows[0].Cells["TmId"].Value.ToString();
            t.TmName = this.dataGridView1.SelectedRows[0].Cells["TmName"].Value.ToString();
            t.TmNum = this.dataGridView1.SelectedRows[0].Cells["TmNum"].Value.ToString();
            if (OnSelectTmLocInfo!=null)
            {
                OnSelectTmLocInfo(t);
            }

            this.Close();
        }

        /// <summary>
        /// 清空查询条件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1_tmName.Text = "";
            this.textBox2_applictName.Text = "";
            this.textBox3_phone.Text = "";
            condition = "";
            GetTmLocInfoList(1);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (pageNo==1)
            {
                MessageBox.Show("已经是第一页");
            }
            else
            {
                GetTmLocInfoList(pageNo-1);
                pageNo--;
                this.label5_pageNo.Text = pageNo.ToString();
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            GetTmLocInfoList(pageNo + 1);
            pageNo++;
            this.label5_pageNo.Text = pageNo.ToString();
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {//string phone=null,string tmName=null,string applicant=null
            pageNo = 1;
            this.label5_pageNo.Text = "1";
            condition = "";
            if (textBox3_phone.Text.Trim()!="")
            {
                condition += "&phone=" + textBox3_phone.Text.Trim();
            }
            else
            {
                condition += "&phone=";
            }
            if (textBox1_tmName.Text.Trim()!="")
            {
                condition += "&tmName=" + textBox1_tmName.Text.Trim();
            }
            else
            {
                condition += "&tmName=";
            }
            if (textBox2_applictName.Text.Trim()!="")
            {
                condition += "&applicant=" + textBox2_applictName.Text.Trim();
            }
            else
            {
                condition += "&applicant=";
            }
            GetTmLocInfoList(1);
        }
    }
}
