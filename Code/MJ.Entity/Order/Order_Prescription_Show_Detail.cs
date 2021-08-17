using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity.Order
{
    /// <summary>
    /// 处方信息-指定门店透出
    /// </summary>
    [Table("Order_Prescription_Show_Detail")]
    public class Order_Prescription_Show_Detail:BaseEntity
    {
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        [Column(IsPrimaryKey =true)]
        public string DataId { get; set; }
        /// <summary>
        /// 处方笺抬头
        /// </summary>
        [Description("处方笺抬头")]
        public string prescription_title { get; set; }
        /// <summary>
        /// 煎药说明中药处方煎药方式
        /// </summary>
        [Description("煎药说明中药处方煎药方式")]
        public string tcm_decoct_desc { get; set; }
        /// <summary>
        /// 用法用量文本字段(中药)
        /// </summary>
        [Description("用法用量文本字段(中药)")]
        public string tcm_dosage_text { get; set; }
        /// <summary>
        /// 服药时间(中药)
        /// </summary>
        [Description("服药时间(中药)")]
        public string tcm_dose_time { get; set; }
        /// <summary>
        /// 每日剂数
        /// </summary>
        [Description("每日剂数")]
        public int? dose_count_perday { get; set; }
        /// <summary>
        /// 处方单号
        /// </summary>
        [Description("处方单号")]
        public string prescription_no { get; set; }
        /// <summary>
        /// 医院处方号
        /// </summary>
        [Description("医院处方号")]
        public string medcare_diagnosis_id { get; set; }
        /// <summary>
        /// 用药频次
        /// </summary>
        [Description("用药频次")]
        public int? frequency { get; set; }
        /// <summary>
        /// 科别(科室)
        /// </summary>
        [Description("科别(科室)")]
        public string doctor_std_dept_name { get; set; }
        /// <summary>
        /// 开方时间
        /// </summary>
        [Description("开方时间")]
        public string prescription_time { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        [Description("患者姓名")]
        public string patient_name { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        [Description("医生姓名")]
        public string doctor_name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Description("年龄")]
        public int? patient_age { get; set; }
        /// <summary>
        /// 医嘱(中药)
        /// </summary>
        [Description("医嘱(中药)")]
        public string tcm_doctor_advice { get; set; }
        /// <summary>
        /// 医院名称
        /// </summary>
        [Description("医院名称")]
        public string hospital_name { get; set; }
        /// <summary>
        /// 用药禁忌(中药)
        /// </summary>
        [Description("用药禁忌(中药)")]
        public string contraindication { get; set; }
        /// <summary>
        /// 剂数
        /// </summary>
        [Description("剂数")]
        public int? tcm_dose_count { get; set; }
        /// <summary>
        /// 临床诊断
        /// </summary>
        [Description("临床诊断")]
        public string doctor_diagnosis { get; set; }
        /// <summary>
        /// 患者性别1男2女3未知
        /// </summary>
        [Description("患者性别1男2女3未知")]
        public int? patient_sex { get; set; }
        /// <summary>
        /// 患者身份证号码
        /// </summary>
        [Description("患者身份证号码")]
        public string patient_idcard_no { get; set; }
        /// <summary>
        /// 电子处方图片
        /// </summary>
        [Description("电子处方图片")]
        public string prescription_jpg_url { get; set; }
        /// <summary>
        /// 药品类型:1草药2颗粒3膏方4秘方(中药)
        /// </summary>
        [Description("药品类型:1草药2颗粒3膏方4秘方(中药)")]
        public int? tcm_drug_type { get; set; }
        /// <summary>
        /// 处方类型1：西药处方2：中药处方
        /// </summary>
        [Description("处方类型1：西药处方2：中药处方")]
        public int? prescription_type { get; set; }
        /// <summary>
        /// 中药处方煎药次数1：一煎2：二煎3：三煎
        /// </summary>
        [Description("中药处方煎药次数1：一煎2：二煎3：三煎")]
        public string decoct_medicine_second { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        [Description("门诊号")]
        public string biz_id { get; set; }
        /// <summary>
        /// 处方类型0-普通;29-慢病处方
        /// </summary>
        [Description("处方类型0-普通;29-慢病处方")]
        public int? extend_type { get; set; }
        /// <summary>
        /// 中药药剂容量包
        /// </summary>
        [Description("中药药剂容量包")]
        public double? package_num { get; set; }


        /// <summary>
        /// 订单读取更新状态0:未读取 1:已更新
        /// </summary>
        [Description("订单读取更新状态0:未读取 1:已更新")]
        public int ReadStatus { get; set; }

        /// <summary>
        /// 数据读取更新时间
        /// </summary>
        [Description("数据读取更新时间")]
        public DateTime? ReadTime { get; set; }

        /// <summary>
        /// 西药处方单药品
        /// </summary>
        public List<OrderDrugs> drugs_list { get; set; }

        /// <summary>
        /// 中药处方单药品
        /// </summary>
        public List<Order_Tcm_Drugs> tcm_drugs_list { get; set; }
    }
}
