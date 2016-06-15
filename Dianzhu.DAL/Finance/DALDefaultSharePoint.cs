using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
namespace Dianzhu.DAL.Finance
{
   public  class DALDefaultSharePoint:DALBase<Model.Finance.DefaultSharePoint>
    {
        
        public DALDefaultSharePoint(string fortest) : base(fortest) { }
        public DALDefaultSharePoint() { }

        public DefaultSharePoint GetDefaultSharePoint(Model.Enums.enum_UserType userType)
        {
            string query = "select s from DefaultSharePoint s where s.UserType=" + (int)userType;
            DefaultSharePoint defaultPoint= GetOneByQuery(query);
            return defaultPoint;

            
        }
    }
}
