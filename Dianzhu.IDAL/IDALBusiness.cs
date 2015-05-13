using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.IDAL
{
    public interface IDALBusiness
    {
        IDALBase<Business> DalBase { get; set; }
        void CreateBusinessAndUser(string code);
    }
}
