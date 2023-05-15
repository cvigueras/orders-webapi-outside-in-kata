using System.Text;
using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders
{
    public class OrderingUpdateFeature
    {
        private HttpClient? _client;

        [SetUp]
        public void Setup()
        {
            _client = new StartupTest().CreateClient();
        }

        [Test]
        public async Task UpdateAnExistingOrderByNumberAfterPost()
        {
            var jsonPost = await GivenPostJson();
            var jsonPut = await GivenPutJson();
            var response = await _client!.PostAsync("/Orders/",
                new StringContent(jsonPost,
                    Encoding.Default,
                    "application/json"));
            response.EnsureSuccessStatusCode();
            response = await _client!.PutAsync("/Orders/ORD765190",
                new StringContent(jsonPut,
                    Encoding.Default,
                    "application/json"));
            response.EnsureSuccessStatusCode();
            response = await _client.GetAsync("/Orders/ORD765190");
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;

            await Verify(result);
        }

        private async Task<string> GivenPutJson()
        {
            using var jsonReader = new StreamReader("./SampleData/UpdatedOrder.json");
            return await jsonReader.ReadToEndAsync();
        }

        private async Task<string> GivenPostJson()
        {
            using var jsonReader = new StreamReader("./SampleData/Order.json");
            return await jsonReader.ReadToEndAsync();
        }
    }
}
