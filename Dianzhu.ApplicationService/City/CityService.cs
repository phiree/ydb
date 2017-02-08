using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ydb.Common.Application;
using Ydb.Common.Domain;
using Ydb.Common.Specification;

namespace Dianzhu.ApplicationService.City
{
    public class CityService: ICityService
    {
        IAreaService areaService;

        public CityService(IAreaService areaService)
        {
            this.areaService = areaService;
        }

        /// <summary>
        /// 根据areacode获得city
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        public cityObj GetCityByAreaCode(string areacode)
        {
            Area area = areaService.GetCityByAreaCode(areacode);
            if (area == null)
            {
                throw new Exception("没有找到资源！");
            }
            cityObj cityobj = Mapper.Map< Area, cityObj>(area);
            return cityobj;
        }

        /// <summary>
        /// 获得所有city,或根据经纬度与areacode获取城市
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public IList<cityObj> GetAllCity(common_Trait_Filtering filter,common_Trait_LocationFiltering location)
        {
            IList<Area> listarea=null;
             Area area;
            if (!string.IsNullOrEmpty(location.longitude) && !string.IsNullOrEmpty(location.latitude))
            {
                RespGeo geoObj = utils.Deserialize<RespGeo>(utils.GetCity(location.longitude, location.latitude));
                area = areaService.GetAreaByAreaname(geoObj.result.addressComponent.province + geoObj.result.addressComponent.city);
                //if (location.code != null && location.code != "" && area != null)
                //{
                //    if (location.code != area.Code)
                //    {
                //        area = null;
                //    }
                //}
                if (area == null)
                {
                    area = new  Area();
                    area.Name = geoObj.result.addressComponent.city;
                    area.Code = geoObj.result.cityCode;
                }
                listarea = new List<Area>();
                listarea.Add(area);

            }
            else
            {
                if (!string.IsNullOrEmpty(location.code))
                {
                    area = areaService.GetCityByAreaCode(location.code);
                    if (area != null)
                    {
                        listarea = new List<Area>();
                        listarea.Add(area);
                    }
                }
                else
                {
                    TraitFilter filter1 = utils.CheckFilter(filter, "Area");
                    listarea = areaService.GetAllCity(filter1);
                }
            }
            if (listarea == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<cityObj>();
            }
            IList<cityObj> listcity = Mapper.Map<IList<Area>, IList<cityObj>>(listarea);
            return listcity;

        }
    }
}
