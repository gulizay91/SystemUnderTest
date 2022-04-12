using Sample.Contract.SwapiModel;

namespace Sample.API.Clients
{
    public class SWAPIPeopleClient : SWAPIClient, ISWAPIPeopleClient
    {
        public SWAPIPeopleClient(HttpClient client) : base(client)
        {
        }

        public async Task<People> GetPeopleAsync(string id)
        {
            string url = string.Format("{0}/{1}", "people", id);
            return await GetAsync<People>(url);
        }

        public async Task<SwapiEntityList<People>> GetPeoplesByPageAsync(int page = 1)
        {
            var url = string.Format(PEOPLE_LIST_QUERY, page.ToString());
            return await GetAsync<SwapiEntityList<People>>(url);
        }

        public async Task<IEnumerable<SwapiEntityList<People>>> GetAllPeopleAsync()
        {
            int currentPage = 1;
            var firstPage = await GetPeoplesByPageAsync(currentPage);
            ArgumentNullException.ThrowIfNull(firstPage);

            var urls = new List<string>();
            var isNext = firstPage.isNext;
            while (isNext)
            {
                urls.Add(string.Format(PEOPLE_LIST_QUERY, (++currentPage).ToString()));
                var nextPage = await GetPeoplesByPageAsync(currentPage);
                isNext = nextPage.isNext;
            }

            return await GetListAsync<SwapiEntityList<People>>(urls);
        }
    }
}
