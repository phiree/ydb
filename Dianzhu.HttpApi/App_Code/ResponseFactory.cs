﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

 

public class ResponseFactory
{
    public static BaseResponse GetApiResponse(BaseRequest request)
    {
        switch (request.protocol_CODE.ToLower())
        {
            case "usm001001":
                return new ResponseUSM001001(request);
               
            case "usm001005":
                return new ResponseUSM001005(request);
                
            case "usm001003":
                return new ResponseUSM001003(request);
               
            case "usm001007":
                return new ResponseUSM001007(request);
            case "usm001008":
                return new ResponseUSM001008(request);

            case "svc001001":
                return new ResponseSVC001001(request);
 
            case "vcm001001":
                return new ResponseVCM001001(request);
            case "vcm001002":
                return new ResponseVCM001002(request);
            case "vcm001003":
                return new ResponseVCM001003(request);
            case "merm001001":
                return new ResponseMERM001001(request);
            case "merm001003":
                return new ResponseMERM001003(request);
            case "merm001005":
                return new ResponseMERM001005(request);

            case "orm001004":
                return new ResponseORM001004(request);
            case "orm001006":
                return new ResponseORM001006(request);
            case "orm001005":
                return new ResponseORM001005(request);

            case "orm002001":
                return new ResponseORM002001(request);
            case "orm002002":
                return new ResponseORM002002(request);
            case "chat001004":
                return new ResponseCHAT001004(request);
            case "chat001006":
                return new ResponseCHAT001006(request);
            case "chat001007":
                return new ResponseCHAT001007(request);

            case "lct001007":
                return new ResponseLCT001007(request);
            case "lct001008":
                return new ResponseLCT001008(request);

            case "sys001001":
                return new ResponseSYS001001(request);

            case "app001001":
                return new ResponseAPP001001(request);
            case "app001002":
                return new ResponseAPP001002(request);

            case "ofp001001":
                return new ResponseOFP001001(request);

            default:
                BaseResponse baeResponse = new BaseResponse(request);
                baeResponse.state_CODE = Dicts.StateCode[1];
                baeResponse.err_Msg = "接口不存在！";
                return baeResponse;
        }
        throw new Exception("No Such Api");
    }
}

