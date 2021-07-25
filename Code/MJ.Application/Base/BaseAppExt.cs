using Chloe;
using MJ.Entity;
using System.Data;
using MJ.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;

namespace MJ.Application
{

    /// <summary>
    /// 扩展BaseApp基类，增加方法
    /// </summary>
    public static class BaseAppExt
    {


        #region 获取业务单号
        /// <summary>
        /// 调用存储过程，获取唯一业务单号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <param name="docType"></param>
        /// <param name="fixedNum"></param>
        /// <returns></returns>
        public static string GetNewDocNum<T>(this BaseApp<T> app, string docType, string fixedNum) where T : BaseEntity, new()
        {
            var pm1 = new DbParam("@DocType", docType);
            var pm2 = new DbParam("@FixedNum", fixedNum);
            var outPm = new DbParam("@BarCode", null, typeof(string)) { Direction = ParamDirection.Output, Size = 20 };
            app.DbContext.Session.ExecuteNonQuery("Proc_GetNewNum", CommandType.StoredProcedure, pm1, pm2, outPm);
            return outPm.Value + "";
        }


        #endregion


        #region 判断是否正确条码

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public static bool IsWmsBarCode<T>(this BaseApp<T> app, string inputCode) where T : BaseEntity, new()
        {
            List<string> barCodeFixedPart = app.GetFixedNumList<T>().Where(t => t.Length == 1).ToList();
            string fixedPart = inputCode.ExtractLetterPart();
            return fixedPart.Length == 1 && barCodeFixedPart.Contains(fixedPart) ? true : false;
        }


        #endregion

        #region 判断是否为正确的单据号

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public static bool IsWmsDocument<T>(this BaseApp<T> app, string inputCode) where T : BaseEntity, new()
        {
            List<string> docFixedPart = app.GetFixedNumList<T>().Where(t => t.Length == 2).ToList();
            string fixedPart = inputCode.ExtractLetterPart();
            return fixedPart.Length == 1 && docFixedPart.Contains(fixedPart) ? true : false;
        }

        #endregion



        #region 私有辅助方法

        /// <summary>
        /// 获取配置表中固定前缀
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<string> GetFixedNumList<T>(this BaseApp<T> app) where T : BaseEntity, new()
        {
            // 直接执行Sql语句
            return app.DbContext.SqlQuery<string>("SELECT FixedNum FROM [dbo].[MD_NumRules]").ToList();
        }


        /// <summary>
        /// 获取配置表中业务类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static string GetDocTypeByFixedNum<T>(this BaseApp<T> app, string fixedNum) where T : BaseEntity, new()
        {
            // 直接执行Sql语句
            return app.DbContext.SqlQuery<string>("SELECT DocType FROM [dbo].[MD_NumRules] WHERE FixedNum =@FixedNum", new DbParam("@FixedNum", fixedNum)).FirstOrDefault();
        }

        #endregion

        #region 获取业务单号
        /// <summary>
        /// 批量条码/单号生成,生成规则同存储过程,存储过程生成数量为1,此方法可指定数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <param name="docType"></param>
        /// <param name="fixedNum"></param>
        /// <param name="count">生成条码数量</param>
        /// <returns></returns>
        public static string[] GetNewDocNumExt<T>(this BaseApp<T> app, string docType, string fixedNum, int count = 1) where T : BaseEntity, new()
        {
            string[] result = new string[count];
            //生成单号逻辑:固定+服务器日期+流水
            string sql = @" DECLARE @NN   INT;
                            DECLARE @FlowNum   INT;
                            DECLARE @date   NVARCHAR (20);
                            DECLARE @COUNT   INT;                            
                            SET @COUNT = 0;
                            SET @date = CONVERT (DATE, GETDATE (), 23);
                            SET @BARCODESTRING='';

                            WHILE (@COUNT < @BarCodeCount)
                               BEGIN
                                  IF EXISTS
                                        (SELECT 1
                                         FROM MD_NumRules
                                         WHERE     DocType = @DocType
                                               AND FixedNum = @FixedNum
                                               AND DocDate = @date)
                                     BEGIN
                                        SELECT @NN = NextNumber, @FlowNum = FlowNum
                                        FROM MD_NumRules
                                        WHERE     DocType = @DocType
                                              AND FixedNum = @FixedNum
                                              AND DocDate = @date;
                                        SET @BARCODESTRING=@BARCODESTRING+','+ @FixedNum
                                               + CONVERT (VARCHAR (100), GETDATE (), 112)
                                               + RIGHT (
                                                      REPLICATE ('0', @FlowNum)
                                                    + CAST (@NN AS VARCHAR (10)),
                                                    @FlowNum);
                                        --更新下一编号
                                        SET @NN = @NN + 1;
                                        UPDATE MD_NumRules
                                        SET NextNumber = @NN, MTime = GETDATE ()
                                        WHERE     DocType = @DocType
                                              AND FixedNum = @FixedNum
                                              AND DocDate = @date;
                                     END
                                  ELSE
                                     BEGIN
                                        SET @NN = 1;
                                        SELECT @FlowNum = FlowNum
                                        FROM MD_NumRules
                                        WHERE DocType = @DocType AND FixedNum = @FixedNum;
                                        SET @BARCODESTRING=@BARCODESTRING+','+ @FixedNum
                                               + CONVERT (VARCHAR (100), GETDATE (), 112)
                                               + RIGHT (
                                                      REPLICATE ('0', @FlowNum)
                                                    + CAST (@NN AS VARCHAR (10)),
                                                    @FlowNum);
                                        --更新下一编号
                                        SET @NN = @NN + 1;
                                        UPDATE MD_NumRules
                                        SET NextNumber = @NN, DocDate = @date, MTime = GETDATE ()
                                        WHERE DocType = @DocType AND FixedNum = @FixedNum;
                                     END;

                                  SET @COUNT = @COUNT + 1
                               END;
                               SELECT @BARCODESTRING;";
            DbParam barcodeString = new DbParam();
            barcodeString.Direction = ParamDirection.Output;
            barcodeString.Name = "@BARCODESTRING";
            barcodeString.Size = 1000;
            barcodeString.DbType = DbType.String;
            var list = app.DbContext.SqlQuery<string>(sql, new DbParam("@BarCodeCount", count), new DbParam("@FixedNum", fixedNum), new DbParam("@DocType", docType), barcodeString)?.ToArray();
            return barcodeString.Value?.ToString()?.TrimStart(',')?.Split(',');
        }
        #endregion
    }

