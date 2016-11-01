using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.LocalStorage;
using Dianzhu.CSClient.ViewModel;
using Dianzhu.CSClient.Presenter.VMAdapter;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;

namespace Dianzhu.CSClient.Presenter
{
   public  class PSearch
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PSearch");
        IViewSearch viewSearch;
        IViewSearchResult viewSearchResult;
        IViewChatList viewChatList;
        IViewIdentityList viewIdentityList;
        IDAL.IDALDZService dalDzService;
        IBLLServiceOrder bllServiceOrder;
        PushService bllPushService;
        IInstantMessage iIM;
        IDAL.IDALServiceType dalServiceType;
        IList<DZService> SelectedServiceList;
        BLL.Common.SerialNo.ISerialNoBuilder serialNoBuilder;
        LocalStorage.LocalChatManager localChatManager;
        LocalStorage.LocalUIDataManager localUIDataManager;
        IVMChatAdapter vmChatAdapter;
        IVMIdentityAdapter vmIdentityAdapter;
        IDZMembershipService memberService;

        IReceptionService receptionService;

        #region 服务类型数据
        Dictionary<ServiceType, IList<ServiceType>> ServiceTypeCach;
        IList<ServiceType> ServiceTypeListTmp;
        ServiceType ServiceTypeFirst;
        ServiceType ServiceTypeSecond;
        ServiceType ServiceTypeThird;
        #endregion

        #region contructor
 
        public PSearch(IInstantMessage iIM, IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,
            IViewChatList viewChatList,IViewIdentityList viewIdentityList,
            IDAL.IDALDZService dalDzService, IBLLServiceOrder bllServiceOrder, IDAL.IDALServiceType dalServiceType,                     
                    PushService bllPushService, BLL.Common.SerialNo.ISerialNoBuilder serialNoBuilder, LocalStorage.LocalChatManager localChatManager, LocalStorage.LocalUIDataManager localUIDataManager, 
                    IVMChatAdapter vmChatAdapter,IVMIdentityAdapter vmIdentityAdapter, IDZMembershipService memberService, IReceptionService receptionService)
        {
            this.serialNoBuilder = serialNoBuilder;
            this.viewSearch = viewSearch; ;
            this.viewSearchResult = viewSearchResult;
            this.dalDzService = dalDzService;
            this.viewChatList = viewChatList;
            this.bllServiceOrder = bllServiceOrder;
            this.iIM = iIM;
            this.dalServiceType = dalServiceType;            
            this.bllPushService = bllPushService; 
            this.viewIdentityList = viewIdentityList;
            this.localChatManager = localChatManager;
            this.localUIDataManager = localUIDataManager;
            this.vmChatAdapter = vmChatAdapter;
            this.vmIdentityAdapter = vmIdentityAdapter;
            this.memberService = memberService;
            this.receptionService = receptionService;

            viewIdentityList.IdentityClick += ViewIdentityList_IdentityClick;

            viewSearch.Search += ViewSearch_Search;
            viewSearch.SaveUIData += ViewSearch_SaveUIData;

            LoadTypes();
 
            this.ServiceTypeFirst = new ServiceType();
            this.ServiceTypeSecond = new ServiceType();
            this.ServiceTypeThird = new ServiceType();

            viewSearchResult.SelectService += ViewSearchResult_SelectService;
            viewSearchResult.PushServices += ViewSearchResult_PushServices;
            viewSearchResult.FilterByBusinessName += ViewSearchResult_FilterByBusinessName; ;
            viewSearch.ServiceTypeFirst_Select += ViewSearch_ServiceTypeFirst_Select;
            viewSearch.ServiceTypeSecond_Select += ViewSearch_ServiceTypeSecond_Select;
            viewSearch.ServiceTypeThird_Select += ViewSearch_ServiceTypeThird_Select;
           
        }

