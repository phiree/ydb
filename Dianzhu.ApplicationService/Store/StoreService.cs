using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
using Ydb.Common.Application;
namespace Dianzhu.ApplicationService.Store
{
    public class StoreService: IStoreService
    {
        IDZMembershipService memberService;
        IBusinessService businessService;
        IStaffService staffService;
        public StoreService(IBusinessService businessService, IDZMembershipService memberService, IStaffService staffService)
        {
            this.businessService = businessService;
            this.memberService = memberService;
            this.staffService = staffService;
        }
        //todo: refactor: 需要提取到领域内的逻辑
        /// <summary>
        /// change business to storeObj
        /// </summary>
        /// <param name="storeobj"></param>
        /// <param name="business"></param>
         public   void changeObj(storeObj storeobj,  Business business)
        {
            foreach ( BusinessImage bimg in business.ChargePersonIdCards)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);//MediaGetUrl
                }
            }
            foreach (BusinessImage bimg in business.BusinessLicenses)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);
                }
            }
            foreach (BusinessImage bimg in business.BusinessShows)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.showImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);
                }
            }
            if (storeobj.location == null)
            {
                storeobj.location = new locationObj();
            }
            storeobj.location.latitude = business.Latitude.ToString();
            storeobj.location.longitude = business.Longitude.ToString();
            storeobj.location.address = business.RawAddressFromMapAPI==null?"":business.RawAddressFromMapAPI;

            storeobj.headCount = int.Parse(staffService.GetStaffsCount("", "", "", "", "", "", business.Id).ToString());


        }

        /// <summary>
        /// 新建店铺
        /// </summary>
        /// <param name="storeobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public storeObj PostStore(storeObj storeobj,Customer customer)
        {
 
           Guid guidUser = utils.CheckGuidID(customer.UserID, "customer.UserID");
           MemberDto member= memberService.GetUserById(guidUser.ToString());
            if (member == null )
            {
                throw new Exception("该商户账号不存在！");
            }
            if (member.UserType != "business")
            {
                throw new Exception("您不是商户用户！");
            }
         ActionResult<Business> result=   businessService.Add(storeobj.name, storeobj.storePhone,string.Empty, guidUser, storeobj.location.latitude,
                storeobj.location.longitude, storeobj.location.address,storeobj.linkMan,storeobj.vintage,storeobj.headCount);

            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrMsg);
            }
            storeobj.id = result.ResultObject.Id.ToString();
            changeObj(storeobj,result.ResultObject);
           
            return storeobj;
        }

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="storefilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<storeObj> GetStores(common_Trait_Filtering filter, common_Trait_StoreFiltering storefilter,Customer customer)
        {
            IList<Business> business = null;
            TraitFilter filter1 = utils.CheckFilter(filter, "Business");
            business = businessService.GetStores(filter1, storefilter.name, customer.UserID);
            if (business == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<storeObj>();
            }
            IList<storeObj> storeobj = Mapper.Map<IList<Business>, IList<storeObj>>(business);
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
        /// <param name="customer"></param>
        /// <returns></returns>
        public countObj GetStoresCount(common_Trait_StoreFiltering storefilter, Customer customer)
        {
            countObj c = new countObj();
            c.count = businessService.GetStoresCount(storefilter.name, utils.CheckGuidID(customer.UserID, "customer.UserID")).ToString();
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
            IList<Business> business = null;
            TraitFilter filter1 = utils.CheckFilter(filter, "Business");
            business = businessService.GetStores(filter1, storefilter.name, storefilter.merchantID);
            if (business == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<storeObj>();
            }
            IList<storeObj> storeobj = Mapper.Map<IList<Business>, IList<storeObj>>(business);
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
            c.count = businessService.GetStoresCount(storefilter.name, utils.CheckGuidID(storefilter.merchantID, "merchantID")).ToString();
            return c;
        }

        /// <summary>
        /// 读取店铺 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public storeObj GetStore(string storeID)
        {
            Business business =  businessService.GetOne(utils.CheckGuidID(storeID, "storeID"));
            if (business == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                //return null;
                throw new Exception("没有找到资源！");
            }
            storeObj storeobj = Mapper.Map<Business, storeObj>(business);
            changeObj(storeobj, business);
            return storeobj;
        }

        /// <summary>
        /// 更新店铺信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="storeobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public storeObj PatchStore(string storeID, storeObj storeobj, Customer customer)
        {
            Guid guidUser = utils.CheckGuidID(customer.UserID, "customer.UserID");
            //Guid guidUser = new Guid();
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            
         ActionResult<Business> result=businessService.ChangeInfo(guidStore.ToString(),guidUser.ToString(),
             storeobj.name,
             storeobj.introduction, storeobj.storePhone, storeobj.address
               , utils.GetFileName(storeobj.imgUrl, "ImageHandler"));

            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrMsg);
            }
            storeobj = Mapper.Map<Business, storeObj>(result.ResultObject);
            changeObj(storeobj, result.ResultObject);
            return storeobj;
        }

        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public object DeleteStore(string storeID, Customer customer)
        {
            Guid guidUser = utils.CheckGuidID(customer.UserID, "customer.UserID");
            //Guid guidUser = new Guid();
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Business business = businessService.GetBusinessByIdAndOwner(guidStore, guidUser);
            if (business == null)
            {
                throw new Exception("该商户不存在该店铺！");
            }
            businessService.Delete(business);
            //business = businessService.GetOne(guidStore);
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
            return new string[] { "删除成功！" };
            //}
            //else
            //{
            //    throw new Exception("删除失败！");
            //}
        }
    }
}
