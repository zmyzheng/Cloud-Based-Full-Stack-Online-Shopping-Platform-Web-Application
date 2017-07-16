using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "AuthService.Test"
)]

namespace AuthService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Amazon.Lambda;
    using Amazon.Lambda.APIGatewayEvents;
    using Amazon.Lambda.Core;
    using Amazon.Lambda.Model;
    using AuthService.Model;
    using AuthService.Policy;
    using AuthService.Validation;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Shared;
    using Shared.EnumHelper;
    using Shared.Http;
    using Shared.Queue;
    using Shared.Request;
    using Shared.Response;

    /// <summary>
    /// Authentication Service entry class
    /// </summary>
    public class Function
    {
        private Dictionary<string, Service> serviceMapping = new Dictionary<string, Service>()
        {
            ["payments"] = Service.PaymentService,
            ["orders"] = Service.OrderService,
            ["users"] = Service.UserService
        };

        /// <summary>
        /// Handle the Authentication Request
        /// </summary>
        /// <param name="request"> Authentication Request </param>
        /// <returns> API Gateway Custom Authorizer Response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public async Task<APIGatewayCustomAuthorizerResponse> FunctionHandler(APIGatewayCustomAuthorizerRequest request)
        {
            var token = request.AuthorizationToken;
            var type = AuthTokenType.JWT;

            var response = new APIGatewayCustomAuthorizerResponse();
            var policy = response.PolicyDocument;
            var statement = new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement()
            {
                Action = new HashSet<string>(),
                Resource = new HashSet<string>()
            };

            if (Validator.Validate(type, token))
            {
                var vars = request.MethodArn.Split(':');
                var apiVars = vars[5].Split('/');
                var region = vars[3];
                var accountId = vars[4];
                var apiId = apiVars[0];
                var stage = apiVars[1];

                Console.WriteLine(apiVars.Length);

                if (apiVars.Length > 4)
                {
                    int id;
                    if (int.TryParse(apiVars[4], out id))
                    {
                        var service = apiVars[3];

                        Console.WriteLine(service);
                        Console.WriteLine(id);

                        var req = new Request()
                        {
                            Operation = Operation.VerifyUser,
                            AuthToken = token,
                            Payload = JObject.FromObject(new VerifyUserPayload()
                            {
                                ResourceId = id
                            })
                        };

                        var lambdaClient = new AmazonLambdaClient();
                        var invokeRequest = new InvokeRequest()
                        {
                            FunctionName = Enum.GetName(typeof(Service), serviceMapping[service]),
                            Payload = JsonConvert.SerializeObject(req)
                        };

                        var lambdaRes = await lambdaClient.InvokeAsync(invokeRequest);
                        var res = new StreamReader(lambdaRes.Payload).ReadToEnd();

                        if (res.Contains(Enum.GetName(typeof(VerifyResult), VerifyResult.Allow)))
                        {
                            var any = Resource.Any.GetStringValue();
                            this.AllowOperation(
                                    statement,
                                    CustomAuthorizerHelper.ComposeAction(Policy.Action.Invoke),
                                    CustomAuthorizerHelper.ComposeResource(region, accountId, apiId, stage, new ResourceAccess(any, any))
                            );
                        }
                        else
                        {
                            this.DenyAll(statement);
                        }
                    }
                    else
                    {
                        var any = Resource.Any.GetStringValue();
                        this.AllowOperation(
                                statement,
                                CustomAuthorizerHelper.ComposeAction(Policy.Action.Invoke),
                                CustomAuthorizerHelper.ComposeResource(region, accountId, apiId, stage, new ResourceAccess(any, any))
                        );
                    }
                }
                else
                {
                    var any = Resource.Any.GetStringValue();
                    this.AllowOperation(
                            statement,
                            CustomAuthorizerHelper.ComposeAction(Policy.Action.Invoke),
                            CustomAuthorizerHelper.ComposeResource(region, accountId, apiId, stage, new ResourceAccess(any, any))
                    );
                }
            }
            else
            {
                this.DenyAll(statement);
            }

            policy.Statement.Add(statement);
            return response;
        }

        private void AllowOperation(APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement statement, string action, string resource)
        {
            statement.Effect = Effect.Allow.GetStringValue();
            statement.Action.Add(action);
            statement.Resource.Add(resource);
        }

        private void DenyAll(APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement statement)
        {
            var any = Resource.Any.GetStringValue();
            statement.Effect = Effect.Deny.GetStringValue();
            statement.Action.Add(CustomAuthorizerHelper.ComposeAction(Policy.Action.All));
            statement.Resource.Add(CustomAuthorizerHelper.ComposeResource(
                any, any, any, any, new ResourceAccess(any, any)));
        }
    }
}
