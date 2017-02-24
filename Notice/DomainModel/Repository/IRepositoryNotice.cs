using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Notice.DomainModel.Repository
{
    public interface IRepositoryNotice : Ydb.Common.Repository.IRepository<Notice, Guid>
    {
    }
}