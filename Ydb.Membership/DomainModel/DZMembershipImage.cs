using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Common.Domain;

namespace Ydb.Membership.DomainModel
{
    public class DZMembershipImage: Entity<Guid>
    {

        public DZMembershipImage()
        {
            IsCurrent = false;
        }

        /// <summary>
        /// 图片名称,图片存储路径由配置确定.
        /// </summary>
        public virtual string ImageName { get; protected internal set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public virtual string Description { get; protected internal set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public virtual DateTime UploadTime { get; protected internal set; }
        /// <summary>
        /// 序号,用于手动排序
        /// </summary>
        public virtual int OrderNumber { get; protected internal set; }
        /// <summary>
        /// 图片大小, 单位 KB
        /// </summary>
        public virtual int Size { get; protected internal set; }
        /// <summary>
        /// 图片的类型,
        /// </summary>
        public virtual DZMembershipImageType ImageType { get; protected internal set; }
        /// <summary>
        /// 是否是当前使用的图片
        /// </summary>
        public virtual bool IsCurrent { get; protected internal set; }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns></returns>
        //public virtual string GetRelativePathByType()
        //{
        //    string type = string.Empty;
        //    switch (ImageType)
        //    {
        //        case DZMembershipImageType.Certificate:
        //            type = "/Certificate/";
        //            break;
        //        case DZMembershipImageType.Diploma:
        //            type = "/Diploma/";
        //            break;
        //        case DZMembershipImageType.Avatar:
        //            type = "/Avatar/";
        //            break;
        //        case DZMembershipImageType.Other:
        //            type = "/Other/";
        //            break;
        //        default:
        //            break;
        //    }
        //    return type;
        //}
    }
}
