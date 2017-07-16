namespace QueueService.Read
{
    using System.Data;
    using System.Linq;
    using Amazon.SQS;
    using Dapper;
    using QueueService.Model;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Queue;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// The command class for the read command
    /// </summary>
    public class ReadCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public ReadCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke the command and process the request
        /// </summary>
        /// <param name="request"> The request to process </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();
            var payload = request.Payload.ToObject<ReadPayload>();
            payload.Validate();

            var qp = this.connection.Query<QueueProcess>($"SELECT * FROM {DbHelper.GetTableName<QueueProcess>()} WHERE QueueId = @QId",
                                                         new { QId = payload.Id }).First();

            if (string.IsNullOrEmpty(qp.ProcessId))
            {
                response.Payload = "Processing...";
            }
            else
            {
                var sqsClient = new AmazonSQSClient();
                var processedQueue = sqsClient.ReceiveMessageAsync(QueueHelper.ProcessedQueueUrl).Result;
                var res = processedQueue.Messages.FirstOrDefault(m => m.MessageId == qp.ProcessId);
                if (res == null)
                {
                    response.Payload = "Processed.";
                }
                else
                {
                    response.Payload = res.Body;
                }
            }

            return response;
        }
    }
}
