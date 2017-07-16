using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "EmailService.Test"
)]

namespace EmailService
{
    using System;
    using Amazon.Lambda.Core;
    using EmailService.Send;
    using Shared;
    using Shared.Command;
    using Shared.Http;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Function handler for email service
        /// </summary>
        /// <param name="request"> The request for the service </param>
        /// <returns> The response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request)
        {
            var container = new CommandContainer();

            container.Register<SendCommand>(Operation.Send);

            try
            {
                request.Validate();
                return container.Process(request);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
