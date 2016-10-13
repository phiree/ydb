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
        //用户超时未回复,删除分配关系
        void DeleteReception(string customerId);

        string AssignCustomerLogin(string customerId,out string errorMessage);

        void AssignCSLogin(string csId);

        void AssignCSLogoff(string csId);
    }
}
