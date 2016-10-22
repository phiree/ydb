using System;
using System.Text;
using System.Collections;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;
using Ydb.Finance.DomainModel.Enums;
namespace Ydb.Finance.DomainModel
{
    public interface IRepositoryDefaultSharePoint : IRepository< DefaultSharePoint,Guid>
    {

        DefaultSharePoint GetDefaultSharePoint( UserType userType);
    }
}
