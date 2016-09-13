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
            int c = 0;
            foreach (Model.Enums.enum_PayType item in Enum.GetValues(typeof(Model.Enums.enum_PayType)))
            {
                if ((dzservice.AllowedPayType & item) == item && item != Model.Enums.enum_PayType.None)
                {
                    servicesobj.eSupportPayWay = item.ToString();
                    c++;
                }
            }
            if (c == Enum.GetValues(typeof(Model.Enums.enum_PayType)).Length-1)
            {
                servicesobj.eSupportPayWay = "all";
            }
            //if (dzservice.ServiceType != null)
            //{
            //servicesobj.serviceType.serviceTypeID = dzservice.ServiceType.Id.ToString();
            //servicesobj.serviceType.fullDescription = dzservice.ServiceType.ToString();
            //servicesobj.serviceType.superID = dzservice.ServiceType.ParentId.ToString();
            //}
        }

        /// <summary>
        /// 新建服务
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="servicesobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public servicesObj PostService(string storeID, servicesObj servicesobj,Customer customer)
        {
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("storeID不能为空！");
            }
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(utils.CheckGuidID(storeID, "storeID"), utils.CheckGuidID(customer.UserID, "customer.UserID"));
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


            decimal dec = 0;
            if (!string.IsNullOrEmpty(servicesobj.startAt))
            {
                if (!decimal.TryParse(servicesobj.startAt, out dec))
                {
                    throw new Exception("起步价格式不正确！");
                }
            }
            if (!string.IsNullOrEmpty(servicesobj.unitPrice))
            {
                if (!decimal.TryParse(servicesobj.unitPrice, out dec))
                {
                    throw new Exception("单价格式不正确！");
                }
            }
            if (!string.IsNullOrEmpty(servicesobj.deposit))
            {
                if (!decimal.TryParse(servicesobj.deposit, out dec))
                {
                    throw new Exception("订金格式不正确！");
                }
            }
            int intNum = 0;
            if (!string.IsNullOrEmpty(servicesobj.appointmentTime))
            {
                if (!int.TryParse(servicesobj.appointmentTime, out intNum))
                {
                    throw new Exception("提前预约的时间值格式不正确！");
                }
            }

            Model.DZService dzservice = new Model.DZService();
            //从字符串转枚举：AEnumType a = (AEnumType)Enum.Parse(typeof(AEnumType), “flag”); 可能失败，代码应包含异常处理机制。
            //可用Enum.IsDefined()检查一个值是否包含在一个枚举中。
            if (!string.IsNullOrEmpty(servicesobj.eSupportPayWay))
            {
                int ee = 0;
                try
                {
                    int allNum = dzservice.GetAllPayTypeNum;
                    if (int.TryParse(servicesobj.eSupportPayWay, out ee))
                    { }
                    else
                    {
                        if (servicesobj.eSupportPayWay.ToLower() == "all")
                        {
                            ee = allNum;
                        }
                        else
                        {
                            ee = (int)(Model.Enums.enum_PayType)Enum.Parse(typeof(Model.Enums.enum_PayType), servicesobj.eSupportPayWay);
                        }
                    }
                    if (ee > allNum || ee==4)
                    {
                        throw new Exception("该支付方式不存在！");
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            if (!string.IsNullOrEmpty(servicesobj.maxCount))
            {
                if (!int.TryParse(servicesobj.maxCount, out intNum))
                {
                    throw new Exception("最大接单量格式不正确！");
                }
            }
            


            dzservice = Mapper.Map<servicesObj, Model.DZService>(servicesobj);
            if (!string.IsNullOrEmpty(servicesobj.chargeUnit))
            {
                dzservice.ChargeUnitFriendlyName = servicesobj.chargeUnit;
            }
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
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("storeID不能为空！");
            }
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Model.Business business = bllBusiness.GetOne(guidStore);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
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
            dzservice = bllDZService.GetServices(filter1, utils.CheckGuidID(servicefilter.serviceTypeID, "type的ID"), servicefilter.name, servicefilter.introduce, dcStartAt, guidStore);
            if (dzservice == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<servicesObj>();
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
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("storeID不能为空！");
            }
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Model.Business business = bllBusiness.GetOne(guidStore);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            decimal dcStartAt = -1;
            if (servicefilter.startAt != null && servicefilter.startAt != "")
            {
                if (!decimal.TryParse(servicefilter.startAt, out dcStartAt))
                {
                    throw new FormatException("起步价必须为大于零的数值！");
                }
            }
            countObj c = new countObj();
            c.count = bllDZService.GetServicesCount(utils.CheckGuidID(servicefilter.serviceTypeID, "type的ID"), servicefilter.name, servicefilter.introduce, dcStartAt, guidStore).ToString();
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
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("storeID不能为空！");
            }
            if (string.IsNullOrEmpty(serviceID))
            {
                throw new FormatException("serviceID不能为空！");
            }
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Model.Business business = bllBusiness.GetOne(guidStore);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            Model.DZService dzservice = bllDZService.GetService(guidStore, utils.CheckGuidID(serviceID, "serviceID"));
            if (dzservice == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                //return null;
                throw new Exception("没有找到资源！");
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
        /// <param name="customer"></param>
        /// <returns></returns>
        public servicesObj PatchService(string storeID, string serviceID, servicesObj servicesobj,Customer customer)
        {
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("storeID不能为空！");
            }
            if (string.IsNullOrEmpty(serviceID))
            {
                throw new FormatException("serviceID不能为空！");
            }
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Guid guidService = utils.CheckGuidID(serviceID, "serviceID");
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, utils.CheckGuidID(customer.UserID, "customer.UserID"));
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
                int ee = 0;
                try
                {
                    int allNum = dzserviceobj.GetAllPayTypeNum;
                    if (int.TryParse(servicesobj.eSupportPayWay, out ee))
                    { }
                    else
                    {
                        if (servicesobj.eSupportPayWay.ToLower() == "all")
                        {
                            ee = allNum;
                        }
                        else
                        {
                            ee = (int)(Model.Enums.enum_PayType)Enum.Parse(typeof(Model.Enums.enum_PayType), servicesobj.eSupportPayWay);
                        }
                    }
                    if (ee > allNum || ee == 4)
                    {
                        throw new Exception("该支付方式不存在！");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                dzserviceobj.AllowedPayType = (Model.Enums.enum_PayType)ee;
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
                dzserviceobj.ChargeUnitFriendlyName = servicesobj.chargeUnit;
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
        /// <param name="customer"></param>
        /// <returns></returns>
        public object DeleteService(string storeID, string serviceID,Customer customer)
        {
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("storeID不能为空！");
            }
            if (string.IsNullOrEmpty(serviceID))
            {
                throw new FormatException("serviceID不能为空！");
            }
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Guid guidService = utils.CheckGuidID(serviceID, "serviceID");
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            Model.DZService dzservice = bllDZService.GetService(guidStore, guidService);
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
            return new string[] { "删除成功！" };
            //}
            //else
            //{
            //    throw new Exception("删除失败！");
            //}
        }

        /// <summary>
        /// 查询 superID 的下级服务类型列表数组,当 superID 为空时，默认查询顶层服务类型列表
        /// </summary>
        /// <param name="superID"></param>
        /// <returns></returns>
        public IList<serviceTypeObj> GetAllServiceTypes(string superID)
        {
            IList < Model.ServiceType> servicetype = bllServiceType.GetAllServiceTypes(utils.CheckGuidID(superID, "superID"));
            if (servicetype == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<serviceTypeObj>();
            }
            IList<serviceTypeObj> servicetypeobj = Mapper.Map<IList<Model.ServiceType>, IList<serviceTypeObj>>(servicetype);
            return servicetypeobj;
        }
    }
}
