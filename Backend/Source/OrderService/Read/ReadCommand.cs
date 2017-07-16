namespace OrderService.Read
{
    using System.Data;
    using System.Linq;
    using Dapper;
    using Shared.DbAccess;
    using Shared.Authentication;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Command for Read Operat ion
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

            //var user = AuthHelper.GetAuthPayload(request.AuthToken);
            foreach (SearchTerm st in payload.SearchTerm) {
                if (st.Field == "UserId") {
                    // st.Value = user.UserId.ToString();
                    st.Value = "100004";
                }
            }
            var res = this.connection.Query<Order>(
                RequestHelper.ComposeSearchExp(payload.SearchTerm, DbHelper.GetTableName<Order>(), payload.PagingInfo != null),
                RequestHelper.GetSearchObject(payload.SearchTerm, payload.PagingInfo)
            );

            // foreach( Order o in res)
            // {
            //     var productsInOrder = this.connection.Query<OrderedProduct>(
            //         $"SELECT * FROM {DbHelper.GetTableName<OrderedProduct>()} WHERE OrderId = @OrderId",
            //         new { OrderId = o.Id }
            //     );
            // }

            // var res = this.connection.Query<Payment>(
            //     $"SELECT ID, UserId, TotalCharge FROM {DbHelper.GetTableName<Order>()}"
            // );

            // var resFiltered = res.Where(r => this.connection.Query<Payment>(
            //     $"SELECT * FROM {DbHelper.GetTableName<Payment>()} WHERE OrderId = @OrderId",
            //         new { OrderId = r.Id }).Count() == 0
            // );

            // var ret = resFiltered.Select(o => new {OrderId = o.Id, Products = this.connection.Query<OrderedProduct>(
            //     $"SELECT * FROM {DbHelper.GetTableName<OrderedProduct>()} WHERE OrderId = @OrderId",
            //         new { OrderId = o.Id })
            // });

            var ret = res.Select(o => new {OrderId = o.Id, TotalCharge = o.TotalCharge, UserId = o.UserId ,PaymentId = o.PaymentId, Products = this.connection.Query<OrderedProduct>(
                $"SELECT * FROM {DbHelper.GetTableName<OrderedProduct>()} WHERE OrderId = @OrderId",
                    new { OrderId = o.Id })
            });
            response.Payload = ret.ToArray();

            return response;
        }
    }
}