    /// <summary>
    /// 单据类型
    /// </summary>
    public class DocType
    {
        /// <summary>
        /// 采购模块
        /// </summary>
        public const string PO = "PO";

        /// <summary>
        /// 生产模块
        /// </summary>
        public const string PP = "PP";


        /// <summary>
        /// 仓库模块
        /// </summary>
        public const string MM = "MM";

        /// <summary>
        /// 销售模块
        /// </summary>
        public const string SD = "SD";

        /// <summary>
        /// 基础数据
        /// </summary>
        public const string MD = "MD";

        /// <summary>
        /// SAP模块
        /// </summary>
        public const string SAP = "SAP";

        /// <summary>
        /// 仓库模块
        /// </summary>
        public const string POST_MM = "POST_MM";

        /// <summary>
        /// 销售模块
        /// </summary>
        public const string POST_SD = "POST_SD";

        /// <summary>
        /// 采购模块
        /// </summary>
        public const string POST_PO = "POST_PO";

        /// <summary>
        /// 生产模块
        /// </summary>
        public const string POST_PP = "POST_PP";

        /// <summary>
        /// 质量模块
        /// </summary>
        public const string POST_QM = "POST_QM";
        /// <summary>
        /// 财务模块
        /// </summary>
        public const string FO = "FO";
        /// <summary>
        /// 返还单号
        /// </summary>
        public const string RO = "RO";
        /// <summary>
        /// 开票单号
        /// </summary>
        public const string IN = "IN";
        /// <summary>
        /// 立项编号
        /// </summary>
        public const string LX = "LX";

        /// <summary>
        /// 采购价格管理
        /// </summary>
        public const string QM = "QM";

        /// <summary>
        /// 审核流程编号
        /// </summary>
        public const string BPM = "BPM";

        /// <summary>
        /// 供货能力管理
        /// </summary>
        public const string SM = "SM";


    }

    /// <summary>
    /// 固定前缀
    /// </summary>
    public class DocFixedNumDef
    {
        #region 标签部分
        /// <summary>
        /// 采购标签
        /// </summary>
        public const string PO_BarCode = "M";

        /// <summary>
        /// 自制品标签
        /// </summary>
        public const string PP_BarCode = "P";

        /// <summary>
        /// 物料标签
        /// </summary>
        public const string MM_BarCode = "O";

        /// <summary>
        /// 销售退货标签
        /// </summary>
        public const string SD_BarCodeReturn = "R";


        /// <summary>
        /// 包装箱???
        /// </summary>
        public const string PackingBoxLabel = "M";


        #endregion


        /// <summary>
        /// 审核流程编号前缀
        /// </summary>
        public const string ProcessNumber = "BPM";

        /// <summary>
        /// 批次号
        /// </summary>
        public const string BatchNo = "B";
        /// <summary>
        /// 返还单号
        /// </summary>
        public const string ReturnNo = "R";
        /// <summary>
        /// 返还单号
        /// </summary>
        public const string InvoiceNo = "IN";
        /// <summary>
        /// 立项
        /// </summary>
        public const string EquipPurchase = "LX";

        #region 单据部分

        /// <summary>
        /// 报检单生成单号
        /// </summary>
        public const string PO_Make_Inspection_Report = "I";

        /// <summary>
        /// 不合格处置单生成序号
        /// </summary>
        public const string SHM_UnqualifiedNoticeNum = "PZ";

        /// <summary>
        /// 采购交货计划单
        /// </summary>
        public const string PO_DeliveryPlan = "DP";

