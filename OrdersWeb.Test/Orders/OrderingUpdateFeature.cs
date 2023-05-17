using OrdersWeb.Test.Start;
using System.Text;

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
            var jsonPost = await OrderClient.GetJsonContent("./SampleData/Order.json");
            var jsonPut = await OrderClient.GetJsonContent("./SampleData/UpdatedOrder.json");
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


    }
}
