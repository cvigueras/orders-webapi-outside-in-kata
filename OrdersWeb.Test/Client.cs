using OrdersWeb.Test.Startup;
using System.Text;

namespace OrdersWeb.Test
{
    public class Client
    {
        private readonly HttpClient? _client;
        private const string MediaType = "application/json";

        public Client()
        {
            _client = new SetupFixture().CreateClient();
        }

        public async Task<string> GetJsonContent(string path)
        {
            using var jsonReader = new StreamReader(path);
            return await jsonReader.ReadToEndAsync();
        }

        public async Task Post(string json, string requestUri)
        {
            var response = await _client!.PostAsync(requestUri,
                new StringContent(json,
                    Encoding.Default,
                    MediaType));
            response.EnsureSuccessStatusCode();
        }

        public async Task Put(string jsonPut, string requestUri)
        {
            var response = await _client!.PutAsync(requestUri,
                new StringContent(jsonPut,
                    Encoding.Default,
                    MediaType));
            response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> Get(string requestUri)
        {
            var response = await _client!.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}