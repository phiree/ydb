﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
using Newtonsoft.Json;

using Ydb.Common.Application;
using BD=Ydb.Common.Domain;
namespace Dianzhu.BLL
{
    public class AddressParser
    {
        public string rawAddressFromMapApi;
        IAreaService areaService;
        public AddressParser(string rawAddressFromMapApi,IAreaService areaService)
        {
            this.rawAddressFromMapApi = rawAddressFromMapApi;
            this.areaService = areaService;
        }
        //todo:refactor: 需要添加AreaDto.
        public void ParseAddress(out BD.Area area, out double latitude, out double longtitude)
        {

            //{"province":"海南省","city":"海口市","district":"秀英区","lat":110.190582,"lng":20.025103}
            
            Address_From_API address_from_api = Parse();
            if (address_from_api == null)
            {
                throw new Exception("地址格式有误.");
            }
            //利用字符串匹配 寻找店铺所在的行政区域
         area= areaService.GetAreaByAreaname(address_from_api.BuildWholeArea());
         latitude = address_from_api.lat;
         longtitude = address_from_api.lng;
        }
        protected Address_From_API Parse()
        {
            var address = JsonConvert.DeserializeObject<Address_From_API>(this.rawAddressFromMapApi);
            return address;
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
