using OrdersWeb.Test.Orders.Fixtures;
using OrdersWeb.Test.Startup;

namespace OrdersWeb.Test.Orders.Features
{
    public class OrderingCreateFeature
    {
        private OrderClient _orderClient;

        [SetUp]
        public void Setup()
        {
            new SetupFixture().CreateClient();
            _orderClient = new OrderClient();
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
            var jsonPost = await _orderClient.GetJsonContent("./Orders/Fixtures/Order.json");
            await _orderClient.PostOrder(jsonPost);
        }

        private async Task<string> WhenGetOrderContent()
        {
            var response = await _orderClient.GetOrder();
            return response.Content.ReadAsStringAsync().Result;
        }

        private static async Task ThenVerifyTheOrderContent(string result)
        {
            await Verify(result);
        }
    }
}
