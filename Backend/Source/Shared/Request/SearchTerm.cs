namespace Shared.Request
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Shared.Validation;

    /// <summary>
    /// SearchTerm class contains essential parts used in a search operation
    /// </summary>
    public class SearchTerm
    {
        /// <summary>
        /// Gets or sets the field to be search from
        /// </summary>
        /// <returns> The field to search </returns>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the operator used in the search term
        /// </summary>
        /// <returns> The search operator </returns>
        [JsonConverter(typeof(StringEnumConverter))]
        [Enum]
        public SearchOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the value used for such search
        /// </summary>
        /// <returns> The search value </returns>
        public string Value { get; set; }
    }
}