using System.Text;

namespace OrdersWeb.Test
{
    public class ProductGetFeature
    {
        private HttpClient? _client;

        [SetUp]
        public void Setup()
        {
            _client = new StartupTest().CreateClient();
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
