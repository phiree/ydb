using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    /// <summary>
    /// bll对象生成. 和web不太一样, 完整的状态保持,
    /// </summary>
   public  class BLLFactory
    {
       public static BLL.DZMembershipProvider BLLMember = new BLL.DZMembershipProvider();
       public static BLL.BLLReception BLLReception = new BLL.BLLReception();
       public static BLL.BLLDZService BLLDZService = new BLL.BLLDZService();
       public static BLL.BLLServiceOrder BLLServiceOrder = new BLL.BLLServiceOrder();
       public static BLL.BLLReceptionStatus BLLRecetionStatus = new BLLReceptionStatus();

    }
}
