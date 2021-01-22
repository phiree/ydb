namespace Dianzhu.ApplicationService
{
    public class common_Trait_OrderFiltering
    {
        /// <summary>
        /// 订单类别("done":[完成的订单]，"pending":[未完成的订单]，"all":[所有的订单])
        /// </summary>
        /// <type>string</type>
        public string statusSort { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <type>string</type>
        public string status { get; set; }

        /// <summary>
        /// 相关店铺 ID
        /// </summary>
        /// <type>string</type>
        public string storeID { get; set; }

        /// <summary>
        /// 负责人 ID (传空字符串，表示未指派的订单)
        /// </summary>
        /// <type>string</type>
        public string formanID { get; set; }

        /// <summary>
        /// 在这个时间之后创建的（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string afterThisTime { get; set; }

        /// <summary>
        /// 在这个时间之前创建的（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string beforeThisTime { get; set; }

        /// <summary>
        /// 是否指派
        /// </summary>
        /// <type>string</type>
        public string assign { get; set; }
    }
}