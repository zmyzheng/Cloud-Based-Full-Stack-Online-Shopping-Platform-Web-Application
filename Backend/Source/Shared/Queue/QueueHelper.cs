namespace Shared.Queue
{
    /// <summary>
    /// Helper class for queue related helpers
    /// </summary>
    public static class QueueHelper
    {
        /// <summary>
        /// Gets the url to the queue
        /// </summary>
        /// <returns> The url to the queue </returns>
        public static string RequestQueueUrl { get; } = "https://sqs.us-east-1.amazonaws.com/165669929949/E6998S6ServiceQueue";

        /// <summary>
        /// Gets the url to the queue holds all the processed message
        /// </summary>
        /// <returns> The url to the queue </returns>
        public static string ProcessedQueueUrl { get; } = "https://sqs.us-east-1.amazonaws.com/165669929949/E6998S6ProcessedQueue";
    }
}