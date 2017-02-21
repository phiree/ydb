using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Repository;

namespace Ydb.ApiClient.DomainModel.IRepository
{
    public interface IRepositoryClient:IRepository<Client,string>
    {

          void RegisterClient(Client client);

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Client FindClient(string clientId);

    }

   
}
