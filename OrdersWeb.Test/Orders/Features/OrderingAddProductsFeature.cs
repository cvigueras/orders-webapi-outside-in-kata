using OrdersWeb.Test.Startup;

namespace OrdersWeb.Test.Orders.Features
{
    public class OrderingAddProductsFeature
    {
        private Client _client;

        [SetUp]
        public void Setup()
        {
            new SetupFixture().CreateClient();
            _client = new Client();
        }

        [Test]
        public async Task UpdateAnExistingOrderByNumberAfterPost()
        {
            await GivenAnUpdatedPost();

            var response = await WhenGetOrderUpdated();

            await ThenVerifyTheOrderContent(response);
        }

        private async Task GivenAnUpdatedPost()
        {
            var jsonPost = await _client.GetJsonContent("./Orders/Fixtures/Order.json");
            await _client.Post(jsonPost, "/Orders/");
            var jsonPut = await _client.GetJsonContent("./Orders/Fixtures/OrderProducts.json");
            await _client.Put(jsonPut, "/Orders/ORD765190");
        }

        private async Task<HttpResponseMessage> WhenGetOrderUpdated()
        {
            return await _client.Get("/Orders/ORD765190");
        }

        private static async Task ThenVerifyTheOrderContent(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            await Verify(result);
        }
    }
}
