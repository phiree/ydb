using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class apiRequestChooser
{

}
/// <summary>
///apiRequest 的摘要说明
/// </summary>
 

public class ResponseFactory
{
    public static BaseResponse GetApiResponse(BaseRequest request)
    {
        switch (request.protocol_CODE.ToLower())
        {
            case "usm001001":
                return new ResponseUSM001001(request);
                break;
            case "usm001002":
                return new ResponseUSM001002(request);
                break;
            case "usm001003":
                return new ResponseUSM001003(request);
                break;
            case "usm001004":
                return new ResponseUSM001004(request);
                break;
            case "svm001001":
                return new ResponseSVM001001(request);
                break;
            case "svm001002":
                return new ResponseSVM001002(request);
                break;
            default: break;
        }
        throw new Exception("No Such Api");
    }
}

