using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;

using Dianzhu.Model;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 标签
    /// </summary>
    public class BLLDZTag
    {
        //增加一个Tag
        public DZTag AddTag(string text, string serviceId, string businessId, string serviceTypeId)
        {
            DZTag dzTag = new DZTag();
            return dzTag;
        }
        //删除tag 
        public void DeleteTag(Guid tagId)
        { 
        
        }
        /// <summary>
        /// 获取一个服务的tag
        /// </summary>
        /// <param name="serviceId">服务ID</param>
        /// <returns></returns>
        public IList<DZTag> GetTagForService(Guid serviceId)
        {
            return null;
        }

    }
}
