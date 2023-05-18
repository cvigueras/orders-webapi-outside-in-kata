using OrdersWeb.Test.Startup;

namespace OrdersWeb.Test.Orders.Features
{
    public class OrderingCreateFeature
    {
        private Client _client;

        [SetUp]
        public void Setup()
        {
            new SetupFixture().CreateClient();
            _client = new Client();
        }

        [Test]
        public async Task GetAnOrderByNumberAfterPost()
        {
            await GivenAnOrderWithSimpleData();

            var result = await WhenGetOrderContent();

            await ThenVerifyTheOrderContent(result);
        }

        private async Task GivenAnOrderWithSimpleData()
        {
            var jsonPost = await _client.GetJsonContent("./Orders/Fixtures/Order.json");
            await _client.Post(jsonPost, "/Orders/");
        }

        private async Task<string> WhenGetOrderContent()
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
