using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 分配关系
    /// </summary>
    public interface IReceptionService
    {
        //用户上线,申请客服
        string AssignToCustomerService(string customerId);

        //用户超时未回复,删除分配关系
        void DeleteReception(string customerId);
        //客服上线,拉取可能的在线用户

        void GetOnlineCustomers(string customerServiceId);
        //客服下线,将用户分配给其他客服.
        void AssignToOtherCustomerService(string customerServiceId);
    }
}
