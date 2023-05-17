using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Products.Features
{
    public class ProductGetFeature
    {
        private HttpClient? _client;
        private SetupFixture _setupFixture;

        [SetUp]
        public void Setup()
        {
            _setupFixture = new SetupFixture();
            _client = _setupFixture.CreateClient();
            _setupFixture.CreateSeed();
        }

        [Test]
        public async Task GetAllProductList()
        {
            var response = await _client!.GetAsync("/Products/");
            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync().Result;

            await Verify(result);
        }
    }
}
