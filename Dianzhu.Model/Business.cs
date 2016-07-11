using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace Dianzhu.Model
{
    
    /// <summary>
    /// 店铺基类.泛指可以提供服务的单位,可以是公司 也可以是个人
    /// </summary>
    public class Business_Abs:DDDCommon.Domain.Entity<Guid>
    {
        public Business_Abs()
        {
            Enabled = true;
            CreatedTime = DateTime.Now;
        }
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public virtual string Contact { get; set; }
        /// <summary>
        /// 公司电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 公司邮箱
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 公司网址
        /// </summary>
        public virtual string WebSite { get; set; }

        /// <summary>
        /// 商户所有者.
        /// </summary>
        public virtual DZMembership Owner { get; set; }

        /// <summary>
        /// 店铺是否可用
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; }
    }

    /// <summary>
    /// 商家下的店铺
    /// </summary>
    public class Business : Business_Abs
    {
        public Business()
        {
            
            BusinessImages = new List<BusinessImage>();
            ServiceType = new List<ServiceType>();
           
            
        }
        /// <summary>
        ///  所在辖区
        /// </summary>
        public virtual Area AreaBelongTo { get; set; }
        
       
        /// <summary>
        ///  地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 员工总人数
        /// </summary>
        public virtual int StaffAmount { get; set; }
        /// <summary>
        /// 从业时长, 比如 1915年进入该行业的, 则是 百  --年老 --店,该值为100.
        /// </summary>
        public virtual int WorkingYears { get; set; }
        /// <summary>
        /// 商家相关的图片
        /// </summary>
        public virtual IList<BusinessImage> BusinessImages { get; set; }
        /// <summary>
        /// 服务类型.
        /// </summary>
        public virtual IList<ServiceType> ServiceType { get; set; }

       
        /// <summary>
        /// 店铺地理坐标,精度，维度。
        /// </summary>
        public virtual double Longitude { get; set; }
        public virtual double Latitude { get; set; }

        /// <summary>
        /// MapAPI返回地址信息
        /// </summary>
        public virtual string RawAddressFromMapAPI { get; set; }
        /// <summary>
        /// 优惠推广的范围.所有的优惠券都是一样的.
        /// </summary>
        public virtual double PromoteScope { get; set; }
        /// <summary>
        /// 是否通过了审核.
        /// </summary>
        public virtual bool IsApplyApproved { get; set; }
        /// <summary>
        /// 审核拒绝信息.
        /// </summary>
        public virtual string ApplyRejectMessage { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public virtual DateTime? DateApply { get; set; }
       
        /// <summary>
        /// 审核通过日期
        /// </summary>
        public virtual DateTime? DateApproved { get; set; }
        /// <summary>
        /// 单张营业执照
        /// </summary>
        public virtual BusinessImage BusinessLicence
        {
            get
            {
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_License).ToArray();
                if (bi.Count() >= 1)
                    return bi[0];
                return new BusinessImage();
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_License).ToList();
                foreach (BusinessImage i in images)
                {
                    BusinessImages.Remove(i);
                }
                BusinessImages.Add(value);
            }
        }
        /// <summary>
        /// 店铺头像
        /// </summary>
        public virtual BusinessImage BusinessAvatar
        {
            get
            {
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_Avatar).OrderByDescending(x=>x.UploadTime).ToArray();
                if (bi.Count() >= 1)
                    return bi[0];
                return new BusinessImage();
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_Avatar).ToList();
                foreach (BusinessImage i in images)
                {
                    BusinessImages.Remove(i);
                }
                BusinessImages.Add(value);
            }
        }
        /// <summary>
        /// 负责人证件类型
        /// </summary>
        public virtual Enums.enum_IDCardType ChargePersonIdCardType
        {
            get;
            set;
        }
        /// <summary>
        /// 证件号码
        /// </summary>
        public virtual string ChargePersonIdCardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 证件图片
        /// </summary>
        public virtual IList<BusinessImage> ChargePersonIdCards
        {
            get
            {
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_ChargePersonIdCard).ToArray();
                return bi;
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_ChargePersonIdCard).ToList();
                foreach (BusinessImage i in images)
                {
                    BusinessImages.Remove(i);
                }
                foreach(BusinessImage i in value)
                {
                BusinessImages.Add(i);
                }
            }

        }

        /// <summary>
        /// 负责人照片
        /// </summary>
        public virtual IList<BusinessImage> BusinessChargePersonIdCards
        {
            get
            {
                return BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_ChargePersonIdCard).OrderBy(x => x.UploadTime).ToList();
            }
        }
        /// <summary>
        /// 多张营业执照
        /// </summary>
        public virtual IList<BusinessImage> BusinessLicenses
        {
            get
            {
                return BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_License).OrderBy(x => x.UploadTime).ToList();
            }
        }

        /// <summary>
        /// 店铺图片展示
        /// </summary>
        public virtual IList<BusinessImage> BusinessShows
        {
            get
            {
                return BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_Show).OrderBy(x => x.UploadTime).ToList();
            }

        }
        /// <summary>
        /// 资料完成度
        /// </summary>
        public virtual int CompetePercent
        {
            get {
                int percent = 0;
                if (!string.IsNullOrEmpty(this.Address))
                {
                    percent += 7;
                }
                if ( this.AreaBelongTo!=null)
                {
                    percent += 14;
                }
                
                if (this.BusinessImages.Count>0)
                {
                    percent += 7;
                }
                if (this.BusinessLicence!=null)//5
                {
                    percent +=7;
                }
                if (this.BusinessShows.Count>0)
                {
                    percent += 7;
                }
                
                if (this.ChargePersonIdCards.Count>0)
                {
                    percent += 7;
                }
                if (!string.IsNullOrEmpty(this.ChargePersonIdCardNo))
                {
                    percent += 7;
                }
                if (!string.IsNullOrEmpty(this.Contact))//10
                {
                    percent += 7;
                }
                if (!string.IsNullOrEmpty(this.Description))
                {
                    percent += 7;
                }

                if (!string.IsNullOrEmpty(this.Email))
                {
                    percent += 7;
                }

                if (!string.IsNullOrEmpty(this.Name))
                {
                    percent += 10;
                }
                if (!string.IsNullOrEmpty(this.Phone))
                {
                    percent += 10;
                }
                 
                if (this.WorkingYears>0)
                {
                    percent += 10;
                }


                return percent;
            }
        }
        
        
      
         /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="newBusiness"></param>
        public virtual void CopyTo(Business newBusiness)
        {
            newBusiness.Id = Id;
            newBusiness.Owner = Owner;
            newBusiness.Name = Name;
            newBusiness.AreaBelongTo = AreaBelongTo;
            newBusiness.Description = Description;
            newBusiness.BusinessAvatar = BusinessAvatar;
            newBusiness.Contact = Contact;
            newBusiness.Phone = Phone;
            newBusiness.Address = Address;
            newBusiness.BusinessImages = BusinessImages;
        }

    }

    /// <summary>
    /// 店铺的一些图片
    /// </summary>
    public class BusinessImage:DDDCommon.Domain.Entity<Guid>
    {
        public BusinessImage()
        {
            IsCurrent = false;
        }
     
        /// <summary>
        /// 图片名称,图片存储路径由配置确定.
        /// </summary>
        public virtual string ImageName { get; set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public virtual DateTime UploadTime { get; set; }
        /// <summary>
        /// 序号,用于手动排序
        /// </summary>
        public virtual int OrderNumber { get; set; }
        /// <summary>
        /// 图片大小, 单位 KB
        /// </summary>
        public virtual int Size { get; set; }
        /// <summary>
        /// 图片的类型,
        /// </summary>
        public virtual Enums.enum_ImageType ImageType { get; set; }
        /// <summary>
        /// 是否是当前使用的图片
        /// </summary>
        public virtual bool IsCurrent { get; set; }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns></returns>
        public virtual string GetRelativePathByType()
        {
            string type = string.Empty;
            switch (ImageType)
            {
                case Enums.enum_ImageType.Advertisement:
                    type = "/Advertisement/";
                    break;
                case Enums.enum_ImageType.Business_Avatar:
                    type = "/BusinessAvatar/";
                    break;
                case Enums.enum_ImageType.Business_ChargePersonIdCard:
                    type = "/BusinessChargePersonIdCard/";
                    break;
                case Enums.enum_ImageType.Business_Image:
                    type = "/BusinessImage/";
                    break;
                case Enums.enum_ImageType.Business_License:
                    type = "/BusinessLicense/";
                    break;
                case Enums.enum_ImageType.Business_License_B:
                    type = "/BusinessLicenseB/";
                    break;
                case Enums.enum_ImageType.Business_Show:
                    type = "/BusinessShow/";
                    break;
                case Enums.enum_ImageType.Chat_Audio:
                    type = "/ChatAudio/";
                    break;
                case Enums.enum_ImageType.Chat_Image:
                    type = "/ChatImage/";
                    break;
                case Enums.enum_ImageType.Chat_Video:
                    type = "/ChatVideo/";
                    break;
                case Enums.enum_ImageType.Staff_Avatar:
                    type = "/StaffAvatar/";
                    break;
                case Enums.enum_ImageType.User_Avatar:
                    type = "/UserAvatar/";
                    break;
                default:
                    break;
            }
            return type;
        }
    }

    /// <summary>
    /// 个人店铺
    /// </summary>
    public class BusinessIndividual : Business_Abs
    {

    }
}
