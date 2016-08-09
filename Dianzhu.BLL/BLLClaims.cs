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
        public IDAL.IDALClaims DALClaims;

        public BLLClaims(IDAL.IDALClaims dal)
        {
            DALClaims = dal;
        }

        public void Save(Claims c)
        {
            DALClaims.Add(c);
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
