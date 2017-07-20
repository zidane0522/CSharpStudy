using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRotate
{
    struct location
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int H { get; set; }

        public int W { get; set; }
    }

    class BaiduOcr
    {
        static string apiKey = "rDKFcffKEv9am0vV0fq1QWo2";
        static string secretKey = "lXS9Mn2NkCqW9rOqBDot9LAwfjm36Gj1";

        public static Image GeneralBasic(Image imageIn)
        {
            bool isBegin = false;

            location fwloc = new ImageRotate.location();
            location dyczloc = new ImageRotate.location();
            

            var client = new Baidu.Aip.Ocr.Ocr(apiKey, secretKey);
            var image = ImageToByteArray(imageIn);
            JObject result = client.GeneralWithLocatin(image, null);

            JToken js = result["words_result"];
            for (int i = 0; i < js.Count(); i++)
            {
                JToken item = js[i];
                if (item["words"].ToString().Contains("发文时间"))
                {
                    fwloc.X = int.Parse(item["location"]["left"].ToString());
                    fwloc.Y = int.Parse(item["location"]["top"].ToString());
                    fwloc.H = int.Parse(item["location"]["height"].ToString());
                    fwloc.W = int.Parse(item["location"]["width"].ToString());
                }
                else if (item["words"].ToString().Contains("本打印操作"))
                {
                    dyczloc.X = int.Parse(item["location"]["left"].ToString());
                    dyczloc.Y = int.Parse(item["location"]["top"].ToString());
                    dyczloc.H = int.Parse(item["location"]["height"].ToString());
                    dyczloc.W = int.Parse(item["location"]["width"].ToString());
                }
            }


            Bitmap bitmap = new Bitmap(imageIn.Width - 40, dyczloc.Y - fwloc.Y - fwloc.H - 40);
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区
            graphic.DrawImage(imageIn, 0, 0, new Rectangle(20, fwloc.Y+20+ fwloc.H, imageIn.Width - 40, dyczloc.Y - fwloc.Y - fwloc.H - 60), GraphicsUnit.Pixel);
            //从作图区生成新图
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            return saveImage;

            //string rid = null, aid = null;
            //int index = 0;
            //foreach (var j in js)
            //{
            //    var s = j["words"].ToString();

            //    if (s.StartsWith("发文时间"))
            //    {
            //        DateTime dt = DateTime.Parse(s.Split(':')[1]);
       
            //    }

            //    if (isBegin)
            //    {
            //        switch (index)
            //        {
            //            case 0:
            //                index++;
            //                rid = s;
            //                break;
            //            case 1:
            //                index++;
            //                aid = s;
            //                break;
            //            case 2:
                            
            //                index = 0;
            //                break;
            //        }
            //    }
            //    if (s.StartsWith("本打印操作"))
            //    {
            //        isBegin = false;
            //    }
            //    if (s == "备注")
            //    {
            //        isBegin = true;
            //    }
            //}
            return null;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static void FormBegin(Image imageIn)
        {
            bool isBegin = false;
 
            var form = new Baidu.Aip.Ocr.Form(apiKey, secretKey);
            //var client = new Baidu.Aip.Ocr.Ocr(apiKey, secretKey);
            var image = ImageToByteArray(imageIn);
            JObject result = form.BeginRecognition(image);
            // JObject result = client.GeneralBasic(image, null);
            var options = new Dictionary<string, object>()
            {
                {"result_type", "json"}  // 或者为excel
            };
            JToken js = result["result"][0]["request_id"];

            JObject result1;

            do
            {
                result1 = form.GetRecognitionResult(js.ToString(), options);

            }
            while (result1["result"]["ret_code"].ToString() != "3");




            string rid = null, aid = null;
            int index = 0;
            foreach (var j in js)
            {
                var s = j["words"].ToString();

                if (s.StartsWith("发文时间"))
                {
                    DateTime dt = DateTime.Parse(s.Split(':')[1]);

                }

                if (isBegin)
                {
                    switch (index)
                    {
                        case 0:
                            index++;
                            rid = s;
                            break;
                        case 1:
                            index++;
                            aid = s;
                            break;
                        case 2:

                            index = 0;
                            break;
                    }
                }
                if (s.StartsWith("本打印操作"))
                {
                    isBegin = false;
                }
                if (s == "备注")
                {
                    isBegin = true;
                }
            }
        }

        public static void CutImg()
        {

        }
    }
}