        /// <summary>
        /// 采购送货单
        /// </summary>
        public const string PO_DeliveryNote = "DN";

        /// <summary>
        /// 采购报检扫描
        /// </summary>
        public const string PO_Inspection_Scan = "PN";

        /// <summary>
        /// 采购质检单
        /// </summary>
        public const string PO_Inspection = "PI";

        /// <summary>
        /// 采购上架扫描单
        /// </summary>
        public const string PO_ShelfScan = "PS";

        /// <summary>
        /// 采购退货扫描单
        /// </summary>
        public const string PO_Inspection_Report = "PR";

        /// <summary>
        /// 采购退供质检单(退给供应商)
        /// </summary>
        public const string PO_InspectionScan = "RS";

        /// <summary>
        /// 采购退供区物料移动扫描单
        /// </summary>
        public const string PO_ITransferScan = "IT";

        /// <summary>
        /// 生产备货波次单
        /// </summary>
        public const string PP_StockingWave = "SW";
        /// <summary>
        /// 生产备货扫描单
        /// </summary>
        public const string PP_StockingScan = "PS";
        /// <summary>
        /// 生产退货扫描单
        /// </summary>
        public const string PP_ReturnScan = "PR";

        /// <summary>
        /// 生产报交扫描单
        /// </summary>
        //public const string PurchaseDeliveryNote = "PI";

        /// <summary>
        /// 生产发料扫描单
        /// </summary>
        public const string PP_OutScan = "";


        /// <summary>
        /// 自制品质检扫描单
        /// </summary>
        public const string PP_InspectionScan = "IS";


        /// <summary>
        /// 销售波次单
        /// </summary>
        public const string SD_Delivery = "SD";




        /// <summary>
        /// 销售备货扫描单
        /// </summary>
        public const string SD_StockingScan = "SS";


        /// <summary>
        /// 销售交货扫描单
        /// </summary>
        public const string SD_DeliveryScan = "DS";


        /// <summary>
        /// 销售装箱单
        /// </summary>
        public const string SD_Packing = "SP";


        /// <summary>
        /// 销售退货扫描单
        /// </summary>
        public const string SD_ReturnScan = "SR";



        /// <summary>
        /// 其他收货扫描单
        /// </summary>
        public const string MM_InScan = "MI";


        /// <summary>
        /// 其他发货扫描单
        /// </summary>
        public const string MM_OutScan = "MO";


        /// <summary>
        /// 物料转移扫描单
        /// </summary>
        public const string MM_TransferScan = "MT";


        /// <summary>
        /// 盘点计划单
        /// </summary>
        public const string MM_TakeStockPlan = "TS";

        #endregion

        #region SAP单据
        /// <summary>
        /// SAP采购收货通知单
        /// </summary>
        public const string PO_DeliveryNotification = "DN";

        /// <summary>
        /// 仓库模块
        /// </summary>
        public const string POST_MM = "W";

        /// <summary>
        /// 销售模块
        /// </summary>
        public const string POST_SD = "S";

        /// <summary>
        /// 采购模块
        /// </summary>
        public const string POST_PO = "B";

        /// <summary>
        /// 生产模块
        /// </summary>
        public const string POST_PP = "P";


        /// <summary>
        /// 质量模块
        /// </summary>
        public const string POST_QM = "Q";

        #endregion

        /// <summary>
        /// 询价计划
        /// </summary>
        public const string PP = "PP";

        /// <summary>
        /// 询价单
        /// </summary>
        public const string IP = "IP";

        /// <summary>
        /// 报价
        /// </summary>
        public const string OP = "OP";

        #region 供货能力管理
        
        /// <summary>
        /// 供应商备货
        /// </summary>
        public const string SP = "SP";

        #endregion
    }
    /// <summary>
    /// 格式化处理器
    /// </summary>
    public static class FormatProcessor
    {
        #region 查询/导出 时间条件格式化
        /// <summary>
        /// 查询/导出 时间条件格式化
        /// </summary>
        /// <param name="dateTimes"></param>
        /// <param name="isDateTime"></param>
        /// <returns></returns>
        public static DateTime[] QueryDateTimesFormat(DateTime[] dateTimes, bool isDateTime = false)
        {
            DateTime fromTime = new DateTime(1900, 1, 1).Date;
            DateTime toTime = DateTime.Now.AddYears(100).Date;
            if (dateTimes != null && dateTimes.Length >= 2)
            {
                if (dateTimes[0].Year > 1900)
                {
                    fromTime = dateTimes[0].Date;
                }
                if (dateTimes[1].Year > 1900)
                {
                    toTime = dateTimes[1].AddDays(1).Date.AddSeconds(-1);
                }

                if (isDateTime)
                {
                    fromTime = dateTimes[0];
                    toTime = dateTimes[1].AddDays(1);
                }
            }
            return new DateTime[] { fromTime, toTime };
        }
        #endregion

    }
    /// <summary>
    /// 获取类的字段
    /// </summary>
    public class GetEntity
    {

    }
}
