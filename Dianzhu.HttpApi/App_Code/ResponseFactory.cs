using System;
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
    IIMSession imSession;
    public ResponseFactory(IIMSession imSession)
    { this.imSession = imSession; }
    public   BaseResponse GetApiResponse(BaseRequest request)
    {
        switch (request.protocol_CODE.ToLower())
        {
            #region usm
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
            case "usm001009":
                return new ResponseUSM001009(request);
            case "usm001010":
                return new ResponseUSM001010(request);
            #endregion

 

            #region merm
            case "merm001001":
                return new ResponseMERM001001(request);
            case "merm001003":
                return new ResponseMERM001003(request);
            case "merm001005":
                return new ResponseMERM001005(request);
            #endregion

            #region orm
            case "orm001004":
                return new ResponseORM001004(request);
            case "orm001006":
                return new ResponseORM001006(request);
            case "orm001005":
                return new ResponseORM001005(request);
            case "orm001007":
                return new ResponseORM001007(request);
            case "orm001008":
                return new ResponseORM001008(request);
            case "orm002001":
                return new ResponseORM002001(request);
            case "orm002003":
                  return new ResponseORM002003(request,imSession);
              //  return  Installer.Container. Resolve<ResponseORM002003>();
            case "orm003005":
                return new ResponseORM003005(request);
            case "orm003006":
                return new ResponseORM003006(request);
            case "orm003007":
                return new ResponseORM003007(request);
            case "orm003008":
                return new ResponseORM003008(request);
            case "orm003009":
                return new ResponseORM003009(request);
            case "orm003010":
                return new ResponseORM003010(request);
            case "orm005001":
                return new ResponseORM005001(request);
              //  return Bootstrap.Container.Resolve<ResponseORM005001>();
            case "orm005007":
                return new ResponseORM005007(request);
            //case "orm005008":
            //    return new ResponseORM005008(request);
            //case "orm005009":
            //    return new ResponseORM005009(request);
            //case "orm005010":
            //    return new ResponseORM005010(request);
            #endregion

            #region chat
            case "chat001004":
                return new ResponseCHAT001004(request);
            case "chat001006":
                return new ResponseCHAT001006(request);
            case "chat001007":
                return new ResponseCHAT001007(request);
            #endregion

            #region lct
            case "lct001007":
                return new ResponseLCT001007(request);
            case "lct001008":
                return new ResponseLCT001008(request);
            #endregion
                
            #region app
            case "app001001":
                return new ResponseAPP001001(request);
            case "app001002":
                return new ResponseAPP001002(request);
            #endregion

            #region slf
            case "slf001007":
                return new ResponseSLF001007(request);
            case "slf002003":
                return new ResponseSLF002003(request);
            case "slf002006":
                return new ResponseSLF002006(request);
            #endregion
                
            #region py
            case "py001007":
                return new ResponsePY001007(request);
            case "py001008":
                return new ResponsePY001008(request);
            #endregion
                
            #region asn
            case "asn001001":
                return new ResponseASN001001(request);
            case "asn001002":
                return new ResponseASN001002(request);
            case "asn001003":
                return new ResponseASN001003(request);
            case "asn001004":
                return new ResponseASN001004(request);
            case "asn001005":
                return new ResponseASN001005(request);
            case "asn001006":
                return new ResponseASN001006(request);
            case "asn002001":
                return new ResponseASN002001(request);
            case "asn002004":
                return new ResponseASN002004(request);
            #endregion

            #region store
            case "store001001":
                return new ResponseSTORE001001(request);
            case "store001002":
                return new ResponseSTORE001002(request);
            case "store001003":
                return new ResponseSTORE001003(request);
            case "store001004":
                return new ResponseSTORE001004(request);
            case "store001005":
                return new ResponseSTORE001005(request);
            case "store001006":
                return new ResponseSTORE001006(request);
            case "store002001":
                return new ResponseSTORE002001(request);
            case "store002002":
                return new ResponseSTORE002002(request);
            case "store002003":
                return new ResponseSTORE002003(request);
            case "store002004":
                return new ResponseSTORE002004(request);
            #endregion

            #region svc
            case "svc001001":
                return new ResponseSVC001001(request);
            case "svc001002":
                return new ResponseSVC001002(request);
            case "svc001003":
                return new ResponseSVC001003(request);
            case "svc001004":
                return new ResponseSVC001004(request);
            case "svc001005":
                return new ResponseSVC001005(request);
            case "svc001006":
                return new ResponseSVC001006(request);
            #endregion

            #region wtm
            case "wtm001001":
                return new ResponseWTM001001(request);
            case "wtm001002":
                return new ResponseWTM001002(request);
            case "wtm001003":
                return new ResponseWTM001003(request);
            case "wtm001004":
                return new ResponseWTM001004(request);
            case "wtm001005":
                return new ResponseWTM001005(request);
            case "wtm001006":
                return new ResponseWTM001006(request);
            #endregion

            #region rmm
            case "rmm001003":
                return new ResponseRMM001003(request);
            case "rmm001004":
                return new ResponseRMM001004(request);
            case "rmm001005":
                return new ResponseRMM001005(request);
            case "rmm001006":
                return new ResponseRMM001006(request);
            #endregion

            case "sys001001":
                return new ResponseSYS001001(request);
            case "shm001007":
                return new ResponseSHM001007(request);
            case "ofp001001":
                return new ResponseOFP001001(request);
            case "ad001006":
                return new ResponseAD001006(request);
            case "u3rd014008":
                return new ResponseU3RD014008(request);
            case "clm001001":
                return new ResponseCLM001001(request);
            case "drm001006":
                return new ResponseDRM001006(request);
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

