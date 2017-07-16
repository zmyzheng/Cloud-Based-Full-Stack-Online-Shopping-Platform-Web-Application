namespace UserService.Test
{
    using Amazon.Lambda.TestUtilities;
    using Shared.Request;
    using UserService;
    using Xunit;

    public class FunctionTest
    {
        // [Fact]
        // public void TestFunction()
        // {
        //     var function = new Function();
        //     var context = new TestLambdaContext();

        //     var request = new Request()
        //     {
        //         AuthToken = "Token",
        //         Operation = Operation.Read,
        //         Payload = null,
        //         PagingInfo = new PagingInfo()
        //         {
        //             Start = 0,
        //             Count = 10
        //         },
        //         SearchTerm = new[] {
        //             new SearchTerm()
        //             {
        //                 Field = "Email",
        //                 Operator = SearchOperator.LIKE,
        //                 Value = "test"
        //             }
        //         }
        //     };

        //     var response = function.FunctionHandler(request, context);
        // }
    }
}
