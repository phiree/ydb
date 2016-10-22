using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    public interface IRepositoryChat: IRepository<ReceptionChat,Guid>
    {
        IList<ReceptionChat> GetListByCustomerId(string customerId);
    }
}
