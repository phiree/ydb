using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Common;

namespace Ydb.Order.DomainModel
{
    public class Complaint: Entity<Guid>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Complaint()
        {
            CreatTime = LastUpdateTime = DateTime.Now;
        }
       
        /// <summary>
        /// 订单id
        /// </summary>
        public virtual string OrderId { get; set; }

        public virtual string CustomerServiceId { get; set; }

        public virtual string BusinessId { get; set; }
        /// <summary>
        /// 投诉目标
        /// </summary>
        public virtual enum_ComplaintTarget Target { get; set; }
        /// <summary>
        /// 投诉内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 投诉的图片链接
        /// </summary>
        public virtual IList<string> ComplaitResourcesUrl { get; set; }


        /// <summary>
        /// 投诉的图片链接
        /// </summary>
        public virtual IList<string> ComplaitResourcesPathUrl {
            get {
                IList<string> pathUrl =new List<string>();
                for (int i = 0; i < ComplaitResourcesUrl.Count; i++)
                {
                    pathUrl.Add(ComplaitResourcesUrl[i] != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + ComplaitResourcesUrl[i] : "");
                }
                return pathUrl;
            }
        }

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
        public virtual string OperatorId { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 该投诉的状态
        /// </summary>
        /// <type>string</type>
        public virtual enum_ComplaintStatus Status { get; set; }
    }
}
