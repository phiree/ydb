using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Store
{
    public class StoreService: IStoreService
    {
        BLL.DZMembershipProvider bllDZM;
        BLL.BLLBusiness bllBusiness;
        public StoreService(BLL.BLLBusiness bllBusiness, BLL.DZMembershipProvider bllDZM)
        {
            this.bllBusiness = bllBusiness;
            this.bllDZM = bllDZM;
        }

        void changeObj(storeObj storeobj, Model.Business business)
        {
            foreach (Model.BusinessImage bimg in business.ChargePersonIdCards)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + bimg.ImageName);
                }
            }
            foreach (Model.BusinessImage bimg in business.BusinessLicenses)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + bimg.ImageName);
                }
            }
            foreach (Model.BusinessImage bimg in business.BusinessShows)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.showImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + bimg.ImageName);
                }
            }
            if (storeobj.location == null)
            {
                storeobj.location = new locationObj();
            }
            storeobj.location.latitude = business.Latitude.ToString();
            storeobj.location.longitude = business.Longitude.ToString();
            storeobj.location.address = business.RawAddressFromMapAPI==null?"":business.RawAddressFromMapAPI;
        }

        /// <summary>
        /// 新建店铺
        /// </summary>
        /// <param name="storeobj"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public storeObj PostStore(storeObj storeobj, common_Trait_Headers headers)
        {
            if (string.IsNullOrEmpty(storeobj.name))
            {
                throw new FormatException("店铺名称不能为空！");
            }
            if (string.IsNullOrEmpty(storeobj.name))
            {
                throw new FormatException("店铺电话不能为空！");
            }
            Customer customer = new Customer();
            customer = customer.getCustomer(headers.token, headers.apiKey, false);
            Guid guidUser = utils.CheckGuidID(customer.UserID, "token.UserID");
            Model.DZMembership dzmember = bllDZM.GetUserById(guidUser);
            if (dzmember == null || dzmember.UserType .ToString()!= "business")
            {
                throw new Exception("该账号不是商户账号！");
            }
            Model.Business business = Mapper.Map<storeObj, Model.Business>(storeobj);
            business.Owner = dzmember;
            DateTime dt = DateTime.Now;
            business.CreatedTime = dt;
            bllBusiness.Add(business);
            //business = bllBusiness.GetOne(business.Id);
            //if (business != null && business.CreatedTime==dt)
            //{
            storeobj = Mapper.Map<Model.Business, storeObj>(business);
            changeObj(storeobj, business);
            //}
            //else
            //{
            //    throw new Exception("新建失败");
            //}
            return storeobj;
        }

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="storefilter"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public IList<storeObj> GetStores(common_Trait_Filtering filter, common_Trait_StoreFiltering storefilter,common_Trait_Headers headers)
        {
            IList<Model.Business> business = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "Business");
            Customer customer = new Customer();
            customer = customer.getCustomer(headers.token, headers.apiKey, false);
            //utils.CheckGuidID(storefilter.merchantID, "merchantID"),
            business = bllBusiness.GetStores(filter1, storefilter.name, customer.UserID);
            if (business == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<storeObj> storeobj = Mapper.Map<IList<Model.Business>, IList<storeObj>>(business);
            for (int i = 0; i < storeobj.Count; i++)
            {
                changeObj(storeobj[i], business[i]);
            }
            return storeobj;
        }

        /// <summary>
        /// 统计店铺数量
        /// </summary>
        /// <param name="storefilter"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public countObj GetStoresCount(common_Trait_StoreFiltering storefilter, common_Trait_Headers headers)
        {
            countObj c = new countObj();
            Customer customer = new Customer();
            customer = customer.getCustomer(headers.token, headers.apiKey, false);
            c.count = bllBusiness.GetStoresCount(storefilter.name, utils.CheckGuidID(customer.UserID, "merchantID")).ToString();
            return c;
        }

        /// <summary>
        /// 条件读取所有店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="storefilter"></param>
        /// <returns></returns>
        public IList<storeObj> GetAllStores(common_Trait_Filtering filter, common_Trait_StoreFiltering storefilter)
        {
            IList<Model.Business> business = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "Business");
            business = bllBusiness.GetStores(filter1, storefilter.name, storefilter.merchantID);
            if (business == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<storeObj> storeobj = Mapper.Map<IList<Model.Business>, IList<storeObj>>(business);
            for (int i = 0; i < storeobj.Count; i++)
            {
                changeObj(storeobj[i], business[i]);
            }
            return storeobj;
        }

        /// <summary>
        /// 统计所有店铺数量
        /// </summary>
        /// <param name="storefilter"></param>
        /// <returns></returns>
        public countObj GetAllStoresCount(common_Trait_StoreFiltering storefilter)
        {
            countObj c = new countObj();
            c.count = bllBusiness.GetStoresCount(storefilter.name, utils.CheckGuidID(storefilter.merchantID, "merchantID")).ToString();
            return c;
        }

        /// <summary>
        /// 读取店铺 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public storeObj GetStore(string storeID)
        {
            Model.Business business =  bllBusiness.GetOne(utils.CheckGuidID(storeID, "storeID"));
            if (business == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            storeObj storeobj = Mapper.Map<Model.Business, storeObj>(business);
            changeObj(storeobj, business);
            return storeobj;
        }

        /// <summary>
        /// 更新店铺信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="storeobj"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public storeObj PatchStore(string storeID, storeObj storeobj, common_Trait_Headers headers)
        {
            Customer customer = new Customer();
            customer = customer.getCustomer(headers.token, headers.apiKey, false);
            Guid guidUser = utils.CheckGuidID(customer.UserID, "token.UserID");
            //Guid guidUser = new Guid();
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidUser);
            Model.Business business1 = new Model.Business();
            if (business == null)
            {
                throw new Exception("该商户不存在该店铺！");
            }
            //Model.Business business1 = new Model.Business();
            //business.CopyTo(business1);
            //Model.Business business2 = Mapper.Map<storeObj, Model.Business>(storeobj);
            if (string.IsNullOrEmpty(storeobj.name)== false && storeobj.name != business.Name)
            {
                business.Name = storeobj.name;
            }
            if (string.IsNullOrEmpty(storeobj.introduction) == false && storeobj.introduction != business.Description)
            {
                business.Description = storeobj.introduction;
            }
            if (string.IsNullOrEmpty(storeobj.storePhone) == false && storeobj.storePhone != business.Phone)
            {
                business.Phone = storeobj.storePhone;
            }
            if (string.IsNullOrEmpty(storeobj.address) == false && storeobj.address != business.Address)
            {
                business.Address = storeobj.address;
            }
            if (!string.IsNullOrEmpty(storeobj.imgUrl))
            {
                //string savedFileName = MediaServer.HttpUploader.Upload(Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl"),
                //   requestData.imgData, "BusinessAvatar", "image");
                //utils.DownloadToMediaserver(storeobj.imgUrl, string.Empty, "BusinessAvatar", "image");
                Model.BusinessImage bi = new Model.BusinessImage();
                bi.ImageName = storeobj.imgUrl;
                bi.ImageType = Model.Enums.enum_ImageType.Business_Avatar;
                bi.IsCurrent = true;
                business.BusinessAvatar = bi;
            }

            //bllBusiness.Update(business2);
            //business2 = bllBusiness.GetOne(business2.Id);


            //if (business2 != null)
            //{
            storeobj = Mapper.Map<Model.Business, storeObj>(business);
            changeObj(storeobj, business);
            //}
            //else
            //{
            //    throw new Exception("更新失败");
            //}
            return storeobj;
        }

        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public object DeleteStore(string storeID, common_Trait_Headers headers)
        {
            Customer customer = new Customer();
            customer = customer.getCustomer(headers.token, headers.apiKey, false);
            Guid guidUser = utils.CheckGuidID(customer.UserID, "token.UserID");
            //Guid guidUser = new Guid();
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidUser);
            if (business == null)
            {
                throw new Exception("该商户不存在该店铺！");
            }
            bllBusiness.Delete(business);
            //business = bllBusiness.GetOne(guidStore);
            //if (business == null)
            //{
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            }
            catch
            {
                throw new Exception("该店铺已经存在服务，不能再删除！");
            }
            return "删除成功！";
            //}
            //else
            //{
            //    throw new Exception("删除失败！");
            //}
        }
    }
}
