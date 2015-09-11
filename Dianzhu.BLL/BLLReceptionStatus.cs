using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
   public  class BLLReceptionStatus
    {
       DAL.DALReceptionStatus dalRS;
       public BLLReceptionStatus(DALReceptionStatus dalRs)
       {
           this.dalRS = dalRs;
       }
       public BLLReceptionStatus()
       {
           this.dalRS = DALFactory.DALReceptionStatus;
       }
       /// <summary>
       /// 客服登录
       /// </summary>
       /// <param name="customerService"></param>
       public void CustomerServiceLogin(DZMembership customerService)
       {
          IList<ReceptionStatus> existedRS= dalRS.GetListByCustomerService(customerService);
          if (existedRS.Count > 0)
          { return; }
           ReceptionStatus rs = new ReceptionStatus { CustomerService=customerService };
           dalRS.Save(rs);
       }
       /// <summary>
       /// 给客户分配一个客服,默认使用随机方式.
       /// </summary>
       /// <param name="customerUserName"></param>
       /// <returns></returns>
       
       public DZMembership Assign(string customerUserName)
       {
           return Assign(customerUserName, new AssignStratageRandom { dalRS=this.dalRS});
         
       }
       public DZMembership Assign(string customerUserName, AssignStratage assignStratage)
       {
           DZMembership assigned= assignStratage.Assign(customerUserName);
           //判断用户是否已经存在

           return assigned;
       }
    }
   public class AssignStratage
   {
       public DAL.DALReceptionStatus dalRS { get; set; }
       
        //在线客服列表
       IList<DZMembership> customerServiceList;
       protected IList<DZMembership> CustomerServiceList { get {
           if (customerServiceList == null)
           {
               customerServiceList = dalRS.GetAll<ReceptionStatus>().Select(x => x.CustomerService).ToArray();
           }
           return customerServiceList;
       } }
       protected int CustomerServiceCount { get { return CustomerServiceList.Count; } }
       
       public virtual DZMembership Assign(string customerUserName) {
           throw new MethodAccessException("不准直接调用,必须调用子类的重写方法.");
       
       }
        
   }
    /// <summary>
    /// 返回一个随机客服.
    /// </summary>
   public class AssignStratageRandom:AssignStratage
   {

       public override DZMembership Assign(string customerUserName)
       {
           if (CustomerServiceCount == 0)
           { 
            //r如果没有在线客服 怎么处理
               throw new Exception("客服离线");
           }
           Random r = new Random();
           int i = r.Next(CustomerServiceCount );
           return CustomerServiceList[i];

       }
   }
}
