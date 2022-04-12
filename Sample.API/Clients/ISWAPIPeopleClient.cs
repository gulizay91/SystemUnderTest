using Sample.Contract.SwapiModel;

namespace Sample.API.Clients
{
    public interface ISWAPIPeopleClient : ISWAPIClient
    {
        Task<People> GetPeopleAsync(string id);
        Task<SwapiEntityList<People>> GetPeoplesByPageAsync(int page = 1);
        Task<IEnumerable<SwapiEntityList<People>>> GetAllPeopleAsync();
    }
}
