using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;


namespace Dianzhu.ApplicationService.Service
{
    public class ServiceService: IServiceService
    {
        BLL.BLLBusiness bllBusiness ;
        BLL.BLLDZService bllDZService ;
        BLL.BLLServiceType bllServiceType ;
        BLL.BLLDZTag bllDZTag;
        public ServiceService(BLL.BLLBusiness bllBusiness, BLL.BLLDZService bllDZService, BLL.BLLServiceType bllServiceType, BLL.BLLDZTag bllDZTag)
        {
            this.bllBusiness = bllBusiness;
            this.bllDZService = bllDZService;
            this.bllServiceType = bllServiceType;
            this.bllDZTag = bllDZTag;
        }

        /// <summary>
        /// 新建服务
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="servicesobj"></param>
        /// <returns></returns>
        public servicesObj PostService(string storeID, servicesObj servicesobj)
        {
            Guid guidUser = new Guid();
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(utils.CheckGuidID(storeID, "storeID"), guidUser);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            string[] typeList = servicesobj.type.Split('>');
            int typeLevel = typeList.Count() > 0 ? typeList.Count() - 1 : 0;
            Model.ServiceType sType = bllServiceType.GetOneByName(typeList[typeLevel], typeLevel);
            if (sType == null)
            {
                throw new Exception("该服务类型有误！");
            }
            Model.DZService dzservice = Mapper.Map<servicesObj, Model.DZService>(servicesobj);
            DateTime dt = new DateTime();
            dzservice.CreatedTime = dt;
            dzservice.LastModifiedTime = dt;
            dzservice.Business = business;
            dzservice.ServiceType = sType;
            ValidationResult validationResult = new ValidationResult();
            bllDZService.SaveOrUpdate(dzservice, out validationResult);

            string[] tagList = servicesobj.tag.Split('|');
            dzservice = bllDZService.GetOne(dzservice.Id);
            if (dzservice != null && dzservice.CreatedTime == dt)
            {
                servicesobj = Mapper.Map<Model.DZService, servicesObj>(dzservice);
                servicesobj.location.longitude = dzservice.Business.Longitude.ToString();
                servicesobj.location.latitude = dzservice.Business.Latitude.ToString();
                servicesobj.location.address = dzservice.Business.RawAddressFromMapAPI;
            }
            else
            {
                throw new Exception("新建失败");
            }
            for (int i = 0; i < tagList.Count(); i++)
            {
                bllDZTag.AddTag(tagList[i], dzservice.Id.ToString(), dzservice.Business.Id.ToString(), dzservice.ServiceType.Id.ToString());
            }
            return servicesobj;
        }

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="filter"></param>
        /// <param name="servicefilter"></param>
        /// <returns></returns>
        public IList<servicesObj> GetServices(string storeID, common_Trait_Filtering filter,common_Trait_ServiceFiltering servicefilter)
        {
            IList<Model.DZService> dzservice = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "DZService");
            decimal dcStartAt = -1;
            if (!decimal.TryParse(servicefilter.startAt, out dcStartAt))
            {
                throw new FormatException("起步价必须为大于零的数值！");
            }
            dzservice = bllDZService.GetServices(filter1, utils.CheckGuidID(servicefilter.type, "type的ID"), servicefilter.name, servicefilter.introduce, dcStartAt, utils.CheckGuidID(storeID, "storeID"));
            if (dzservice == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<servicesObj> serviceobj = Mapper.Map<IList<Model.DZService>, IList<servicesObj>>(dzservice);
            for (int i = 0; i < serviceobj.Count; i++)
            {
                serviceobj[i].location.longitude = dzservice[i].Business.Longitude.ToString();
                serviceobj[i].location.latitude = dzservice[i].Business.Latitude.ToString();
                serviceobj[i].location.address = dzservice[i].Business.RawAddressFromMapAPI;
            }
            return serviceobj;
        }

        /// <summary>
        /// 统计服务的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="servicefilter"></param>
        /// <returns></returns>
        public countObj GetServicesCount(string storeID, common_Trait_ServiceFiltering servicefilter)
        {
            decimal dcStartAt = -1;
            if (!decimal.TryParse(servicefilter.startAt, out dcStartAt))
            {
                throw new FormatException("起步价必须为大于零的数值！");
            }
            countObj c = new countObj();
            c.count = bllDZService.GetServicesCount(utils.CheckGuidID(servicefilter.type, "type的ID"), servicefilter.name, servicefilter.introduce, dcStartAt, utils.CheckGuidID(storeID, "storeID")).ToString();
            return c;
        }

        /// <summary>
        /// 读取服务 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public servicesObj GetService(string storeID, string serviceID)
        {
            Model.DZService dzservice = null;
            dzservice = bllDZService.GetService(utils.CheckGuidID(storeID, "storeID"), utils.CheckGuidID(serviceID, "serviceID"));
            servicesObj servicesobj = Mapper.Map<Model.DZService, servicesObj>(dzservice);
            servicesobj.location.longitude = dzservice.Business.Longitude.ToString();
            servicesobj.location.latitude = dzservice.Business.Latitude.ToString();
            servicesobj.location.address = dzservice.Business.RawAddressFromMapAPI;
            return servicesobj;
        }

