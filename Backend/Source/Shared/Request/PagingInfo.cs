namespace Shared.Request
{
    /// <summary>
    /// Class contains the required paging info
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// Gets or sets the start number
        /// </summary>
        /// <returns> The Start number </returns>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the count
        /// </summary>
        /// <returns> The Count </returns>
        public int Count { get; set; }
    }
}