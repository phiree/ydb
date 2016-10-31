using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.ViewModel
{
    public class VMShelfService
    {
        public VMShelfService(Guid serviceId, int number,bool isVerify,string businessName,string servieName,int appraiseScore,string timeInterval,decimal unitPrice,decimal depositPrice)
        {
            ServiceId = serviceId;
            Number = number;
            IsVerify = isVerify;
            BusinessName = businessName;
            ServiceName = servieName;
            AppraiseScore = appraiseScore;
            TimeInterval = timeInterval;
            UnitPrice = unitPrice;
            DepositPrice = depositPrice;
        }

        public Guid ServiceId { get; protected internal set; }
        public int Number { get; protected internal set; }
        public bool IsVerify { get; protected internal set; }
        public string BusinessName { get; protected internal set; }
        public string ServiceName { get; protected internal set; }
        public int AppraiseScore { get; protected internal set; }
        public string TimeInterval { get; protected internal set; }
        public decimal UnitPrice { get; protected internal set; }
        public decimal DepositPrice { get; protected internal set; }
    }
}
