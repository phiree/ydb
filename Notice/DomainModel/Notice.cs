using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;

namespace Ydb.Notice.DomainModel
{
    public class Notice : Ydb.Common.Domain.Entity<Guid>
    {
        public Notice()
        {
            TimeCreated = DateTime.Now;
            IsApproved = false;
        }

        public virtual enum_UserType TargetUserType { get; protected internal set; }
        public virtual string Title { get; protected internal set; }
        public virtual string Body { get; protected internal set; }
        public virtual DateTime TimeCreated { get; protected internal set; }
        public virtual Guid AuthorId { get; protected internal set; }

        public virtual bool IsApproved { get; protected internal set; }
        public virtual Guid ApproverId { get; protected internal set; }
        public virtual DateTime ApprovedTime { get; protected internal set; }
        /// <summary>
        /// 审核备注. 
        /// </summary>
        public virtual string ApproveMemo { get; set; }

        public virtual string TargetUserTypeString
        {
            get
            {
                string result = string.Empty;
                if ((TargetUserType & enum_UserType.business) == enum_UserType.business)
                {
                    result += "商家,";
                }
                if ((TargetUserType & enum_UserType.customer) == enum_UserType.customer)
                {
                    result += "用户,";
                }
                if ((TargetUserType & enum_UserType.agent) == enum_UserType.agent)
                {
                    result += "代理,";
                }
                if ((TargetUserType & enum_UserType.customerservice) == enum_UserType.customerservice)
                {
                    result += "客服,";
                    
                }
                return result.TrimEnd(',');
                

            }

        }

        public virtual void SetApproved(Guid approverId)
        {
            IsApproved = true;
            ApprovedTime = DateTime.Now;
            ApproverId = approverId;
        }
        public virtual void SetRefused(Guid approverId,string refusedReason)
        {
            IsApproved = false;
            ApprovedTime = DateTime.Now;
            ApproveMemo = DateTime.Now+ refusedReason+Environment.NewLine+ApproveMemo;
            
        }
    }
}