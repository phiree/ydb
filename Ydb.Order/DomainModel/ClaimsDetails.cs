using Ydb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;

namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 理赔详情
    /// </summary>
    public class ClaimsDetails :  Entity<Guid>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected ClaimsDetails()
        {
            
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        /// <param name="resourcesUrl"></param>
        /// <param name="target"></param>
        /// <param name="memberId">提交理赔的用户</param>
        public ClaimsDetails(Claims claims, string context,decimal amount,IList<string> resourcesUrl,enum_ChatTarget target,string memberId)
        {
            this.CreatTime = this.LastUpdateTime = DateTime.Now;

            this.Claims = claims;
            this.Context = context;
            this.Amount = amount;
            this.ClaimsDetailsResourcesUrl = resourcesUrl;
            this.Target = target;
            this.MemberId = memberId;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public virtual Claims Claims { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Context { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Amount { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        /// 20160622_longphui_modify
        ///public virtual string ResourcesUrl { get; set; }
        public virtual IList<string> ClaimsDetailsResourcesUrl { get; set; }

        /// <summary>
        /// 投诉的图片链接
        /// </summary>
        public virtual IList<string> ClaimsDetailsResourcesPathUrl
        {
            get
            {
                IList<string> pathUrl = new List<string>();
                for (int i = 0; i < ClaimsDetailsResourcesUrl.Count; i++)
                {
                    pathUrl.Add(ClaimsDetailsResourcesUrl[i] != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + ClaimsDetailsResourcesUrl[i] : "");
                }
                return pathUrl;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatTime { get; set; }
        /// <summary>
        /// 提交本次理赔的目标
        /// </summary>
        public virtual enum_ChatTarget Target { get; set; }        
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual string MemberId { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
    }
}
