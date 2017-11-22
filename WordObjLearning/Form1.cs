using Microsoft.Office.Interop.Excel;
using NPOI.HSSF.UserModel;
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
using Spire.Doc;
using Spire.Doc.Fields;

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

        private void button4_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            //Workbooks wbks = app.Workbooks;
            //_Workbook _wbk = wbks.Add(true);
            //Sheets shs = _wbk.Sheets;
            //_Worksheet _wsh = (_Worksheet)shs.get_Item(1);
            //_wsh.Name = "顺丰等待执行快递单";

            ////添加行
            //((Range)_wsh.Rows[2, Missing.Value]).Insert(Missing.Value, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);

            ////添加列
            ////_wsh.get_Range(_wsh.Cells[1, 1], _wsh.Cells[_wsh.Rows.Count, 1]).Insert(Missing.Value, XlInsertShiftDirection.xlShiftToRight);
            //app.AlertBeforeOverwriting = false;
            //if (File.Exists(@"D:\neweck.xls"))
            //{
            //    File.Delete(@"D:\neweck.xls");
            //}
            //_wsh.SaveAs(@"D:\neweck.xls", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //ExcelHelper ExcelHelper = new WordObjLearning.ExcelHelper();
            //string[,] dataarray =  { {"a","a","a" }, { "b", "b", "b" }, { "c", "c", "c" } };
            //ExcelHelper.ArrayToExcel(dataarray,1,1);
            //ExcelHelper.OutputFilePath = @"D:\ad.xls";
            //ExcelHelper.SaveAsFile();


        }

        private void NPOI_OutputExcel(string writefilePath)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("test_01");

            // 第一列
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("第一列第一行");

            // 第二列
            NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(1);
            row2.CreateCell(0).SetCellValue("第二列第一行");

            // ...

            using (System.IO.MemoryStream ms=new MemoryStream())
            {
                book.Write(ms);
                using (FileStream fs = new FileStream(writefilePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
                book = null;
            }

        }

        private void NPOI_InputExcel(string readfilePath)
        {
            HSSFWorkbook hssfworkbook;
            #region//初始化信息  
            try
            {
                using (FileStream file = new FileStream(readfilePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void TestSpireWord_Click(object sender, EventArgs e)
        {
            //tmName
            //ictm
            //regNum
            //year
            //month
            //day

            string tmName = "tmName";
            string ictm = "56";
            string regNum = "regNum";
            string year = "2000";
            string month = "12";
            string day = "11";

            try
            {
                using (Document doc = new Document(@"C:\Users\Administrator\Desktop\驳回复审模板\商标评审案件申请材料目录模板.docx"))
                {
                    //清除表单阴影
                    doc.Properties.FormFieldShading = false;
                    foreach (FormField field in doc.Sections[0].Body.FormFields)
                    {
                        switch (field.Name)
                        {
                            case "tmName":
                                field.Text = tmName;
                                break;
                            case "ictm":
                                field.Text = ictm;
                                break;
                            case "regNum":
                                field.Text = regNum;
                                break;
                            case "nian":
                                field.Text = year;
                                break;
                            case "yue":
                                field.Text = month;
                                break;
                            case "ri":
                                field.Text = day;
                                break;
                            default:
                                break;
                        }
                    }

                    doc.SaveToFile(@"C:\Users\Administrator\Desktop\驳回复审文件\商标评审案件申请材料目录文件.doc");
                }
                
                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
