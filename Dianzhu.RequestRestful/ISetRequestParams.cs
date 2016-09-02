using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.RequestRestful
{
    public interface ISetRequestParams
    {
        /// <summary>
        /// 根据请求信息设置请求参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appName"></param>
        /// <param name="appKey"></param>
        /// <param name="strHost"></param>
        /// <returns></returns>
        RequestParams SetParamByRequestInfo(HttpContext context, string appName, string appKey, string strHost);
    }
}
