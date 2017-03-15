using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
namespace Ydb.Push.DomainModel.IRepository
{
   public interface IRepositoryAdvertisement:IRepository<Advertisement,Guid>
    {
        
    }
}
