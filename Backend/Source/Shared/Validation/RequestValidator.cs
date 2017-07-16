namespace Shared.Validation
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Request;

    /// <summary>
    /// Static class contains the extension method used for validating requests
    /// </summary>
    public static class RequestValidator
    {
        /// <summary>
        /// Extension method used to validate the request
        /// </summary>
        /// <param name="request"> The request to validate </param>
        public static void Validate(this Request request)
        {
            Validator.ValidateObject(request, new ValidationContext(request), true);
        }
    }
}