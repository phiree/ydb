using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common;

using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseSVC001005 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Ydb.HttpApi");

    public ResponseSVC001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVC001005 requestData = this.request.ReqData.ToObject<ReqDataSVC001005>();

        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

        IDZServiceService dzServiceService = Bootstrap.Container.Resolve<IDZServiceService>();

        IDZTagService tagService = Bootstrap.Container.Resolve<IDZTagService>();
        
        try
        {
            string svd_id = requestData.svcID;

            Guid svcID;

            bool isStoreId = Guid.TryParse(svd_id, out svcID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "svcId格式有误";
                return;
            }
            try
            {
                DZService service = dzServiceService.GetOne2(svcID);

                if (service == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该服务不存在！";
                    return;
                }

                if (service.IsDeleted)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该服务已删除！";
                    return;
                }

                IList<DZTag> tagsList = tagService.GetTagForService(svcID);

                RespDataSVC_svcObj SvcObj = new RespDataSVC_svcObj().Adapt(service, tagsList);

                RespDataSVC001005 respData = new RespDataSVC001005();
                respData.svcObj = SvcObj;
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


