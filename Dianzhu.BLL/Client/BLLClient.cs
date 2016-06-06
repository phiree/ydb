using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.DAL;

namespace Dianzhu.BLL.Client
{
    public class BLLClient : IBLLClient
    {
        DALClient dalclient;
        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient(Model.Client client)
        {
            dalclient.RegisterClient(client);
        }

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Model.Client FindClient(string clientId)
        {
            return dalclient.FindClient(clientId);
        }
    }
}
