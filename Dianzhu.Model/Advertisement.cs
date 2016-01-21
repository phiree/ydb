using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 城市
    /// </summary>
    public class Advertisement
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public virtual string ImgUrl { get; set; }
        /// <summary>
        /// 目标地址
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int Num { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public virtual DateTime SaveTime { get; set; }
        /// <summary>
        /// 保存人员
        /// </summary>
        public virtual DZMembership SaveController { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public virtual DZMembership UpdateController { get; set; }
    }
}
