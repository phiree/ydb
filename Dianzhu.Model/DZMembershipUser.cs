using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 基本的用户类.
    /// </summary>
    public class DZMembership
    {
        public virtual Guid Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime TimeCreated { get; set; }
        
    }
    /// <summary>
    /// 商家相关用户.
    /// </summary>
    public class BusinessUser : DZMembership
    {
        /// <summary>
        /// 用户所属商家.
        /// </summary>
        public virtual Business BelongTo { get; set; }
    }
    public class Customer : DZMembership
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Gender { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public virtual string Phone { get; set; }
    }
}
