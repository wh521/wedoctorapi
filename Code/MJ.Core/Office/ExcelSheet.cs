using MJ.Core.Utilities;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MJ.Core.Extensions;

namespace MJ.Core.Office
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExcelSheet
    {
        #region 填充数据
        /// <summary>
        /// 将数据填充到指定的范围内(必须是连续的区域)
        /// 填充数据暂时不做校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="collection"></param>
        /// <param name="columns"></param>
        /// <param name="startRow"></param>
        public static ISheet FillData<T>(this ISheet sheet, List<T> collection, List<ExcelColumn> columns, int startRow = 1)
        {
            PropertyAccess<T> protyAccess = new PropertyAccess<T>();
            for (int rowIndex = startRow; rowIndex < collection.Count() + startRow; rowIndex++)
            {
                T entity = collection[rowIndex - startRow];
                IRow row = sheet.CreateRow(rowIndex);
                foreach (ExcelColumn xlsColumn in columns)
                {
                    ICell cell = row.CreateCell(xlsColumn.Index);
                    xlsColumn.OnHeadCellCreate?.Invoke(cell, sheet);
                    Type t = PropertyUtil.GetProperty<T>(xlsColumn.ColumnName).PropertyType;
                    cell.SetCellValue(protyAccess.GetValue(entity, xlsColumn.ColumnName), t, xlsColumn.DataCellStyle);
                    
                }
            }
            return sheet;
        }

        #endregion

        #region 填充数据
        /// <summary>
        /// 将数据填充到指定的范围内(必须是连续的区域)
        /// 填充数据暂时不做校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="collection"></param>
        /// <param name="columns"></param>
        /// <param name="startRow"></param>
        public static ISheet FillDataWithHeader<T>(this ISheet sheet, List<T> collection, List<ExcelColumn<T>> columns, int startRow = 0,int startCol=0)
        {
            PropertyAccess<T> protyAccess = new PropertyAccess<T>();
            // 表头填充
            IRow headerRow = sheet.CreateRow(startRow);
            int iHeaderCol = startCol;
            foreach (ExcelColumn col in columns)
            {
                ICell cell = headerRow.CreateCell(iHeaderCol);
                col.OnHeadCellCreate?.Invoke(cell, sheet);
                cell.SetCellValue(col.Description, typeof(string), col.HeadCellStyle);
                iHeaderCol++;
            }


            int iRow = startRow+1;
            // 表数据填充int rowIndex = startRow+1; rowIndex < collection.Count() + startRow+1; rowIndex++
            foreach (T entity in collection)
            {
                //T entity = collection[rowIndex - startRow-1];
                IRow row = sheet.CreateRow(iRow);
                int iCol = startCol;

                foreach (ExcelColumn xlsColumn in columns)
                {
                    ICell cell = row.CreateCell(iCol);
                    xlsColumn.OnDataCellCreate?.Invoke(cell, sheet);
                    Type t = PropertyUtil.GetProperty<T>(xlsColumn.ColumnName).PropertyType.GetRealType();

                    object cellValue = protyAccess.GetValue(entity, xlsColumn.ColumnName);
                    if (xlsColumn is ExcelColumn<T>)
                    {
                        var genericColumn = xlsColumn as ExcelColumn<T>;
                        if (genericColumn.Formattor != null)
                        {
                            cellValue = genericColumn.Formattor(cellValue, entity);
                        }
                    }
                    else
                    {
                        if (xlsColumn.Formattor != null)
                        {
                            cellValue = xlsColumn.Formattor(cellValue, entity);
                        }
                    }
                    
                    cell.SetCellValue(cellValue, t, xlsColumn.DataCellStyle);
                    iCol++;
                }
                iRow++;
            }
            return sheet;
        }

        #endregion

        #region 获取数据的列信息--辅助方法

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>

        public static List<ExcelColumn<T>> FetchDefaultColumnList<T>()
        {
            List<ExcelColumn<T>> columns = new List<ExcelColumn<T>>();
            Type typ = typeof(T);
            int colIndex = 0;
            foreach (PropertyInfo pi in typ.GetProperties())
            {
                ExcelColumn<T> column = new ExcelColumn<T>();
                column.Index = colIndex++;
                string colDescription = AttributeUtil.GetPropertyDescriptionAttributeValue<T>(pi.Name);
                column.Description =string.IsNullOrEmpty(colDescription)?pi.Name: colDescription;   // 字段中文描述,未设置则取字段名
                column.ColumnName = pi.Name; //字段名
                column.DataType = pi.PropertyType.GetRealType();
                if (column.DataType == typeof(DateTime))
                {
                    column.ColumnDataFormat = ExcelCellDataFormat.StandardDate;
                }
                columns.Add(column);
            }
            return columns;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<ExcelColumn> FetchDefaultColumnList(DataTable dt)
        {
            List<ExcelColumn> columns = new List<ExcelColumn>();
            for (int iCol = 0; iCol < dt.Columns.Count; iCol++)
            {
                ExcelColumn clmn = new ExcelColumn();
                DataColumn dc = dt.Columns[iCol];
                clmn.Index = iCol;
                clmn.ColumnName = dc.ColumnName;
                clmn.Description = string.IsNullOrEmpty(dc.Caption)?dc.ColumnName:dc.Caption;

                if(dc.DataType == typeof(DateTime))
                {
                    clmn.ColumnDataFormat = ExcelCellDataFormat.StandardDateTime;
                }

                columns.Add(clmn);
            }

            return columns;

        }




        #endregion

        #region 设置自动列宽

        /// <summary>
        /// sheet1.SetColumnWidth(1, 100 * 256);SetColumnWidth的第二个参数要乘以256，因为这个参数的单位是1/256个字符宽度
        /// </summary>
        /// <param name="sheet"></param>
        public static void AutoColumnWidth(this ISheet sheet)
        {
            int iMaxColumn = sheet.GetRow(0).LastCellNum;

            for(int iCol=0; iCol< iMaxColumn; iCol++)
            {
                sheet.SetColumnWidth(iCol, sheet.GetColumnMaxWidth(iCol));
            }
        }

        #endregion

        #region 获取某列内容最大长度
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="iColumnIndex"></param>
        public static int GetColumnMaxWidth(this ISheet sheet,int iColumnIndex=0)
        {
            int iMaxRow = sheet.LastRowNum;
            int iMaxLength = 0;
            int MIN_LENGTH = 8*256;

            for (int rowIndex = 0; rowIndex < iMaxRow; rowIndex++)
            {
                ICell cell =sheet.GetRow(rowIndex).GetCell(iColumnIndex);
                int currentLength = Encoding.Default.GetByteCount(cell.GetCellValue().ToString()) * 256;

                if (iMaxLength < currentLength)
                    iMaxLength = currentLength;
            }

            return iMaxLength> MIN_LENGTH? iMaxLength:MIN_LENGTH;
        }

        #endregion

    }
}
