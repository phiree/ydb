using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;

using Ydb.Common.Domain;
using Ydb.Common;
using Ydb.Order.DomainModel;
using Ydb.Order.Application;
/// <summary>
/// VMCustomerListAdapter 的摘要说明
/// </summary>
public class VMCustomerAdapter
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.AdminWeb.VMCustomerAdapter");
   IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

    //  Dianzhu.BLL.BLLServiceOrder bllOrder = new Dianzhu.BLL.BLLServiceOrder();
    IServiceOrderService orderService;
    public VMCustomerAdapter(IServiceOrderService orderService)
    {
        this.orderService = orderService;        
    }
    string errMsg;
    public VMCustomer  Adapt(MemberDto member)
    {
        if (member.UserType !=  enum_UserType.customer.ToString())
        {
            errMsg = "错误,该用户类型不是'客户'";
            log.Error(errMsg);
            throw new Exception(errMsg);
        }
        VMCustomer vm = new VMCustomer();
        //vm.CallTimes =dalRSA.GetReceptionAmount(member);
        vm.Email = member.Email;
        vm.FriendlyUserType = member.FriendlyUserType;
        vm.LoginTimes = member.LoginTimes;
        vm.OrderAmount = orderService.GetServiceOrderAmountWithoutDraft(member.Id, false);
        vm.OrderCount = orderService.GetServiceOrderCountWithoutDraft(member.Id, false);
        vm.TimeCreated = member.TimeCreated;
        //vm.LoginDates = dalRSA.GetReceptionDates(member);
        vm.UserName = member.UserName;
        return vm;
    }
    public IList<VMCustomer> AdaptList(IList<MemberDto> memberlist)
    {
        IList<VMCustomer> vmList = new List<VMCustomer>();
        foreach (MemberDto m in memberlist)
        {
            VMCustomer vmc = Adapt(m);
            vmList.Add(vmc);

        }
        return vmList;
    }
}