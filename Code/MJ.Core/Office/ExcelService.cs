using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Office
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExcelService
    {
        #region 获取Workbook


        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelType"></param>
        /// <returns></returns>
        public static IWorkbook GetWorkbook(ExcelFileType excelType)
        {
            if (ExcelFileType.xls.Equals(excelType))
                return new HSSFWorkbook();
            else
                return new XSSFWorkbook();
        }


        #endregion

        #region 导出Excel


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="fileType"></param>
        /// <param name="sheetName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static MemoryStream ExportToExcel<T>(List<T> list, ExcelFileType fileType, string sheetName = "Sheet1", List<ExcelColumn<T>> columns = null)
        {

            MemoryStream xlsFile = new MemoryStream();
            IWorkbook workbook = GetWorkbook(fileType);
            ISheet sheet = workbook.CreateSheet(sheetName);
            columns = columns ?? ExcelSheet.FetchDefaultColumnList<T>();
            columns.ForEach((item) => {
                item.HeadCellStyle = GetDefaultHeaderStyle(workbook);
                item.DataCellStyle = GetDefaultDataStyle(workbook, item.DataType);
            });
            sheet.FillDataWithHeader(list, columns);
            sheet.AutoColumnWidth();        // 根据内容自动计算列宽
            workbook.Write(xlsFile);

            byte[] mst = xlsFile.ToArray();
            MemoryStream moExcelFile3 = new MemoryStream(mst);
            moExcelFile3.Seek(0, SeekOrigin.Begin);
            return moExcelFile3;
            //xlsFile.Seek(0, SeekOrigin.Begin);
            //return xlsFile;
        }

        #endregion

        #region 导出到文件

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exportFileName"></param>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <param name="fileType"></param>
        /// <param name="sheetName"></param>
        public static void SaveAs<T>(string exportFileName, List<T> list,List<ExcelColumn<T>> columns=null, ExcelFileType fileType = ExcelFileType.xls, string sheetName = "Sheet1")
        {


            IWorkbook workbook = GetWorkbook(fileType);
            ISheet sheet = workbook.CreateSheet(sheetName);
            columns = columns ?? ExcelSheet.FetchDefaultColumnList<T>();
            columns.ForEach((item) => {
                item.HeadCellStyle = GetDefaultHeaderStyle(workbook);
                item.DataCellStyle = GetDefaultDataStyle(workbook, item.DataType);
            });
            sheet.FillDataWithHeader(list, columns);
            sheet.AutoColumnWidth();        // 根据内容自动计算列宽

            using (FileStream fs = new FileStream(exportFileName, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }

        }

        #endregion

        #region 泛型获取默认数据列

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<ExcelColumn<T>> FetchDefaultColumnList<T>()
        {
            return ExcelSheet.FetchDefaultColumnList<T>();
        }

        

        #endregion

        #region 获取默认表头样式

        /// <summary>
        /// 默认表头样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static ICellStyle GetDefaultHeaderStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;//背景色
            style.FillPattern = FillPattern.Squares;
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;//前景色

            IFont font = workbook.CreateFont();
            font.FontHeight = 10 * 20;
            font.Boldweight =(short)FontBoldWeight.Bold;        //设置粗体

            style.SetFont(font);
            return style;
        }


        #endregion

        #region 获取默认数据样式
        /// <summary>
        /// 默认内容样式
        /// </summary>
        /// <param name="workBook"></param>
        /// <returns></returns>
        public static ICellStyle GetDefaultDataStyle(IWorkbook workBook)
        {
            ICellStyle style = workBook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;//背景色
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;//前景色
            style.FillPattern = FillPattern.Squares;
            style.WrapText = true;  // 自动换行
            SetCellDataFormat(workBook, style, ExcelCellDataFormat.Text);

            IFont font = workBook.CreateFont();
            font.FontHeight = 10 * 20;
            style.SetFont(font);
            return style;
        }

        public static ICellStyle GetDefaultDataStyle(IWorkbook workBook,Type type)
        {
            ICellStyle style = workBook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;//背景色
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;//前景色
            style.FillPattern = FillPattern.Squares;
            style.WrapText = true;  // 自动换行
            SetCellDataFormat(workBook, style, GetExcelCellDataFormat(type));

            IFont font = workBook.CreateFont();
            font.FontHeight = 10 * 20;
            style.SetFont(font);
            return style;
        }


        #endregion

        #region 获取数据格式
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="cellStyle"></param>
        /// <param name="excelDataFormat"></param>
        public static void SetCellDataFormat(IWorkbook workbook ,ICellStyle cellStyle ,ExcelCellDataFormat excelDataFormat)
        {
            //cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("text");
            IDataFormat format = workbook.CreateDataFormat();

            switch (excelDataFormat)
            {
                case ExcelCellDataFormat.StandardDate:
                    cellStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                    break;
                //case ExcelCellDataFormat.Numeric:
                //    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                //    break;
                //case ExcelCellDataFormat.Currency:
                //    cellStyle.DataFormat = format.GetFormat("￥#,##0");
                //    break;
                //case ExcelCellDataFormat.Percent:
                //    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                //    break;
                //case ExcelCellDataFormat.ChineseCurrency:
                //    cellStyle.DataFormat = format.GetFormat("[DbNum2][$-804]0");
                //    break;
                //case ExcelCellDataFormat.Scientific:
                //    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
                //    break;
                default:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("text");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ExcelCellDataFormat GetExcelCellDataFormat(Type type)
        {
            ExcelCellDataFormat result = ExcelCellDataFormat.Text;
            switch (type.ToString())
            {
                case "System.String":
                    result = ExcelCellDataFormat.Text;
                    break;
                case "System.DateTime":
                    result = ExcelCellDataFormat.StandardDate;
                    break;
                case "System.Boolean":
                    result = ExcelCellDataFormat.Text;
                    break;
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    result = ExcelCellDataFormat.Convention;
                    break;
                case "System.Decimal"://浮点型
                    result = ExcelCellDataFormat.Currency;
                    break;
                case "System.Double":
                    result = ExcelCellDataFormat.Numeric;
                    break;
                default:
                    break;
            }

            return result;
        }

        #endregion
    }
}
