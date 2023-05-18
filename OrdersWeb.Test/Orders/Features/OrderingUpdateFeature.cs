using OrdersWeb.Test.Startup;

namespace OrdersWeb.Test.Orders.Features
{
    public class OrderingUpdateFeature
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
            await GivenAnUpdatedOrder();

            var result = await WhenGetOrderUpdated();

            await ThenVerifyTheOrderContent(result);
        }

        private async Task GivenAnUpdatedOrder()
        {
            var jsonPost = await _client.GetJsonContent("./Orders/Fixtures/Order.json");
            await _client.Post(jsonPost, "/Orders/");
            var jsonPut = await _client.GetJsonContent("./Orders/Fixtures/UpdatedOrder.json");
            await _client.Put(jsonPut, $"/Orders/{"ORD765190"}");
        }

        private async Task<string> WhenGetOrderUpdated()
        {
            var response = await _client.Get("/Orders/ORD765190");
            return response.Content.ReadAsStringAsync().Result;
        }

        private static async Task ThenVerifyTheOrderContent(string result)
        {
            await Verify(result);
        }
    }
}