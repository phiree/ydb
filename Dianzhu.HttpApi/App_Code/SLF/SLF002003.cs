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

        string open_Id = requestData.openTimeForDayId;

        Guid openId;

        bool isOpenId = Guid.TryParse(open_Id, out openId);
        if (!isOpenId)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "openTimeForDayId格式有误";
            return;
        }

        try
        {
            ReqDataSLF002003_PostData postData = requestData.postData;

            ServiceOpenTimeForDay item = bllOpentime.GetOne(openId);
            if (item == null)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "服务时间不存在！";
                return;
            }

            item.Enabled = postData.timeEnable;
            item.MaxOrderForOpenTime = postData.maxNum;

            bllOpentime.Update(item);
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

 