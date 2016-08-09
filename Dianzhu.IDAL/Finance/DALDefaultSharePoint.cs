using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.IDAL.Finance
{
    public interface IDALDefaultSharePoint:IRepository<Dianzhu.Model.Finance.DefaultSharePoint,Guid>
    {

        Dianzhu.Model.Finance.DefaultSharePoint GetDefaultSharePoint(Model.Enums.enum_UserType userType);
    }
}
