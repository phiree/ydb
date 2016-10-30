using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Ydb.Membership.DomainModel.Repository
{
   public interface IRepositoryUserToken:IRepository<UserToken,Guid>
    {
        UserToken GetToken(string userID);
    }
}