        private void ViewIdentityList_IdentityClick(VMIdentity vmIdentity)
        {
            if (vmIdentity != null)
            {
                string id = vmIdentity.CustomerId.ToString();
                localUIDataManager.InitUIData(id);
                viewSearch.ServiceCustomerName = localUIDataManager.LocalUIDatas[id].Name;
                viewSearch.SearchKeywordTime = localUIDataManager.LocalUIDatas[id].Date;
                if (localUIDataManager.LocalUIDatas[id].ServiceType != null)
                {
                    if (localUIDataManager.LocalUIDatas[id].ServiceType.DeepLevel == 2)
                    {
                        ServiceType typeF = localUIDataManager.LocalUIDatas[id].ServiceType.Parent.Parent;
                        ServiceType typeS = localUIDataManager.LocalUIDatas[id].ServiceType.Parent;
                        ServiceType typeT = localUIDataManager.LocalUIDatas[id].ServiceType;

                        viewSearch.setServiceTypeFirst = typeF;
                        viewSearch.setServiceTypeSecond = typeS;
                        viewSearch.setServiceTypeThird = typeT;
                    }
                    else if (localUIDataManager.LocalUIDatas[id].ServiceType.DeepLevel == 1)
                    {
                        viewSearch.setServiceTypeFirst = localUIDataManager.LocalUIDatas[id].ServiceType.Parent;
                        viewSearch.setServiceTypeSecond = localUIDataManager.LocalUIDatas[id].ServiceType;
                    }
                    else if (localUIDataManager.LocalUIDatas[id].ServiceType.DeepLevel == 0)
                    {
                        viewSearch.setServiceTypeFirst = localUIDataManager.LocalUIDatas[id].ServiceType;
                    }
                }
                else
                {
                    viewSearch.setServiceTypeFirst = ServiceTypeFirst;
                }
                viewSearch.ServiceName = localUIDataManager.LocalUIDatas[id].ServiceName;
                viewSearch.ServiceTargetPriceMin = localUIDataManager.LocalUIDatas[id].PriceMin;
                viewSearch.ServiceTargetPriceMax = localUIDataManager.LocalUIDatas[id].PriceMax;
                viewSearch.ServiceCustomerPhone = localUIDataManager.LocalUIDatas[id].Phone;
                viewSearch.UnitAmount = localUIDataManager.LocalUIDatas[id].Amount;
                viewSearch.ServiceTargetAddress = localUIDataManager.LocalUIDatas[id].Address;
                viewSearch.ServiceMemo = localUIDataManager.LocalUIDatas[id].Memo;
                viewSearch.ServiceTargetAddressObj = localUIDataManager.LocalUIDatas[id].TargetAddressObj;
            }
        }

        private void ViewSearch_SaveUIData(string key, object value)
        {
            if (IdentityManager.CurrentIdentity != null)
            {
                localUIDataManager.Save(IdentityManager.CurrentIdentity.CustomerId, key, value);
            }
        }

        private void ViewSearchResult_FilterByBusinessName(string businessName)
        {
            if (SelectedServiceList != null)
            {
                IList<DZService> list = new List<DZService>();
                foreach (DZService item in SelectedServiceList)
                {
                    if (item.Business.Name.Contains(businessName))
                    {
                        list.Add(item);
                    }
                }

                viewSearchResult.SearchedService = list; 
            }
        }

