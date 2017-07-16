namespace PaymentService.Update
{
    using System;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.DbAccess;
    using Shared.Validation;
    using PaymentService.Model;

    /// <summary>
    /// The command for Update Operation
    /// </summary>
    public class UpdateCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public UpdateCommand(IDbConnection connection)
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

            var payload = request.Payload.ToObject<UpdatePayload>();
            payload.Validate();

            var payment = this.connection.Query<Payment>(
                $"SELECT * FROM {DbHelper.GetTableName<Payment>()} WHERE Id = @Id",
                new { Id = payload.Id }
            ).First();

            string val;

            payment.OrderId = payload.Change.TryGetValue("OrderId", out val) ? Convert.ToInt32(val) : payment.OrderId;
            payment.StripeToken = payload.Change.TryGetValue("StripeToken", out val) ? val : payment.StripeToken;
            payment.DateTime = payload.Change.TryGetValue("DateTime", out val) ? Convert.ToDateTime(val) : payment.DateTime;
            payment.Charge = payload.Change.TryGetValue("Charge", out val) ? Convert.ToDecimal(val) : payment.Charge;
            payment.Validate();

            this.connection.Update<Payment>(payment);

            return response;
        }
    }
}