namespace UserService.SignUp
{
    using System.Net;
    using Shared.Authentication;
    using Shared.Interface;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;

    /// <summary>
    /// Command for SignUp operation
    /// </summary>
    public class SignUpCommand : ICommand
    {
        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> Request used for invoke </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<SignUpPayload>();
            payload.Validate();
            var emailToken = WebUtility.UrlEncode(AuthHelper.GenerateCustomAuthToken(payload));

            response.Payload = new
            {
                Operation = Operation.Send,
                Payload = new
                {
                    To = payload.Email,
                    Subject = EmailTemplate.Subject.Replace("%%NAME%%", payload.FirstName),
                    Body = EmailTemplate.Body.Replace("%%NAME%%", payload.FirstName)
                                             .Replace("%%TOKEN%%", emailToken)
                }
            };
            return response;
        }
    }
}
