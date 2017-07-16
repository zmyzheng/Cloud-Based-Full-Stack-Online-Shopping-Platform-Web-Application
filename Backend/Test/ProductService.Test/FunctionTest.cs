namespace ProductService.Test
{
    using System;
    using Amazon.Lambda.TestUtilities;
    using Newtonsoft.Json.Linq;
    using ProductService;
    using Shared.Model;
    using Shared.Request;
    using Xunit;

    public class FunctionTest
    {
        [Fact]
        public void TestProductServiceFunction()
        {
            var function = new Function(); // Invoke the lambda function.
            var context = new TestLambdaContext();

            var req = new Request()
            {
                AuthToken = "Token",
                Operation = Operation.Read,
                Payload = new JObject(
                    new JProperty("PagingInfo", new JObject(new JProperty("Start", 0), new JProperty("Count", 10))),
                    new JProperty("SearchTerm", new JArray(new JObject(new JProperty("Field", "Id"), new JProperty("Operator", "EQ"), new JProperty("Value", "1"))))
                )
            };
            var res = function.FunctionHandler(req);
            Assert.Equal(1, ((Shared.Model.Product[])res.Payload)[0].Id);
        }
    }
}
