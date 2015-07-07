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
        public virtual DateTime LastLoginTime { get; set; }
        public virtual string Email{ get; set; }
        public virtual string  Phone{ get; set; }
        public virtual string NickName { get; set; }
        public virtual string Address { get; set; }

        public virtual void CopyTo(DZMembership newMember)
        {
            newMember.Id = Id;
            newMember.UserName = UserName;
            newMember.Password = Password;
            newMember.TimeCreated = TimeCreated;
            newMember.LastLoginTime = LastLoginTime;
            newMember.Email = Email;
            newMember.Phone = Phone;
            newMember.NickName = NickName;
            newMember.Address = Address;
        }

        
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
         
        
    }
}
