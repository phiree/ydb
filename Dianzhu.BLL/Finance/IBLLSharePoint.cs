using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model.Finance;
using Dianzhu.DAL.Finance;
namespace Dianzhu.BLL.Finance
{
    
    public interface IBLLSharePoint
    {

          void Save(DefaultSharePoint defaultSharePoint);
          decimal GetSharePoint(Model.DZMembership member);
          IList<Dianzhu.Model.Finance.DefaultSharePoint> GetAll();
          DefaultSharePoint GetOne(Guid id);
    }
}
