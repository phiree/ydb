using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Membership.Application.Dto
{
   public class MemberDto
    {
         
        public Guid Id { get;  set; }
        public string UserName{ get;  set; }
        public string PlainPassword { get;  set; }
        public virtual DateTime TimeCreated { get; set; }
        public virtual DateTime LastLoginTime { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Address { get; set; }
        public virtual string QQNumber { get; set; }


        public virtual string WXNumber { get; set; }
        public virtual string WBNumber { get; set; }
        public virtual DateTime LockTime { get; set; }
        public virtual bool IsLocked { get; set; }
        public virtual string LockReason { get; set; }

        public virtual string RealName { get; set; }
        public virtual bool Sex { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual string PersonalID { get; set; }
        public virtual string AreaId { get; set; }
        public virtual bool IsRegisterValidated { get; set; }
        public virtual string RegisterValidateCode { get; set; }
        public virtual string UserType { get; set; }        
        public virtual string RecoveryCode { get; set; }
        public virtual string AvatarUrl { get; set; }
        public virtual string AvatarPathUrl { get; set; }
        public virtual string FriendlyUserType { get; set; }
        public virtual int LoginTimes { get; set; }
        public virtual string UserCity { get; set; }

        /// <summary>
        /// 用户所在位置经度
        /// </summary>
        public virtual string Longitude { get; set; }
        /// <summary>
        /// 用户所在位置纬度
        /// </summary>
        public virtual string Latitude { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual void CopyTo(MemberDto newMember)
        {
            newMember.Id = Id;
            newMember.UserName = UserName;
           
            newMember.TimeCreated = TimeCreated;
            newMember.LastLoginTime = LastLoginTime;
            newMember.Email = Email;
            newMember.Phone = Phone;
            newMember.NickName = NickName;
            newMember.Address = Address;
        }
    }
}
