using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "ProcessService.Test"
)]

namespace ProcessService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Amazon.Lambda;
    using Amazon.Lambda.Core;
    using Amazon.Lambda.Model;
    using Amazon.SQS;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Newtonsoft.Json;
    using Shared;
    using Shared.DbAccess;
    using Shared.Model;
    using Shared.Queue;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Process lambda function handler
        /// </summary>
        /// <returns> Nothing </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public async Task FunctionHandler()
        {
            var connection = DbHelper.Connection;

            var sqsClient = new AmazonSQSClient();
            var sqsResponse = await sqsClient.ReceiveMessageAsync(QueueHelper.RequestQueueUrl);
            var lambdaClient = new AmazonLambdaClient();

            foreach (var message in sqsResponse.Messages)
            {
                var body = message.Body;
                var handler = message.ReceiptHandle;

                var queueRequest = JsonConvert.DeserializeObject<QueueRequest>(body);

                var invokeRequest = new InvokeRequest()
                {
                    FunctionName = Enum.GetName(typeof(Service), queueRequest.TargetService),
                    Payload = JsonConvert.SerializeObject(queueRequest)
                };

                var lambdaResponse = await lambdaClient.InvokeAsync(invokeRequest);
                var response = new StreamReader(lambdaResponse.Payload).ReadToEnd();

                if (!string.IsNullOrEmpty(queueRequest.CallbackUrl))
                {
                    var httpClient = new HttpClient();
                    await httpClient.PostAsync(
                        queueRequest.CallbackUrl,
                        new StringContent(response)
                    );
                }

                await sqsClient.DeleteMessageAsync(QueueHelper.RequestQueueUrl, handler);
                var processed = await sqsClient.SendMessageAsync(
                    QueueHelper.ProcessedQueueUrl,
                    response
                );

                connection.Execute($"UPDATE {DbHelper.GetTableName<QueueProcess>()} SET ProcessId = @PId WHERE QueueId = @QId",
                                   new { PId = processed.MessageId, QId = message.MessageId });
            }
        }
    }
}
