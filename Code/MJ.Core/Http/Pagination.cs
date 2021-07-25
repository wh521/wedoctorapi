namespace MJ.Core.Http
{
    public class Pagination
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序列(ID desc,CTime asc)
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        public object Items { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (Total > 0 && PageSize > 0)
                {
                    return Total % this.PageSize == 0 ? Total / this.PageSize : Total / this.PageSize + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
