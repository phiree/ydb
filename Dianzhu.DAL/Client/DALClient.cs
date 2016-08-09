using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALClient : Dianzhu.DAL.NHRepositoryBase<Model.Client,string>,IDAL.IDALClient
    {
 
        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient(Model.Client client)
        {
           Add(client);
        }

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Model.Client FindClient(string clientId)
        {
           
           return FindOne(x => x.Id == clientId);
            
        }


    }
}
