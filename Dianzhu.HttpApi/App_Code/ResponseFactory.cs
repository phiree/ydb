using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

 

public class ResponseFactory
{
    public ResponseFactory()
    {  }
    public   BaseResponse GetApiResponse(BaseRequest request)
    {
        switch (request.protocol_CODE.ToLower())
        {



            case "ofp001001":
                return new ResponseOFP001001(request);

            case "chat001008":
                return new ResponseCHAT001008(request);
            default:
                BaseResponse baeResponse = new BaseResponse(request);
                baeResponse.state_CODE = Dicts.StateCode[1];
                baeResponse.err_Msg = "接口不存在！";
                return baeResponse;
        }
        throw new Exception("No Such Api");
    }
}

