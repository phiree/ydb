using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseORM001006 : BaseResponse
{
    public ResponseORM001006(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM001006 requestData = this.request.ReqData.ToObject<ReqDataORM001006>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        string raw_id = requestData.userID;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
            if (request.NeedAuthenticate)
            {
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[8];
                    this.err_Msg = "用户不存在,可能是传入的uid有误";
                    return;
                } 
            }
            //验证用户的密码
            if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.pWord, "MD5"))
            {
                this.state_CODE = Dicts.StateCode[9];
                this.err_Msg = "用户密码错误";
                return;
            }
            try
            {
                string srvTarget = requestData.target;
                string strPageSize = requestData.pageSize;
                string strPageNum = requestData.pageNum;//base on 1
                int pageSize, pageNum;
                if (!int.TryParse(strPageSize, out pageSize) || !int.TryParse(strPageNum, out pageNum))
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "分页大小或者分页索引不是数值格式";
                    return;
                }
                enum_OrderSearchType searchType = (enum_OrderSearchType)Enum.Parse(typeof(enum_OrderSearchType), srvTarget);

                IList<ServiceOrder> orderList = bllServiceOrder.GetServiceOrderList(uid, searchType, pageNum, pageSize);

                RespDataORM001006 respData = new RespDataORM001006();

                respData.AdapList(orderList);

                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];

            }
            catch (Exception ex)
            {
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
 


