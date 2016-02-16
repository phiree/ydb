using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Net;
using Dianzhu.Model.Enums;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// Summary description for AD001006
/// </summary>
public class ResponseAD001006:BaseResponse
{
    BLLDeviceBind bllDeviceBind;

    public ResponseAD001006(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        try
        {
            RequestDataAD001006 requestData = this.request.ReqData.ToObject<RequestDataAD001006>();

            BLLAdvertisement bllAD = new BLLAdvertisement();
            IList<Advertisement> adList = bllAD.GetADListForUseful();            
            if (adList.Count > 0)
            {
                string datetimeStr = "";
                foreach(Advertisement ad in adList)
                {
                    datetimeStr += ad.LastUpdateTime.ToString("yyyyMMddHHmmss") + " ";
                }
                datetimeStr = datetimeStr.TrimEnd(' ');

                //转为MD5
                string datetimeMd5 = "";
                MD5 md5Obj = MD5.Create();
                byte[] d = md5Obj.ComputeHash(Encoding.GetEncoding("Utf-8").GetBytes(datetimeStr));
                for(int i = 0; i < d.Length; i++)
                {
                    datetimeMd5 += d[i].ToString("x2");// 2 表示保留2为数字
                }

                if (datetimeMd5 == requestData.md5.ToLower())
                {
                    this.state_CODE = Dicts.StateCode[0];
                    return;
                }

                this.state_CODE = Dicts.StateCode[0];
                RespDataAD001006 respData = new RespDataAD001006();
                respData.AdapList(adList);
                this.RespData = respData;
                return;
            }
            this.state_CODE = Dicts.StateCode[4];
            this.err_Msg = "当前没有广告！";
            return;
        }
        catch (Exception ex)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = ex.Message;
            return;
        }
    }
}

public class RequestDataAD001006
{
    public string md5 { get; set; }
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
            RespDataADObj adapted_order = new RespDataADObj().Adap(ad);
            arrayData.Add(adapted_order);
        }

    }
}