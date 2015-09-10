using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
using Newtonsoft.Json;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 为用户分配客服ID
    /// </summary>
    public class ReceptionAssigner
    {
        public string Assign(DZMembership customer,List<DZMembership> customerServiceList )
        { 
            //1 获取当前在线的所有客服
            //2 通过算法 分配一个客服
            if (customerServiceList.Count == 0)
            {
                return string.Empty;
            }
            Random r = new Random(customerServiceList.Count);
            DZMembership selectedCS=customerServiceList[ r.Next()-1];


            return string.Empty;
            
        }
    }
}
