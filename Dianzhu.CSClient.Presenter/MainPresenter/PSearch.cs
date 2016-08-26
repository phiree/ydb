using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
using Dianzhu.BLL;

namespace Dianzhu.CSClient.Presenter
{
   public  class PSearch
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PSearch");
        IViewSearch viewSearch;
        IViewSearchResult viewSearchResult;
        IViewOrder viewOrder;
        IViewChatList viewChatList;
        IViewIdentityList viewIdentityList;
        IDAL.IDALDZService dalDzService;
        IBLLServiceOrder bllServiceOrder;
        PushService bllPushService;
        IInstantMessage.InstantMessage iIM;
        IDAL.IDALReceptionChat dalReceptionChat;
        IDAL.IDALServiceType dalServiceType;
        BLLReceptionStatus bllReceptionStatus;
        IList<DZService> SelectedServiceList;
        BLL.Common.SerialNo.ISerialNoBuilder serialNoBuilder;
        #region 服务类型数据
        Dictionary<ServiceType, IList<ServiceType>> ServiceTypeCach;
        IList<ServiceType> ServiceTypeListTmp;
        ServiceType ServiceTypeFirst;
        ServiceType ServiceTypeSecond;
        ServiceType ServiceTypeThird;
        #endregion
        #region contructor
 
