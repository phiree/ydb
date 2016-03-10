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
    protected override void BuildRespData()
    {
        ReqDataASN002001 requestData = this.request.ReqData.ToObject<ReqDataASN002001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();
        BLLStaff bllStaff = new BLLStaff();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();

        try
        {
            string store_id = requestData.merchantID;
            IList<RespDataASN_assignObj> arrayData = requestData.arrayData;

            Guid merchantID;
            bool isStoreId = Guid.TryParse(store_id, out merchantID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            Business store = bllBusiness.GetOne(merchantID);
            if (store == null)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "该店铺不存在！";
                return;
            }

            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(store.Owner.Id, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            try
            {
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
                            oa = bllOrderAssignment.FindByOrderAndStaff(order,staff);
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


