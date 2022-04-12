using Sample.Contract.SwapiModel;

namespace Sample.API.Clients
{
    public interface ISWAPIClient
    {
        //HttpClient ApiClient { get; }

        Task<T> GetAsync<T>(string url);
        Task<IEnumerable<T>> GetListAsync<T>(IEnumerable<string> urls);
    }
}
