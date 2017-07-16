namespace Shared.Validation
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Request;

    /// <summary>
    /// Static class contains the extension method used for validating search payloads
    /// </summary>
    public static class SearchPayloadValidator
    {
        /// <summary>
        /// Extension method used for validate search payload
        /// </summary>
        /// <param name="payload"> The search payload to validate </param>
        public static void Validate(this SearchPayload payload)
        {
            foreach (var term in payload.SearchTerm)
            {
                Validator.ValidateObject(term, new ValidationContext(term), true);
            }
        }
    }
}