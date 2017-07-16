namespace Shared.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Shared.Interface;

    /// <summary>
    /// Define OrderedProduct class
    /// </summary>
    [Table("OrderedProducts")]
    public sealed class OrderedProduct : IModel
    {
        /// <summary>
        /// Gets or sets order id
        /// </summary>
        /// <returns>Return order id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets product id
        /// </summary>
        /// <returns>Return product id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets product count
        /// </summary>
        /// <returns>Return count</returns>
        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }
    }
}