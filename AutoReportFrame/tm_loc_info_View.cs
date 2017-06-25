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
    public partial class tm_loc_info_View : Form
    {
        public tm_loc_info_View()
        {
            InitializeComponent();
            GetTmLocInfoList();
        }


        private void GetTmLocInfoList()
        {
            //获取未被提交的一标一类信息列表
            //http://localhost:3153/
            List<Tm_loc_info> list = new List<Models.Tm_loc_info>();
            var res =JObject.Parse(CommonTool.GetResult("http://localhost:3153/api/AutoReport/TmLocInfoList?pageIndex=1"));
            if (res["error"].ToString()=="")
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
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
