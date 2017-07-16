namespace QueueService.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// Payload for read command
    /// </summary>
    public class ReadPayload : IModel
    {
        /// <summary>
        /// Gets or sets the ID for the queued request
        /// </summary>
        /// <returns> The ID </returns>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }
    }
}