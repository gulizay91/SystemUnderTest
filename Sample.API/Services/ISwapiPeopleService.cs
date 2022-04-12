using Sample.Contract.SwapiModel;

namespace Sample.API.Services
{
    public interface ISwapiPeopleService
    {
        Task<People> GetPeopleAsync(string id);
        Task<IEnumerable<SwapiEntityList<People>>> GetAllPeopleAsync();
    }
}
