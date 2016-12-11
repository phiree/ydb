using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.DomainModel;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 服务类别联动筛选控件
    /// </summary>
   public interface IViewTypeSelect
    {
        void Init(IList<ServiceType> initData);
        Guid SelectedTypeId { get ; }


    }
}
