using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.City
{
    public interface ICityService
    {
        /// <summary>
        /// 根据areacode获得city
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        cityObj GetCityByAreaCode(string areacode);

        /// <summary>
        /// 获得所有city
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        IList<cityObj> GetAllCity(common_Trait_Filtering filter, common_Trait_LocationFiltering location, Customer customer);
    }
}
