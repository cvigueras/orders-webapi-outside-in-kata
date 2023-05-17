﻿using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders
{
    public class OrderingUpdateFeature
    {
        private OrderClient _orderClient;

        [SetUp]
        public void Setup()
        {
            new StartupTest().CreateClient();
            _orderClient = new OrderClient();
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
            var jsonPost = await _orderClient.GetJsonContent("./SampleData/Order.json");
            await _orderClient.PostOrder(jsonPost);
            var jsonPut = await _orderClient.GetJsonContent("./SampleData/UpdatedOrder.json");
            await _orderClient.PutOrder(jsonPut, "ORD765190");
        }

        private async Task<string> WhenGetOrderUpdated()
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