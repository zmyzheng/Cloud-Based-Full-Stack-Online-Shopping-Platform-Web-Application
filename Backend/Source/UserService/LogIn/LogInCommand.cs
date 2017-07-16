namespace UserService.LogIn
{
    using System;
    using System.Data;
    using System.Linq;
    using BCrypt.Net;
    using Dapper;
    using Shared.Authentication;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;

    /// <summary>
    /// Command for LogIn operation
    /// </summary>
    public class LogInCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInCommand"/> class.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public LogInCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> The request for this command </param>
        /// <returns> The respose </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<LogInPayload>();
            payload.Validate();

            var user = this.connection.Query<User>(
                $"SELECT * FROM {DbHelper.GetTableName<User>()} WHERE Email = @Email",
                new { Email = payload.Email }
            ).First();

            if (BCrypt.Verify(payload.Password, user.PwdHash))
            {
                var loginToken = AuthHelper.GenerateAuthToken(new AuthPayload()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateTime = DateTime.Now
                });
                response.Payload = new
                {
                    Token = loginToken,
                    UserInfo = new
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber
                    }
                };
            }
            else
            {
                throw new Exception("Email or Password is invalid!");
            }

            return response;
        }
    }
}
