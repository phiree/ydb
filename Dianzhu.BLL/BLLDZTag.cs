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
        DALDZTag dalTag =null;// DALFactory.DALDZTag;

        //增加一个Tag
        public BLLDZTag(DAL.DALDZTag dal)
        {
            dalTag = dal;
        }
        public BLLDZTag():this(DALFactory.DALDZTag)
        { 
        
        }
        
        public DZTag AddTag(string text, string serviceId, string businessId, string serviceTypeId)
        {
            DZTag dzTag = new DZTag();
            dzTag.ForPK = serviceId;
            dzTag.ForPK2 = serviceTypeId;
            dzTag.ForPK3 = businessId;
            dzTag.Text = text;
            dzTag.OriginalText = text;
            dzTag.CreateDate = DateTime.Now;
            dalTag.Add(dzTag);
            return dzTag;
        }
        //删除tag 
        public void DeleteTag(Guid tagId)
        {
            DZTag tag = dalTag.FindById(tagId);
            dalTag.Delete(tag);
        }
        /// <summary>
        /// 获取一个服务的tag
        /// </summary>
        /// <param name="serviceId">服务ID</param>
        /// <returns></returns>
        public IList<DZTag> GetTagForService(Guid serviceId)
        {
          return  dalTag.GetTagsForService(serviceId);
        }

        public void DeleteByServiceId(Guid serviceId)
        {
            IList<DZTag> tagList = dalTag.GetTagsForService(serviceId);
            foreach (DZTag tag in tagList)
            {
                dalTag.Delete(tag);
            }
        }
    }
}
