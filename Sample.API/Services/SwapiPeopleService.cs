using Sample.API.Clients;
using Sample.Contract.SwapiModel;

namespace Sample.API.Services
{
    public class SwapiPeopleService : ISwapiPeopleService
    {
        private readonly ISWAPIPeopleClient _swapiPeopleClient;
        public SwapiPeopleService(ISWAPIPeopleClient swapiPeopleClient)
        {
            _swapiPeopleClient = swapiPeopleClient;
        }

        public Task<IEnumerable<SwapiEntityList<People>>> GetAllPeopleAsync()
        {
            return _swapiPeopleClient.GetAllPeopleAsync();
        }

        public Task<People> GetPeopleAsync(string id)
        {
            return _swapiPeopleClient.GetPeopleAsync(id);
        }
    }
}
