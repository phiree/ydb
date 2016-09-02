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

    }
}
