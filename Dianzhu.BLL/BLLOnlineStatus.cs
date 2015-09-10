using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 客户/客服 在线状态管理
    /// </summary>
  public  class BLLOnlineStatus
    {
      public void Add(DZMembership customerService, DZMembership customer)
      { 
        
      }
      public void Delete(DZMembership customerService, DZMembership customer)
      { 
        
      }
      /// <summary>
      /// 获取一个客服的分配列表
      /// </summary>
      /// <param name="customerService"></param>
      public IList<DZMembership> GetAssign(DZMembership customerService)
      {

          throw new NotImplementedException();
      }
    }
  public interface IOnlineStatus
  {
      IList<DZMembership> GetOnlineCustomerServices();
  }
}
