using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model.Finance;
using Dianzhu.DAL.Finance;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.BLL.Finance
{
    
    public interface IBLLSharePoint
    {

          void Save(DefaultSharePoint defaultSharePoint);
          decimal GetSharePoint(MemberDto member);
          IList<Dianzhu.Model.Finance.DefaultSharePoint> GetAll();
          DefaultSharePoint GetOne(Guid id);
    }
}
