using System.Text;

namespace OrdersWeb.Test
{
    public class ProductGetFeature
    {
        private HttpClient? _client;
        private StartupTest _startupTest;

        [SetUp]
        public void Setup()
        {
            _startupTest = new StartupTest();
            _client = _startupTest.CreateClient();
            _startupTest.CreateSeed();
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
