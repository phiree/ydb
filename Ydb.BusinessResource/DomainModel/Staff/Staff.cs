﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;
using Ydb.BusinessResource.DomainModel;
namespace Ydb.BusinessResource.DomainModel
{
    /// <summary>
    /// 商家职员信息
    /// </summary>
    public class Staff: Entity<Guid>
    {
        public Staff()
        {

            StaffAvatar = new List<BusinessImage>();
            Enable = true;
            EnableTime = DateTime.Now;
        }
    
        /// <summary>
        /// 所属商家
        /// </summary>
        public virtual Business Belongto { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        /// <summary>
        /// 员工登录app的用户名
        /// </summary>
        public virtual string LoginName { get; set; }
        /// <summary>
        /// 员工的用户ID
        /// </summary>
        public virtual string UserID { get; set; }
        public virtual int Age { get; set; }
        /// <summary>
        /// 工作年数
        /// </summary>
        public virtual int WorkingYears { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string NickName { get; set; }
        /// <summary>
        /// 用于客户端显示
        /// </summary>
        public virtual string DisplayName
        {
            get { return string.IsNullOrEmpty(NickName) ? Name : NickName; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 是否已经被分配.
        /// </summary>
        public virtual bool IsAssigned { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public virtual string Photo { get; set; }
        /// <summary>
        /// 是否在职
        /// </summary>
        public virtual bool Enable { get; set; }

        /// <summary>
        /// 是否在职变更时间
        /// </summary>
        public virtual DateTime EnableTime { get; set; }

        /// <summary>
        /// 职员的头像. 可能会有多个,但是只有一个是
        /// </summary>
        public virtual IList<BusinessImage> StaffAvatar { get; set; }
        public virtual BusinessImage AvatarCurrent
        {
            get
            {
                IList<BusinessImage> list = StaffAvatar.Where(x => x.IsCurrent).ToList();
                if (list.Count == 0)
                {
                    return null;
                }
                else if (list.Count == 1)
                {
                    return list[0];
                }
                else
                {
                    throw new Exception("错误:用户有多个头像");
                }
            }
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="newStaff"></param>
        public virtual void CopyTo(Staff newStaff)
        {
            newStaff.Id = Id;
            newStaff.Belongto = Belongto;
            newStaff.Code = Code;
            newStaff.Name = Name;
            newStaff.Age = Age;
            newStaff.WorkingYears = WorkingYears;
            newStaff.NickName = NickName;
            newStaff.Gender = Gender;
            newStaff.Email = Email;
            newStaff.Phone = Phone;
            newStaff.Address = Address;
            newStaff.IsAssigned = IsAssigned;
            newStaff.Photo = Photo;
            newStaff.Enable = Enable;
        }

        public virtual void EnableStaff(bool enable)
        {
            //if (!enable)
            //{
            //    if (!Enabled)
            //    {
            //        throw new Exception("该员工已经离职！");
            //    }
            //}
            this.Enable = enable;
            this.EnableTime = DateTime.Now;
        }

    }

}
