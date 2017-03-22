﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Ydb.Common.Repository;
using Ydb.Common.Domain;
using Newtonsoft.Json;
namespace Ydb.Common.Application
{
    public class AreaService : IAreaService
    {


        //暴露 数据库实现,用于单元测试mock
        public IRepositoryArea  repoArea;
        
      
        public AreaService(IRepositoryArea repoArea )  {
            this.repoArea = repoArea;
           
        }
 
        /// <summary>
        /// 获取省份下的市,
        /// </summary>
        /// <param name="areaid">6位数中的前两位,如浙江的33</param>
        /// <returns>330000,330100,330200------331100</returns>
       
        public IList< Area> GetArea(int areaid) 
        {
            
         // iuw.BeginTransaction();
            Expression<Func<Area, bool>> where = i => i.Id == areaid;
        //    iuw.Commit();
            return repoArea.Find(where).ToList();
        }

        /// <summary>
        /// 获取直接下级地区.
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        public IList<Area> GetSubArea(string areacode)
        {
            string sql = "";
            //开始2位编号
            string bCode = areacode.Substring(0, 2);
            //中间2位编号
            string mCode = areacode.Substring(2, 2);
            //最后2位编号
            string lCode = areacode.Substring(4, 2);
 
            //城市
            Expression<Func<Area, bool>> where = i=>true ;
            if (mCode == "00")
            {
                //查找市级区域单位
                where = i => i.Code.StartsWith(bCode); //&& i.Code.Substring(2, 2) != "00" && i.Code.EndsWith("00");
                //注释后面的是为了获取所有的子区域

            }
            else if (lCode == "00")
            {
                //查找市内区、县级区域单位(并排除市和辖区)
                where = i => i.Code.StartsWith(bCode + mCode);//&& !i.Code.EndsWith("00");
                //注释后面的是为了获取所有的子区域
            }
            else
            {
                //Func<Area, bool> where2 = i => i.Code == "" && i.Code == "dd";
                where = i => i.Code == areacode;//.StartsWith(bCode + mCode +lCode);
            }
         //  iuw.BeginTransaction();
            var result= repoArea.Find(where).ToList();
       //   iuw.Commit();
            return result;
        }


        /// <summary>
        /// 根据areaname获得area
        /// </summary>
        /// <param name="areaname">area名称</param>
        /// <returns>area实体</returns>
        public Area GetAreaByAreaname(string areaname)
        {
            if (string.IsNullOrEmpty(areaname))
            {
                return null;
            }
            //byte[] srcarr = Encoding.Default.GetBytes(areaname);
            //byte[] desarr = Encoding.Convert(Encoding.Default, Encoding.UTF8, srcarr);
            //string s = Encoding.UTF8.GetString(desarr, 0, desarr.Length);
            Expression<Func<Area, bool>> where = i => i.Name == areaname;
         //   iuw.BeginTransaction();
           
            var list= repoArea.FindOne(where);
            //     iuw.Commit();
            //return null;
            return list;

        }

        /// <summary>
        /// 根据areacode获得city
        /// </summary>
        /// <param name="areacode">code代码</param>
        /// <returns>area实体</returns>
        public Area GetCityByAreaCode(string areacode)
        {
            if (string.IsNullOrEmpty(areacode))
            {
                return null;
            }
            Expression<Func<Area, bool>> where = i => (i.Code == areacode && !i.Code.EndsWith("0000") && i.Code.EndsWith("00"))|| i.BaiduCode== areacode;
            var list = repoArea.FindOne(where);
            return list;

        }

