using System;
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
        IView.IViewSearch viewSearch;
        IView.IViewSearchResult viewSearchResult;
        IViewOrder viewOrder;
        DAL.DALDZService dalService;
        DAL.DALServiceOrder dalOrder;
        BLL.PushService bllPushService;
        IInstantMessage.InstantMessage iIM;
        BLL.BLLReceptionChat bllReceptionChat;
        #region contructor
        public PSearch(IInstantMessage.InstantMessage iIM, IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,IViewOrder viewOrder)
            : this(iIM,viewSearch, viewSearchResult,viewOrder, new DAL.DALDZService(),new DAL.DALServiceOrder(),new BLL.PushService(),new BLL.BLLReceptionChat())
        { }
        public PSearch(IInstantMessage.InstantMessage iIM, IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,
            IView.IViewOrder viewOrder,DAL.DALDZService dalService,DAL.DALServiceOrder dalOrder,BLL.PushService bllPushService,BLL.BLLReceptionChat bllReceptionChat)
        {
            this.viewSearch = viewSearch; ;
            this.viewSearchResult = viewSearchResult;
            this.dalService = dalService;
            this.viewOrder = viewOrder;
            this.dalOrder = dalOrder;
            this.iIM = iIM;
            this.bllReceptionChat = bllReceptionChat;
            viewSearch.Search += ViewSearch_Search;
            this.bllPushService = bllPushService;
            viewSearchResult.SelectService += ViewSearchResult_SelectService;
            viewSearchResult.PushServices += ViewSearchResult_PushServices;
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
                serviceOrderPushedServices.Add(new ServiceOrderPushedService(IdentityManager.CurrentIdentity,service,1,viewSearchResult.TargetAddress,viewSearchResult.TargetTime ));
            }
            bllPushService.Push(IdentityManager.CurrentIdentity, serviceOrderPushedServices, viewSearchResult.TargetAddress, viewSearchResult.TargetTime);

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
        private void ViewSearch_Search()
        {
            int total;
          IList<Model.DZService> services=  dalService.SearchService(viewSearch.SearchKeyword, 0, 10, out total);
            viewSearchResult.SearchedService = services;
        }
    }
}
