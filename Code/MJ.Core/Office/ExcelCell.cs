using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Office
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExcelCell
    {
        #region 获取单元格值
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static object GetCellValue(this ICell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue;
                    }
                    return cell.NumericCellValue;
                case CellType.String:
                    return (cell.StringCellValue ?? string.Empty).Trim().Trim('`');
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:      // 公式
                    var evaluator = cell.Row.Sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
                    return GetCellValue(evaluator.EvaluateInCell(cell));
                default:
                    return cell.StringCellValue ?? "";
            }
        }

        #endregion

        #region 设置单元格值

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        /// <param name="dataType"></param>
        public static void SetCellValue(this ICell cell, object value, Type dataType)
        {

            switch (dataType.ToString())
            {
                case "System.String"://字符串类型
                    cell.SetCellValue(value?.ToString());
                    break;
                case "System.DateTime"://日期类型
                    DateTime dateV;
                    if (value == null)
                    {
                        cell.SetCellValue("");
                    }
                    else
                    {
                        bool isTime = DateTime.TryParse(value.ToString(), out dateV);
                        if (isTime)
                        {
                            cell.SetCellValue(dateV);
                        }
                        else
                        {
                            throw new Exception(string.Format("单元格:{0}的值不是正确的时间格式", "行：" + cell.RowIndex.ToString() + "列：" + cell.ColumnIndex.ToString()));
                        }
                    }

                    break;
                case "System.Boolean"://布尔型
                    if (value == null)
                    {
                        cell.SetCellValue("");
                    }
                    else
                    {
                        cell.SetCellValue(value?.ToString());
                    }

                    break;
                case "System.Int16"://整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    if (value == null)
                    {
                        cell.SetCellValue("");
                    }
                    else
                    {
                        cell.SetCellValue(value.ToString());
                    }

                    break;
                case "System.Decimal"://浮点型
                case "System.Double":
                    double doubV = 0;
                    if (value == null)
                    {
                        value = 0;
                        cell.SetCellValue("");
                    }
                    else
                    {
                        double.TryParse(value.ToString(), out doubV);
                        cell.SetCellValue(doubV);
                    }
                    break;
                case "System.DBNull"://空值处理
                    cell.SetCellValue("");
                    break;
                default:
                    cell.SetCellValue("");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        /// <param name="dataType"></param>
        /// <param name="cellStyle"></param>
        public static void SetCellValue(this ICell cell, object value, Type dataType, ICellStyle cellStyle)
        {
            cell.CellStyle = cellStyle;
            cell.SetCellValue(value, dataType);

        }


        #endregion

        #region 获取单元格对应C#数据类型

        /// <summary>
        /// 将单元格类型转换为C# 对应的数据类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static Type GetCellBaseType(this ICell cell)
        {


            if (cell == null)
            {
                return typeof(string);
            }
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return typeof(string);
                case CellType.Boolean:
                    return typeof(bool);
                case CellType.Numeric:

                    return typeof(decimal?);
                case CellType.String:
                    return typeof(string);
                case CellType.Error:
                    return null;
                case CellType.Formula:      // 公式
                    var evaluator = cell.Row.Sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
                    return GetCellBaseType(evaluator.EvaluateInCell(cell));
                default:
                    return typeof(string);
            }
        }

        #endregion

        #region 根据类型推断默认自定义格式

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static ExcelCellDataFormat GetCellDefaultDataFormat(this ICell cell)
        {
            return ExcelCellDataFormat.Text;
        }


        #endregion
    }
}
