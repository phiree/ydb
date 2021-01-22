using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.RequestRestful
{
    public interface IRequestRestful
    {
        /// <summary>
        /// 请求RestfulApi
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        RequestResponse RequestRestfulApi(RequestParams param);

        /// <summary>
        /// 请求用户认证
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        RequestResponse RequestRestfulApiForAuthenticated(string serviceUrl, string loginName, string password);


        /// <summary>
        /// 请求RestfulApi获取UserCity
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="userId"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        RequestResponse RequestRestfulApiForUserCity(string serviceUrl, string userId, string userToken);

    }
}
