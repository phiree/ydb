using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using Ydb.Finance.DomainModel.Enums;
namespace Ydb.Finance.Application
{
   public interface IUserTypeSharePointService
    {
        void Add(string userType,decimal sharepoint);
        decimal GetSharePoint(string userType,out string errMsg);
        IList<UserTypeSharePointDto> GetAll();


    }
}
