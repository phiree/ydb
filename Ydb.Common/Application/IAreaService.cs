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
    }
}