        private void LoadTypes()
        {
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();
                //Action ac = () =>
                //{
                System.Threading.Thread.Sleep(1000);
                if (this.ServiceTypeListTmp != null) { return; }


                this.ServiceTypeListTmp = dalServiceType.GetTopList();
                this.ServiceTypeCach = new Dictionary<ServiceType, IList<ServiceType>>();

                foreach (ServiceType t in ServiceTypeListTmp)
                {
                    if (!ServiceTypeCach.ContainsKey(t))
                    {
                        ServiceTypeCach.Add(t, null);
                    }
                }
                viewSearch.ServiceTypeFirst = ServiceTypeListTmp;
            }
            catch (Exception ee)
            {
                log.Error(ee.ToString());
            }
            finally
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);            
        }
        private void ViewSearch_ServiceTypeThird_Select(ServiceType type)
        {
            ServiceTypeThird = type;
            if (IdentityManager.CurrentIdentity != null)
            {
                localUIDataManager.Save(IdentityManager.CurrentIdentity.CustomerId, "ServiceType", type);
            }
        }

        private void ViewSearch_ServiceTypeSecond_Select(ServiceType type)
        {
            //NHibernateUnitOfWork.UnitOfWork.Start();
            //Action ac = () =>
            //{
            try
            {
                if (type != null)
                {

                    //if (NHibernateUnitOfWork.UnitOfWork.IsStarted)
                    //{

                    //    NHibernateUnitOfWork.UnitOfWork.CurrentSession.Refresh(type);
                    //}
                    ServiceTypeSecond = type;
                    ServiceTypeThird = null;
                    if (!ServiceTypeCach.ContainsKey(type))
                    {
                        bool isSecondTypeStart = false;
                        if (!NHibernateUnitOfWork.UnitOfWork.IsStarted)
                        {
                            NHibernateUnitOfWork.UnitOfWork.Start();
                            isSecondTypeStart = true;
                        }
                        //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                        //NHibernateUnitOfWork.UnitOfWork.Start();
                        ServiceTypeCach[type] = type.Children;
                        if (isSecondTypeStart)
                        {
                            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
                        }
                        if (IdentityManager.CurrentIdentity != null)
                        {
                            localUIDataManager.Save(IdentityManager.CurrentIdentity.CustomerId, "ServiceType", type);
                        }
                    }
                    viewSearch.ServiceTypeThird = ServiceTypeCach[type];

                }
            }
            catch (Exception e)
            {
                PHSuit.ExceptionLoger.ExceptionLog(log, e);
            }
            //};
            //if (NHibernateUnitOfWork.UnitOfWork.IsStarted)
            //{
            //    ac();
            //}
            //else
            //{
            //    NHibernateUnitOfWork.With.Transaction(ac);
            //}
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        private void ViewSearch_ServiceTypeFirst_Select(ServiceType type)
        {

            //Action ac = () =>
            //{
                if (type != null)
            {
                    //NHibernateUnitOfWork.UnitOfWork.CurrentSession.Refresh(type);

                    ServiceTypeFirst = type;
                ServiceTypeSecond = null;
                ServiceTypeThird = null;
                if (ServiceTypeCach[type] == null)
                {
                    NHibernateUnitOfWork.UnitOfWork.Start();
                    ServiceTypeCach[type] = type.Children;
                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                    NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
                }
                viewSearch.ServiceTypeSecond = ServiceTypeCach[type];
            }
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);

        }

        private ServiceOrder ViewSearchResult_PushServices(IList<Model.DZService> pushedServices,out string errorMsg)
        {
            errorMsg = string.Empty;
            SearchObj searchObj;
            if (IdentityManager.CurrentIdentity == null)
            {
                errorMsg = "请选择用户后推送";
                return null;
            }
            if (localUIDataManager.LocalSearchTempObj.ContainsKey(IdentityManager.CurrentIdentity.CustomerId))
            {
                searchObj = localUIDataManager.LocalSearchTempObj[IdentityManager.CurrentIdentity.CustomerId];
            }
            else
            {
                errorMsg = "订单已过期，请重新搜索";
                return null;
            }
            if (string.IsNullOrEmpty(viewSearch.ServiceCustomerName))
            {
                errorMsg = "服务对象不能为空";
                return null;
            }
            if (string.IsNullOrEmpty(viewSearch.ServiceCustomerPhone))
            {
                errorMsg = "联系电话不能为空";
                return null;
            }
            if (searchObj.TargetTime < DateTime.Now)
            {
                errorMsg = "订单已过期，请重新搜索";
                return null;
            }
            if (pushedServices.Count == 0)
            {
                errorMsg = "服务已过期，请重新搜索";
                return null;
            }

            //禁用推送按钮
            //viewSearchResult.BtnPush = false;

            //NHibernateUnitOfWork.UnitOfWork.Start();
            IList<ServiceOrderPushedService> serviceOrderPushedServices = new List<ServiceOrderPushedService>();
            foreach (DZService service in pushedServices)
            {
                //NHibernateUnitOfWork.UnitOfWork.Current.Refresh(service);//来自上个session，需刷新

                serviceOrderPushedServices.Add(new ServiceOrderPushedService(IdentityManager.CurrentIdentity,service,viewSearch.UnitAmount, viewSearch.ServiceCustomerName, viewSearch.ServiceCustomerPhone, searchObj.Address, searchObj.TargetTime, viewSearch.ServiceMemo ));
            }
            bllPushService.Push(IdentityManager.CurrentIdentity, serviceOrderPushedServices, viewSearch.ServiceTargetAddress, viewSearch.SearchKeywordTime);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

            //获取之前orderid
            ServiceOrder oldOrder = bllServiceOrder.GetOne(IdentityManager.CurrentIdentity.Id);
            string serialNoForOrder = serialNoBuilder.GetSerialNo("FW" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),2);
            oldOrder.SerialNo = serialNoForOrder;
            oldOrder.OrderStatus = Model.Enums.enum_OrderStatus.DraftPushed;
            oldOrder.CustomerServiceId = GlobalViables.CurrentCustomerService.Id.ToString();
            bllServiceOrder.Update(oldOrder);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

            
            //ReceptionChatFactory chatFactory = new ReceptionChatFactory(Guid.NewGuid(), GlobalViables.CurrentCustomerService.Id.ToString(),
            //    IdentityManager.CurrentIdentity.Customer.Id.ToString(),"推送的服务",IdentityManager.CurrentIdentity.Id.ToString(), Model.Enums.enum_XmppResource.YDBan_CustomerService, Model.Enums.enum_XmppResource.YDBan_User);
            
            IList<Ydb.InstantMessage.DomainModel.Chat.PushedServiceInfo> pushedServiceInfos = new List<Ydb.InstantMessage.DomainModel.Chat.PushedServiceInfo>();
            foreach (var pushedService in serviceOrderPushedServices)
            {
                MemberDto businessDto = memberService.GetUserById(pushedService.OriginalService.Business.OwnerId.ToString());

                Ydb.InstantMessage.DomainModel.Chat.PushedServiceInfo psi = new Ydb.InstantMessage.DomainModel.Chat.PushedServiceInfo(
                    pushedService.OriginalService.Id.ToString(),
                    pushedService.ServiceName,
                    pushedService.OriginalService.ServiceType.ToString(),
                    pushedService.TargetTime.ToString(),
                    string.Empty,
                    pushedService.OriginalService.Business.OwnerId.ToString(),
                    businessDto.NickName,
                    businessDto.AvatarUrl
                    );
                pushedServiceInfos.Add(psi);
            }
            //ReceptionChat chat = chatFactory.CreateChatPushService(pushedServiceInfos);

            log.Debug("推送的订单：" + IdentityManager.CurrentIdentity.Id.ToString());

            //iim发送消息
            Guid messageId = Guid.NewGuid();
            iIM.SendMessagePushService(messageId, pushedServiceInfos, "推送的服务", IdentityManager.CurrentIdentity.CustomerId, "YDBan_User", IdentityManager.CurrentIdentity.Id.ToString());



            //助理工具显示发送的消息
            //VMChat vmChat = vmChatAdapter.ChatToVMChat(chat);
            VMChatPushServie vmChatPushService = new VMChatPushServie(
                pushedServices[0].Name,
                true,
                pushedServices[0].Business.BusinessAvatar.ImageName,
                5,
                pushedServices[0].UnitPrice,
                pushedServices[0].DepositAmount,
                pushedServices[0].Description,
                messageId.ToString(),
                GlobalViables.CurrentCustomerService.Id.ToString(),
                GlobalViables.CurrentCustomerService.NickName,
                IdentityManager.CurrentIdentity.CustomerId,
                DateTime.Now,
                (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds,
                "pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png",
                string.Empty,
                "#b3d465",
                true);
            viewChatList.AddOneChat(vmChatPushService);
            //存储消息到内存中
            localChatManager.Add(vmChatPushService.ToId, vmChatPushService);

            //生成新的草稿单并发送给客户端
            MemberDto newCS = memberService.GetUserById(GlobalViables.CurrentCustomerService.Id.ToString());//  NHibernateUnitOfWork.UnitOfWork.CurrentSession.Merge(GlobalViables.CurrentCustomerService);
            MemberDto newC = memberService.GetUserById(IdentityManager.CurrentIdentity.CustomerId);//  NHibernateUnitOfWork.UnitOfWork.CurrentSession.Merge(GlobalViables.CurrentCustomerService);
            ServiceOrder newOrder = new ServiceOrder()
            {
                CustomerId = IdentityManager.CurrentIdentity.CustomerId,
                CustomerServiceId = GlobalViables.CurrentCustomerService.Id.ToString()
            };
            bllServiceOrder.Save(newOrder);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();           

            //更新当前订单
            IdentityTypeOfOrder type;
            log.Debug("更新当前界面的订单");
            IdentityManager.UpdateIdentityList(newOrder, out type);
            log.Debug("当前订单的id：" + IdentityManager.CurrentIdentity.Id.ToString());

            //更新view
            VMIdentity vmIdentityNew = vmIdentityAdapter.OrderToVMIdentity(IdentityManager.CurrentIdentity, localChatManager.LocalCustomerAvatarUrls[IdentityManager.CurrentIdentity.CustomerId]);
            viewIdentityList.UpdateIdentityBtnName(oldOrder.Id, vmIdentityNew);

            //更新接待分配表
            log.Debug("更新ReceptionStatus，customerId:" + IdentityManager.CurrentIdentity.CustomerId + ",csId:" + GlobalViables.CurrentCustomerService.Id + ",orderId:" + newOrder.Id);
            //bllReceptionStatus.UpdateOrder(IdentityManager.CurrentIdentity.Customer, GlobalViables.CurrentCustomerService, newOrder);
            receptionService.UpdateOrderId(IdentityManager.CurrentIdentity.CustomerId, GlobalViables.CurrentCustomerService.Id.ToString(), newOrder.Id.ToString());

            //清空搜索选项 todo:为了测试方便，先注释掉
            //viewSearch.ClearData();
            //发送订单通知.

            return newOrder;

            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.Current.Dispose();
        }

        private void ViewSearchResult_SelectService(Model.DZService selectedService)
        {
            if (IdentityManager.CurrentIdentity == null)
            {
                
                return;
            }
            IdentityManager.CurrentIdentity.AddDetailFromIntelService(selectedService, viewSearch.UnitAmount,string.Empty,string.Empty, "实施服务的地点", DateTime.Now,string.Empty);
            //viewOrder.Order = IdentityManager.CurrentIdentity;
            bllServiceOrder.Update(IdentityManager.CurrentIdentity);

            

        }
        #endregion
        
        private void ViewSearch_Search(DateTime targetTime, decimal minPrice, decimal maxPrice, Guid servieTypeId,string name,string lng,string lat)
        {
            SearchObj searchObj = new SearchObj(name, minPrice, maxPrice, servieTypeId, targetTime, double.Parse(lng), double.Parse(lat), viewSearch.ServiceTargetAddress);
            if (IdentityManager.CurrentIdentity != null)
            {
                localUIDataManager.SaveSearchObj(IdentityManager.CurrentIdentity.CustomerId, searchObj);
            }
            
            //Action a = () =>
            //{
            int total;

            IList<DZService> services = dalDzService.SearchService(name, minPrice, maxPrice, servieTypeId, targetTime, double.Parse(lng), double.Parse(lat), 0, 999, out total);
            //foreach (DZService service in services)
            //{

            //}
            viewSearchResult.SearchedService = services;
            SelectedServiceList = services;

            //NHibernateUnitOfWork.With.Transaction(a);

            //foreach (DZService s in services)
            //{

            //}
            //if (services.Count > 0)
            //{
            //    //启用推送按钮
            //    viewSearchResult.BtnPush = true;
            //}
        }
    }
}
