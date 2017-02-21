using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

using Ydb.ApiClient.DomainModel.IRepository;
using Ydb.ApiClient.DomainModel;
using Ydb.ApiClient.Intrastructure.YdbNhibernate.UnitOfWork;

namespace Ydb.ApiClient.Intrastructure.YdbNhibernate.Repository
{
    public class RepositoryClient : NHRepositoryBase< Client,string>,IRepositoryClient
    {
 
        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient( Client client)
        {
           Add(client);
        }

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public  Client FindClient(string clientId)
        {
           
           return FindOne(x => x.Id == clientId);
            
        }


    }
}
