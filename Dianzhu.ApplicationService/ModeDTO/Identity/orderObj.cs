using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class orderObj
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        /// <type>string</type>
        public string title { get; set; }

        /// <summary>
        /// 订单创建的时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string createTime { get; set; }

        /// <summary>
        /// 订单关闭的时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string closeTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <type>string</type>
        public string notes { get; set; }

        /// <summary>
        /// 参考价格
        /// </summary>
        /// <type>string</type>
        public string orderAmount { get; set; }

        /// <summary>
        /// 协商价格（未协商时与orderAmount相同）
        /// </summary>
        /// <type>string</type>
        public string negotiateAmount { get; set; }

        /// <summary>
        /// 服务的地址
        /// </summary>
        /// <type>string</type>
        public string serviceAddress { get; set; }

        /// <summary>
        /// 订单的状态对象
        /// </summary>
        /// <type>orderStatusObj</type>
        public orderStatusObj currentStatusObj { get; set; }

        /// <summary>
        /// 服务项的模型
        /// </summary>
        /// <type>servicesObj</type>
        public servicesObj serviceSnapshotObj { get; set; }

        /// <summary>
        /// 用户模型
        /// </summary>
        /// <type>userObj</type>
        public userObj userObj { get; set; }

        /// <summary>
        /// 店铺模型
        /// </summary>
        /// <type>storeObj</type>
        public storeObj storeObj { get; set; }

        /// <summary>
        /// 客服模型
        /// </summary>
        /// <type>customerServicesObj</type>
        public customerServicesObj customerServicesObj { get; set; }
    }
}
