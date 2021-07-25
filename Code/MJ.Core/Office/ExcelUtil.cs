using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Office
{
    /// <summary>
    /// Excel导出帮助类
    /// </summary>
    public class ExcelUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MemoryStream CreateExcel()
        {
            return this.CreateExcel("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public MemoryStream CreateExcel(string sheetName="Sheet1")
        {
            MemoryStream xlsFile = new MemoryStream();
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);
                SetFileProperty(workbook);
                //HSSFRow row = (HSSFRow)sheet.CreateRow(0);
                //row.CreateCell(0, CellType.STRING).SetCellValue("CELLS1");                            
                workbook.Write(xlsFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xlsFile;
        }

        /// <summary>
        /// 根据DataTable创建Excel文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public MemoryStream CreateExcel(DataTable dt, string sheetName= "Sheet1")
        {
            MemoryStream moExcelFile = new MemoryStream();
            HSSFWorkbook workbook = new HSSFWorkbook();
            try
            {
                int nRow = 1;
                int nCol = dt.Columns.Count;
                
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);
                SetFileProperty(workbook);

                HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

                foreach (DataRow dr in dt.Rows)
                {
                    HSSFRow row = (HSSFRow)sheet.CreateRow(nRow++);
                    foreach (DataColumn col in dt.Columns)
                    {
                        HSSFCell newCell = (HSSFCell)row.CreateCell(col.Ordinal);
                        string drValue = dr[col.ColumnName].ToString();

                        switch (col.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                    }
                }
                workbook.Write(moExcelFile);
                moExcelFile.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return moExcelFile;
        }

        /// <summary>
        /// 创建带有列标题的Excel文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strSheetName"></param>
        /// <param name="dtTitle"></param>
        /// <returns></returns>
        public MemoryStream CreateExcel(DataTable dt, DataTable dtTitle, string strSheetName="Sheet1")
        {
            MemoryStream xlsFile = new MemoryStream();
            try
            {
                int nRow = 1;
                int nCol = dt.Columns.Count;
                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(strSheetName);
                SetFileProperty(workbook);

                HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                #region 根据单元格内容计算列宽
                //取得列宽
                int[] arrColWidth = new int[dt.Columns.Count];
                foreach (DataColumn item in dt.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dt.Rows[i][j].ToString()).Length;//936  gb2312 简体中文 (GB2312) //65001 utf-8 Unicode (UTF-8)

                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
                #endregion
                #region 创建列标题
                HSSFRow rowHeader = (HSSFRow)sheet.CreateRow(0);
                foreach (DataColumn col in dt.Columns)
                {
                    DataRow[] drs = dtTitle.Select("field ='" + col.ColumnName + "'");
                    if (drs.Length <= 0)//列标题数据集中不存在的列，不创建
                    {
                        continue;
                    }
                    HSSFCell newCell = (HSSFCell)rowHeader.CreateCell(col.Ordinal);
                    newCell.SetCellValue(drs[0]["title"].ToString());
                    //设置列宽
                    //sheet.SetColumnWidth(col.Ordinal, int.Parse(drs[0]["width"].ToString())*25);//1px = 0.04cm 按照界面显示的列宽设置
                    sheet.SetColumnWidth(col.Ordinal, (arrColWidth[col.Ordinal] + 1) * 256);//按照单元格内容计算所得列宽设置

                }
                #endregion

                #region 创建行和列并填写内容
                foreach (DataRow dr in dt.Rows)
                {
                    HSSFRow row = (HSSFRow)sheet.CreateRow(nRow++);
                    foreach (DataColumn col in dt.Columns)
                    {
                        DataRow[] drs = dtTitle.Select("field ='" + col.ColumnName + "'");
                        if (drs.Length <= 0)//列标题数据集中不存在的列，不创建
                        {
                            continue;
                        }
                        HSSFCell newCell = (HSSFCell)row.CreateCell(col.Ordinal);
                        string drValue = dr[col.ColumnName].ToString();

                        switch (col.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                    }
                }
                #endregion

                workbook.Write(xlsFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xlsFile;
        }

        /// <summary>
        /// 创建字段名作为列标题的Excel文件
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="sheetName">文件名称</param>
        /// <returns></returns>
        public MemoryStream CreateExcelHasTitle(DataTable dt, string sheetName="Sheet1")
        {
            MemoryStream xlsFile = new MemoryStream();
            try
            {
                int nRow = 1;
                int nCol = dt.Columns.Count;
                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);
                SetFileProperty(workbook);

                HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                #region 根据单元格内容计算列宽
                //取得列宽
                int[] arrColWidth = new int[dt.Columns.Count];
                foreach (DataColumn item in dt.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dt.Rows[i][j].ToString()).Length;//936  gb2312 简体中文 (GB2312) //65001 utf-8 Unicode (UTF-8)

                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
                #endregion
                #region 创建列标题
                HSSFRow rowHeader = (HSSFRow)sheet.CreateRow(0);
                foreach (DataColumn col in dt.Columns)
                {
                    HSSFCell newCell = (HSSFCell)rowHeader.CreateCell(col.Ordinal);
                    newCell.SetCellValue(col.ColumnName);
                    //设置列宽
                    //sheet.SetColumnWidth(col.Ordinal, int.Parse(drs[0]["width"].ToString())*25);//1px = 0.04cm 按照界面显示的列宽设置
                    sheet.SetColumnWidth(col.Ordinal, (arrColWidth[col.Ordinal] + 1) * 256);//按照单元格内容计算所得列宽设置
                }
                #endregion

                #region 创建行和列并填写内容
                foreach (DataRow dr in dt.Rows)
                {
                    HSSFRow row = (HSSFRow)sheet.CreateRow(nRow++);
                    foreach (DataColumn col in dt.Columns)
                    {
                        HSSFCell newCell = (HSSFCell)row.CreateCell(col.Ordinal);
                        string drValue = dr[col.ColumnName].ToString();

                        switch (col.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                    }
                }
                #endregion

                workbook.Write(xlsFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xlsFile;
        }

        /// <summary>
        /// 设置Excel文件的属性信息
        /// </summary>
        /// <param name="workbook">Excel文件</param>
        private void SetFileProperty(HSSFWorkbook workbook)
        {
            // CommonClass.ClientInfo client = new ClientInfo();
            #region 文件属性信息

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "上海骜升信息科技有限公司";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "admin"; //填加xls文件作者信息
            si.ApplicationName = "Auto"; //填加xls文件创建程序信息
            si.LastAuthor = "admin"; //填加xls文件最后保存者信息
            si.Comments = "admin"; //填加xls文件作者信息
            si.Title = ""; //填加xls文件标题信息
            si.Subject = "";//填加文件主题信息
            si.CreateDateTime = DateTime.Now;
            workbook.SummaryInformation = si;

            #endregion
        }
    }
}
