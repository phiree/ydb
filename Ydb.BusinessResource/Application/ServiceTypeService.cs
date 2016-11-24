﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.IO;
using System.Data;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
namespace Ydb.BusinessResource.Application
{
    public class ServiceTypeService : IServiceTypeService
    {
        IRepositoryServiceType repositoryServiceType;

        public ServiceTypeService(IRepositoryServiceType repositoryServiceType)
        {
            this.repositoryServiceType = repositoryServiceType;
        }

        public void Save(ServiceType serviceType)
        {
            repositoryServiceType.Add(serviceType);
        }
        public void Update(ServiceType serviceType)
        {
            repositoryServiceType.Update(serviceType);
        }
        public ServiceType GetOne(Guid id)
        {
            return repositoryServiceType.FindById(id);
        }

        public ServiceType GetOneByName(string name, int level)
        {
            return repositoryServiceType.GetOneByName(name, level);
        }

        public IList<ServiceType> GetAll()
        {
            return repositoryServiceType.Find(x => true);
        }

        /// <summary>
        /// 查询 superID 的下级服务类型列表数组,当 superID 为空时，默认查询顶层服务类型列表
        /// </summary>
        /// <param name="guidSuperID"></param>
        /// <returns></returns>
        public IList<ServiceType> GetAllServiceTypes(Guid guidSuperID)
        {
            var where = PredicateBuilder.True<ServiceType>();
            if (guidSuperID == Guid.Empty)
            {
                where = where.And(x => x.DeepLevel == 0);
            }
            else
            {
                where = where.And(x => x.Parent.Id == guidSuperID);
            }
            return repositoryServiceType.Find(where);
        }

        /// <summary>
        /// 获取最顶层的类型
        /// </summary>
        /// <returns></returns>
        public IList<ServiceType> GetTopList()
        {
            return repositoryServiceType.GetTopList();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="serviceName">属性名称</param>
        /// <param name="parentId">所属分类ID</param>
        /// <param name="values">属性值, 多个用逗号分隔</param>
        /// <returns></returns>


        /*
         * todo: 此处删除了 ServiceTypePoint相关的部分
         */
    }
}