namespace QueueService.Queue
{
    using System.Data;
    using Amazon.SQS;
    using Dapper.Contrib.Extensions;
    using Newtonsoft.Json;
    using Shared.Http;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Queue;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Command class for queue command
    /// </summary>
    public class QueueCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueCommand"/> class.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public QueueCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke the command and get the response from SQS
        /// </summary>
        /// <param name="request"> The request to perform </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();
            var queueRequest = request as QueueRequest;
            queueRequest.Validate();

            var sqsClient = new AmazonSQSClient();

            var sqsResponse = sqsClient.SendMessageAsync(
                QueueHelper.RequestQueueUrl,
                JsonConvert.SerializeObject(queueRequest)
            ).Result;

            var qp = new QueueProcess()
            {
                QueueId = sqsResponse.MessageId
            };

            this.connection.Insert<QueueProcess>(qp);

            response.Payload = sqsResponse.MessageId;
            response.Status = HttpCode.Accepted;

            return response;
        }
    }
}
