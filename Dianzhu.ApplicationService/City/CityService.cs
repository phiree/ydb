using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.City
{
    public class CityService: ICityService
    {
        BLL.BLLArea bllarea;
        public CityService(BLL.BLLArea bllarea)
        {
            this.bllarea = bllarea;
        }

        /// <summary>
        /// 根据areacode获得city
        /// </summary>
        /// <param name="areacode">code代码</param>
        /// <returns>area实体</returns>
        public cityObj GetCityByAreaCode(string areacode)
        {
            Model.Area area = bllarea.GetCityByAreaCode(areacode);
            cityObj cityobj = Mapper.Map<Model.Area, cityObj>(area);
            return cityobj;
        }

        /// <summary>
        /// 获得所有city,或根据经纬度与areacode获取城市
        /// </summary>
        /// <returns>area实体list</returns>
        public IList<cityObj> GetAllCity(common_Trait_Filtering filter,common_Trait_LocationFiltering location)
        {
            IList<Model.Area> listarea=null;
            Model.Area area;
            if (location.longitude != null && location.latitude != null && location.longitude != "" && location.latitude != "")
            {
                RespGeo geoObj = utils.Deserialize<RespGeo>(utils.GetCity(location.longitude, location.latitude));
                area = bllarea.GetAreaByAreaname(geoObj.result.addressComponent.province + geoObj.result.addressComponent.city);
                if (location.code != null && location.code != "" && area != null)
                {
                    if (location.code != area.Code)
                    {
                        area = null;
                    }
                }
                if (area != null)
                {
                    listarea = new List<Model.Area>();
                    listarea.Add(area);
                }
            }
            else
            {
                if (location.code != null && location.code != "")
                {
                    area = bllarea.GetCityByAreaCode(location.code);
                    if (area != null)
                    {
                        listarea = new List<Model.Area>();
                        listarea.Add(area);
                    }
                }
                else
                {
                    int intsize = 0;
                    int intnum = 0;
                    if (filter.pageSize != null && filter.pageNum != null)
                    {
                        try
                        {
                            intsize = int.Parse(filter.pageSize);
                            intnum = int.Parse(filter.pageNum);
                            if (intsize <= 0 || intnum < 1)
                            {
                                throw new Exception("分页参数pageSize,pageNum错误！");
                            }
                        }
                        catch
                        {
                            throw new Exception("分页参数pageSize,pageNum错误！");
                        }
                    }
                    listarea = bllarea.GetAllCity(intsize,intnum);
                }
            }
            if (listarea == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<cityObj> listcity = Mapper.Map<IList<Model.Area>, IList<cityObj>>(listarea);
            return listcity;

        }
    }
}