        /// <summary>
        /// 获得所有city
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<Area> GetAllCity(Ydb.Common.Specification.TraitFilter filter)
        {
            Expression<Func<Area, bool>> where = i =>  !i.Code.EndsWith("0000") && i.Code.EndsWith("00");

            Area baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repoArea.FindByBaseId(int.Parse(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize==0?repoArea.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList(): repoArea.Find(where,filter.pageNum,filter.pageSize,out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList(); 
            return list;

        }

        /// <summary>
        /// 获取所有的省份
        /// </summary>
        /// <returns></returns>
        public IList<Area> GetAreaProvince()
        {
         //  iuw.BeginTransaction();
            Expression<Func<Area, bool>> where = i => i.Code.EndsWith("0000");
            var result= repoArea.Find(where).ToList();
         //   iuw.Commit();
            return result;
        }

        public Area GetOne(int areaId)
        {
           //  iuw.BeginTransaction();
            var area = repoArea.FindById(areaId);
          //  iuw.Commit();
            return area;
        }

        /// <summary>
        /// 根据areaname获得area
        /// </summary>
        /// <param name="areaname">area名称</param>
        /// <returns>area实体</returns>
        public  Area GetAreaByBaiduName(string areaname)
        {
            if (string.IsNullOrEmpty(areaname))
            {
                return null;
            }
            //byte[] srcarr = Encoding.Default.GetBytes(areaname);
            //byte[] desarr = Encoding.Convert(Encoding.Default, Encoding.UTF8, srcarr);
            //string s = Encoding.UTF8.GetString(desarr, 0, desarr.Length);
            Expression<Func< Area, bool>> where = i => i.Name.EndsWith(areaname + "市");
            //   iuw.BeginTransaction();

            var list = repoArea.FindOne(where);

            if (list == null)
            {
                where = i => i.Name.EndsWith(areaname + "藏族羌族自治州") || i.Name.EndsWith(areaname + "盟") || i.Name.EndsWith(areaname + "地区")
                            || i.Name.EndsWith(areaname + "傣族景颇族自治州") || i.Name.EndsWith(areaname + "藏族自治州")
                            || i.Name.EndsWith(areaname + "蒙古自治州") || i.Name.EndsWith(areaname + "蒙古族藏族自治州")
                            || i.Name.EndsWith(areaname + "哈尼族彝族自治州") || i.Name.EndsWith(areaname + "自治州")
                            || i.Name.EndsWith(areaname + "彝族自治州") || i.Name.EndsWith(areaname + "傈僳族自治州")
                            || i.Name.EndsWith(areaname + "苗族侗族自治州") || i.Name.EndsWith(areaname + "布依族苗族自治州")
                            || i.Name.EndsWith(areaname + "壮族苗族自治州") || i.Name.EndsWith(areaname + "土家族苗族自治州")
                            || i.Name.EndsWith(areaname + "傣族自治州") || i.Name.EndsWith(areaname + "维吾尔自治区")
                            || i.Name.EndsWith(areaname + "朝鲜族自治州") || i.Name.EndsWith(areaname + "特别行政区");
                list = repoArea.FindOne(where);
            }

            //     iuw.Commit();
            //return null;
            return list;

        }
      
        public ActionResult  UpdateAreaWithBaiduMap( string baiduCode, string baiduName)
        {
            ActionResult result = new ActionResult();
            Area area = GetAreaByBaiduName(baiduName);
            if (area != null)
            {
                area.BaiduName = baiduName;
                area.BaiduCode = baiduCode;
                //支撑域没有用application层的unitofwork.
                repoArea.Update(area);
            }
            else
            {
                result.IsSuccess = false;
                result.ErrMsg = "没有找到对应区域:" + baiduName;
            }
            return result;

        }

        public void ParseAddress(string rawAddressFromMapApi, out  Area area, out double latitude, out double longtitude)
        {

            //{"province":"海南省","city":"海口市","district":"秀英区","lat":110.190582,"lng":20.025103}

            Address_From_API address_from_api = JsonConvert.DeserializeObject<Address_From_API>(rawAddressFromMapApi);
            if (address_from_api == null)
            {
                throw new Exception("地址格式有误.");
            }
            //利用字符串匹配 寻找店铺所在的行政区域
            area = 　GetAreaByAreaname(address_from_api.BuildWholeArea());
            latitude = address_from_api.lat;
            longtitude = address_from_api.lng;
        }
      　
        public class Address_From_API
        {
            public string province { get; set; }
            public string city { get; set; }
            public string district { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public string BuildWholeArea()
            {
                if (province == city)
                {
                    return city + district;
                }
                else
                {
                    return province + city + district;
                }
            }
        }
    }
}
