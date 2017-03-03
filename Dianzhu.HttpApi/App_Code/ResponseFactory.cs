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

            case "app001001":
                return new ResponseAPP001001(request);
            case "orm002001":
                return new ResponseORM002001(request);
           
            case "usm001005":
                return new ResponseUSM001005(request);


            //
            case "wtm001006":
                return new ResponseWTM001006(request);
            case "wtm001005":
                return new ResponseWTM001005(request);
            case "wtm001004":
                return new ResponseWTM001004(request);
            case "wtm001003":
                return new ResponseWTM001003(request);
            case "wtm001002":
                return new ResponseWTM001002(request);
            case "wtm001001":
                return new ResponseWTM001001(request);

            case "shm001007":
                return new ResponseSHM001007(request);

            case "svc001003":
                return new ResponseSVC001003(request);

            

            case "svc001005":
                return new ResponseSVC001005(request);
 


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

