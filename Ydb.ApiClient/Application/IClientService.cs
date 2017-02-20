using Ydb.ApiClient.DomainModel;
namespace Ydb.ApiClient.Application
{
    public interface IClientService
    {
        Client FindClient(string clientId);
        void RegisterClient(Client client);
    }
}