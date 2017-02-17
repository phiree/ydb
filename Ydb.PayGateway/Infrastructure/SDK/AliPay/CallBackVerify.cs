using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.PayGateway.Infrastructure.SDK.AliPay
{
    public interface ICallBackVerify
    {
        bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign);
    }

    public class CallBaclVerifyAli : ICallBackVerify
    {
        public bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign)
        {
            throw new NotImplementedException();
        }
    }
}
