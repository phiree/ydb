using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Enums;

namespace Ydb.Membership.DomainModel
{
    public class DZMembershipCustomerService:DZMembership
    {
        public DZMembershipCustomerService():base()
        {
            DZMembershipImages = new List<DZMembershipImage>();
            IsVerified = false;
            VerificationIsAgree = false;
            IsLocked = false;
        }

        public virtual IList<DZMembershipImage> DZMembershipImages { get; set; }
        public virtual string ApplyMemo { get; set; }
        public virtual DateTime ApplyTime { get; set; }
        public virtual DateTime VerifyTime { get; set; }
        public virtual string RefuseReason { get; set; }
        public virtual bool IsAgentCustomerService { get; set; }
        public virtual bool IsVerified { get; set; }
        public virtual bool VerificationIsAgree { get; set; }



        /// <summary>
        /// 用户头像
        /// </summary>
        //public virtual DZMembershipImage DZMembershipAvatar
        //{
        //    get
        //    {
        //        DZMembershipImage[] mi = DZMembershipImages.Where(x => x.ImageType == DZMembershipImageType.Avatar && x.IsCurrent).OrderByDescending(x => x.UploadTime).ToArray();
        //        if (mi.Count() >= 1)
        //        {
        //            return mi[0];
        //        }
        //        return new DZMembershipImage();
        //    }
        //    set
        //    {
        //        if (value.ImageType != DZMembershipImageType.Avatar)
        //        {
        //            throw new FormatException("图片类型必须为头像！");
        //        }
        //        IList<DZMembershipImage> images = DZMembershipImages.Where(x => x.ImageType == DZMembershipImageType.Avatar).ToList();
        //        foreach (DZMembershipImage i in images)
        //        {
        //            i.IsCurrent = false;
        //        }
        //        value.IsCurrent = true;
        //        DZMembershipImages.Add(value);
        //    }
        //}

        /// <summary>
        /// 学位证明
        /// </summary>
        public virtual DZMembershipImage DZMembershipDiploma
        {
            get
            {
                DZMembershipImage[] mi = DZMembershipImages.Where(x => x.ImageType == DZMembershipImageType.Diploma && x.IsCurrent).OrderByDescending(x => x.UploadTime).ToArray();
                if (mi.Count() >= 1)
                {
                    return mi[0];
                }
                return new DZMembershipImage();
            }
            set
            {
                AddDZMembershipImage(value, DZMembershipImageType.Diploma, true);
            }
        }

        /// <summary>
        /// 证件照片
        /// </summary>
        public virtual IList<DZMembershipImage> DZMembershipCertificates
        {
            get
            {
                DZMembershipImage[] mi = DZMembershipImages.Where(x => x.ImageType == DZMembershipImageType.Certificate).ToArray();
                return mi;
            }
            set
            {
                IList<DZMembershipImage> images = DZMembershipImages.Where(x => x.ImageType == DZMembershipImageType.Certificate).ToList();
                foreach (DZMembershipImage i in value)
                {
                    AddDZMembershipImage(i, DZMembershipImageType.Certificate, false);
                }
                foreach (DZMembershipImage i in images)
                {
                    DZMembershipImages.Remove(i);
                }
                
            }
        }

        /// <summary>
        /// 证件图片
        /// </summary>
        public virtual IList<DZMembershipImage> DZMembershipOthers
        {
            get
            {
                DZMembershipImage[] mi = DZMembershipImages.Where(x => x.ImageType == DZMembershipImageType.Other).ToArray();
                return mi;
            }
            set
            {
                IList<DZMembershipImage> images = DZMembershipImages.Where(x => x.ImageType == DZMembershipImageType.Other).ToList();
                foreach (DZMembershipImage i in value)
                {
                    AddDZMembershipImage(i,DZMembershipImageType.Other,false);
                }
                foreach (DZMembershipImage i in images)
                {
                    DZMembershipImages.Remove(i);
                }
            }
        }

        public virtual void AddDZMembershipImage(DZMembershipImage image, DZMembershipImageType imagetype,bool isCurrent)
        {
            if (image.ImageType != imagetype)
            {
                throw new FormatException("图片类型必须为"+imagetype.ToString());
            }
            if (isCurrent)
            {
                IList<DZMembershipImage> images = DZMembershipImages.Where(x => x.ImageType == imagetype).ToList();
                foreach (DZMembershipImage i in images)
                {
                    i.IsCurrent = false;
                }
                image.IsCurrent = true;
            }
            DZMembershipImages.Add(image);
        }

        /// <summary>
        /// 认证助理
        /// </summary>
        public virtual void Verification(bool isAgree, string strReason)
        {
            if (!IsVerified)
            {
                if (!isAgree && string.IsNullOrEmpty(strReason))
                {
                    throw new FormatException("拒绝原因不能为空!");
                }
                this.IsVerified = true;
                this.RefuseReason = strReason;
                this.VerificationIsAgree = isAgree;
                this.VerifyTime = DateTime.Now;
            }
            else
            {
                throw new FormatException("该客服已经验证过了!");
            }
        }



    }
}
