namespace UserService.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// Payload for SignUp Command
    /// </summary>
    public sealed class SignUpPayload : IModel
    {
        /// <summary>
        /// Gets or sets user's email
        /// </summary>
        /// <returns>Return email</returns>
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        /// <returns> Password </returns>
        [Required(AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets user's firstname
        /// </summary>
        /// <returns>Return firstname</returns>
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user's lastname
        /// </summary>
        /// <returns>Return lastname</returns>
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user's phone number
        /// </summary>
        /// <returns>Return phone number</returns>
        [Phone]
        public string PhoneNumber { get; set; }
    }
}