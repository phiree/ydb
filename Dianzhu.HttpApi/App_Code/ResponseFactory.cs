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
               
            case "usm001002":
                return new ResponseUSM001002(request);
                
            case "usm001003":
                return new ResponseUSM001003(request);
               
            case "usm001004":
                return new ResponseUSM001004(request);
                
            case "svm001001":
                return new ResponseSVM001001(request);
                
            case "svm001002":
                return new ResponseSVM001002(request);
                
            case "svm001003":
                return new ResponseSVM001003(request);
            case "vcm001001":
                return new ResponseVCM001001(request);
            case "vcm001002":
                return new ResponseVCM001002(request);     
            default: break;
        }
        throw new Exception("No Such Api");
    }
}

