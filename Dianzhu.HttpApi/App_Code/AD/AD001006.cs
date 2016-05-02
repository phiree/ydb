using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Net;
using Dianzhu.Model.Enums;
using System.Security.Cryptography;
using System.Text;
using Castle.Windsor;
using Dianzhu.Api.Model;
/// <summary>
/// Summary description for AD001006
/// </summary>
public class ResponseAD001006:BaseResponse
{
    BLLDeviceBind bllDeviceBind;

    public ResponseAD001006(BaseRequest request):base(request)
    {
 
    }
    protected override void BuildRespData()
    {
        try
        {
            RequestDataAD001006 requestData = this.request.ReqData.ToObject<RequestDataAD001006>();

            BLLAdvertisement bllAD = Installer.Container.Resolve<BLLAdvertisement>();  // new BLLAdvertisement();
            IEnumerable<Advertisement> adList = bllAD.GetADListForUseful();            
            if (adList.Count()> 0)
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
                respData.AdapList(adList.ToList());
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

 