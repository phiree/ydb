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
        /// 对象处理
        /// </summary>
        /// <param name="servicesobj"></param>
        /// <param name="dzservice"></param>
        void changeObj(servicesObj servicesobj, Model.DZService dzservice)
        {
            servicesobj.location.longitude = dzservice.Business.Longitude.ToString();
            servicesobj.location.latitude = dzservice.Business.Latitude.ToString();
            servicesobj.location.address = dzservice.Business.RawAddressFromMapAPI == null ? "" : dzservice.Business.RawAddressFromMapAPI;
            if (dzservice.ServiceType != null)
            {
                //servicesobj.serviceType.serviceTypeID = dzservice.ServiceType.Id.ToString();
                servicesobj.serviceType.fullDescription = dzservice.ServiceType.ToString();
                //servicesobj.serviceType.superID = dzservice.ServiceType.ParentId.ToString();
            }
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
            //Model.Business business = bllBusiness.GetBusinessByIdAndOwner(utils.CheckGuidID(storeID, "storeID"), guidUser);
            Model.Business business = bllBusiness.GetOne(utils.CheckGuidID(storeID, "storeID"));
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }

            //待定是否只传ID过来
            //string[] typeList = servicesobj.type.Split('>');
            //int typeLevel = typeList.Count() > 0 ? typeList.Count() - 1 : 0;
            //Model.ServiceType sType = bllServiceType.GetOneByName(typeList[typeLevel], typeLevel);
            Guid guidServiceType = utils.CheckGuidID(servicesobj.serviceType.id, "type的ID");
            Model.ServiceType sType = bllServiceType.GetOne(guidServiceType);
            if (sType == null)
            {
                throw new Exception("该服务类型有误！");
            }
            //待定是否只传ID过来

            Model.DZService dzservice = Mapper.Map<servicesObj, Model.DZService>(servicesobj);
            DateTime dt = new DateTime();
            dzservice.CreatedTime = dt;
            dzservice.LastModifiedTime = dt;
            dzservice.Business = business;
            dzservice.ServiceType = sType;
            dzservice.BusinessAreaCode = "{\"serPointCirle\":{\"lng\":110.31047,\"lat\":20.017031,\"radius\":\"1000\"},\"serPointComp\":{\"streetNumber\":\"\",\"street\":\"海濂路\",\"district\":\"龙华区\",\"city\":\"海口市\",\"province\":\"海南省\"},\"serPointAddress\":\"海南省海口市龙华区海濂路\"}";
            ValidationResult validationResult = new ValidationResult();
            bllDZService.SaveOrUpdate(dzservice, out validationResult);
            if (!validationResult.IsValid)
            {
                string strErrors = "[";
                for (int i = 0;i< validationResult.Errors.Count; i++)
                {
                    strErrors += "{";
                    strErrors += "ErrorCode:" + validationResult.Errors[i].ErrorCode + ",";
                    strErrors += "ErrorMessage:" +validationResult.Errors[i].ErrorMessage+"";
                    strErrors += "},";
                }
                strErrors.TrimEnd(',');
                strErrors += "]";
                throw new Exception(strErrors);
            }
            string[] tagList = servicesobj.tag.Split('|');
            //dzservice = bllDZService.GetOne(dzservice.Id);
            //if (dzservice != null && dzservice.CreatedTime == dt)
            //{
            servicesobj = Mapper.Map<Model.DZService, servicesObj>(dzservice);
            changeObj(servicesobj, dzservice);
            //}
            //else
            //{
            //    throw new Exception("新建失败");
            //}
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
            if (servicefilter.startAt != null && servicefilter.startAt != "")
            {
                if (!decimal.TryParse(servicefilter.startAt, out dcStartAt))
                {
                    throw new FormatException("起步价必须为大于零的数值！");
                }
            }
            dzservice = bllDZService.GetServices(filter1, utils.CheckGuidID(servicefilter.serviceTypeID, "type的ID"), servicefilter.name, servicefilter.introduce, dcStartAt, utils.CheckGuidID(storeID, "storeID"));
            if (dzservice == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<servicesObj> serviceobj = Mapper.Map<IList<Model.DZService>, IList<servicesObj>>(dzservice);
            for (int i = 0; i < serviceobj.Count; i++)
            {
                changeObj(serviceobj[i], dzservice[i]);
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
            if (servicefilter.startAt != null && servicefilter.startAt != "")
            {
                if (!decimal.TryParse(servicefilter.startAt, out dcStartAt))
                {
                    throw new FormatException("起步价必须为大于零的数值！");
                }
            }
            countObj c = new countObj();
            c.count = bllDZService.GetServicesCount(utils.CheckGuidID(servicefilter.serviceTypeID, "type的ID"), servicefilter.name, servicefilter.introduce, dcStartAt, utils.CheckGuidID(storeID, "storeID")).ToString();
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
            Model.DZService dzservice = bllDZService.GetService(utils.CheckGuidID(storeID, "storeID"), utils.CheckGuidID(serviceID, "serviceID"));
            if (dzservice == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            servicesObj servicesobj = Mapper.Map<Model.DZService, servicesObj>(dzservice);
            changeObj(servicesobj, dzservice);
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
            //Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidUser);
            Model.Business business = bllBusiness.GetOne(utils.CheckGuidID(storeID, "storeID"));
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            Model.DZService dzserviceobj = bllDZService.GetService(guidStore, guidService);
            if (dzserviceobj == null)
            {
                throw new Exception("该服务不存在！");
            }

            //Model.DZService dzservice1 = new Model.DZService();
            //dzserviceobj.CopyTo(dzservice1);
            //Model.DZService dzservice = Mapper.Map<servicesObj, Model.DZService>(servicesobj);
            if (string.IsNullOrEmpty(servicesobj.name) == false && servicesobj.name != dzserviceobj.Name)
            {
                dzserviceobj.Name = servicesobj.name;
            }
            if (!string.IsNullOrEmpty(servicesobj.serviceType.id))
            {
                //待定是否只传ID过来
                //string[] typeList = servicesobj.type.Split('>');
                //int typeLevel = typeList.Count() > 0 ? typeList.Count() - 1 : 0;
                //Model.ServiceType sType = bllServiceType.GetOneByName(typeList[typeLevel], typeLevel);
                Guid guidServiceType = utils.CheckGuidID(servicesobj.serviceType.id, "type的ID");
                Model.ServiceType sType = bllServiceType.GetOne(guidServiceType);
                if (sType == null)
                {
                    throw new Exception("该服务类型有误！");
                }
                dzserviceobj.ServiceType = sType;
            }
            if (string.IsNullOrEmpty(servicesobj.introduce) == false && servicesobj.introduce != dzserviceobj.Description)
            {
                dzserviceobj.Description = servicesobj.introduce;
            }
            decimal dec = 0;
            if (!string.IsNullOrEmpty(servicesobj.startAt))
            {
                if (!decimal.TryParse(servicesobj.startAt, out dec))
                {
                    throw new Exception("起步价格式不正确！");
                }
                dzserviceobj.MinPrice = dec;
            }
            if (!string.IsNullOrEmpty(servicesobj.unitPrice))
            {
                if (!decimal.TryParse(servicesobj.unitPrice, out dec))
                {
                    throw new Exception("单价格式不正确！");
                }
                dzserviceobj.UnitPrice = dec;
            }
            if (!string.IsNullOrEmpty(servicesobj.deposit))
            {
                if (!decimal.TryParse(servicesobj.deposit, out dec))
                {
                    throw new Exception("订金格式不正确！");
                }
                dzserviceobj.DepositAmount = dec;
            }
            int intNum = 0;
            if (!string.IsNullOrEmpty(servicesobj.appointmentTime))
            {
                if (!int.TryParse(servicesobj.appointmentTime, out intNum))
                {
                    throw new Exception("提前预约的时间值格式不正确！");
                }
                dzserviceobj.OrderDelay = intNum;
            }
            if (servicesobj.bDoorService)
            {
                dzserviceobj.ServiceMode = Model.Enums.enum_ServiceMode.ToHouse;
            }
            else
            {
                dzserviceobj.ServiceMode = Model.Enums.enum_ServiceMode.NotToHouse;
            }
            if (!string.IsNullOrEmpty(servicesobj.eServiceTarget))
            {
                dzserviceobj.IsForBusiness = servicesobj.eServiceTarget=="all";
            }
            //从字符串转枚举：AEnumType a = (AEnumType)Enum.Parse(typeof(AEnumType), “flag”); 可能失败，代码应包含异常处理机制。
            //可用Enum.IsDefined()检查一个值是否包含在一个枚举中。
            if (!string.IsNullOrEmpty(servicesobj.eSupportPayWay))
            {
                dzserviceobj.AllowedPayType = (Model.Enums.enum_PayType)Enum.Parse(typeof(Model.Enums.enum_PayType), servicesobj.eSupportPayWay);
            }
            dzserviceobj.Enabled = servicesobj.bOpen;
            if (!string.IsNullOrEmpty(servicesobj.maxCount))
            {
                if (!int.TryParse(servicesobj.maxCount, out intNum))
                {
                    throw new Exception("最大接单量格式不正确！");
                }
                dzserviceobj.MaxOrdersPerDay = intNum;
            }
            if (!string.IsNullOrEmpty(servicesobj.chargeUnit))
            {
                dzserviceobj.ChargeUnit = (Model.Enums.enum_ChargeUnit)Enum.Parse(typeof(Model.Enums.enum_ChargeUnit), servicesobj.chargeUnit);
            }
            DateTime dt = new DateTime();
            dzserviceobj.LastModifiedTime = dt;
            dzserviceobj.Business = business;
            //ValidationResult validationResult = new ValidationResult();
            //bllDZService.SaveOrUpdate(dzservice1, out validationResult);


            List<string> tagList=new List<string>();
            if (!string.IsNullOrEmpty(servicesobj.tag))
            {
                tagList = servicesobj.tag.Split('|').ToList();
            }
            for (int i = 0; i < tagList.Count(); i++)
            {
                bllDZTag.AddTag(tagList[i], dzserviceobj.Id.ToString(), dzserviceobj.Business.Id.ToString(), dzserviceobj.ServiceType.Id.ToString());
            }
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

            //dzservice = bllDZService.GetOne(dzservice.Id);
            //if (dzservice != null && dzservice.LastModifiedTime == dt)
            //{
            servicesobj = Mapper.Map<Model.DZService, servicesObj>(dzserviceobj);
            changeObj(servicesobj, dzserviceobj);
            //}
            //else
            //{
            //    throw new Exception("更新失败");
            //}
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
            dzservice.IsDeleted = true;
            dzservice.LastModifiedTime = DateTime.Now;
            //bllDZService.Delete(dzservice);
            //dzservice = bllDZService.GetService(utils.CheckGuidID(storeID, "storeID"), utils.CheckGuidID(serviceID, "serviceID"));
            //if (dzservice == null)
            //{
            return "删除成功！";
            //}
            //else
            //{
            //    throw new Exception("删除失败！");
            //}
        }
    }
}
