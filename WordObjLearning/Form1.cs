using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordObjLearning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            object path;
            string strContent;
            Word.Application wordApp;
            Word.Document wordDoc;

            path = @"D:\MyWord.docx";
            wordApp = new Word.ApplicationClass();
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }
            Object Nothing = Missing.Value;
            wordDoc = wordApp.Documents.Add(ref Nothing,ref Nothing,ref Nothing,ref Nothing);
            strContent = "写入文本\n";
            wordDoc.Paragraphs.Last.Range.Text = strContent;
            strContent = "写入第二行";
            wordDoc.Paragraphs.Last.Range.Text = strContent;
            object format = Word.WdSaveFormat.wdFormatDocumentDefault;
            //wordDoc.SaveAs2(ref path,ref format,ref Nothing,ref Nothing,ref Nothing, ref format, ref Nothing, ref Nothing, ref Nothing, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            wordDoc.SaveAsQuickStyleSet((string)path);
            wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            Console.WriteLine(path+"创建完毕！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object path;
            string strContent;
            Word.Application wordApp;
            Word.Document wordDoc;
            path = @"D:\MyWord.pdf";
            wordApp = new Word.ApplicationClass();
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }
            Object Nothing = Missing.Value;
            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing);
            Word.Table table = wordDoc.Tables.Add(wordApp.Selection.Range,5,5,ref Nothing,ref Nothing);
            table.Borders.Enable = 1;
            for (int i = 1; i <=5; i++)
            {
                for (int j = 0; j <=5; j++)
                {
                    table.Cell(i, j).Range.Text = "第" + i + "行，第" + j + "列";
                }
            }
            object format = Word.WdSaveFormat.wdFormatPDF;
            //wordDoc.SaveAsQuickStyleSet((string)path);
            wordDoc.SaveAs2(ref path,ref format,ref Nothing,ref Nothing,ref Nothing,ref Nothing,ref Nothing,false,ref Nothing,ref Nothing,ref Nothing,ref Nothing,ref Nothing,ref Nothing,ref Nothing,ref Nothing,ref Nothing);
            wordDoc.Close(false, ref Nothing, ref Nothing);
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            Console.WriteLine(path+"创建完毕！");

            //wordDoc.sa
        }

        private void button3_Click(object sender, EventArgs e)
        {
            object path;
            string strContent;
            Word.Application wordApp;
            Word.Document wordDoc;
            path = @"D:\MyWord.docx";
            object tpath = @"D:\呵呵呵.dotx";

            wordApp = new Word.ApplicationClass();
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }
            Object Nothing = Missing.Value;
            wordDoc = wordApp.Documents.Open(tpath,ref Nothing,true,ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,true, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            //Word.Table table = wordDoc.Tables.Add(wordApp.Selection.Range, 5, 5, ref Nothing, ref Nothing);
            //table.Borders.Enable = 1;
            object oFindText = "[%替换%]";
            wordApp.Selection.SetRange(0,0);
            wordApp.Selection.Find.Execute(ref oFindText,ref Nothing,ref Nothing, ref Nothing, ref Nothing, ref Nothing,true, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            if (wordApp.Selection.Find.Found)
            {
                wordApp.Selection.Range.Text = "替换成功";
            }
            object format = Word.WdSaveFormat.wdFormatDocumentDefault;
            //wordDoc.SaveAsQuickStyleSet((string)path);
            wordDoc.SaveAs2(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, false, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            Console.WriteLine(path + "创建完毕！");
        }


    }
}
