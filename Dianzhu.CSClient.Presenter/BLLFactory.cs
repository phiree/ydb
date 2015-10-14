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
        private static BLL.DZMembershipProvider bLLMember;
        public BLL.DZMembershipProvider BLLMember
        {
            get
            {
                if (bLLMember == null) bLLMember = new DZMembershipProvider();
                return bLLMember;
            }
            set {
                
                bLLMember = value; }
        }
        

    }
}
