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
        #region contructor
        public PSearch(IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,IViewOrder viewOrder)
            : this(viewSearch, viewSearchResult,viewOrder, new DAL.DALDZService())
        { }
        public PSearch(IView.IViewSearch viewSearch, IView.IViewSearchResult viewSearchResult,IView.IViewOrder viewOrder,DAL.DALDZService dalService)
        {
            this.viewSearch = viewSearch; ;
            this.viewSearchResult = viewSearchResult;
            this.dalService = dalService;
            this.viewOrder = viewOrder;
            viewSearch.Search += ViewSearch_Search;
            viewSearchResult.SelectService += ViewSearchResult_SelectService;
        }

        private void ViewSearchResult_SelectService(Model.DZService selectedService)
        {
            IdentityManager.CurrentIdentity.AddDetailFromIntelService(selectedService, 1, "实施服务的地点", DateTime.Now);
            viewOrder.Order = IdentityManager.CurrentIdentity;

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
