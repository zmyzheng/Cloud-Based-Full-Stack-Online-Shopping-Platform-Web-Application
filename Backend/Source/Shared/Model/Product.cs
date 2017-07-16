namespace Shared.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Shared.Interface;

    /// <summary>
    /// Define  product class
    /// </summary>
    [Table("Products")]
    public sealed class Product : IModel
    {
        /// <summary>
        /// Gets or sets product's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets product's name
        /// </summary>
        /// <returns>Return name</returns>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product's price
        /// </summary>
        /// <returns>Return price</returns>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets product's description
        /// </summary>
        /// <returns>Return description</returns>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets product's picture url
        /// </summary>
        /// <returns>Return PicUri</returns>
        [Required(AllowEmptyStrings = false)]
        [Url]
        public string PicUri { get; set; }
    }
}