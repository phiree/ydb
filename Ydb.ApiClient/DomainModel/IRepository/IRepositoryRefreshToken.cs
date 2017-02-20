using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Ydb.ApiClient.DomainModel;
using Ydb.Common.Repository;
namespace Ydb.ApiClient.DomainModel.IRepository
{
    public interface IRepositoryRefreshToken:IRepository<RefreshToken,string>
    {
        bool AddRefreshToken(RefreshToken token);
         RefreshToken FindRefreshToken(string refreshTokenId);
        System.Collections.Generic.IList<RefreshToken> GetAllRefreshTokens();
        void RemoveRefreshToken(string refreshTokenId);
        void RemoveRefreshToken(RefreshToken refreshtoken);
    }

   
}
