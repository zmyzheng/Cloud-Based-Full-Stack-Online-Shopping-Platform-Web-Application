using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "ProductService.Test"
)]

namespace ProductService
{
    using System;
    using System.Data;
    using Amazon.Lambda.Core;
    using ProductService.Create;
    using ProductService.Delete;
    using ProductService.Read;
    using ProductService.Update;
    using Shared;
    using Shared.Command;
    using Shared.DbAccess;
    using Shared.Http;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Lambda function handler for product service
        /// </summary>
        /// <param name="request"> Input for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request)
        {
            var container = new CommandContainer();

            container.RegisterRequirement<IDbConnection>(() => DbHelper.Connection)
                     .Register<ReadCommand>(Operation.Read)
                     .Register<DeleteCommand>(Operation.Delete)
                     .Register<CreateCommand>(Operation.Create)
                     .Register<UpdateCommand>(Operation.Update);

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
