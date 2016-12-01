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
        IList<VMShelfService> SelectedServiceList;
        Ydb.Common.Infrastructure.ISerialNoBuilder serialNoBuilder;
        LocalChatManager localChatManager;
        IDZMembershipService memberService;

        IReceptionService receptionService;
        IViewTabContentTimer viewTabContentTimer;

        string identity = string.Empty;
        public IViewSearch ViewSearch
        {
            get
            {
                return viewSearch;
            }
        }

        public IViewSearchResult ViewSearchResult
        {
            get
            {
                return viewSearchResult;
            }
        }

        #region 服务类型数据
        Dictionary<ServiceType, IList<ServiceType>> ServiceTypeCach;
        IList<ServiceType> ServiceTypeListTmp;
        ServiceType ServiceTypeFirst;
        ServiceType ServiceTypeSecond;
        ServiceType ServiceTypeThird;
        #endregion

        #region contructor

        public PSearch(
            IInstantMessage iIM, IViewSearch viewSearch, IViewSearchResult viewSearchResult,
            IViewChatList viewChatList, IViewIdentityList viewIdentityList,
            IDAL.IDALDZService dalDzService, IBLLServiceOrder bllServiceOrder, IDAL.IDALServiceType dalServiceType,
                    PushService bllPushService, Ydb.Common.Infrastructure.ISerialNoBuilder serialNoBuilder,
                    LocalChatManager localChatManager, IDZMembershipService memberService, IReceptionService receptionService,
                    IViewTabContentTimer viewTabContentTimer, string identity)
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
            this.memberService = memberService;
            this.receptionService = receptionService;
            this.viewTabContentTimer = viewTabContentTimer;
            this.identity = identity;
            
            viewSearch.Search += ViewSearch_Search;

            LoadTypes();
 
            this.ServiceTypeFirst = new ServiceType();
            this.ServiceTypeSecond = new ServiceType();
            this.ServiceTypeThird = new ServiceType();
            
            viewSearchResult.PushServices += ViewSearchResult_PushServices;
            viewSearchResult.FilterByBusinessName += ViewSearchResult_FilterByBusinessName; ;
            viewSearch.ServiceTypeFirst_Select += ViewSearch_ServiceTypeFirst_Select;
            viewSearch.ServiceTypeSecond_Select += ViewSearch_ServiceTypeSecond_Select;
            viewSearch.ServiceTypeThird_Select += ViewSearch_ServiceTypeThird_Select;
           
        }

        private void ViewSearchResult_FilterByBusinessName(string businessName)
        {
            if (SelectedServiceList != null)
            {
                IList<VMShelfService> list = new List<VMShelfService>();
                foreach (var item in SelectedServiceList)
                {
                    if (item.BusinessName.Contains(businessName))
                    {
                        list.Add(item);
                    }
                }

                viewSearchResult.SearchedService = list; 
            }
        }

        #region 服务相关方法
        private void LoadTypes()
        {
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();

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
        }
        private void ViewSearch_ServiceTypeThird_Select(ServiceType type)
        {
            ServiceTypeThird = type;
        }

        private void ViewSearch_ServiceTypeSecond_Select(ServiceType type)
        {
            try
            {
                if (type != null)
                {
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

                        ServiceTypeCach[type] = type.Children;
                        if (isSecondTypeStart)
                        {
                            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
                        }
                    }
                    viewSearch.ServiceTypeThird = ServiceTypeCach[type];

                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        private void ViewSearch_ServiceTypeFirst_Select(ServiceType type)
        {
            if (type != null)
            {
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
        }
        #endregion

        private ServiceOrder ViewSearchResult_PushServices(IList<Guid> pushedServices,out string errorMsg)
        {
            errorMsg = string.Empty;

            if (string.IsNullOrEmpty(identity))
            {
                errorMsg = "请选择用户后推送";
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
            if (viewSearch.TargetTime < DateTime.Now)
            {
                errorMsg = "订单已过期，请重新搜索";
                return null;
            }
            if (pushedServices.Count == 0)
            {
                errorMsg = "服务已过期，请重新搜索";
                return null;
            }            

            IList<ServiceOrderPushedService> serviceOrderPushedServices = new List<ServiceOrderPushedService>();
            DZService service;
            ServiceOrder oldOrder = bllServiceOrder.GetOne(new Guid(IdentityManager.CurrentOrderId));

            //订单不是草稿单的话，需生成新的草稿单后进行推送
            if (oldOrder.OrderStatus != Model.Enums.enum_OrderStatus.Draft)
            {
                oldOrder = CreateDraftOrder(GlobalViables.CurrentCustomerService.Id.ToString(), identity);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            }

            foreach (var serviceId in pushedServices)
            {
                service = dalDzService.FindById(serviceId);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

                serviceOrderPushedServices.Add(new ServiceOrderPushedService(oldOrder, service,viewSearch.UnitAmount, viewSearch.ServiceCustomerName, viewSearch.ServiceCustomerPhone, viewSearch.ServiceTargetAddress, viewSearch.TargetTime, viewSearch.ServiceMemo ));
            }
            bllPushService.Push(oldOrder, serviceOrderPushedServices, viewSearch.ServiceTargetAddress, viewSearch.SearchKeywordTime);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

            //获取之前orderid
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

            log.Debug("推送的订单：" + oldOrder.Id);

            //iim发送消息
            Guid messageId = Guid.NewGuid();
            iIM.SendMessagePushService(messageId, pushedServiceInfos, "推送的服务", identity, "YDBan_User", oldOrder.Id.ToString());

            //启动计时器
            viewTabContentTimer.StartTimer();


            //助理工具显示发送的消息
            string imageUrl = string.IsNullOrEmpty(serviceOrderPushedServices[0].OriginalService.Business.BusinessAvatar.ImageName) ? string.Empty : Dianzhu.Config.Config.GetAppSetting("ImageHandler") + serviceOrderPushedServices[0].OriginalService.Business.BusinessAvatar.ImageName;
            VMChatPushServie vmChatPushService = new VMChatPushServie(
                serviceOrderPushedServices[0].ServiceName,
                true,
                imageUrl,
                5,
                serviceOrderPushedServices[0].UnitPrice,
                serviceOrderPushedServices[0].DepositAmount,
                serviceOrderPushedServices[0].Description,
                messageId.ToString(),
                GlobalViables.CurrentCustomerService.Id.ToString(),
                GlobalViables.CurrentCustomerService.DisplayName,
                identity,
                DateTime.Now,
                (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds,
                "pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png",
                string.Empty,
                "#b3d465",
                true);
            viewChatList.AddOneChat(vmChatPushService);

            //生成新的草稿单
            ServiceOrder newOrder = CreateDraftOrder(GlobalViables.CurrentCustomerService.Id.ToString(), identity);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

            //更新当前订单
            //IdentityTypeOfOrder type;
            log.Debug("更新当前界面的订单");
            //IdentityManager.UpdateIdentityList(newOrder, out type);
            IdentityTypeOfOrder type = IdentityManager.UpdateCustomerList(identity, newOrder.Id.ToString());
            log.Debug("当前订单的id：" + IdentityManager.CurrentOrderId);

            //更新view
            MemberDto cusomter = memberService.GetUserById(identity);
            VMIdentity vmIdentityNew = new VMIdentity(newOrder.Id.ToString(), identity,cusomter.DisplayName, localChatManager.LocalCustomerAvatarUrls[identity]);
            viewIdentityList.UpdateIdentityBtnName(identity, vmIdentityNew);

            //更新接待分配表
            log.Debug("更新ReceptionStatus，customerId:" + identity + ",csId:" + GlobalViables.CurrentCustomerService.Id + ",orderId:" + newOrder.Id);
            //bllReceptionStatus.UpdateOrder(IdentityManager.CurrentIdentity.Customer, GlobalViables.CurrentCustomerService, newOrder);
            receptionService.UpdateOrderId(identity, GlobalViables.CurrentCustomerService.Id.ToString(), newOrder.Id.ToString());

            //清空搜索选项 todo:为了测试方便，先注释掉
            //viewSearch.ClearData();
            //发送订单通知.

            return newOrder;
        }

        private ServiceOrder CreateDraftOrder(string csId, string customerId)
        {
            ServiceOrder newOrder = ServiceOrderFactory.CreateDraft(GlobalViables.CurrentCustomerService.Id.ToString(), identity);
            bllServiceOrder.Save(newOrder);

            return newOrder;
        }
        #endregion

        private void ViewSearch_Search(DateTime targetTime, decimal minPrice, decimal maxPrice, Guid servieTypeId,string name,string lng,string lat)
        {
            viewSearch.TargetTime = targetTime;

            int total;

            IList<DZService> services = dalDzService.SearchService(name, minPrice, maxPrice, servieTypeId, targetTime, double.Parse(lng), double.Parse(lat), 0, 999, out total);
            IList<VMShelfService> vmShelfServiceList = new List<VMShelfService>();
            VMShelfService vmShelfService;
            int num = 1;//用来显示查询出来的服务数量
            foreach (DZService service in services)
            {
                var opentimeObj = service.GetOpenTimeSnapShot(targetTime);
                string timeBegin = PHSuit.StringHelper.ConvertPeriodToTimeString(opentimeObj.PeriodBegin);
                string timeEnd = PHSuit.StringHelper.ConvertPeriodToTimeString(opentimeObj.PeriodEnd);
                string time = timeBegin + "-" + timeEnd;

                vmShelfService = new VMShelfService(service.Id, num, true, service.Business.Name, service.Name, 5, time, service.UnitPrice, service.DepositAmount);
                vmShelfServiceList.Add(vmShelfService);

                num++;
            }
            viewSearchResult.SearchedService = vmShelfServiceList;
            SelectedServiceList = vmShelfServiceList;
        }
    }
}
