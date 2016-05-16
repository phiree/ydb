﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
namespace Dianzhu.CSClient.Presenter
{
   public  class PSearch
    {
        IViewSearch viewSearch;
        IViewSearchResult viewSearchResult;
        IViewOrder viewOrder;
        IViewChatList viewChatList;
        DAL.DALDZService dalService;
        DAL.DALServiceOrder dalOrder;
        BLL.PushService bllPushService;
        IInstantMessage.InstantMessage iIM;
        BLL.BLLReceptionChat bllReceptionChat;
        BLL.BLLServiceType bllServcieType;
        #region 服务类型数据
        Dictionary<ServiceType, IList<ServiceType>> ServiceTypeCach;
        IList<ServiceType> ServiceTypeListTmp;
        ServiceType ServiceTypeFirst;
        ServiceType ServiceTypeSecond;
        ServiceType ServiceTypeThird;
        #endregion
        #region contructor
        public PSearch(IInstantMessage.InstantMessage iIM, IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,IViewOrder viewOrder,IViewChatList viewChatList)
            : this(iIM,viewSearch, viewSearchResult,viewOrder, viewChatList, new DAL.DALDZService(),new DAL.DALServiceOrder(),new BLL.PushService(),new BLL.BLLReceptionChat(),new BLL.BLLServiceType())
        { }
        public PSearch(IInstantMessage.InstantMessage iIM, IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,
            IView.IViewOrder viewOrder, IViewChatList viewChatList,DAL.DALDZService dalService,DAL.DALServiceOrder dalOrder,BLL.PushService bllPushService,BLL.BLLReceptionChat bllReceptionChat, BLL.BLLServiceType bllServcieType)
        {
            this.viewSearch = viewSearch; ;
            this.viewSearchResult = viewSearchResult;
            this.dalService = dalService;
            this.viewOrder = viewOrder;
            this.viewChatList = viewChatList;
            this.dalOrder = dalOrder;
            this.iIM = iIM;
            this.bllReceptionChat = bllReceptionChat;
            this.bllServcieType = bllServcieType;
            viewSearch.Search += ViewSearch_Search;
            this.bllPushService = bllPushService;

            this.ServiceTypeListTmp = bllServcieType.GetTopList();
            this.ServiceTypeCach = new Dictionary<ServiceType, IList<ServiceType>>();
            
            foreach (ServiceType t in ServiceTypeListTmp)
            {
                if (!ServiceTypeCach.ContainsKey(t))
                {
                    ServiceTypeCach.Add(t, null);
                }
            }
            viewSearch.ServiceTypeFirst = ServiceTypeListTmp;
            this.ServiceTypeFirst = new ServiceType();
            this.ServiceTypeSecond = new ServiceType();
            this.ServiceTypeThird = new ServiceType();

            viewSearchResult.SelectService += ViewSearchResult_SelectService;
            viewSearchResult.PushServices += ViewSearchResult_PushServices;
            viewSearch.ServiceTypeFirst_Select += ViewSearch_ServiceTypeFirst_Select;
            viewSearch.ServiceTypeSecond_Select += ViewSearch_ServiceTypeSecond_Select;
            viewSearch.ServiceTypeThird_Select += ViewSearch_ServiceTypeThird_Select;
        }

        private void ViewSearch_ServiceTypeThird_Select(ServiceType type)
        {
            ServiceTypeThird = type;
        }

        private void ViewSearch_ServiceTypeSecond_Select(ServiceType type)
        {
            if (type != null)
            {
                ServiceTypeSecond = type;
                ServiceTypeThird = null;
                if (!ServiceTypeCach.ContainsKey(type))
                {
                    ServiceTypeCach[type] = type.Children;
                }
                viewSearch.ServiceTypeThird = ServiceTypeCach[type];
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
                    ServiceTypeCach[type] = type.Children;
                }
                viewSearch.ServiceTypeSecond = ServiceTypeCach[type];
            }
        }

        private void ViewSearchResult_PushServices(IList<Model.DZService> pushedServices)
        {
            if (pushedServices.Count == 0)
            {
                return;
            }
            if (IdentityManager.CurrentIdentity == null)
            {
                return;
            }
            IList<ServiceOrderPushedService> serviceOrderPushedServices = new List<ServiceOrderPushedService>();
            foreach (DZService service in pushedServices)
            {
                serviceOrderPushedServices.Add(new ServiceOrderPushedService(IdentityManager.CurrentIdentity,service,1,viewSearch.ServiceAddress, viewSearch.SearchKeywordTime ));
            }
            bllPushService.Push(IdentityManager.CurrentIdentity, serviceOrderPushedServices, viewSearch.ServiceAddress, viewSearch.SearchKeywordTime);

            //iim发送消息
            ReceptionChat chat = new ReceptionChatPushService
            {
                ServiceOrder=IdentityManager.CurrentIdentity,
                ChatTarget = Model.Enums.enum_ChatTarget.cer,
                From = GlobalViables.CurrentCustomerService,
                To = IdentityManager.CurrentIdentity.Customer,
                MessageBody = "推送的服务",
                PushedServices = serviceOrderPushedServices,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,

            };
            bllReceptionChat.Save(chat);
            iIM.SendMessage(chat);

            //助理工具显示发送的消息
            viewChatList.AddOneChat(chat);

            //生成新的草稿单并发送给客户端
            ServiceOrder newOrder = ServiceOrderFactory.CreateDraft(GlobalViables.CurrentCustomerService,IdentityManager.CurrentIdentity.Customer);
            dalOrder.SaveOrUpdate(newOrder);
            string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
            string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{0}"" from = ""{1}"">
                                                    <active xmlns = ""http://jabber.org/protocol/chatstates""></active><ext xmlns=""ihelper:notice:draft:new""><orderID>{3}</orderID></ext></message>", 
                                                    IdentityManager.CurrentIdentity.Customer.Id + "@" + server, IdentityManager.CurrentIdentity.CustomerService.Id, Guid.NewGuid() + "@" + server, newOrder.Id);
            iIM.SendMessage(noticeDraftNew);

            //更新当前订单
            IdentityTypeOfOrder type;
            IdentityManager.UpdateIdentityList(newOrder, out type);

            //禁用推送按钮
            viewSearchResult.BtnPush = false;
            //清空搜索选项 todo:为了测试方便，先注释掉
            //viewSearch.ClearData();
        }

        private void ViewSearchResult_SelectService(Model.DZService selectedService)
        {
            if (IdentityManager.CurrentIdentity == null)
            {
                
                return;
            }
            IdentityManager.CurrentIdentity.AddDetailFromIntelService(selectedService, 1, "实施服务的地点", DateTime.Now);
            viewOrder.Order = IdentityManager.CurrentIdentity;
            dalOrder.Update(IdentityManager.CurrentIdentity);

            

        }
        #endregion
        private void ViewSearch_Search(DateTime targetTime, decimal minPrice, decimal maxPrice, Guid servieTypeId)
        {
            int total;
           
            IList<Model.DZService> services = dalService.SearchService(minPrice,maxPrice, servieTypeId,targetTime,  0, 10, out total);
            viewSearchResult.SearchedService = services;
            if (services.Count > 0)
            {
                //启用推送按钮
                viewSearchResult.BtnPush = true;
            }
        }
    }
}
