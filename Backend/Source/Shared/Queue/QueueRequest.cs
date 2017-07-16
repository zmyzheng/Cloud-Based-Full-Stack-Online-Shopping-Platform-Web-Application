namespace Shared.Queue
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Shared.Request;
    using Shared.Validation;

    /// <summary>
    /// Request for QueueService
    /// </summary>
    public class QueueRequest : Request
    {
        /// <summary>
        /// Gets or sets the ARN of the target service
        /// </summary>
        /// <returns> The ARN of the target service </returns>
        [Required(AllowEmptyStrings = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Enum]
        public Service TargetService { get; set; }

        /// <summary>
        /// Gets or sets the callback url for the request
        /// </summary>
        /// <returns> The callback url </returns>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the operations for QueueService
        /// </summary>
        /// <returns> The Operation for QueueService </returns>
        public Operation QueueOperation { get; set; }
    }
}
