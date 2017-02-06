using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel;
namespace Ydb.InstantMessage.Infrastructure
{
    public interface IReceptionSessionFactory
    {
        DomainModel.Reception.IReceptionSession Create(string restApiUrl, string restApiSecretKey);
    }
}
