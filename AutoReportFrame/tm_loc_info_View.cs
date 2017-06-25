using AutoReportFrame.Models;
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
            //
            List<Tm_loc_info> list = new List<Models.Tm_loc_info>();

        }

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
