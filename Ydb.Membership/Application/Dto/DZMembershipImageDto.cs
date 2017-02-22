using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application.Dto
{
    public class DZMembershipImageDto
    {
        /// <summary>
        /// 图片名称,图片存储路径由配置确定.
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }
        /// <summary>
        /// 序号,用于手动排序
        /// </summary>
        public int OrderNumber { get; set; }
        /// <summary>
        /// 图片大小, 单位 KB
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 图片的类型,
        /// </summary>
        public string ImageType { get; set; }
        /// <summary>
        /// 是否是当前使用的图片
        /// </summary>
        public bool IsCurrent { get; set; }

    }
}
