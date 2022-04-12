using System.Net.Http.Headers;

namespace Sample.API.Clients
{
    public class SWAPIClient : ISWAPIClient
    {
        public readonly static string BaseAddress = @"https://swapi.dev/api/";
        protected readonly string AcceptHeader = "application/json";

        public const string PEOPLE_LIST_QUERY = "people?page={0}";
        private readonly HttpClient _client;
        
        public SWAPIClient(HttpClient client)
        {
            _client = SetClient(client);
            //_client = client??GetClient();
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AcceptHeader));
            return client;
        }

        static HttpClient SetClient(HttpClient client)
        {
            ArgumentNullException.ThrowIfNull(client);
            client.BaseAddress = new Uri(BaseAddress);
            return client;
        }

        /// <summary>
        ///  Get Data From StarWars API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url)
        {
            T result = default!;

            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            result = await response.Content.ReadFromJsonAsync<T>();
            //result = System.Text.Json.JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync())!;
            //result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;

            return result;
        }

        /// <summary>
        /// Get List Data From StarWars API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urls"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListAsync<T>(IEnumerable<string> urls)
        {

            Task<IEnumerable<T>> t = Task.Run(() =>
            {
                List<T> items = new List<T>();
                foreach (var url in urls)
                {
                    T item = GetAsync<T>(url).Result;
                    items.Add(item);

                }
                return items.AsEnumerable();
            });

            return await t;

        }

    }
}
