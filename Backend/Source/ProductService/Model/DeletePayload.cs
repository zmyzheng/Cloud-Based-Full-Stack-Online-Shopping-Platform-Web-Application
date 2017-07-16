namespace ProductService.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// The payload for delete command
    /// </summary>
    public class DeletePayload : IModel
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        /// <returns> The Id </returns>
        [Required]
        public int Id { get; set; }
    }
}