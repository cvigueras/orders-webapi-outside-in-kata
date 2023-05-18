using OrdersWeb.Test.Startup;

namespace OrdersWeb.Test.Products.Features
{
    public class ProductCreateFeature
    {
        private Client _client;

        [SetUp]
        public void Setup()
        {
            new SetupFixture().CreateClient();
            _client = new Client();
        }

        [Test]
        public async Task GetAProductByIdAfterPost()
        {
            var jsonPost = await _client.GetJsonContent("./Products/Fixtures/Product.json");
            await _client.Post(jsonPost, "/Products/");

            var response = await _client.Get("/Products/5");
            var result = response.Content.ReadAsStringAsync().Result;

            await Verify(result);
        }
    }
}