using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class servicesObj
    {
        /// <summary>
        /// 服务ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 服务项的名称
        /// </summary>
        /// <type>string</type>
        public string name { get; set; }

        /// <summary>
        /// 服务项的类型（“>”分级）
        /// </summary>
        /// <type>string</type>
        public string type { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        /// <type>string</type>
        public string introduce { get; set; }

        /// <summary>
        ///地位信息模型
        /// </summary>
        /// <type>locationObj</type>
        public locationObj location { get; set; }

        /// <summary>
        ///起步价
        /// </summary>
        /// <type>string</type>
        public string startAt { get; set; }

        /// <summary>
        ///单价
        /// </summary>
        /// <type>string</type>
        public string unitPrice { get; set; }

        /// <summary>
        ///订金
        /// </summary>
        /// <type>string</type>
        public string deposit { get; set; }

        /// <summary>
        ///提前预约的时间（分）
        /// </summary>
        /// <type>string</type>
        public string appointmentTime { get; set; }

        /// <summary>
        ///是否提供上门服务（true Or false）
        /// </summary>
        /// <type>boolean</type>
        public bool bDoorService { get; set; }

        /// <summary>
        ///服务面向的对象：“all”：[全部]，“company”：[公司]，“personal”：[个人]
        /// </summary>
        /// <type>string</type>
        public string eServiceTarget { get; set; }

        /// <summary>
        ///支持的支付方式：“all”：[全部]，“wePay”：[微支付]，“aliPay”：[支付宝]
        /// </summary>
        /// <type>string</type>
        public string eSupportPayWay { get; set; }

        /// <summary>
        ///标签（“|”区分）
        /// </summary>
        /// <type>string</type>
        public string tag { get; set; }

        /// <summary>
        ///是否开启（true Or false）
        /// </summary>
        /// <type>bool</type>
        public bool bOpen { get; set; }

        /// <summary>
        ///该服务最大接单量
        /// </summary>
        /// <type>string</type>
        public string maxCount { get; set; }

        /// <summary>
        ///服务计费单位
        /// </summary>
        /// <type>string</type>
        public string chargeUnit { get; set; }
    }
}
