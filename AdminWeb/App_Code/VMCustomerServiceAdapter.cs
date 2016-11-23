using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
/// <summary>
/// VMAgentAdapter 的摘要说明
/// </summary>
public class VMCustomerServiceAdapter
{
    ILog log = LogManager.GetLogger("Dianzhu.Web.AdminWeb.VMAgentAdapter");
    Dianzhu.BLL.BLLServiceOrder bllOrder;
    public VMCustomerServiceAdapter(Dianzhu.BLL.BLLServiceOrder bllOrder)
    {
        this.bllOrder = bllOrder;
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    string errMsg;
    public VMCustomerService Adapt(MemberDto agentUser)
    {
        if (agentUser.UserType != .enum_UserType.customerservice.ToString())
        {
            errMsg = "该用户不是助理";
            log.Error(errMsg);
            throw new Exception(errMsg);
        }
        VMCustomerService agent = new VMCustomerService();
        agent.City = agentUser.UserCity;
        agent.UserName = agentUser.UserName;
        agent.RealName = agentUser.NickName;
        

        return agent;
    }
    
}