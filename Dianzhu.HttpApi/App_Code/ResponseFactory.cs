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
               
            case "usm001005":
                return new ResponseUSM001005(request);
                
            case "usm001003":
                return new ResponseUSM001003(request);
               
            case "usm001007":
                return new ResponseUSM001007(request);
                
            case "svm001001":
                return new ResponseSVM001001(request);
                
            case "svm001002":
                return new ResponseSVM001002(request);
                
            
            case "vcm001001":
                return new ResponseVCM001001(request);
            case "vcm001002":
                return new ResponseVCM001002(request);
            case "vcm001003":
                return new ResponseVCM001003(request);
            case "merm001001":
                return new ResponseMERM001001(request);
            case "orm001001":
                return new ResponseORM001001(request);
            case "orm001003":
                return new ResponseORM001003(request); 
            default: break;
        }
        throw new Exception("No Such Api");
    }
}

