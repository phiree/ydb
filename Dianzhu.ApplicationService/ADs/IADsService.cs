using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.ADs
{
    public interface IADsService
    {
        /// <summary>
        /// 条件读取广告
        /// </summary>
        /// <param name="adf"></param>
        /// <returns></returns>
        IList<adObj> GetADs(common_Trait_AdFiltering adf);
    }
}
