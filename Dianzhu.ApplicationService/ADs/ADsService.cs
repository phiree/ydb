using AutoMapper;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Ydb.Push.Application;
using Ydb.Push.DomainModel;

namespace Dianzhu.ApplicationService.ADs
{
    public class ADsService : IADsService
    {
        private IAdvertisementService advService;

        public ADsService(IAdvertisementService advService)
        {
            this.advService = advService;
        }

        /// <summary>
        /// 条件读取广告
        /// </summary>
        /// <param name="adf"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<adObj> GetADs(common_Trait_AdFiltering adf, Customer customer)
        {
            IList<Advertisement> listad = null;
            listad = advService.GetADListForUseful(customer.UserType);
            //if (listad == null)
            //{
            //    throw new Exception(Dicts.StateCode[4]);
            //}

            if (listad.Count > 0)
            {
                string datetimeStr = "";
                foreach (Advertisement ad in listad)
                {
                    datetimeStr += ad.LastUpdateTime.ToString("yyyyMMddHHmmss") + " ";
                }
                datetimeStr = datetimeStr.TrimEnd(' ');

                if (adf.md5 != null && adf.md5 != "")
                {
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
            }
            IList<adObj> adobj = Mapper.Map<IList<Advertisement>, IList<adObj>>(listad);
            return adobj;
        }
    }
}