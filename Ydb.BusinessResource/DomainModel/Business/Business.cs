using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common;
using Ydb.Common.Domain;
namespace Ydb.BusinessResource.DomainModel
{

    /// <summary>
    /// 店铺基类.泛指可以提供服务的单位,可以是公司 也可以是个人
    /// </summary>
    public class Business_Abs : Ydb.Common.Domain.Entity<Guid>
    {
        public Business_Abs()
        {
            Enabled = true;
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public virtual string Name { get;protected internal set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public virtual string Contact { get;protected internal set; }
        /// <summary>
        /// 公司电话
        /// </summary>
        public virtual string Phone { get;protected internal set; }
        /// <summary>
        /// 公司邮箱
        /// </summary>
        public virtual string Email { get;protected internal set; }
        /// <summary>
        /// 简介
        /// </summary>
        public virtual string Description { get;protected internal set; }

        /// <summary>
        /// 公司网址
        /// </summary>
        public virtual string WebSite { get;protected internal set; }

        /// <summary>
        /// 商户所有者.
        /// </summary>
        public virtual Guid OwnerId { get;protected internal set; }

        /// <summary>
        /// 商家用户名.
        /// </summary>
        public virtual string OwnerName { get; set; }

        /// <summary>
        /// 店铺是否可用
        /// </summary>
        public virtual bool Enabled { get;protected internal set; }

        /// <summary>
        /// 店铺是否可用变更时间
        /// </summary>
        public virtual DateTime EnabledTime { get; protected internal set; }


        /// <summary>
        /// 封停或删除说明
        /// </summary>
        public virtual string EnabledMemo { get; protected internal set; }

        public virtual void EnableBusiness (bool enable,string memo)
        {
            //if (!enable)
            //{
            //    if (!Enabled)
            //    {
            //        throw new Exception("该店铺已经封停！");
            //    }
            //    if (memo == "")
            //    {
            //        throw new Exception("请输入封停原因！");
            //    }
            //}
            this.Enabled = enable;
            this.EnabledTime = DateTime.Now;
            this.EnabledMemo = memo;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get;protected internal set; }
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
            CreatedTime = DateTime.Now;
        }
        public Business(string name,string phone,string email,Guid ownerId,string latitude,string longtitude
            ,string rawAddressFromMapApi, string contact, int workingYears, int staffAmount) :this()
        {
            if (string.IsNullOrEmpty( name))
            {
                throw new FormatException("店铺名称不能为空！");
            }
            //if (string.IsNullOrEmpty(storeobj.introduction))
            //{
            //    throw new FormatException("店铺简介不能为空！");
            //}
            if (string.IsNullOrEmpty( phone))
            {
                throw new FormatException("店铺电话不能为空！");
            }

            Email = email;
            Name = name;
            OwnerId = ownerId;
            Phone = phone;

            double dd = 0;
            if (double.TryParse(latitude, out dd))
            {
                 Latitude = dd;
            }
            if (double.TryParse(longtitude, out dd))
            {
               Longitude = dd;
            }
             RawAddressFromMapAPI = rawAddressFromMapApi;
            Contact = contact;
            WorkingYears = workingYears;
            StaffAmount = staffAmount;
            
        }
        /// <summary>
        ///  所在辖区
        /// </summary>
        //public virtual Area AreaBelongTo { get;protected internal set; }
        public virtual string AreaBelongTo { get; protected internal set; }

        /// <summary>
        ///  地址
        /// </summary>
        public virtual string Address { get;protected internal set; }
        /// <summary>
        /// 员工总人数
        /// </summary>
        public virtual int StaffAmount { get;protected internal set; }
        /// <summary>
        /// 服务数量
        /// </summary>
        public virtual int ServiceAmount { get; protected internal set; }
        /// <summary>
        /// 从业时长, 比如 1915年进入该行业的, 则是 百  --年老 --店,该值为100.
        /// </summary>
        public virtual int WorkingYears { get;protected internal set; }
        /// <summary>
        /// 商家相关的图片
        /// </summary>
        public virtual IList<BusinessImage> BusinessImages { get;protected internal set; }
        /// <summary>
        /// 服务类型.
        /// </summary>
        public virtual IList<ServiceType> ServiceType { get;protected internal set; }


        /// <summary>
        /// 店铺地理坐标,精度，维度。
        /// </summary>
        public virtual double Longitude { get;protected internal set; }
        public virtual double Latitude { get;protected internal set; }

        /// <summary>
        /// MapAPI返回地址信息
        /// </summary>
        public virtual string RawAddressFromMapAPI { get;protected internal set; }
        /// <summary>
        /// 优惠推广的范围.所有的优惠券都是一样的.
        /// </summary>
        public virtual double PromoteScope { get;protected internal set; }
        /// <summary>
        /// 是否通过了审核.
        /// </summary>
        public virtual bool IsApplyApproved { get;protected internal set; }
        /// <summary>
        /// 审核拒绝信息.
        /// </summary>
        public virtual string ApplyRejectMessage { get;protected internal set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public virtual DateTime? DateApply { get;protected internal set; }

        /// <summary>
        /// 审核通过日期
        /// </summary>
        public virtual DateTime? DateApproved { get;protected internal set; }
        /// <summary>
        /// 单张营业执照
        /// </summary>
        public virtual BusinessImage BusinessLicence
        {
            get
            {
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_License).ToArray();
                if (bi.Count() >= 1)
                    return bi[0];
                return new BusinessImage();
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_License).ToList();
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
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_Avatar).OrderByDescending(x => x.UploadTime).ToArray();
                if (bi.Count() >= 1)
                    return bi[0];
                return new BusinessImage();
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_Avatar).ToList();
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
        public virtual enum_IDCardType ChargePersonIdCardType
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
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_ChargePersonIdCard).ToArray();
                return bi;
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_ChargePersonIdCard).ToList();
                foreach (BusinessImage i in images)
                {
                    BusinessImages.Remove(i);
                }
                foreach (BusinessImage i in value)
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
                return BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_ChargePersonIdCard).OrderBy(x => x.UploadTime).ToList();
            }
        }
        /// <summary>
        /// 多张营业执照
        /// </summary>
        public virtual IList<BusinessImage> BusinessLicenses
        {
            get
            {
                return BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_License).OrderBy(x => x.UploadTime).ToList();
            }
        }

        /// <summary>
        /// 店铺图片展示
        /// </summary>
        public virtual IList<BusinessImage> BusinessShows
        {
            get
            {
                return BusinessImages.Where(x => x.ImageType == enum_ImageType.Business_Show).OrderBy(x => x.UploadTime).ToList();
            }

        }
        /// <summary>
        /// 资料完成度
        /// </summary>
        public virtual int CompetePercent
        {
            get
            {
                int percent = 0;
                if (!string.IsNullOrEmpty(this.Address))
                {
                    percent += 7;
                }
                if (this.AreaBelongTo != null)
                {
                    percent += 14;
                }

                if (this.BusinessImages.Count > 0)
                {
                    percent += 7;
                }
                if (this.BusinessLicence != null)//5
                {
                    percent += 7;
                }
                if (this.BusinessShows.Count > 0)
                {
                    percent += 7;
                }

                if (this.ChargePersonIdCards.Count > 0)
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

                if (this.WorkingYears > 0)
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
            newBusiness.OwnerId = OwnerId;
            newBusiness.Name = Name;
            newBusiness.AreaBelongTo = AreaBelongTo;
            newBusiness.Description = Description;
            newBusiness.BusinessAvatar = BusinessAvatar;
            newBusiness.Contact = Contact;
            newBusiness.Phone = Phone;
            newBusiness.Address = Address;
            newBusiness.BusinessImages = BusinessImages;
        }
        public virtual void ChangeInfo(string name,string description,string phone,
            string address,string avatarImageName)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Name = name;
            }
            if (!string.IsNullOrEmpty(description) )
            {
                Description = description;
            }
            if (string.IsNullOrEmpty(phone) == false && phone != Phone)
            {
                Phone = phone;
            }
            if (string.IsNullOrEmpty(address) == false && address != Address)
            {
                Address = address;
            }
            if (!string.IsNullOrEmpty(avatarImageName))
            {
                //string savedFileName = MediaServer.HttpUploader.Upload(Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl"),
                //   requestData.imgData, "BusinessAvatar", "image");
                //utils.DownloadToMediaserver(imgUrl, string.Empty, "BusinessAvatar", "image");
                BusinessImage bi = new BusinessImage();
                bi.ImageName = avatarImageName;
                bi.ImageType = enum_ImageType.Business_Avatar;
                bi.IsCurrent = true;
                BusinessAvatar = bi;
            }
        }

    }

    /// <summary>
    /// 店铺的一些图片
    /// </summary>
    public class BusinessImage : Entity<Guid>
    {
        public BusinessImage()
        {
            IsCurrent = false;
        }

        /// <summary>
        /// 图片名称,图片存储路径由配置确定.
        /// </summary>
        public virtual string ImageName { get;protected internal set; }

        public virtual string ImageUrl {
            get {
                return string.IsNullOrEmpty(ImageName) ? "" : Dianzhu.Config.Config.GetAppSetting("ImageHandler") + ImageName;
            }
        }

        /// <summary>
        /// 图片描述
        /// </summary>
        public virtual string Description { get;protected internal set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public virtual DateTime UploadTime { get;protected internal set; }
        /// <summary>
        /// 序号,用于手动排序
        /// </summary>
        public virtual int OrderNumber { get;protected internal set; }
        /// <summary>
        /// 图片大小, 单位 KB
        /// </summary>
        public virtual int Size { get;protected internal set; }
        /// <summary>
        /// 图片的类型,
        /// </summary>
        public virtual enum_ImageType ImageType { get;protected internal set; }
        /// <summary>
        /// 是否是当前使用的图片
        /// </summary>
        public virtual bool IsCurrent { get;protected internal set; }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns></returns>
        public virtual string GetRelativePathByType()
        {
            string type = string.Empty;
            switch (ImageType)
            {
                case enum_ImageType.Advertisement:
                    type = "/Advertisement/";
                    break;
                case enum_ImageType.Business_Avatar:
                    type = "/BusinessAvatar/";
                    break;
                case enum_ImageType.Business_ChargePersonIdCard:
                    type = "/BusinessChargePersonIdCard/";
                    break;
                case enum_ImageType.Business_Image:
                    type = "/BusinessImage/";
                    break;
                case enum_ImageType.Business_License:
                    type = "/BusinessLicense/";
                    break;
                case enum_ImageType.Business_License_B:
                    type = "/BusinessLicenseB/";
                    break;
                case enum_ImageType.Business_Show:
                    type = "/BusinessShow/";
                    break;
                case enum_ImageType.Chat_Audio:
                    type = "/ChatAudio/";
                    break;
                case enum_ImageType.Chat_Image:
                    type = "/ChatImage/";
                    break;
                case enum_ImageType.Chat_Video:
                    type = "/ChatVideo/";
                    break;
                case enum_ImageType.Staff_Avatar:
                    type = "/StaffAvatar/";
                    break;
                case enum_ImageType.User_Avatar:
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
