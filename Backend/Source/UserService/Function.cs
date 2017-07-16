using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "UserService.Test"
)]

namespace UserService
{
    using System;
    using System.Data;
    using Amazon.Lambda.Core;
    using Shared;
    using Shared.Command;
    using Shared.DbAccess;
    using Shared.Http;
    using Shared.Notification;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Delete;
    using UserService.LogIn;
    using UserService.Read;
    using UserService.SignUp;
    using UserService.Update;
    using UserService.VerifyEmail;
    using UserService.VerifyUser;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// UserService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <returns> Lambda response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request)
        {
            var container = new CommandContainer();

            container.RegisterRequirement<IDbConnection>(() => DbHelper.Connection)

                     .Register<LogInCommand>(Operation.LogIn)
                     .Register<ReadCommand>(Operation.Read)
                     .Register<SignUpCommand>(Operation.SignUp)
                     .Register<UpdateCommand>(Operation.Update)
                     .Register<DeleteCommand>(Operation.Delete)
                     .Register<VerifyEmailCommand>(Operation.VerifyEmail)
                     .Register<VerifyUserCommand>(Operation.VerifyUser);

            try
            {
                request.Validate();
                NotifyHelper.PushNotification($"{request.Operation} on UserService with payload {request.Payload.ToString()}.");
                return container.Process(request);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
