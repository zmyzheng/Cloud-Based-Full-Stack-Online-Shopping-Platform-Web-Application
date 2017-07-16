using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "OrderProductService.Test"
)]

namespace OrderProductService
{
    using System;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Shared.Command;
    using Shared.Http;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using Amazon.Lambda.Core;
    using Shared;
    using OrderProductService.Read;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// OrderProductService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <returns> Lambda response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request)
        {
            var container = new CommandContainer();

            container.RegisterRequirement<IDbConnection>(() => DbHelper.Connection)
                     .Register<ReadCommand>(Operation.Read);

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
