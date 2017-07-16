namespace EmailService.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// The payload for send request to the email service
    /// </summary>
    public class SendPayload : IModel
    {
        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        /// <returns> Email Address </returns>
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        /// <returns> Email Subject </returns>
        [Required(AllowEmptyStrings = false)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        /// <returns> Email Body </returns>
        [Required(AllowEmptyStrings = false)]
        public string Body { get; set; }
    }
}
