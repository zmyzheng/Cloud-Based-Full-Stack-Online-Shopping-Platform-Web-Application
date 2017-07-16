namespace AuthService
{
    using AuthService.Model;
    using AuthService.Policy;
    using Shared.EnumHelper;

    /// <summary>
    /// Static helper class for composing authorizer response contents
    /// </summary>
    public static class CustomAuthorizerHelper
    {
        /// <summary>
        /// Compose the action string used in authorizer response
        /// </summary>
        /// <param name="action"> Action allowed for the user </param>
        /// <returns> Composed action string </returns>
        public static string ComposeAction(Action action)
        {
            return $"execute-api:{action.GetStringValue()}";
        }

        /// <summary>
        /// Compose the resource string used in authorizer response
        /// </summary>
        /// <param name="region"> The AWS region (such as us-east-1 or * for all AWS regions) that corresponds to the deployed API for the method </param>
        /// <param name="accountId"> The 12-digit AWS account Id of the REST API owner </param>
        /// <param name="apiId"> The identifier API Gateway has assigned to the API for the method (* can be used for all APIs, regardless of the API's identifier) </param>
        /// <param name="stage"> The name of the stage associated with the method (* can be used for all stages, regardless of the stage's name) </param>
        /// <param name="access"> The access to the resources </param>
        /// <returns> Composed resource string </returns>
        public static string ComposeResource(string region, string accountId, string apiId, string stage, params ResourceAccess[] access)
        {
            var rule = $"arn:aws:execute-api:{region}:{accountId}:{apiId}/{stage}/";

            foreach (var a in access)
            {
                if (!rule.EndsWith("/"))
                {
                    rule += "/";
                }

                rule += $"{a.HttpVerb}/{a.Resource}";
            }

            return rule;
        }
    }
}
