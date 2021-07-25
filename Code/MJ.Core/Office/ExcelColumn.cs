using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Office
{
    /// <summary>
    /// 基础类定义
    /// </summary>
    public class ExcelColumn
    {
        /// <summary>
        /// 列索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 列对应字段(英文)
        /// </summary>

        public string ColumnName { get; set; }

        /// <summary>
        /// 列名(中文)
        /// </summary>
        public string Description { get; set; }



        /// <summary>
        /// 列宽度
        /// </summary>

        public int Width { get; set; }

        /// <summary>
        /// 合并列数
        /// </summary>

        public int Colspan { get; set; }

        /// <summary>
        /// 合并行数
        /// </summary>

        public int Rowspan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type DataType{ get; set; }

        /// <summary>
        /// 列头样式
        /// </summary>
        public ICellStyle HeadCellStyle { get; set; }


        /// <summary>
        /// 数据行样式
        /// </summary>
        public ICellStyle DataCellStyle { get; set; }


        /// <summary>
        /// 单元格数据设定之前
        /// </summary>
        public Action<ICell, ISheet> OnDataCellCreate { get; set; }


        /// <summary>
        /// 列头单元格数据设定之前
        /// </summary>
        public Action<ICell, ISheet> OnHeadCellCreate { get; set; }

        /// <summary>
        /// 数据格式化器(导出)
        /// </summary>
        public Func<object, object, object> Formattor { get; set; }

        /// <summary>
        /// 数据校验器(导入)
        /// </summary>
        public Func<object, object, bool> Validator { get; set; }

        /// <summary>
        /// 列单元格公式
        /// </summary>
        public string CellFormula { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ExcelCellDataFormat ColumnDataFormat { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelColumn<T> : ExcelColumn
    {
        /// <summary>
        /// 列格式化器
        /// </summary>
        public new Func<object, T, object> Formattor { get; set; }

        /// <summary>
        /// 列字段选择器
        /// </summary>
        public Expression<Func<T, object>> FieldExpr { get; set; }
    }
}
