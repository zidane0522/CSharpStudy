using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WordObjLearning
{
    public class ExcelHelper:IDisposable
    {
        #region 构造函数

        /// <summary>
        /// 构造函数，将一个已有Excel工作簿作为模板，并指定输出路径
        /// </summary>
        /// <param name="templetFilePath">Excel模板文件路径</param>
        /// <param name="outputFilePath">输出Excel文件路径</param>
        public ExcelHelper(string templetFilePath, string outputFilePath)
        {
            if (templetFilePath == null)
                throw new Exception("Excel模板文件路径不能为空！");

            if (outputFilePath == null)
                throw new Exception("输出Excel文件路径不能为空！");

            if (!File.Exists(templetFilePath))
                throw new Exception("指定路径的Excel模板文件不存在！");

            this.templetFile = templetFilePath;
            this.outputFile = outputFilePath;

            excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelApp.Visible = false;

            excelApp.DisplayAlerts = false;  //是否需要显示提示
            excelApp.AlertBeforeOverwriting = false;  //是否弹出提示覆盖

            //打开模板文件，得到WorkBook对象
            workBook = excelApp.Workbooks.Open(templetFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, Type.Missing, Type.Missing);

            //得到WorkSheet对象
            workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Sheets.get_Item(1);
        }

        /// <summary>
        /// 构造函数，新建一个工作簿
        /// </summary>
        public ExcelHelper()
        {
            excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelApp.Visible = false;

            //设置禁止弹出保存和覆盖的询问提示框
            excelApp.DisplayAlerts = false;
            excelApp.AlertBeforeOverwriting = false;

            //新建一个WorkBook
            workBook = excelApp.Workbooks.Add(Type.Missing);

            //得到WorkSheet对象
            workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Sheets.get_Item(1);
        }

        #endregion

        #region 私有变量

        private string templetFile = null;
        private string outputFile = null;
        private object missing = System.Reflection.Missing.Value;
        private Microsoft.Office.Interop.Excel.Application excelApp;
        private Microsoft.Office.Interop.Excel.Workbook workBook;
        private Microsoft.Office.Interop.Excel.Worksheet workSheet;
        private Microsoft.Office.Interop.Excel.Range range;
        private Microsoft.Office.Interop.Excel.Range range1;
        private Microsoft.Office.Interop.Excel.Range range2;

        #endregion

        #region 公共属性

        /// <summary>
        /// WorkSheet数量
        /// </summary>
        public int WorkSheetCount
        {
            get { return workBook.Sheets.Count; }
        }

        /// <summary>
        /// Excel模板文件路径
        /// </summary>
        public string TempletFilePath
        {
            set { this.templetFile = value; }
        }

        /// <summary>
        /// 输出Excel文件路径
        /// </summary>
        public string OutputFilePath
        {
            set { this.outputFile = value; }
        }

        #endregion


        #region 批量写入Excel内容

        /// <summary>
        /// 将二维数组数据写入Excel文件
        /// </summary>
        /// <param name="arr">二维数组</param>
        /// <param name="top">行索引</param>
        /// <param name="left">列索引</param>
        public void ArrayToExcel(object[,] arr, int top, int left)
        {
            int rowCount = arr.GetLength(0); //二维数组行数（一维长度）
            int colCount = arr.GetLength(1); //二维数据列数（二维长度）

            range = (Microsoft.Office.Interop.Excel.Range)workSheet.Cells[top, left];
            range = range.get_Resize(rowCount, colCount);
            range.FormulaArray = arr;
        }

        #endregion

        #region 行操作

        /// <summary>
        /// 插行（在指定行上面插入指定数量行）
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="count"></param>
        public void InsertRows(int rowIndex, int count)
        {
            try
            {
                range = (Microsoft.Office.Interop.Excel.Range)workSheet.Rows[rowIndex, this.missing];
                for (int i = 0; i < count; i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlDirection.xlDown, missing);
                }
            }
            catch (Exception e)
            {
                this.KillExcelProcess(false);
                throw e;
            }
        }

        /// <summary>
        /// 复制行（在指定行下面复制指定数量行）
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="count"></param>
        public void CopyRows(int rowIndex, int count)
        {
            try
            {
                range1 = (Microsoft.Office.Interop.Excel.Range)workSheet.Rows[rowIndex, this.missing];
                for (int i = 1; i <= count; i++)
                {
                    range2 = (Microsoft.Office.Interop.Excel.Range)workSheet.Rows[rowIndex + i, this.missing];
                    range1.Copy(range2);
                }
            }
            catch (Exception e)
            {
                this.KillExcelProcess(false);
                throw e;
            }
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="count"></param>
        public void DeleteRows(int rowIndex, int count)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    range = (Microsoft.Office.Interop.Excel.Range)workSheet.Rows[rowIndex, this.missing];
                    range.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                }
            }
            catch (Exception e)
            {
                this.KillExcelProcess(false);
                throw e;
            }
        }

        #endregion

        #region 列操作

        /// <summary>
        /// 插列（在指定列右边插入指定数量列）
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="count"></param>
        public void InsertColumns(int columnIndex, int count)
        {
            try
            {
                range = (Microsoft.Office.Interop.Excel.Range)(workSheet.Columns[columnIndex, this.missing]);  //注意：这里和VS的智能提示不一样，第一个参数是columnindex

                for (int i = 0; i < count; i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlDirection.xlDown, missing);
                }
            }
            catch (Exception e)
            {
                this.KillExcelProcess(false);
                throw e;
            }
        }

        /// <summary>
        /// 删除列
        /// </summary> 
        /// <param name="columnIndex"></param>
        /// <param name="count"></param>
        public void DeleteColumns(int columnIndex, int count)
        {
            try
            {
                for (int i = columnIndex + count - 1; i >= columnIndex; i--)
                {
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[1, i]).EntireColumn.Delete(0);
                }
            }
            catch (Exception e)
            {
                this.KillExcelProcess(false);
                throw e;
            }
        }

        #endregion

        #region 单元格操作

        /// <summary>
        /// 合并单元格，并赋值，对指定WorkSheet操作
        /// </summary>
        /// <param name="sheetIndex">WorkSheet索引</param>
        /// <param name="beginRowIndex">开始行索引</param>
        /// <param name="beginColumnIndex">开始列索引</param>
        /// <param name="endRowIndex">结束行索引</param>
        /// <param name="endColumnIndex">结束列索引</param>
        /// <param name="text">合并后Range的值</param>
        public void MergeCells(int beginRowIndex, int beginColumnIndex, int endRowIndex, int endColumnIndex, string text)
        {
            range = workSheet.get_Range(workSheet.Cells[beginRowIndex, beginColumnIndex], workSheet.Cells[endRowIndex, endColumnIndex]);

            range.ClearContents(); //先把Range内容清除，合并才不会出错
            range.MergeCells = true;

            range.Value2 = text;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
        }

        /// <summary>
        /// 向单元格写入数据，对当前WorkSheet操作
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <param name="text">要写入的文本值</param>
        public void SetCells(int rowIndex, int columnIndex, string text)
        {
            try
            {
                workSheet.Cells[rowIndex, columnIndex] = text;
            }
            catch
            {
                this.KillExcelProcess(false);
                throw new Exception("向单元格[" + rowIndex + "," + columnIndex + "]写数据出错！");
            }
        }

        /// <summary>
        /// 向单元格写入数据，对当前WorkSheet操作
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <param name="text">要写入的文本值</param>
        public void SetCells(int rowIndex, int columnIndex, string text, string comment)
        {
            try
            {
                workSheet.Cells[rowIndex, columnIndex] = text;
                SetCellComment(rowIndex, columnIndex, comment);
            }
            catch
            {
                this.KillExcelProcess(false);
                throw new Exception("向单元格[" + rowIndex + "," + columnIndex + "]写数据出错！");
            }
        }

        /// <summary>
        /// 向单元格写入数据，对当前WorkSheet操作
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <param name="text">要写入的文本值</param>
        public void SetCellComment(int rowIndex, int columnIndex, string comment)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Range range = workSheet.Cells[rowIndex, columnIndex] as Microsoft.Office.Interop.Excel.Range;
                range.AddComment(comment);
            }
            catch
            {
                this.KillExcelProcess(false);
                throw new Exception("向单元格[" + rowIndex + "," + columnIndex + "]写数据出错！");
            }
        }


        #endregion

        #region 保存文件

        /// <summary>
        /// 另存文件
        /// </summary>
        public void SaveAsFile()
        {
            if (this.outputFile == null)
                throw new Exception("没有指定输出文件路径！");

            try
            {
                workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.Quit();
            }
        }

        /// <summary>
        /// 将Excel文件另存为指定格式
        /// </summary>
        /// <param name="format">HTML，CSV，TEXT，EXCEL，XML</param>
        public void SaveAsFile(SaveAsFileFormat format)
        {
            if (this.outputFile == null)
                throw new Exception("没有指定输出文件路径！");

            try
            {
                switch (format)
                {
                    case SaveAsFileFormat.HTML:
                        {
                            workBook.SaveAs(outputFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                            break;
                        }
                    case SaveAsFileFormat.CSV:
                        {
                            workBook.SaveAs(outputFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                            break;
                        }
                    case SaveAsFileFormat.TEXT:
                        {
                            workBook.SaveAs(outputFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlUnicodeText, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                            break;
                        }
                    case SaveAsFileFormat.XML:
                        {
                            workBook.SaveAs(outputFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet, Type.Missing, Type.Missing,
                             Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                             Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            break;
                        }
                    default:
                        {
                            workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.Quit();
            }
        }

        #endregion

        #region 杀进程释放资源

        /// <summary>
        /// 结束Excel进程
        /// </summary>
        public void KillExcelProcess(bool bAll)
        {
            if (bAll)
            {
                KillAllExcelProcess();
            }
            else
            {
                KillSpecialExcel();
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);


        /// <summary>
        /// 杀特殊进程的Excel
        /// </summary>
        public void KillSpecialExcel()
        {
            try
            {
                if (excelApp != null)
                {
                    int lpdwProcessId;
                    GetWindowThreadProcessId((IntPtr)excelApp.Hwnd, out lpdwProcessId);

                    if (lpdwProcessId > 0)    //c-s方式
                    {
                        System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                    }
                    else
                    {
                        Quit();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Quit()
        {
            if (workBook != null)
                workBook.Close(null, null, null);
            if (excelApp != null)
            {
                excelApp.Workbooks.Close();
                excelApp.Quit();
            }
            if (range != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                range = null;
            }
            if (range1 != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range1);
                range1 = null;
            }
            if (range2 != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range2);
                range2 = null;
            }
            if (workSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                workSheet = null;
            }
            if (workBook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                workBook = null;
            }
            if (excelApp != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                excelApp = null;
            }
            GC.Collect();
        }

        /// <summary>
        /// 接口方法 释放资源
        /// </summary>
        public void Dispose()
        {
            Quit();
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 杀Excel进程
        /// </summary>
        public static void KillAllExcelProcess()
        {
            Process[] myProcesses;
            myProcesses = Process.GetProcessesByName("Excel");

            //得不到Excel进程ID，暂时只能判断进程启动时间
            foreach (Process myProcess in myProcesses)
            {
                myProcess.Kill();
            }
        }

        /// <summary>
        /// 打开相应的excel
        /// </summary>
        /// <param name="filepath"></param>
        public static void OpenExcel(string filepath)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
            xlsApp.Workbooks.Open(filepath);
            xlsApp.Visible = true;
        }

        #endregion

        /// <summary>
        /// HTML，CSV，TEXT，EXCEL，XML
        /// </summary>
        public enum SaveAsFileFormat
        {
            HTML,
            CSV,
            TEXT,
            EXCEL,
            XML
        }
    }
}
