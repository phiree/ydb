using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Net;
using Dianzhu.Model.Enums;
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
            BLLAdvertisement bllAD = new BLLAdvertisement();
            int total;
            IList<Advertisement> adList = bllAD.GetADList(1, 10,out total);
            RespDataAD001006 respData = new RespDataAD001006();
            respData.AdapList(adList);
            if (adList.Count > 0)
            {
                this.state_CODE = Dicts.StateCode[0];
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
        this.time = ad.LastUpdateTime.ToString();

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