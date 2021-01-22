using System.Collections.Generic;

namespace Dianzhu.ApplicationService.ADs
{
    public interface IADsService
    {
        /// <summary>
        /// 条件读取广告
        /// </summary>
        /// <param name="adf"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        IList<adObj> GetADs(common_Trait_AdFiltering adf, Customer customer);
    }
}