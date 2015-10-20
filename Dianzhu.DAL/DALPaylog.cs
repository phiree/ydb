using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;

namespace Dianzhu.DAL
{
    public class DALPaylog : DALBase<Paylog> 
    {
        public DALPaylog() { }
        public DALPaylog(string fortest) : base(fortest) { }

        public IList<Paylog> GetList(ServiceOrder order)
        {
            throw new NotImplementedException();
        }

        public void Save(Paylog paylog)
        {
            throw new NotImplementedException();
        }
    }
}
