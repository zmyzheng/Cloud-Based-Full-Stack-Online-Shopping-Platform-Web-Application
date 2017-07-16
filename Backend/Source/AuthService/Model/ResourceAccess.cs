using Shared.EnumHelper;
using Shared.Http;

namespace AuthService.Model
{
    /// <summary>
    /// Class for holding access to resource
    /// </summary>
    public class ResourceAccess
    {
        /// <summary>
        /// Gets or sets the Http verb related to the access
        /// </summary>
        /// <returns> The Http Verb </returns>
        public string HttpVerb { get; set; }

        /// <summary>
        /// Gets or sets the resource to which the access is performed
        /// </summary>
        /// <returns> The Resource </returns>
        public string Resource { get; set; }

        public ResourceAccess(string verb, string resource)
        {
            this.HttpVerb = verb;
            this.Resource = resource;
        }
    }
}
