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
            await GivenAnUpdatedOrder();

            var result = await WhenGetOrderUpdated();

            await ThenVerifyTheOrderContent(result);
        }

        private async Task GivenAnUpdatedOrder()
        {
            var jsonPost = await OrderClient.GetJsonContent("./SampleData/Order.json");
            await OrderClient.PostOrder(jsonPost, _client);
            var jsonPut = await OrderClient.GetJsonContent("./SampleData/UpdatedOrder.json");
            await OrderClient.PutOrder(jsonPut, _client);
        }

        private async Task<string> WhenGetOrderUpdated()
        {
            var response = await OrderClient.GetOrder(_client);
            return response.Content.ReadAsStringAsync().Result;
        }

        private static async Task ThenVerifyTheOrderContent(string result)
        {
            await Verify(result);
        }
    }
}