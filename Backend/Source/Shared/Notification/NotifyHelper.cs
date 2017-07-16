namespace Shared.Notification
{
    using Amazon.SimpleNotificationService;
    using Amazon.SimpleNotificationService.Model;

    /// <summary>
    /// Helper class for sending notifications
    /// </summary>
    public static class NotifyHelper
    {
        /// <summary>
        /// Gets the arn of the notification topic
        /// </summary>
        /// <returns> The Arn </returns>
        public static string TopicArn { get; } = "arn:aws:sns:us-east-1:165669929949:E6998SlackTopic";

        /// <summary>
        /// Push a notification to the topic
        /// </summary>
        /// <param name="message"> The message to publish </param>
        /// <returns> The publish response </returns>
        public static PublishResponse PushNotification(string message)
        {
            var client = new AmazonSimpleNotificationServiceClient();
            var res = client.PublishAsync(TopicArn, message).Result;
            return res;
        }
    }
}
