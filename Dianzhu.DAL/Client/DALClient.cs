using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALClient : DALBase<Model.Client>
    {

        public DALClient()
        {

        }

        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient(Model.Client client)
        {
            Save(client);
        }

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Model.Client FindClient(string clientId)
        {
            Model.Client client = null;
            IQuery query = Session.CreateQuery("select m from  Client as m where Id='" + clientId + "'");
            Action a = () => { client = query.UniqueResult<Model.Client>(); };
            TransactionCommit(a);
            return client;
        }


    }
}
