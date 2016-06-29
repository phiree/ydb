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
            storeobj.location.latitude = business.Latitude.ToString();
            storeobj.location.longitude = business.Longitude.ToString();
            storeobj.location.address = business.RawAddressFromMapAPI;
        }

        /// <summary>
        /// 新建店铺
        /// </summary>
        /// <param name="storeobj"></param>
        /// <returns></returns>
        public storeObj PostStore(storeObj storeobj)
        {
            if (storeobj.alias == null || storeobj.alias == "")
            {
                throw new FormatException("店铺名称不能为空！");
            }
            if (storeobj.storePhone == null || storeobj.storePhone == "")
            {
                throw new FormatException("店铺电话不能为空！");
            }
            Guid guidUser = new Guid();
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
            business = bllBusiness.GetOne(business.Id);
            if (business != null && business.CreatedTime==dt)
            {
                storeobj = Mapper.Map<Model.Business, storeObj>(business);
                changeObj(storeobj, business);
            }
            else
            {
                throw new Exception("新建失败");
            }
            return storeobj;
        }

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="storefilter"></param>
        /// <returns></returns>
        public IList<storeObj> GetStores(common_Trait_Filtering filter, common_Trait_StoreFiltering storefilter)
        {
            IList<Model.Business> business = null;
            int[] page = utils.CheckFilter(filter);
            business = bllBusiness.GetStores(page[0], page[1], storefilter.alias,  utils.CheckGuidID(storefilter.merchantID, "merchantID"));
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
        /// <returns></returns>
        public countObj GetStoresCount(common_Trait_StoreFiltering storefilter)
        {
            countObj c = new countObj();
            c.count = bllBusiness.GetStoresCount(storefilter.alias, utils.CheckGuidID(storefilter.merchantID, "merchantID")).ToString();
            return c;
        }

        /// <summary>
        /// 读取店铺 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public storeObj GetStore(string storeID)
        {
            Model.Business business = null;
            business = bllBusiness.GetOne(utils.CheckGuidID(storeID, "storeID"));
            storeObj storeobj = Mapper.Map<Model.Business, storeObj>(business);
            return storeobj;
        }

        /// <summary>
        /// 更新店铺信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="storeobj"></param>
        /// <returns></returns>
        public storeObj PatchStore(string storeID, storeObj storeobj)
        {
            Guid guidUser = new Guid();
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidUser);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            Model.Business business1 = new Model.Business();
            business.CopyTo(business1);
            Model.Business business2 = Mapper.Map<storeObj, Model.Business>(storeobj);
            if (business2.Name != null && business2.Name != business1.Name)
            {
                business1.Name = business2.Name;
            }
            if (business2.Description != null && business2.Description != business1.Description)
            {
                business1.Description = business2.Description;
            }
            if (business2.Phone != null && business2.Phone != business1.Phone)
            {
                business1.Phone = business2.Phone;
            }
            if (business2.Address != null && business2.Address != business1.Address)
            {
                business1.Address = business2.Address;
            }
            if (storeobj.imgUrl != null && storeobj.imgUrl != "")
            {
                //string savedFileName = MediaServer.HttpUploader.Upload(Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl"),
                //   requestData.imgData, "BusinessAvatar", "image");
                //utils.DownloadToMediaserver(storeobj.imgUrl, string.Empty, "BusinessAvatar", "image");
            }

            bllBusiness.Update(business2);
            business2 = bllBusiness.GetOne(business2.Id);


            if (business2 != null)
            {
                storeobj = Mapper.Map<Model.Business, storeObj>(business2);
                changeObj(storeobj, business2);
            }
            else
            {
                throw new Exception("更新失败");
            }
            return storeobj;
        }

        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public object DeleteStore(string storeID)
        {
            Model.Business business = null;
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            business = bllBusiness.GetOne(guidStore);
            if (business == null)
            {
                throw new Exception("该员店铺不存在！");
            }
            bllBusiness.Delete(business);
            business = bllBusiness.GetOne(guidStore);
            if (business == null)
            {
                return "删除成功！";
            }
            else
            {
                throw new Exception("删除失败！");
            }
        }
    }
}
