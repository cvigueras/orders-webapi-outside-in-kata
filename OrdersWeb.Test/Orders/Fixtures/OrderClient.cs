using OrdersWeb.Test.Start;
using System.Text;

namespace OrdersWeb.Test.Orders.Fixtures
{
    public class OrderClient
    {
        private HttpClient? _client;

        public OrderClient()
        {
            _client = new SetupFixture().CreateClient();
        }

        public async Task<string> GetJsonContent(string path)
        {
            using var jsonReader = new StreamReader(path);
            return await jsonReader.ReadToEndAsync();
        }

        public async Task PostOrder(string json)
        {
            var response = await _client!.PostAsync("/Orders/",
                new StringContent(json,
                    Encoding.Default,
                    "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task PutOrder(string jsonPut, string orderNumber)
        {
            var response = await _client!.PutAsync($"/Orders/{orderNumber}",
                new StringContent(jsonPut,
                    Encoding.Default,
                    "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> GetOrder()
        {
            var response = await _client!.GetAsync("/Orders/ORD765190");
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}