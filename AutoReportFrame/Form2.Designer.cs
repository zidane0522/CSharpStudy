namespace AutoReportFrame
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3_tmNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5_ictm = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3_tmName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2_applicant = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label3 = new System.Windows.Forms.Label();
            this.label5_itemCount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5_itemCount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label3_tmNum);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5_ictm);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3_tmName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2_applicant);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1230, 143);
            this.panel1.TabIndex = 1;
            // 
            // label3_tmNum
            // 
            this.label3_tmNum.AutoSize = true;
            this.label3_tmNum.Location = new System.Drawing.Point(890, 108);
            this.label3_tmNum.Name = "label3_tmNum";
            this.label3_tmNum.Size = new System.Drawing.Size(44, 18);
            this.label3_tmNum.TabIndex = 11;
            this.label3_tmNum.Text = "编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(810, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "编号";
            // 
            // label5_ictm
            // 
            this.label5_ictm.AutoSize = true;
            this.label5_ictm.Location = new System.Drawing.Point(890, 82);
            this.label5_ictm.Name = "label5_ictm";
            this.label5_ictm.Size = new System.Drawing.Size(116, 18);
            this.label5_ictm.TabIndex = 9;
            this.label5_ictm.Text = "当前商标类别";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(810, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "类别";
            // 
            // label3_tmName
            // 
            this.label3_tmName.AutoSize = true;
            this.label3_tmName.Location = new System.Drawing.Point(890, 50);
            this.label3_tmName.Name = "label3_tmName";
            this.label3_tmName.Size = new System.Drawing.Size(116, 18);
            this.label3_tmName.TabIndex = 7;
            this.label3_tmName.Text = "当前商标名字";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(810, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "商标名";
            // 
            // label2_applicant
            // 
            this.label2_applicant.AutoSize = true;
            this.label2_applicant.Location = new System.Drawing.Point(890, 20);
            this.label2_applicant.Name = "label2_applicant";
            this.label2_applicant.Size = new System.Drawing.Size(134, 18);
            this.label2_applicant.TabIndex = 5;
            this.label2_applicant.Text = "当前申请人名字";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(810, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "申请人";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(553, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 42);
            this.button4.TabIndex = 3;
            this.button4.Text = "选择一标一类";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(389, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 43);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(217, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 143);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(1230, 395);
            this.webBrowser1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(577, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "小项总数";
            // 
            // label5_itemCount
            // 
            this.label5_itemCount.AutoSize = true;
            this.label5_itemCount.Location = new System.Drawing.Point(679, 108);
            this.label5_itemCount.Name = "label5_itemCount";
            this.label5_itemCount.Size = new System.Drawing.Size(80, 18);
            this.label5_itemCount.TabIndex = 13;
            this.label5_itemCount.Text = "小项总数";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 538);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "自动上报系统";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2_applicant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5_ictm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3_tmName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3_tmNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5_itemCount;
        private System.Windows.Forms.Label label3;
    }
}