using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// bll对象生成. 和web不太一样, 完整的状态保持,
    /// </summary>
   public  class BLLFactory
    {
       public static BLLReception BLLReception = new BLLReception();
       public static DZMembershipProvider BLLMembership = new DZMembershipProvider();

    }
}
