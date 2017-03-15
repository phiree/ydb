using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.MediaServer.DomainModel;
using Ydb.MediaServer.DomainModel.Repository;

namespace Ydb.MediaServer.Infrastructure
{
    public class RepositoryStorageFileInfo : NHRepositoryBase<StorageFileInfo,Guid>, IRepositoryStorageFileInfo
    {
    }
}
