﻿namespace ImageRotate
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Base64ToImg_btn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.newImgBox = new System.Windows.Forms.PictureBox();
            this.orgImgBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ImgToBase64_Btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newImgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orgImgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ImgToBase64_Btn);
            this.panel1.Controls.Add(this.Base64ToImg_btn);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1535, 80);
            this.panel1.TabIndex = 2;
            // 
            // Base64ToImg_btn
            // 
            this.Base64ToImg_btn.Location = new System.Drawing.Point(498, 12);
            this.Base64ToImg_btn.Name = "Base64ToImg_btn";
            this.Base64ToImg_btn.Size = new System.Drawing.Size(130, 23);
            this.Base64ToImg_btn.TabIndex = 5;
            this.Base64ToImg_btn.Text = "Base64ToImg";
            this.Base64ToImg_btn.UseVisualStyleBackColor = true;
            this.Base64ToImg_btn.Click += new System.EventHandler(this.Base64ToImg_btn_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(316, 48);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(98, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "保存表格图片";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(316, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(98, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "切割表格";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(180, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "保存校正图片";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(180, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "校正";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(69, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "选择原图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 503F));
            this.tableLayoutPanel1.Controls.Add(this.newImgBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.orgImgBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 80);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1535, 642);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // newImgBox
            // 
            this.newImgBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newImgBox.Location = new System.Drawing.Point(519, 3);
            this.newImgBox.Name = "newImgBox";
            this.newImgBox.Size = new System.Drawing.Size(510, 636);
            this.newImgBox.TabIndex = 1;
            this.newImgBox.TabStop = false;
            // 
            // orgImgBox
            // 
            this.orgImgBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgImgBox.Location = new System.Drawing.Point(3, 3);
            this.orgImgBox.Name = "orgImgBox";
            this.orgImgBox.Size = new System.Drawing.Size(510, 636);
            this.orgImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.orgImgBox.TabIndex = 0;
            this.orgImgBox.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(1035, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(497, 636);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // ImgToBase64_Btn
            // 
            this.ImgToBase64_Btn.Location = new System.Drawing.Point(648, 12);
            this.ImgToBase64_Btn.Name = "ImgToBase64_Btn";
            this.ImgToBase64_Btn.Size = new System.Drawing.Size(114, 23);
            this.ImgToBase64_Btn.TabIndex = 6;
            this.ImgToBase64_Btn.Text = "ImgToBase64";
            this.ImgToBase64_Btn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1535, 722);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.newImgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orgImgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox newImgBox;
        private System.Windows.Forms.PictureBox orgImgBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button Base64ToImg_btn;
        private System.Windows.Forms.Button ImgToBase64_Btn;
    }
}

