using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "NotificationService.Test"
)]

namespace NotificationService
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Amazon.Lambda.Core;
    using Amazon.Lambda.SNSEvents;
    using Newtonsoft.Json;
    using Shared;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        private const string SlackUrl = "https://hooks.slack.com/services/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";

        /// <summary>
        /// Lambda handler for notification service
        /// </summary>
        /// <param name="request"> SNS event object </param>
        /// <returns> Nothing </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed.")]
        public async Task FunctionHandler(SNSEvent request)
        {
            var client = new HttpClient();

            foreach (var r in request.Records)
            {
                await client.PostAsync(
                    SlackUrl,
                    new StringContent(
                        JsonConvert.SerializeObject(
                            new
                            {
                                text = r.Sns.Message
                            }
                        )
                    )
                );
            }
        }
    }
}
