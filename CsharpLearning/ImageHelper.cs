using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
    public class ImageHelper
    {
        /// <summary>
        /// 比较两个图像是否相同，使用特征码比较
        /// </summary>
        /// <param name="source">源图像</param>
        /// <param name="target">目标图像</param>
        /// <param name="per">判定相同的阈值</param>
        /// <returns>是否相同</returns>
        private static bool ComparFeatureCode(Bitmap source, Bitmap target, int per)
        {
            byte[] sourceCode = GetImgeFeatureCode(source);
            byte[] targetCode = GetImgeFeatureCode(target);
            int sameCount = 0;
            int unsameCount = 0;
            for (int i = 0; i < sourceCode.Count(); i++)
            {
                if (sourceCode[i] == targetCode[i])
                    sameCount++;
                else
                    unsameCount++;
            }
            int bi = (sameCount / (sameCount + unsameCount)) * 100;
            return bi >= per;
        }


        private static byte[] GetImgeFeatureCode(Bitmap source)
        {
            byte averageGray = 0;
            return GetGrayComparCode(Get64GrayImage(ResizeUsingGDIPlus(source, 32, 32), out averageGray), averageGray);

        }

        /// <summary>
        /// 获取图像的字符串特征码
        /// </summary>
        /// <param name="source">图像</param>
        /// <returns>字符串特征码</returns>
        public static string GetImgeFeatureStringCode(Bitmap source)
        {
            byte averageGray = 0;
            return GetGrayComparStringCode(Get64GrayImage(ResizeUsingGDIPlus(source, 32, 32), out averageGray), averageGray);

        }

        /// <summary>
        /// 与平均值比较生成指纹
        /// </summary>
        /// <param name="bitmap">图像</param>
        /// <param name="averageGray">平均值</param>
        /// <returns>比较结果</returns>
        private static byte[] GetGrayComparCode(Bitmap srcBmp, byte averageGray)
        {
            int count = 0;
            byte[] result = new byte[srcBmp.Width * srcBmp.Height];
            Rectangle rect = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
            BitmapData srcBmpData = srcBmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr srcPtr = srcBmpData.Scan0;
            int scanWidth = srcBmpData.Width * 3;
            int src_bytes = scanWidth * srcBmp.Height;
            byte[] srcRGBValues = new byte[src_bytes];
            Marshal.Copy(srcPtr, srcRGBValues, 0, src_bytes);
            //灰度化处理
            int k = 0;
            for (int i = 0; i < srcBmp.Height; i++)
            {
                for (int j = 0; j < srcBmp.Width; j++)
                {
                    k = j * 3;
                    result[count++] = byte.Parse(srcRGBValues[i * scanWidth + k + 0] > averageGray ? "1" : "0");
                }
            }
            Marshal.Copy(srcRGBValues, 0, srcPtr, src_bytes);
            //解锁位图
            srcBmp.UnlockBits(srcBmpData);
            return result;
        }

        /// <summary>
        /// 与平均值比较生成指纹
        /// </summary>
        /// <param name="bitmap">图像</param>
        /// <param name="averageGray">平均值</param>
        /// <returns>比较结果</returns>
        private static string GetGrayComparStringCode(Bitmap srcBmp, byte averageGray)
        {
            StringBuilder sb = new StringBuilder();
            Rectangle rect = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
            BitmapData srcBmpData = srcBmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr srcPtr = srcBmpData.Scan0;
            int scanWidth = srcBmpData.Width * 3;
            int src_bytes = scanWidth * srcBmp.Height;
            byte[] srcRGBValues = new byte[src_bytes];
            Marshal.Copy(srcPtr, srcRGBValues, 0, src_bytes);
            //灰度化处理
            int k = 0;
            for (int i = 0; i < srcBmp.Height; i++)
            {
                for (int j = 0; j < srcBmp.Width; j++)
                {
                    k = j * 3;
                    sb.Append(byte.Parse(srcRGBValues[i * scanWidth + k + 0] > averageGray ? "1" : "0"));
                }
            }
            Marshal.Copy(srcRGBValues, 0, srcPtr, src_bytes);
            //解锁位图
            srcBmp.UnlockBits(srcBmpData);
            return sb.ToString();
        }

        /// <summary>
        /// 改变尺寸
        /// </summary>
        /// <param name="original"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        private static Bitmap ResizeUsingGDIPlus(Bitmap original, int newWidth, int newHeight)
        {
            try
            {
                Bitmap bitmap = new Bitmap(newWidth, newHeight);
                Graphics graphics = Graphics.FromImage(bitmap);
                // 插值算法的质量
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(original, new Rectangle(0, 0, newWidth, newHeight),
                    new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
                graphics.Dispose();
                return bitmap;
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// 获取64级灰度图像，将制定图片转化为64级灰度图
        /// </summary>
        /// <param name="srcBmp">源图像</param>
        /// <returns>灰度图像</returns>
        private static Bitmap Get64GrayImage(Bitmap srcBmp, out byte averageGray)
        {
            int count = 0;
            int sum = 0;
            Rectangle rect = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
            BitmapData srcBmpData = srcBmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr srcPtr = srcBmpData.Scan0;
            int scanWidth = srcBmpData.Width * 3;
            int src_bytes = scanWidth * srcBmp.Height;
            byte[] srcRGBValues = new byte[src_bytes];
            Marshal.Copy(srcPtr, srcRGBValues, 0, src_bytes);
            //灰度化处理
            int k = 0;
            for (int i = 0; i < srcBmp.Height; i++)
            {
                for (int j = 0; j < srcBmp.Width; j++)
                {
                    count++;
                    k = j * 3;
                    //把256级灰度压缩为64级灰度，取值为3,7,11,15...255
                    //只处理每行中图像像素数据,舍弃未用空间
                    //注意位图结构中RGB按BGR的顺序存储
                    //0.299*R + 0.587*G + 0.144*B = 亮度或灰度
                    //只处理每行中图像像素数据,舍弃未用空间
                    //注意位图结构中RGB按BGR的顺序存储
                    byte intensity = (byte)(srcRGBValues[i * scanWidth + k + 2] * 0.299
                         + srcRGBValues[i * scanWidth + k + 1] * 0.587
                         + srcRGBValues[i * scanWidth + k + 0] * 0.114);
                    //生成64位灰度值
                    intensity = byte.Parse(((Math.Ceiling(((decimal)(intensity + 1) / 4))) * 4 - 1).ToString());
                    srcRGBValues[i * scanWidth + k + 0] = intensity;
                    srcRGBValues[i * scanWidth + k + 1] = intensity;
                    srcRGBValues[i * scanWidth + k + 2] = intensity;
                    sum += intensity;
                }
            }
            Marshal.Copy(srcRGBValues, 0, srcPtr, src_bytes);
            //解锁位图
            srcBmp.UnlockBits(srcBmpData);
            averageGray = byte.Parse((sum / count).ToString());
            return srcBmp;
        }


    }
}
