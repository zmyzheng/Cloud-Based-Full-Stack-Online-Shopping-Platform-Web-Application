namespace OrderService.Create
{
    using System;
    using System.Data;
    using System.Linq;
    using Dapper.Contrib.Extensions;
    using Dapper;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using OrderService.Model;

    /// <summary>
    /// The command for Create Operation
    /// </summary>
    public class CreateCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public CreateCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="request"> The request for this command </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<CreatePayload>();
            payload.Validate();
            DateTime now = DateTime.Now;
            var order = new Order()
            {
                DateTime = now.ToString(),
                UserId = payload.UserId,
                TotalCharge = payload.TotalCharge,
            };
            order.Validate();

            this.connection.Insert<Order>(order);

            var createdOrder = this.connection.Query<Order>(
                $"SELECT * FROM {DbHelper.GetTableName<Order>()} WHERE UserId = @UserId ORDER BY Id DESC LIMIT 1",
                new { UserId = order.UserId}
            ).First();

            //var FirstOrder = createdOrder.First();

            foreach(Item it in payload.Products) {
                var orderProduct = new OrderedProduct()
                {
                    ProductId = it.Id,
                    Count = it.Count,
                    OrderId = createdOrder.Id
                };
                orderProduct.Validate();
                this.connection.Insert<OrderedProduct>(orderProduct);
            }
            response.Payload = new { OrderId = createdOrder.Id};
            return response;
        }
    }
}