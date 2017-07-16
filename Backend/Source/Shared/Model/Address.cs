namespace Shared.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Shared.Interface;
    using Shared.Validation;

    /// <summary>
    /// Define address class
    /// </summary>
    [Table("Addresses")]
    [Address]
    public sealed class Address : IModel
    {
        /// <summary>
        /// Gets or sets id in address
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets state in address
        /// </summary>
        /// <returns>Return state</returns>
        [Required(AllowEmptyStrings = false)]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets city in address
        /// </summary>
        /// <returns>Return city</returns>
        [Required(AllowEmptyStrings = false)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets street in address
        /// </summary>
        /// <returns>Return street</returns>
        [Required(AllowEmptyStrings = false)]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets apartment number in address
        /// </summary>
        /// <returns>Return aptNumber</returns>
        public string AptNumber { get; set; }

        /// <summary>
        /// Gets or sets zipcode in address
        /// </summary>
        /// <returns>Return zipcode</returns>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^\d{5,6}$")]
        public string Zipcode { get; set; }
    }
}