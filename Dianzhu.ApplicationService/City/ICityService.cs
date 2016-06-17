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
        /// <param name="areacode">code代码</param>
        /// <returns>area实体</returns>
        cityObj GetCityByAreaCode(string areacode);

        /// <summary>
        /// 获得所有city
        /// </summary>
        /// <returns>area实体list</returns>
        IList<cityObj> GetAllCity(common_Trait_Filtering filter, common_Trait_LocationFiltering location);
    }
}
