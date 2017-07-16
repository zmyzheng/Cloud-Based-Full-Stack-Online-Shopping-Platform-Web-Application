namespace Shared.Request
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// Payload for verify user operation
    /// </summary>
    public class VerifyUserPayload : IModel
    {
        /// <summary>
        /// Gets or sets the Id for the requested resource
        /// </summary>
        /// <returns> The resource Id </returns>
        [Required]
        public int ResourceId { get; set; }
    }
}
