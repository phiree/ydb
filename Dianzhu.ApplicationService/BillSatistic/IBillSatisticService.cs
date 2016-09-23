using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.BillSatistic
{
   public interface IBillSatisticService
    {
        /// <summary>
        /// 根据日期统计账单结果
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        IList<billStatement> GetBillSatistics(common_Trait_BillFiltering bill, Customer customer);

        /// <summary>
        /// 根据月份统计账单结果
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        IList<billStatement> GetMonthBillStatement(common_Trait_BillFiltering bill, Customer customer);


    }
}
