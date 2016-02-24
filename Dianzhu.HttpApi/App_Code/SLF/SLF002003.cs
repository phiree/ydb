using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
/// <summary>
/// 获取某一个服务  一周7天的 简要信息
/// </summary>
public class ResponseSLF002003:BaseResponse
{
    
    public ResponseSLF002003(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataSLF002003 requestData = this.request.ReqData.ToObject<ReqDataSLF002003>();
         
        //todo: 使用 ninject,注入依赖.
        BLLServiceOpenTimeForDay bllOpentime = new BLLServiceOpenTimeForDay();
 
        try
        {
            ReqDataSLF002003_PostData postData = requestData.postData;
 
            bllOpentime.Update(new Guid(requestData.openTimeForDayId),
                                postData.maxNum,
                                postData.timeEnable);
            this.state_CODE = Dicts.StateCode[0];
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

 