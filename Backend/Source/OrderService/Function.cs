using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "OrderService.Test"
)]

namespace OrderService
{
    using System;
    using System.Data;
    using Amazon.Lambda.Core;
    using Shared;
    using Shared.Command;
    using Shared.DbAccess;
    using Shared.Http;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using OrderService.Read;
    using OrderService.Update;
    using OrderService.Create;
    using OrderService.Delete;
    using OrderService.VerifyUser;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// OrderService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <returns> Lambda response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request)
        {
            var container = new CommandContainer();

            container.RegisterRequirement<IDbConnection>(() => DbHelper.Connection)
                     .Register<CreateCommand>(Operation.Create)
                     .Register<ReadCommand>(Operation.Read)
                     .Register<UpdateCommand>(Operation.Update)
                     .Register<DeleteCommand>(Operation.Delete)
                     .Register<VerifyUserCommand>(Operation.VerifyUser);
            try
            {
                request.Validate();
                return container.Process(request);
            }
            catch(Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
