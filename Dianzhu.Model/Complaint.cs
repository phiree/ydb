using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 投诉类
    /// </summary>
    public class Complaint
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Complaint()
        {
            CreatTime = LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
        /// <summary>
        /// 投诉目标
        /// </summary>
        public virtual enum_ChatTarget Target { get; set; }
        /// <summary>
        /// 投诉内容
        /// </summary>
        public virtual string Context { get; set; }
        /// <summary>
        /// 投诉的图片链接
        /// </summary>
        public virtual string ResourcesUrl { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatTime { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public virtual string Result { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual DZMembership Operator { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
    }
}
