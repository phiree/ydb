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

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseSTORE001005 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSTORE001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSTORE001005 requestData = this.request.ReqData.ToObject<ReqDataSTORE001005>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
        try
        {
            string store_id = requestData.storeID;

            Guid storeID;

            bool isStoreId = Guid.TryParse(store_id, out storeID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "storeId格式有误";
                return;
            }
            try
            {
                Business b = bllBusiness.GetOne(storeID);
                if (b == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺不存在！";
                    return;
                }

                RespDataSTORE_storeObj storeObj = new RespDataSTORE_storeObj().Adapt(b);

                RespDataSTORE001005 respData = new RespDataSTORE001005();
                respData.storeObj = storeObj;
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


