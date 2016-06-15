using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using NUnit.Framework;
using FizzWare.NBuilder;
using Dianzhu.BLL;

namespace Dianzhu.Test.PayTest
{
    [TestFixture]
   public class payTest
    {
        [Test]
        public void CreatePayLinkTest() {
            ServiceOrder order = Builder<ServiceOrder>
                   .CreateNew().Build();
           // string url
        }
        [Test]
        public void PayCallBackTest()
        {
            Uri uri=new Uri(@"http://119.29.39.211:8168/alipay/notify_url.aspx?payment_type=1&subject=%u5e72%u6d17&trade_no=2016021821001004170256026479&buyer_email=hanpengfei8166%40aliyun.com&gmt_create=2016-02-18+19%3a27%3a24&notify_type=trade_status_sync&quantity=1&out_trade_no=42a17a85-be99-488f-b67d-a5b00140960d&notify_time=2016-02-18+19%3a51%3a15&seller_id=2088021632422534&trade_status=TRADE_SUCCESS&is_total_fee_adjust=N&total_fee=0.01&gmt_payment=2016-02-18+19%3a27%3a24&seller_email=jsyk_company%40126.com&price=0.01&buyer_id=2088102945231175&notify_id=b03ff37a383d47363af7bc0207aea5dhba&use_coupon=N&sign_type=RSA&sign=e3rTu4Ckttu6jsbceNCgBLRREdFtXFElmZ6g3JOAlafSWehxMr%2fjTM01AvEFKrWb8YbzVbsGPRaCrDX2uyT2FZIhB0HvFXER9juKNtSNwy%2b37AUKKhGy6NFTxN33kiwVVPClWNuqifl6W%2bFU5QCC7jYDgdo1GZCfShMzHBjP%2b0w%3d"
);
            BLL.BLLPay bllPay = Bootstrap.Container.Resolve<BLLPay>();
            bllPay.ReceiveAPICallBack(Model.Enums.enum_PaylogType.ResultReturnFromAli,
                new Dianzhu.Pay.PayCallBackAli(),
                uri.ToString(), System.Web.HttpUtility.ParseQueryString(uri.Query));
        }
    }
}
