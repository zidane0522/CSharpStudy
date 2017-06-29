using CommonLibrary;
using Newtonsoft.Json.Linq;
using sidaxin.common;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.loginName_txt.Text == "" || this.loginPwd_txt.Text == "")
                {
                    MessageBox.Show("请输入用户名和密码！");
                }
                else
                {
                    string loginName = this.loginName_txt.Text;
                    string loginPwd = Crypto.GetMD5(this.loginPwd_txt.Text);

                    JObject obj = null;
                    string postData = "n={0}&p={1}";
                    string datas = string.Format(postData, loginName,loginPwd);
                    string url = CommonLibrary.CommonTool.BaseUrl + "api/Admin/GetLogin?" + datas;

                    var result = CommonTool.GetResult(url);
                    obj = JObject.Parse(result);
                    string a = obj["Successful"].ToString();
                    if (obj["Successful"].ToString() == "True")
                    {
                        this.Hide();
                        Form2 f2 = new Form2(this,this.textBox1_PIN.Text);
                        f2.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录出现异常！" + ex.Message);
                this.Close();
            }
        }

        /// <summary>
        /// 取消登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
