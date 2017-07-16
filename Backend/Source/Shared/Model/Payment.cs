namespace Shared.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;
    using Shared.Interface;

    /// <summary>
    /// Define the payment class
    /// </summary>
    [Table("Payments")]
    public sealed class Payment : IModel
    {
        /// <summary>
        /// Gets or sets payment's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets payment's orderId
        /// </summary>
        /// <returns>Return orderId</returns>
        [Required]
        [Range(0, int.MaxValue)]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets payment's userId
        /// </summary>
        /// <returns>Return userId</returns>
        [Required]
        [Range(0, int.MaxValue)]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets payment's stripeToken
        /// </summary>
        /// <returns>Return stripeToken</returns>
        [Required(AllowEmptyStrings = false)]
        [StringLength(28, MinimumLength = 28)]
        public string StripeToken { get; set; }

        /// <summary>
        /// Gets or sets payment's dateTime
        /// </summary>
        /// <returns>Return datetime</returns>
        [Required]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets payment's charge
        /// </summary>
        /// <returns>Return charge</returns>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Charge { get; set; }
    }
}