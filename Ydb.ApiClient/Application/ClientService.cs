using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ydb.ApiClient.DomainModel;
using Ydb.ApiClient.DomainModel.IRepository;

namespace Ydb.ApiClient.Application
{
    public class ClientService : IClientService
    {

        IRepositoryClient repoClient;
        public ClientService(IRepositoryClient repoClient)
        {
            this.repoClient = repoClient;
        }
        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient( Client client)
        {
            repoClient.RegisterClient(client);
        }

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Client FindClient(string clientId)
        {
            return repoClient.FindClient(clientId);
        }
    }
}
