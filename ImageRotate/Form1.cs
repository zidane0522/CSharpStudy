﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageRotate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.orgImgBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.newImgBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private Bitmap RotateImage(Bitmap bmp, double angle)
        {
            Graphics g = null;
            Bitmap tmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppRgb);
            tmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
            g = Graphics.FromImage(tmp);
            try
            {
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                g.RotateTransform((float)angle);
                g.DrawImage(bmp, 0, 0);
            }
            finally
            {
                g.Dispose();
            }
            return tmp;
        }

        private void work()
        {
            //string fnIn="";
            //string fnOut = "";
            Bitmap bmpIn = new Bitmap(this.orgImgBox.Image);

            gmseDeskew sk = new gmseDeskew(bmpIn);
            double skewangle = sk.GetSkewAngle();
            Bitmap bmpOut = RotateImage(bmpIn, -skewangle);
            
            this.newImgBox.Image = bmpOut as Image;
            //bmpOut.Save(fnOut, ImageFormat.Jpeg);//此处简单保存，可采用压缩方式保存
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                this.orgImgBox.Image = Image.FromFile(ofd.FileName);
   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            work();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.jpg)|*.jpg"; 
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                this.newImgBox.Image.Save(sfd.FileName,ImageFormat.Jpeg);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cutExcel();
        }

        private void cutExcel()
        {
            this.pictureBox1.Image= BaiduOcr.GeneralBasic(this.newImgBox.Image);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.jpg)|*.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void Base64ToImg_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Title = "选择要转换的base64编码的文本";
            dlg.Filter = "txt files|*.txt";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                for (int i = 0; i < dlg.FileNames.Length; i++)
                {
                    Base64StringToImage(dlg.FileNames[i].ToString());
                }

            }
        }
        public string ImageToBase64(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(arr);
        }
        //base64编码的文本 转为    图片
        private void Base64StringToImage(string txtFileName)
        {
            try
            {
                FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(ifs);

                String inputStr = sr.ReadToEnd();
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();
                sr.Close();
                ifs.Close();
                this.pictureBox1.Image = bmp;
                if (File.Exists(txtFileName))
                {
                    File.Delete(txtFileName);
                }
                //MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }
    }



    #region 算法处理类
    public class gmseDeskew
    {
        public class HougLine
        {
            // Count of points in the line.
            public int Count;
            // Index in Matrix.
            public int Index;
            // The line is represented as all x,y that solve y*cos(alpha)-x*sin(alpha)=d 
            public double Alpha;
            public double d;
        }
        Bitmap cBmp;
        double cAlphaStart = -20;
        double cAlphaStep = 0.2;
        int cSteps = 40 * 5;
        double[] cSinA;
        double[] cCosA;
        double cDMin;
        double cDStep = 1;
        int cDCount;
        // Count of points that fit in a line.
        int[] cHMatrix;
        public double GetSkewAngle()
        {
            gmseDeskew.HougLine[] hl = null;
            int i = 0;
            double sum = 0;
            int count = 0;
            // Hough Transformation 
            Calc();
            // Top 20 of the detected lines in the image.
            hl = GetTop(20);
            // Average angle of the lines
            for (i = 0; i <= 19; i++)
            {
                sum += hl[i].Alpha;
                count += 1;
            }
            return sum / count;
        }
        private HougLine[] GetTop(int Count)
        {
            HougLine[] hl = null;
            int i = 0;
            int j = 0;
            HougLine tmp = null;
            int AlphaIndex = 0;
            int dIndex = 0;
            hl = new HougLine[Count + 1];
            for (i = 0; i <= Count - 1; i++)
            {
                hl[i] = new HougLine();
            }
            for (i = 0; i <= cHMatrix.Length - 1; i++)
            {
                if (cHMatrix[i] > hl[Count - 1].Count)
                {
                    hl[Count - 1].Count = cHMatrix[i];
                    hl[Count - 1].Index = i;
                    j = Count - 1;
                    while (j > 0 && hl[j].Count > hl[j - 1].Count)
                    {
                        tmp = hl[j];
                        hl[j] = hl[j - 1];
                        hl[j - 1] = tmp; j -= 1;
                    }
                }
            }
            for (i = 0; i <= Count - 1; i++)
            {
                dIndex = hl[i].Index / cSteps;
                AlphaIndex = hl[i].Index - dIndex * cSteps;
                hl[i].Alpha = GetAlpha(AlphaIndex);
                hl[i].d = dIndex + cDMin;
            }
            return hl;
        }
        public gmseDeskew(Bitmap bmp)
        {
            cBmp = bmp;
        }
        private void Calc()
        {
            int x = 0;
            int y = 0;
            int hMin = cBmp.Height / 4;
            int hMax = cBmp.Height * 3 / 4;
            Init();
            for (y = hMin; y <= hMax; y++)
            {
                for (x = 1; x <= cBmp.Width - 2; x++)
                {    // Only lower edges are considered.
                    if (IsBlack(x, y))
                    {
                        if (!IsBlack(x, y + 1))
                        {
                            Calc(x, y);
                        }
                    }
                }
            }
        }
        private void Calc(int x, int y)
        {
            int alpha = 0;
            double d = 0;
            int dIndex = 0;
            int Index = 0;
            for (alpha = 0; alpha <= cSteps - 1; alpha++)
            {
                d = y * cCosA[alpha] - x * cSinA[alpha];
                dIndex = (int)CalcDIndex(d);
                Index = dIndex * cSteps + alpha;
                try
                {
                    cHMatrix[Index] += 1;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
        private double CalcDIndex(double d)
        {
            return Convert.ToInt32(d - cDMin);
        }
        private bool IsBlack(int x, int y)
        {
            Color c = default(Color);
            double luminance = 0;
            c = cBmp.GetPixel(x, y);
            luminance = (c.R * 0.299) + (c.G * 0.587) + (c.B * 0.114);
            return luminance < 140;
        }
        private void Init()
        {
            int i = 0;
            double angle = 0;
            // Precalculation of sin and cos. 
            cSinA = new double[cSteps];
            cCosA = new double[cSteps];
            for (i = 0; i <= cSteps - 1; i++)
            {
                angle = GetAlpha(i) * Math.PI / 180.0;
                cSinA[i] = Math.Sin(angle);
                cCosA[i] = Math.Cos(angle);
            }  // Range of d: 
            cDMin = -cBmp.Width;
            cDCount = (int)(2 * (cBmp.Width + cBmp.Height) / cDStep);
            cHMatrix = new int[cDCount * cSteps + 1];
        }
        public double GetAlpha(int Index)
        {
            return cAlphaStart + Index * cAlphaStep;
        }
    }
    #endregion 类结束
}
