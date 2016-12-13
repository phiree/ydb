using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
namespace Ydb.BusinessResource.Application
{
    /// <summary>
    /// 标签
    /// </summary>
    public class DZTagService : IDZTagService
    {
      IRepositoryDZTag repositoryDZTag;// DALFactory.DALDZTag;

        //增加一个Tag
        public DZTagService(IRepositoryDZTag repositoryDZTag)
        {
            this.repositoryDZTag = repositoryDZTag;
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
            repositoryDZTag.Add(dzTag);
            return dzTag;
        }
        //删除tag 
        public void DeleteTag(Guid tagId)
        {
            DZTag tag = repositoryDZTag.FindById(tagId);
            repositoryDZTag.Delete(tag); 
        }
        /// <summary>
        /// 获取一个服务的tag
        /// </summary>
        /// <param name="serviceId">服务ID</param>
        /// <returns></returns>
        public IList<DZTag> GetTagForService(Guid serviceId)
        {
          return repositoryDZTag.GetTagsForService(serviceId);
        }

        public void DeleteByServiceId(Guid serviceId)
        {
            IList<DZTag> tagList = repositoryDZTag.GetTagsForService(serviceId);
            foreach (DZTag tag in tagList)
            {
                repositoryDZTag.Delete(tag);
            }
        }
    }
}
