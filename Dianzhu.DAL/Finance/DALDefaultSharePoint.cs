using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
namespace Dianzhu.DAL.Finance
{
   public  class DALDefaultSharePoint:NHRepositoryBase<DefaultSharePoint,Guid>,IDAL.Finance.IDALDefaultSharePoint
    {
        
        

        public DefaultSharePoint GetDefaultSharePoint(Model.Enums.enum_UserType userType)
        {
            

            return FindOne(x => x.UserType == userType);

            
        }
    }
}