        /// <summary>
        /// 更新服务信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="servicesobj"></param>
        /// <returns></returns>
        public servicesObj PatchService(string storeID, string serviceID, servicesObj servicesobj)
        {
            Guid guidUser = new Guid();
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Guid guidService = utils.CheckGuidID(serviceID, "serviceID");
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidUser);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            Model.DZService dzserviceobj = bllDZService.GetService(guidStore, guidService);
            if (dzserviceobj == null)
            {
                throw new Exception("该服务不存在！");
            }

            Model.DZService dzservice1 = new Model.DZService();
            dzserviceobj.CopyTo(dzservice1);
            Model.DZService dzservice = Mapper.Map<servicesObj, Model.DZService>(servicesobj);
            if (dzservice.Name != null && dzservice.Name != dzservice1.Name)
            {
                dzservice1.Name = dzservice.Name;
            }
            if (servicesobj.type != null && servicesobj.type != "")
            {
                string[] typeList = servicesobj.type.Split('>');
                int typeLevel = typeList.Count() > 0 ? typeList.Count() - 1 : 0;
                Model.ServiceType sType = bllServiceType.GetOneByName(typeList[typeLevel], typeLevel);
                if (sType == null)
                {
                    throw new Exception("该服务类型有误！");
                }
                dzservice1.ServiceType = sType;
            }
            if (dzservice.Description != null && dzservice.Description != dzservice1.Description)
            {
                dzservice1.Description = dzservice.Description;
            }
            if (servicesobj.startAt != null && servicesobj.startAt != "")
            {
                dzservice1.MinPrice = dzservice.MinPrice;
            }
            if (servicesobj.deposit != null && servicesobj.deposit != "")
            {
                dzservice1.DepositAmount = dzservice.DepositAmount;
            }
            if (servicesobj.appointmentTime != null && servicesobj.appointmentTime != "")
            {
                dzservice1.OrderDelay = dzservice.OrderDelay;
            }
            if (dzservice.IsForBusiness != dzservice1.IsForBusiness)
            {
                dzservice1.IsForBusiness = dzservice.IsForBusiness;
            }
            if (dzservice.AllowedPayType != dzservice1.AllowedPayType)
            {
                dzservice1.AllowedPayType = dzservice.AllowedPayType;
            }
            if (dzservice.Enabled != dzservice1.Enabled)
            {
                dzservice1.Enabled = dzservice.Enabled;
            }
            if (servicesobj.maxCount != null && servicesobj.maxCount != "")
            {
                dzservice1.MaxOrdersPerDay = dzservice.MaxOrdersPerDay;
            }
            if (dzservice.ChargeUnit != dzservice1.ChargeUnit)
            {
                dzservice1.ChargeUnit = dzservice.ChargeUnit;
            }
            DateTime dt = new DateTime();
            dzservice.LastModifiedTime = dt;
            dzservice.Business = business;
            ValidationResult validationResult = new ValidationResult();
            bllDZService.SaveOrUpdate(dzservice1, out validationResult);


            List<string> tagList=new List<string>();
            if (servicesobj.tag != null && servicesobj.tag != "")
            {
                tagList = servicesobj.tag.Split('|').ToList();
            }
            dzservice = bllDZService.GetOne(dzservice.Id);
            if (dzservice != null && dzservice.LastModifiedTime == dt)
            {
                servicesobj = Mapper.Map<Model.DZService, servicesObj>(dzservice);
                servicesobj.location.longitude = dzservice.Business.Longitude.ToString();
                servicesobj.location.latitude = dzservice.Business.Latitude.ToString();
                servicesobj.location.address = dzservice.Business.RawAddressFromMapAPI;
            }
            else
            {
                throw new Exception("更新失败");
            }

            for (int i = 0; i < tagList.Count(); i++)
            {
                bllDZTag.AddTag(tagList[i], dzservice.Id.ToString(), dzservice.Business.Id.ToString(), dzservice.ServiceType.Id.ToString());
            }
            return servicesobj;
        }

        /// <summary>
        /// 删除服务 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public object DeleteService(string storeID, string serviceID)
        {
            Model.DZService dzservice = null;
            dzservice = bllDZService.GetService(utils.CheckGuidID(storeID, "storeID"), utils.CheckGuidID(serviceID, "serviceID"));
            if (dzservice == null)
            {
                throw new Exception("该服务不存在！");
            }
            bllDZService.Delete(dzservice);
            dzservice = bllDZService.GetService(utils.CheckGuidID(storeID, "storeID"), utils.CheckGuidID(serviceID, "serviceID"));
            if (dzservice == null)
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
