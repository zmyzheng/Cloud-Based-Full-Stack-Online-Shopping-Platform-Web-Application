namespace OrderProductService.Read
{
    using System.Data;
    using System.Linq;
    using Dapper;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    /// <summary>
    /// Command for Read Operation
    /// </summary>
    public class ReadCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public ReadCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> Request for the command </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var payload = request.Payload.ToObject<SearchPayload>();
            payload.Validate();

            var response = new Response();

            var res = this.connection.Query<OrderedProduct>(
                RequestHelper.ComposeSearchExp(payload.SearchTerm, DbHelper.GetTableName<OrderedProduct>(), payload.PagingInfo != null),
                RequestHelper.GetSearchObject(payload.SearchTerm, payload.PagingInfo)
            );
            response.Payload = res.ToArray();

            return response;
        }
    }
}