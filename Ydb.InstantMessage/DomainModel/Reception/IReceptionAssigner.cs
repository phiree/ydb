using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using Ydb.InstantMessage.DomainModel.Enums;
namespace Ydb.InstantMessage.DomainModel.Reception
{

    public interface IReceptionAssigner
    {
        string AssignCustomerLogin(IList<ReceptionStatus> existedReception, string customerId);

        Dictionary<string, string> AssignCSLogin(IList<ReceptionStatus> existReceptionDD, string csId);

        Dictionary<string, string> AssignCSLogoff(IList<ReceptionStatus> existReceptionCS);
    }
}
