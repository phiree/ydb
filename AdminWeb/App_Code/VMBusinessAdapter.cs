using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
/// <summary>
/// VMBusinessAdapter 的摘要说明
/// </summary>
public class VMBusinessAdapter
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.AdminWeb.VMBusinessAdapter");
    Dianzhu.BLL.BLLServiceOrder bllOrder;
    public VMBusinessAdapter(BLLServiceOrder bllOrder)
    {
        this.bllOrder = bllOrder;
    }
    string errMsg;
    public VMShop Adapt(Dianzhu.Model.Business business)
    {
        
        VMShop vmShop = new VMShop();
        vmShop.BusinessName = business.Name;
        vmShop.CityName = business.AreaBelongTo==null?string.Empty: business.AreaBelongTo.Name;
        vmShop.Score = 0;
        vmShop.ShopName = business.Name;

        vmShop.ServiceTypes = business.ServiceType.Select(x => x.Name).ToList();
        vmShop.RegisterTime = business.CreatedTime;

        
        vmShop.OrderCount = bllOrder.GetAllOrdersForBusiness(business.Id).Count;
        vmShop.OrderCompleteCount = bllOrder.GetAllCompleteOrdersForBusiness(business.Id).Count;
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