        public PSearch(IInstantMessage.InstantMessage iIM, IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,
            IView.IViewOrder viewOrder, IViewChatList viewChatList,IViewIdentityList viewIdentityList,
            IDAL.IDALDZService dalDzService, IBLLServiceOrder bllServiceOrder,IDAL.IDALReceptionChat dalReceptionChat, IDAL.IDALServiceType dalServiceType,                     
                    PushService bllPushService, BLLReceptionStatus bllReceptionStatus,BLL.Common.SerialNo.ISerialNoBuilder serialNoBuilder)
        {
            this.serialNoBuilder = serialNoBuilder;
            this.viewSearch = viewSearch; ;
            this.viewSearchResult = viewSearchResult;
            this.dalDzService = dalDzService;
            this.viewOrder = viewOrder;
            this.viewChatList = viewChatList;
            this.bllServiceOrder = bllServiceOrder;
            this.iIM = iIM;
            this.dalReceptionChat = dalReceptionChat;
            this.dalServiceType = dalServiceType;
            viewSearch.Search += ViewSearch_Search;
            this.bllPushService = bllPushService;
 
            this.bllReceptionStatus = bllReceptionStatus;
            this.viewIdentityList = viewIdentityList;
 
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
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }
        private void ViewSearch_ServiceTypeThird_Select(ServiceType type)
        {
            ServiceTypeThird = type;
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

                    }
                        viewSearch.ServiceTypeThird = ServiceTypeCach[type];

                    }
                }
                catch (Exception e)
                {
                    throw e;
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

        private ReceptionChat ViewSearchResult_PushServices(IList<Model.DZService> pushedServices,out string errorMsg)
        {
            errorMsg = string.Empty;
            if (pushedServices.Count == 0)
            {
                return null;
            }
            if (IdentityManager.CurrentIdentity == null)
            {
                return null;
            }
            if (viewSearch.SearchKeywordTime < DateTime.Now)
            {
                errorMsg = "订单已过期，请重新搜索";
                return null;
            }
            
            //禁用推送按钮
            //viewSearchResult.BtnPush = false;

            //NHibernateUnitOfWork.UnitOfWork.Start();
            IList<ServiceOrderPushedService> serviceOrderPushedServices = new List<ServiceOrderPushedService>();
            foreach (DZService service in pushedServices)
            {
                //NHibernateUnitOfWork.UnitOfWork.Current.Refresh(service);//来自上个session，需刷新

                serviceOrderPushedServices.Add(new ServiceOrderPushedService(IdentityManager.CurrentIdentity,service,viewSearch.UnitAmount, viewSearch.ServiceCustomerName, viewSearch.ServiceCustomerPhone, viewSearch.ServiceAddress, viewSearch.SearchKeywordTime ));
            }
           // bllPushService.Push(IdentityManager.CurrentIdentity, serviceOrderPushedServices, viewSearch.ServiceAddress, viewSearch.SearchKeywordTime);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

            //获取之前orderid
            ServiceOrder oldOrder = bllServiceOrder.GetOne(IdentityManager.CurrentIdentity.Id);
            oldOrder.OrderStatus = Model.Enums.enum_OrderStatus.DraftPushed;
            bllServiceOrder.Update(oldOrder);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

            //iim发送消息
            ReceptionChat chat = new ReceptionChatPushService
            {
                Id = Guid.NewGuid(),
                ServiceOrder =IdentityManager.CurrentIdentity,
                ChatTarget = Model.Enums.enum_ChatTarget.cer,
                From = GlobalViables.CurrentCustomerService,
                To = IdentityManager.CurrentIdentity.Customer,
                MessageBody = "推送的服务",
                PushedServices = serviceOrderPushedServices,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,

            };
            //dalReceptionChat.Add(chat);//openfire插件存储消息


            //加到缓存数组中
            //if (PChatList.chatHistoryAll != null)
            //{
            //    PChatList.chatHistoryAll[IdentityManager.CurrentIdentity.Customer.Id].Add(chat);
            //}

            log.Debug("推送的订单：" + IdentityManager.CurrentIdentity.Id.ToString());

            //助理工具显示发送的消息
            viewChatList.AddOneChat(chat);

            //生成新的草稿单并发送给客户端
            string serialNoForOrder = serialNoBuilder.GetSerialNo("FW" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            ServiceOrder newOrder = ServiceOrderFactory.CreateDraft(GlobalViables.CurrentCustomerService,IdentityManager.CurrentIdentity.Customer,serialNoForOrder);
            bllServiceOrder.Save(newOrder);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            log.Debug("新草稿订单的id：" + newOrder.Id.ToString());
            string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
            string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{0}"" from = ""{1}"">
                                                    <active xmlns = ""http://jabber.org/protocol/chatstates""></active><ext xmlns=""ihelper:notice:draft:new""><orderID>{3}</orderID></ext></message>", 
                                                    IdentityManager.CurrentIdentity.Customer.Id + "@" + server, IdentityManager.CurrentIdentity.CustomerService.Id, Guid.NewGuid() + "@" + server, newOrder.Id);
            iIM.SendMessage(noticeDraftNew);
            

            //更新当前订单
            IdentityTypeOfOrder type;
            log.Debug("更新当前界面的订单");
            IdentityManager.UpdateIdentityList(newOrder, out type);
            viewIdentityList.SetCustomerOrder(oldOrder.Id, newOrder.Id);
            log.Debug("当前订单的id：" + IdentityManager.CurrentIdentity.Id.ToString());

            //更新view
            viewIdentityList.UpdateIdentityBtnName(oldOrder.Id, IdentityManager.CurrentIdentity);

            //更新接待分配表
            bllReceptionStatus.UpdateOrder(IdentityManager.CurrentIdentity.Customer, GlobalViables.CurrentCustomerService, newOrder);

            //清空搜索选项 todo:为了测试方便，先注释掉
            //viewSearch.ClearData();
            //发送订单通知.

            return chat;
            iIM.SendMessage(chat);
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.Current.Dispose();
        }

        private void ViewSearchResult_SelectService(Model.DZService selectedService)
        {
            if (IdentityManager.CurrentIdentity == null)
            {
                
                return;
            }
            IdentityManager.CurrentIdentity.AddDetailFromIntelService(selectedService, viewSearch.UnitAmount,string.Empty,string.Empty, "实施服务的地点", DateTime.Now);
            viewOrder.Order = IdentityManager.CurrentIdentity;
            bllServiceOrder.Update(IdentityManager.CurrentIdentity);

            

        }
        #endregion
        private void ViewSearch_Search(DateTime targetTime, decimal minPrice, decimal maxPrice, Guid servieTypeId,string name)
        {
            //Action a = () =>
            //{
                int total;

                IList<DZService> services = dalDzService.SearchService(name, minPrice, maxPrice, servieTypeId, targetTime, 0, 10, out total);
                foreach (DZService service in services)
                {
                    
                }
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
