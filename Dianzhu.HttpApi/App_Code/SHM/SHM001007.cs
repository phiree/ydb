using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseSHM001007 : BaseResponse
{
    public ResponseSHM001007(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataSHM001007 requestData = this.request.ReqData.ToObject<ReqDataSHM001007>();

        bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        //todo:用户验证的复用
        BLLDZTag bllDZTag = Bootstrap.Container.Resolve<BLLDZTag>();

        string start_Time = requestData.stratTime;
        string end_Time = requestData.endTime;
        string type_Str = requestData.type;

        try
        {
            DateTime startTime, endTime;
            if (!DateTime.TryParse(start_Time, out startTime))
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "startTime不是正确的日期格式!";
                return;
            }

            if (!DateTime.TryParse(end_Time, out endTime))
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "endTime不是正确的日期格式!";
                return;
            }

            if (startTime < endTime)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "startTime不得小于endTime!";
                return;
            }

            try
            {
                string[] typeList = type_Str.Split('|');
                if (typeList.Count() > 0)
                {
                    for(int i = 0; i < typeList.Count(); i++)
                    {
                        switch (typeList[i])
                        {
                            case "maxOrder":

                                break;
                            case "workTime":

                                break;
                            case "order":

                                break;
                        }
                    }
                }

               

                RespDataSHM001007 respData = new RespDataSHM001007();

                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];

            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }

    }

}
 


