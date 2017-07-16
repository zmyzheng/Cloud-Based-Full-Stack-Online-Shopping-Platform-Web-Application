namespace PaymentService.Read
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

            var res = this.connection.Query<Payment>(
                RequestHelper.ComposeSearchExp(payload.SearchTerm, DbHelper.GetTableName<Payment>(), payload.PagingInfo != null),
                RequestHelper.GetSearchObject(payload.SearchTerm, payload.PagingInfo)
            );
            var ret = res.Select(o => new {OrderId = o.Id, TotalCharge = o.Charge, PaymentId = o.Id});
            response.Payload = res.ToArray();
            return response;
        }
    }
}