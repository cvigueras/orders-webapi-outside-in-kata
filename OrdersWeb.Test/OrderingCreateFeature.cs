using System.Text;

namespace OrdersWeb.Test
{
    public class OrderingCreateFeature
    {
        private HttpClient? _client;

        [SetUp]
        public void Setup()
        {
            _client = new StartupTest().CreateClient();
        }

        [Test]
        public async Task GetAnOrderByNumberAfterPost()
        {
            var jsonPost = await GivenJson();
            var response = await _client!.PostAsync("/Orders/",
                new StringContent(jsonPost,
                    Encoding.Default,
                    "application/json"));
            response.EnsureSuccessStatusCode();
            response = await _client.GetAsync("/Orders/ORD765190");
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;

            await Verify(result);
        }

        private async Task<string> GivenJson()
        {
            using var jsonReader = new StreamReader("./SampleData/Order.json");
            return await jsonReader.ReadToEndAsync();
        }
    }
}
