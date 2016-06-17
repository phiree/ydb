using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Client
{
    public interface IBLLClient
    {
        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
         void RegisterClient(Model.Client client);

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
         Model.Client FindClient(string clientId);
    }
}
