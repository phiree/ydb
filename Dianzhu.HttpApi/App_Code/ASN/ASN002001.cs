using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using Newtonsoft.Json;

/// <summary>
/// 删除员工
/// </summary>
public class ResponseASN002001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN002001(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataASN002001 requestData = this.request.ReqData.ToObject<ReqDataASN002001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>(); BLLStaff bllStaff = new BLLStaff();

        BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();

        try
        {
            string merchant_id = requestData.merchantID;
            string store_id = requestData.storeID;
            IList<RespDataASN_assignObj> arrayData = requestData.arrayData;

            Guid merchantID, storeID;
            bool isMerchantId = Guid.TryParse(merchant_id, out merchantID);
            if (!isMerchantId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            bool isStoreId = Guid.TryParse(store_id, out storeID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "storeID格式有误";
                return;
            }

            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(merchantID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            try
            {
                Business store = bllBusiness.GetOne(storeID);
                if (store == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺不存在！";
                    return;
                }

                if (store.Owner.Id != merchantID)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "您没有该店铺！";
                    return;
                }

                Staff staff;
                ServiceOrder order;
                OrderAssignment oa;
                IList<RespDataASN_assignObj> arrayError = new List<RespDataASN_assignObj>();
                foreach (RespDataASN_assignObj obj in arrayData)
                {
                    staff = bllStaff.GetOne(new Guid(obj.userID));
                    if (staff != null)
                    {
                        order = bllServiceOrder.GetOne(new Guid(obj.orderID));
                        if (order != null)
                        {
                            oa = bllOrderAssignment.FindByOrderAndStaff(order, staff);
                            if (oa == null)
                            {
                                oa = new OrderAssignment();
                            }
                            oa.AssignedStaff = staff;
                            oa.Order = order;

                            switch (obj.mark)
                            {
                                case "Y":
                                    oa.Enabled = true;
                                    oa.AssignedTime = DateTime.Now;
                                    staff.IsAssigned = true;
                                    break;
                                case "N":
                                    oa.Enabled = false;
                                    oa.DeAssignedTime = DateTime.Now;
                                    staff.IsAssigned = false;
                                    break;
                                default:
                                    arrayError.Add(obj);
                                    continue;
                            }

                            bllOrderAssignment.SaveOrUpdate(oa);

                            bllStaff.SaveOrUpdate(staff);
                        }
                        else
                        {
                            arrayError.Add(obj);
                        }
                    }
                    else
                    {
                        arrayError.Add(obj);
                    }
                }

                RespDataASN002001 respData = new RespDataASN002001();
                respData.arrayError = arrayError;
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

    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}


