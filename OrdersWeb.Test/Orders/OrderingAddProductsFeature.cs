using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders
{
    public class OrderingAddProductsFeature
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
            await GivenAnUpdatedPost();

            var response = await WhenGetOrderUpdated();

            await ThenVerifyTheOrderContent(response);
        }
        private async Task GivenAnUpdatedPost()
        {
            var jsonPost = await OrderClient.GetJsonContent("./SampleData/Order.json");
            await OrderClient.PostOrder(jsonPost, _client);
            var jsonPut = await OrderClient.GetJsonContent("./SampleData/OrderProducts.json");
            await OrderClient.PutOrder(jsonPut, _client);
        }

        private async Task<HttpResponseMessage> WhenGetOrderUpdated()
        {
            return await OrderClient.GetOrder(_client);
        }

        private static async Task ThenVerifyTheOrderContent(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            await Verify(result);
        }


    }
}
