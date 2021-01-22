namespace Dianzhu.ApplicationService
{
    public class common_Trait_PayFiltering
    {
        private string _payStatus = "";

        /// <summary>
        /// 支付的状态("waitForPay":[等待支付]，"waitForVerify":[等待审核]，"success":[支付成功]，"failed":[支付失败])
        /// </summary>
        /// <type>string</type>
        public string payStatus
        {
            get
            {
                return _payStatus;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _payStatus = "";
                }
                else
                {
                    switch (value.ToLower())
                    {
                        case "waitforpay":
                            _payStatus = "Wait_Buyer_Pay";
                            break;

                        case "success":
                            _payStatus = "Trade_Success";
                            break;

                        case "failed":
                            _payStatus = "Fail";
                            break;

                        case "waitforverify":
                        ///没有对应状态
                        default:
                            _payStatus = value;
                            break;
                    }
                }
            }
        }

        private string _payType = "";

        /// <summary>
        /// 类型("deposit":[订金]，"finalPayment":[尾款]，"compensation":[赔偿金])
        /// </summary>
        /// <type>string</type>
        public string payType
        {
            get
            {
                return _payType;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _payType = "";
                }
                else
                {
                    switch (value.ToLower())
                    {
                        case "deposit":
                            _payType = "Deposit";
                            break;

                        case "finalpayment":
                            _payType = "FinalPayment";
                            break;

                        case "compensation":
                            _payType = "Compensation";
                            break;

                        default:
                            _payType = value;
                            break;
                    }
                }
            }
        }
    }
}