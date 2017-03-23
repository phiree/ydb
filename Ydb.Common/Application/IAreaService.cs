using System.Collections.Generic;
using Ydb.Common.Domain;
using Ydb.Common.Specification;

namespace Ydb.Common.Application
{
    public interface IAreaService
    {
        IList<Area> GetAllCity(TraitFilter filter);
        IList<Area> GetArea(int areaid);
        Area GetAreaByAreaname(string areaname);
        IList<Area> GetAreaProvince();
        Area GetCityByAreaCode(string areacode);
        Area GetOne(int areaId);
        IList<Area> GetSubArea(string areacode);
       Area GetAreaByBaiduName(string areaname);
        void ParseAddress(string rawAddressFromMapApi, out Area area, out double latitude, out double longtitude);
        /// <summary>
        /// 写入百度地图api 对应的 名称和代码
        /// </summary>
        /// <param name="baiduCode"></param>
        /// <param name="baiduName"></param>
        ActionResult UpdateAreaWithBaiduMap(string baiduCode, string baiduName);

    }
}