using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLClaims
    {
        public DALClaims DALClaims = DALFactory.DALClaims;

        public void Save(Claims c)
        {
            DALClaims.Save(c);
        }

        public void Update(Claims c)
        {
            c.LastUpdateTime = DateTime.Now;
            DALClaims.Update(c);
        }

        public Claims GetOneByOrder(ServiceOrder order)
        {
            return DALClaims.GetOneByOrder(order);
        }
    }
}
