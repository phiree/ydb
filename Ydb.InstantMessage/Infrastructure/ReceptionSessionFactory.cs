using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel;
namespace Ydb.InstantMessage.Infrastructure
{
    public class ReceptionSessionFactory: IReceptionSessionFactory
    {
        public ReceptionSessionFactory()
        { }
       public DomainModel.Reception.IReceptionSession Create(string restApiUrl, string restApiSecretKey)
        {
            return new ReceptionSessionOpenfireRestapi(restApiUrl, restApiSecretKey);

        }
    }
}
