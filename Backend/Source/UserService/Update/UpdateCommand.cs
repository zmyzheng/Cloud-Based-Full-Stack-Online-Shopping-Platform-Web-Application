namespace UserService.Update
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
    /// The command for Update Operation
    /// </summary>
    public class UpdateCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommand"/> class.
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

            var user = this.connection.Query<User>(
                $"SELECT * FROM {DbHelper.GetTableName<User>()} WHERE Id = @Id",
                new { Id = payload.Id }
            ).First();

            string val;
            user.Email = payload.Change.TryGetValue("Email", out val) ? val : user.Email;
            user.FirstName = payload.Change.TryGetValue("FirstName", out val) ? val : user.FirstName;
            user.LastName = payload.Change.TryGetValue("LastName", out val) ? val : user.LastName;
            user.PhoneNumber = payload.Change.TryGetValue("PhoneNumber", out val) ? val : user.PhoneNumber;
            user.Validate();

            this.connection.Update<User>(user);

            return response;
        }
    }
}