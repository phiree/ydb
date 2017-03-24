namespace Dianzhu.ApplicationService
{
    public class common_Trait_BillFiltering
    {
        /// <summary>
        /// 开始日期(yyyyMMdd)/月份(yyyyMM)
        /// </summary>
        /// <type>string</type>
        public string startTime { get; set; }

        /// <summary>
        /// 结束日期(yyyyMMdd)/月份(yyyyMM)
        /// </summary>
        /// <type>string</type>
        public string endTime { get; set; }

        private string _serviceTypeLevel = "1";

        /// <summary>
        /// 按服务类型的层级的来返回明细内容，默认按level 2查询
        /// </summary>
        /// <type>string</type>
        public string serviceTypeLevel
        {
            get
            {
                return _serviceTypeLevel;
            }
            set
            {
                _serviceTypeLevel = value;
            }
        }
    }
}