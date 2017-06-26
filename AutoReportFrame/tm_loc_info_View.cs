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
        public tm_loc_info_View()
        {
            InitializeComponent();
            GetTmLocInfoList();
        }


        private void GetTmLocInfoList()
        {
            try
            {
                //获取未被提交的一标一类信息列表
                //http://localhost:3153/
                List<Tm_loc_info> list = new List<Models.Tm_loc_info>();
                var res = JObject.Parse(CommonTool.GetResult("http://localhost:3153/api/AutoReport/TmLocInfoList?pageIndex=1"));
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static event SelectTmLocInfo OnSelectTmLocInfo;
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
    }
}
