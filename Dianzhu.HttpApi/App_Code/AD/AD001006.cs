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
            IList<Advertisement> adList = bllAD.GetADList();
            if (adList.Count > 0)
            {
                this.state_CODE = Dicts.StateCode[0];
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