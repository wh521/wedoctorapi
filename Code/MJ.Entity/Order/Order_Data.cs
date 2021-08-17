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
    /// 发货主单
    /// </summary>
    [Table("Order_Data")]
    public class Order_Data:BaseEntity
    {
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        [Column(IsPrimaryKey = true)]
        public string DataId { get; set; }
        /// <summary>
        /// 收货人所在市
        /// </summary>
        [Description("收货人所在市")]
        public string receiver_city { get; set; }
        /// <summary>
        /// 订单邮费
        /// </summary>
        [Description("订单邮费")]
        public double? logistics_fee { get; set; }
        /// <summary>
        /// 订单支付方式(0线上支付1医保支付2线下支付3第三方支付)
        /// </summary>
        [Description("订单支付方式(0线上支付1医保支付2线下支付3第三方支付)")]
        public int? pay_way_type { get; set; }
        /// <summary>
        /// 收货人所在省份
        /// </summary>
        [Description("收货人所在省份")]
        public string receiver_state { get; set; }
        /// <summary>
        /// 配送方式0送药上门4门店自提2无物流
        /// </summary>
        [Description("配送方式0送药上门4门店自提2无物流")]
        public int? delivery_type { get; set; }
        /// <summary>
        /// 处方结算总金额
        /// </summary>
        [Description("处方结算总金额")]
        public double p_total_price { get; set; }
        /// <summary>
        /// 状态（0待确认待发货;10确认超时;20已拒单;30已取消;40已确认;50已完成;60部分发货）
        /// </summary>
        [Description("状态(0待确认待发货;10确认超时;20已拒单;30已取消;40已确认;50已完成;60部分发货)")]
        public int? send_status { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        [Description("收货人姓名")]
        public string receiver_name { get; set; }
        /// <summary>
        /// 发货单主单ID
        /// </summary>
        [Description("发货单主单ID")]
        public long? id { get; set; }
        /// <summary>
        /// 云药房名称
        /// </summary>
        [Description("云药房名称")]
        public string business_name { get; set; }
        /// <summary>
        /// 医生第一执业点医院ID（后期跟踪药品流向，需要对接方保存）
        /// </summary>
        [Description("医生第一执业点医院ID（后期跟踪药品流向，需要对接方保存）")]
        public string first_hospital_id { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        [Description("订单总金额")]
        public double? total_price { get; set; }
        /// <summary>
        /// 收货人所在区
        /// </summary>
        [Description("收货人所在区")]
        public string receiver_district { get; set; }
        /// <summary>
        /// 处方单ID
        /// </summary>
        [Description("处方单ID")]
        public string prescription_id { get; set; }
        /// <summary>
        /// 云药房店铺名称
        /// </summary>
        [Description("云药房店铺名称")]
        public string shop_name { get; set; }
        /// <summary>
        /// 医生第一执业点医院名称（后期跟踪药品流向，需要对接方保存）
        /// </summary>
        [Description("医生第一执业点医院名称（后期跟踪药品流向，需要对接方保存）")]
        public string first_hospital_name { get; set; }
        /// <summary>
        /// 收货人手机号码
        /// </summary>
        [Description("收货人手机号码")]
        public string receiver_mobile { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        [Description("支付时间")]
        public string pay_time { get; set; }
        /// <summary>
        /// 是否线上订单（产生支付单并且通过收银台支付）True线上False线下
        /// </summary>
        [Description("是否线上订单（产生支付单并且通过收银台支付）True线上False线下")]
        public string is_internal_pay_order { get; set; }
        /// <summary>
        /// 是否代煎false非代煎，true代煎
        /// </summary>
        [Description("是否代煎false非代煎，true代煎")]
        public bool? is_boil { get; set; }
        /// <summary>
        /// 云药房店铺id
        /// </summary>
        [Description("云药房店铺id")]
        public long? shop_id { get; set; }
        /// <summary>
        /// 拒单的原因
        /// </summary>
        [Description("拒单的原因")]
        public string refuse_order_type { get; set; }
        /// <summary>
        /// 收货人详细地址
        /// </summary>
        [Description("收货人详细地址")]
        public string receiver_address { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        [Description("订单ID")]
        public long? order_id { get; set; }
        /// <summary>
        /// 云药房id
        /// </summary>
        [Description("云药房id")]
        public long? business_id { get; set; }
        /// <summary>
        /// 核销码(自提)
        /// </summary>
        [Description("核销码(自提)")]
        public string verification_code { get; set; }

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
        /// 发货单子单
        /// </summary>
        public List<Order_Details> details { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public Order_Ext_Info ext_info { get; set; }

        /// <summary>
        /// 处方信息--指定门店透出
        /// </summary>
        public Order_Prescription_Show_Detail prescription_show_detail { get; set; }
    }
}
