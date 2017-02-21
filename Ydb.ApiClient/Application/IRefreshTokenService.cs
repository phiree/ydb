using Ydb.ApiClient.DomainModel;
namespace Ydb.ApiClient.Application
{
    public interface IRefreshTokenService
    {
        bool AddRefreshToken(RefreshToken token);
        RefreshToken FindRefreshToken(string refreshTokenId);
        System.Collections.Generic.IList<RefreshToken> GetAllRefreshTokens();
        void RemoveRefreshToken(string refreshTokenId);
        void RemoveRefreshToken(RefreshToken refreshtoken);
    }
}