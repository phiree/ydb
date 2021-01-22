using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dianzhu.Config;
using Ydb.Push.DomainModel;

namespace Dianzhu.Api.Model
{
    public class RequestDataAD001006
    {
        public string md5 { get; set; }
        public string viewType { get; set; }
    }

    public class RespDataADObj
    {
        public string imgUrl { get; set; }
        public string url { get; set; }
        public string num { get; set; }
        public string time { get; set; }
        public RespDataADObj Adap(Advertisement ad)
        {
            this.imgUrl = ad.ImgUrl != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + ad.ImgUrl : "";
            this.url = ad.Url;
            this.num = ad.Num.ToString();
            this.time = string.Format("{0:yyyyMMddHHmmss}", ad.LastUpdateTime);

            return this;
        }
    }

    public class RespDataAD001006
    {
        public IList<RespDataADObj> arrayData { get; set; }

        public RespDataAD001006()
        {
            arrayData = new List<RespDataADObj>();
        }

        public void AdapList(IList<Advertisement> adList)
        {
            foreach (Advertisement ad in adList)
            {
                RespDataADObj adaptedOrder = new RespDataADObj().Adap(ad);
                arrayData.Add(adaptedOrder);
            }

        }
    }
}
