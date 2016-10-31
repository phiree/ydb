﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application.Dto
{
   public class MemberDto
    {
         
        public Guid Id { get; internal set; }
        public string UserName{ get; internal set; }
        public virtual DateTime TimeCreated { get; set; }
        public virtual DateTime LastLoginTime { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Address { get; set; }
        public virtual bool IsRegisterValidated { get; set; }
        public virtual string RegisterValidateCode { get; set; }
        public virtual string UserType { get; set; }        
        public virtual string RecoveryCode { get; set; }
        public virtual string AvatarUrl { get; set; }
    }
}
