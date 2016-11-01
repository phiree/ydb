using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
namespace Ydb.Membership.DomainModel.Repository
{
   public interface IRepositoryUserToken:IRepository<UserToken,Guid>
    {
        UserToken GetToken(string userID);
    }
}
