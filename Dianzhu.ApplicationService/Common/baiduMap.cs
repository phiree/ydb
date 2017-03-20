using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    class baiduMap
    {
    }
    /// <summary>
    /// 坐标转换后的josn数据转为对象
    /// </summary>
    public class RespTran
    {
        public int status { get; set; }
        public IList<RespXY> result { get; set; }
    }
    public class RespXY
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    /// <summary>
    /// 经纬度上传百度地图API返回数据对象
    /// </summary>
    public class RespGeo
    {
        public int status { get; set; }
        public GeoResult result { get; set; }
    }
    public class GeoResult
    {
        public GeoLocation location { get; set; }
        public string formatted_address { get; set; }
        public string business { get; set; }
        public GeoAddressComponent addressComponent { get; set; }
        public string cityCode { get; set; }
        public IList<Object> poiRegions { get; set; }
        public string semtic_description { get; set; }
    }
    public class GeoLocation
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }
    public class GeoAddressComponent
    {
        public string street_number { get; set; }
        public string street { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string counntryCode { get; set; }
        public string direction { get; set; }
        public string distance { get; set; }
        public string adcode { get; set; }
    }
}
