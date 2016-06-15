using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Cryptography;

namespace Dianzhu.ApplicationService.ADs
{
    public class ADsService : IADsService
    {
        BLL.BLLAdvertisement bllad;
        public ADsService(BLL.BLLAdvertisement bllad)
        {
            this.bllad = bllad;
        }

        /// <summary>
        /// 条件读取广告
        /// </summary>
        /// <returns>area实体list</returns>
        public IList<adObj> GetADs(common_Trait_AdFiltering adf)
        {
            IList<Model.Advertisement> listad = null;
            listad = bllad.GetADListForUseful();
            if (listad == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            if (adf.md5 == null || adf.md5 == "")
            {
                throw new Exception("必须传入参数md5！");
            }
            if (listad.Count > 0)
            {
                string datetimeStr = "";
                foreach (Model.Advertisement ad in listad)
                {
                    datetimeStr += ad.LastUpdateTime.ToString("yyyyMMddHHmmss") + " ";
                }
                datetimeStr = datetimeStr.TrimEnd(' ');

                //转为MD5
                string datetimeMd5 = "";
                MD5 md5Obj = MD5.Create();
                byte[] d = md5Obj.ComputeHash(Encoding.GetEncoding("Utf-8").GetBytes(datetimeStr));
                for (int i = 0; i < d.Length; i++)
                {
                    datetimeMd5 += d[i].ToString("x2");// 2 表示保留2为数字
                }
                if (datetimeMd5 == adf.md5.ToLower())
                {
                    return new List<adObj>();
                }
            }
            IList<adObj> adobj = Mapper.Map<IList<Model.Advertisement>, IList<adObj>>(listad);
            return adobj;
        }
    }
}
