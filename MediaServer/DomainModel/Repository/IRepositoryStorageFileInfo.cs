using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository; 

namespace Ydb.MediaServer.DomainModel.Repository
{
    public interface IRepositoryStorageFileInfo : IRepository<StorageFileInfo, Guid>
    {
    }
}
