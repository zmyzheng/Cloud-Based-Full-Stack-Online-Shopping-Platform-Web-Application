namespace EmailService.Send
{
    using System.Collections.Generic;
    using System.Net.Http;
    using EmailService.Mailgun;
    using EmailService.Model;
    using Shared.Interface;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Command for sending emails
    /// </summary>
    public class SendCommand : ICommand
    {
        /// <summary>
        /// Invoke the send command to send emails
        /// </summary>
        /// <param name="request"> Request for send command </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<SendPayload>();
            payload.Validate();

            var query = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", MailgunHelper.From),
                new KeyValuePair<string, string>("to", payload.To),
                new KeyValuePair<string, string>("subject", payload.Subject),
                new KeyValuePair<string, string>("html", payload.Body)
            });

            var handler = new HttpClientHandler()
            {
                Credentials = new System.Net.NetworkCredential("api", Key.MailgunApiKey)
            };
            var client = new HttpClient(handler);

            response.Payload = client.PostAsync(MailgunHelper.APIUrl, query).Result.IsSuccessStatusCode;
            return response;
        }
    }
}
