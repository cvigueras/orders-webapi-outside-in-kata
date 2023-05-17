using System.Text;

namespace OrdersWeb.Test.Orders
{
    public class OrderClient
    {
        public static async Task<string> GetJsonContent(string path)
        {
            using var jsonReader = new StreamReader(path);
            return await jsonReader.ReadToEndAsync();
        }

        public static async Task PostOrder(string json, HttpClient httpClient)
        {
            var response = await httpClient!.PostAsync("/Orders/",
                new StringContent(json,
                    Encoding.Default,
                    "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public static async Task PutOrder(string jsonPut, HttpClient httpClient)
        {
            var response = await httpClient!.PutAsync("/Orders/ORD765190",
                new StringContent(jsonPut,
                    Encoding.Default,
                    "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public static async Task<HttpResponseMessage> GetOrder(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync("/Orders/ORD765190");
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
