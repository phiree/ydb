﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.Model;
using Dianzhu.BLL;
/// <summary>
/// VMCustomerListAdapter 的摘要说明
/// </summary>
public class VMCustomerAdapter
{
    Dianzhu.BLL.DZMembershipProvider bllMember = new Dianzhu.BLL.DZMembershipProvider();
    Dianzhu.BLL.BLLReceptionStatusArchieve bllRSA = new Dianzhu.BLL.BLLReceptionStatusArchieve();
    Dianzhu.BLL.BLLServiceOrder bllOrder = new Dianzhu.BLL.BLLServiceOrder();
    public VMCustomerAdapter()
    {
       
        
    }
    public VMCustomer  Adapt(Dianzhu.Model.DZMembership member)
    {
        VMCustomer vm = new VMCustomer();
        vm.CallTimes = bllRSA.GetReceptionAmount(member);
        vm.Email = member.Email;
        vm.FriendlyUserType = member.FriendlyUserType;
        vm.LoginTimes = member.LoginTimes;
        vm.OrderAmount = bllOrder.GetServiceOrderAmountWithoutDraft(member.Id, false);
        vm.OrderCount = bllOrder.GetServiceOrderCountWithoutDraft(member.Id, false);
        vm.TimeCreated = member.TimeCreated;
        vm.UserName = member.UserName;
        return vm;
    }
    public IList<VMCustomer> AdaptList(IList<Dianzhu.Model.DZMembership> memberlist)
    {
        IList<VMCustomer> vmList = new List<VMCustomer>();
        foreach (DZMembership m in memberlist)
        {
            VMCustomer vmc = Adapt(m);
            vmList.Add(vmc);

        }
        return vmList;
    }
}