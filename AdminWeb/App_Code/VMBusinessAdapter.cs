using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.BusinessResource.DomainModel;
using Ydb.Order.DomainModel;
using Ydb.Order.Application;
/// <summary>
/// VMBusinessAdapter 的摘要说明
/// </summary>
public class VMBusinessAdapter
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.AdminWeb.VMBusinessAdapter");
    //  Dianzhu.BLL.IBLLServiceOrder bllOrder;
    IServiceOrderService orderService;
    public VMBusinessAdapter(IServiceOrderService orderService)
    {
        this.orderService = orderService;
    }
    string errMsg;
    public VMShop Adapt( Business business)
    {
        VMShop vmShop = new VMShop();
        vmShop.BusinessName = business.Name;
        vmShop.CityName = business.AreaBelongTo==null?string.Empty: business.AreaBelongTo.Name;
        vmShop.Score = 0;
        vmShop.ShopName = business.Name;

        vmShop.ServiceTypes = business.ServiceType.Select(x => x.Name).ToList();
        vmShop.RegisterTime = business.CreatedTime;
        
        vmShop.OrderCount = orderService.GetAllOrdersForBusiness(business.Id).Count;
        vmShop.OrderCompleteCount = orderService.GetAllCompleteOrdersForBusiness(business.Id).Count;
        //vmShop.OrderCompleteRate = null;
        //vmShop.OrderAmount = bllOrder.GetServiceOrderAmountWithoutDraft(busines)
        //vmShop.ExposureRate = null;

        //vmShop.CustomerCancelRate = null;
        return vmShop;
    }
    public IList<VMShop> AdaptList(IList<Business> businessList)
    {
        IList<VMShop> shopList = new List<VMShop>();
        foreach (Business b in businessList)
        {
            VMShop vm = Adapt(b);
            shopList.Add(vm);
        }
        return shopList;
    }
}