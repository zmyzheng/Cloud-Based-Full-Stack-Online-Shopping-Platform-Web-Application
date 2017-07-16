namespace Shared.Request
{
    using System.Collections.Generic;

    /// <summary>
    /// Payload for search related operation
    /// </summary>
    public class SearchPayload
    {
        /// <summary>
        /// Gets or sets the paging info
        /// </summary>
        /// <returns> The Paging Info </returns>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// Gets or sets the search term that the request specifies
        /// </summary>
        /// <returns> The search term </returns>
        public IEnumerable<SearchTerm> SearchTerm { get; set; }
    }
}