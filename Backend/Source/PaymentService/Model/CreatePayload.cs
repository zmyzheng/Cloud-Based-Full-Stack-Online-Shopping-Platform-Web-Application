namespace PaymentService.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;
    using System;

    /// <summary>
    /// Payload for Create Command
    /// </summary>
    public class CreatePayload : IModel
    {
        /// <summary>
        /// Gets or sets payment's orderId
        /// </summary>
        /// <returns>Return orderId</returns>
        [Required]
        [Range(0, int.MaxValue)]
        public int OrderId { get; set; }
        /// <summary>
        /// Gets or sets payment's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
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
        // [Required]
        // public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets payment's charge
        /// </summary>
        /// <returns>Return charge</returns>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Charge { get; set; }
    }
}