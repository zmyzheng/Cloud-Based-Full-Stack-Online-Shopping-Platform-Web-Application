using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "QueueService.Test"
)]

namespace QueueService
{
    using System;
    using System.Data;
    using Amazon.Lambda.Core;
    using QueueService.Queue;
    using QueueService.Read;
    using Shared;
    using Shared.Command;
    using Shared.DbAccess;
    using Shared.Http;
    using Shared.Queue;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// QueueService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(QueueRequest request)
        {
            var container = new CommandContainer();

            container.RegisterRequirement<IDbConnection>(() => DbHelper.Connection)

                     .Register<QueueCommand>(Operation.Queue)
                     .Register<ReadCommand>(Operation.Read);

            try
            {
                request.Validate();
                return container.ProcessWith(request, request.QueueOperation);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
