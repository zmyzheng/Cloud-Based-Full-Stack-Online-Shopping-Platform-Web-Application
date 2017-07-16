namespace Shared.Response
{
    using Shared.Http;

    /// <summary>
    /// Response from common service
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the status code for the response
        /// </summary>
        /// <returns> Status code </returns>
        public HttpCode Status { get; set; } = HttpCode.OK;

        /// <summary>
        /// Gets or sets the results returned from the service
        /// </summary>
        /// <returns> Results </returns>
        public object Payload { get; set; }
    }
}