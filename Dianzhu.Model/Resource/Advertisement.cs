using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 城市
    /// </summary>
    public class Advertisement:DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
      
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
        /// 推送目标的标签
        /// </summary>
        public virtual string PushTarget { get; set; }
        /// <summary>
        /// 广告开始时间
        /// </summary>
        public virtual DateTime StartTime { get; set; }
        /// <summary>
        /// 广告结束时间
        /// </summary>
        public virtual DateTime EndTime { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public virtual bool IsUseful { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public virtual DateTime SaveTime { get; set; }
        /// <summary>
        /// 保存人员
        /// </summary>
        public virtual string SaveControllerId { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public virtual string UpdateControllerId { get; set; }
        /// <summary>
        /// 广告推送的用户类型:customer(用户),business(商家)...
        /// </summary>
        public virtual string PushType { get; set; }
    }
}
