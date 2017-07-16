namespace UserService.Delete
{
    using System.Data;
    using System.Linq;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;

    /// <summary>
    /// Command for deleting the user
    /// </summary>
    public class DeleteCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand"/> class.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public DeleteCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke the DeleteCommand
        /// </summary>
        /// <param name="request"> The request to perform </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<DeletePayload>();
            payload.Validate();

            var user = this.connection.Query<User>(
                $"SELECT * FROM {DbHelper.GetTableName<User>()} WHERE Id = @Id",
                new { Id = payload.Id }
            ).First();

            this.connection.Delete<User>(user);

            return response;
        }
    }
}