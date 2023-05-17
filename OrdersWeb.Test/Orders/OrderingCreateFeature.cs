using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders
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
            await GivenAnOrderWithSimpleData();

            var result = await WhenGetOrderContent();

            await ThenVerifyTheOrderContent(result);
        }

        private async Task GivenAnOrderWithSimpleData()
        {
            var jsonPost = await OrderClient.GetJsonContent("./SampleData/Order.json");
            await OrderClient.PostOrder(jsonPost, _client);
        }

        private async Task<string> WhenGetOrderContent()
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
