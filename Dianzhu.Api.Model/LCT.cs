using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class ReqDataLCT001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string code { get; set; }
    }

    public class ReqDataLCT001008
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class RespDataLCT001008
    {
        public RespDataLCT001008_locationObj locationObj { get; set; }

    }
    public class RespDataLCT001008_locationObj
    {
        public string name { get; set; }
        public string key { get; set; }
        public string code { get; set; }
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
        public string streetNumber { get; set; }
        public string street { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string counntryCode { get; set; }
        public string direction { get; set; }
        public string distance { get; set; }
    }
}
