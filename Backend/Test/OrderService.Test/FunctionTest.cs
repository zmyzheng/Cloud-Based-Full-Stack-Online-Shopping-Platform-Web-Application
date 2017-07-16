namespace OrderService.Test
{
    using System;
    using Amazon.Lambda.TestUtilities;
    using Newtonsoft.Json.Linq;
    using OrderService;
    using Xunit;
    using Shared.Model;
    using Shared.Request;

    public class FunctionTest
    {
        [Fact]
        public void TestOrderServiceFunction()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            // var req = new Request()
            // {
            //     AuthToken = "Token",
            //     Operation = Operation.Create,
            //     Payload = new JObject(
            //         new JProperty("UserId", 100004),
            //         new JProperty("TotalCharge", 5000),
            //         new JProperty("Products", new JArray(new JObject(new JProperty("Id", 1), new JProperty("Count", 100))))
            //     )
            // };
            var req = new Request()
            {
                AuthToken = "Token",
                Operation = Operation.Read,
                Payload = new JObject(
                    new JProperty("PagingInfo", new JObject(new JProperty("Start", 0), new JProperty("Count", 10))),
                    new JProperty("SearchTerm", new JArray(new JObject(new JProperty("Field", "Id"), new JProperty("Operator", "LT"), new JProperty("Value", "10"))))
                )
            };
            var res = function.FunctionHandler(req);
        }
    }
}
