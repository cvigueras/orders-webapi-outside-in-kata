using System.Text;

namespace OrdersWeb.Test
{
    public class OrderingFeature
    {
        private HttpClient? _client;

        [SetUp]
        public void Setup()
        {
            _client = new StartupTest().CreateClient();
        }

        [Test]
        public async Task GetAnOrderByIdAfterPostOneOrder()
        {
            var jsonPost = await GivenJson();
            var response = await _client!.PostAsync("/Order/",
                new StringContent(jsonPost,
                    Encoding.Default,
                    "application/json"));
            response.EnsureSuccessStatusCode();
            response = await _client.GetAsync("/Order/ORD765190");
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;

            await Verify(result);

        }

        private async Task<string> GivenJson()
        {
            using var r = new StreamReader("./SampleData/Order.json");
            return await r.ReadToEndAsync();
        }
    }
}
