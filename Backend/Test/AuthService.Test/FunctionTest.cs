namespace AuthService.Test
{
    using Amazon.Lambda.APIGatewayEvents;
    using Amazon.Lambda.TestUtilities;
    using AuthService;
    using AuthService.Policy;
    using Shared.Authentication;
    using Shared.EnumHelper;
    using Xunit;

    public class FunctionTest
    {
        private readonly AuthPayload payload = new AuthPayload()
        {
            UserId = 0,
            Email = "admin@admin.com",
            FirstName = "Admin",
            LastName = "Admin"
        };

        [Fact]
        public void AuthSuccessWithValidToken()
        {
            var function = new Function();

            var validRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = AuthHelper.GenerateAuthToken(this.payload),
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var validPolicy = function.FunctionHandler(validRequest).PolicyDocument;

            Assert.Equal(validPolicy.Statement[0].Effect, Effect.Allow.GetStringValue());
            Assert.True(validPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(Action.Invoke)));
            Assert.True(validPolicy.Statement[0].Resource.Contains("arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/*/*"));
        }

        [Fact]
        public void AuthFailForInvalidTokenWithin5Min()
        {
            var function = new Function();

            var invalidRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = "invalid",
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var invalidPolicy = function.FunctionHandler(invalidRequest).PolicyDocument;

            Assert.Equal(invalidPolicy.Statement[0].Effect, Effect.Deny.GetStringValue());
            Assert.True(invalidPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(Action.All)));
            Assert.True(invalidPolicy.Statement[0].Resource.Contains("arn:aws:execute-api:*:*:*/*/*/*"));
        }
    }
}
