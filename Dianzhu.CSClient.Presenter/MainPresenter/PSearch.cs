using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IView;
namespace Dianzhu.CSClient.Presenter
{
   public  class PSearch
    {
        IView.IViewSearch viewSearch;
        IView.IViewSearchResult viewSearchResult;
        IViewOrder viewOrder;
        DAL.DALDZService dalService;
        DAL.DALServiceOrder dalOrder;
        #region contructor
        public PSearch(IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,IViewOrder viewOrder)
            : this(viewSearch, viewSearchResult,viewOrder, new DAL.DALDZService(),new DAL.DALServiceOrder())
        { }
        public PSearch(IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,
            IView.IViewOrder viewOrder,DAL.DALDZService dalService,DAL.DALServiceOrder dalOrder)
        {
            this.viewSearch = viewSearch; ;
            this.viewSearchResult = viewSearchResult;
            this.dalService = dalService;
            this.viewOrder = viewOrder;
            this.dalOrder = dalOrder;
            viewSearch.Search += ViewSearch_Search;
            viewSearchResult.SelectService += ViewSearchResult_SelectService;
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
