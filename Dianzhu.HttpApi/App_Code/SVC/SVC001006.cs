using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using FluentValidation.Results;

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseSVC001006 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSVC001006(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVC001006 requestData = this.request.ReqData.ToObject<ReqDataSVC001006>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Installer.Container.Resolve<BLLBusiness>();
        BLLDZService bllDZService = new BLLDZService();
        BLLDZTag bllDZTag = new BLLDZTag();

        try
        {
            string raw_id = requestData.merchantID;
            string store_id = requestData.storeID;

            Guid userID,storeID;
            bool isUserId = Guid.TryParse(raw_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            DZMembership member = null;
            if (request.NeedAuthenticate)
            {                
                bool validated = new Account(p).ValidateUser(userID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = p.GetUserById(userID);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该商户！";
                    return;
                }
            }
            try
            {
                bool isStoreId = Guid.TryParse(store_id, out storeID);
                if (!isStoreId)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "storeId格式有误";
                    return;
                }

                Business business = bllBusiness.GetBusinessByIdAndOwner(storeID, userID);
                if (business == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺不存在！";
                    return;
                }

                int total;
                IList<DZService> svcList = bllDZService.GetServiceByBusiness(business.Id, 0, 9999, out total);

                IList<RespDataSVC_svcObj> svcObjList = new List<RespDataSVC_svcObj>();
                IList< DZTag> tagList = new List< DZTag>();
                RespDataSVC_svcObj svcObj = new RespDataSVC_svcObj();
                foreach (DZService service in svcList)
                {
                    tagList = bllDZTag.GetTagForService(service.Id);
                    svcObj = new RespDataSVC_svcObj().Adapt(service,tagList);
                    svcObjList.Add(svcObj);
                }

                RespDataSVC001006 respData = new RespDataSVC001006();
                respData.arrayData = svcObjList;
                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData;
            }
            catch (Exception ex)
            {
                ilog.Error(ex.Message);
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


