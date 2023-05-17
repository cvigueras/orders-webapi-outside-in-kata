using OrdersWeb.Test.Orders.Fixtures;
using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders.Features
{
    public class OrderingAddProductsFeature
    {
        private OrderClient _orderClient;

        [SetUp]
        public void Setup()
        {
            new SetupFixture().CreateClient();
            _orderClient = new OrderClient();
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
            var jsonPost = await _orderClient.GetJsonContent("./SampleData/Order.json");
            await _orderClient.PostOrder(jsonPost);
            var jsonPut = await _orderClient.GetJsonContent("./SampleData/OrderProducts.json");
            await _orderClient.PutOrder(jsonPut, "ORD765190");
        }

        private async Task<HttpResponseMessage> WhenGetOrderUpdated()
        {
            return await _orderClient.GetOrder();
        }

        private static async Task ThenVerifyTheOrderContent(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            await Verify(result);
        }
    }
}
