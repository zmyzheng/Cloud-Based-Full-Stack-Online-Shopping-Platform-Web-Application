namespace AddressService.Test
{
    using Amazon.Lambda.TestUtilities;
    using AddressService;
    using Xunit;

    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var upperCase = function.FunctionHandler("hello world");

            Assert.Equal("HELLO WORLD", upperCase);
        }
    }
}
