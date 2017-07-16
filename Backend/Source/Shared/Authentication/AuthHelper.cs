namespace Shared.Authentication
{
    using System.Text;
    using Jose;
    using Newtonsoft.Json;

    /// <summary>
    /// Helper class for Authentication
    /// </summary>
    public class AuthHelper
    {
        private static byte[] jwtSecretKey = Encoding.ASCII.GetBytes("secret 4 6998@S6");

        /// <summary>
        /// Get the decoded Auth payload from the token
        /// </summary>
        /// <param name="token"> Auth token </param>
        /// <returns> Decoded Auth payload </returns>
        public static AuthPayload GetAuthPayload(string token)
        {
            return GetCustomAuthPayload<AuthPayload>(token);
        }

        /// <summary>
        /// Get the decoded Auth payload from custom token
        /// </summary>
        /// <param name="token"> Auth token </param>
        /// <typeparam name="T"> Type for payload </typeparam>
        /// <returns> Decoded custom Auth payload </returns>
        public static T GetCustomAuthPayload<T>(string token)
        {
            return JsonConvert.DeserializeObject<T>(JWT.Decode(token.Substring(7), jwtSecretKey));
        }

        /// <summary>
        /// Get the encoded Auth token with the given payload
        /// </summary>
        /// <param name="payload"> Payload to contain </param>
        /// <returns> Encoded Auth token </returns>
        public static string GenerateAuthToken(AuthPayload payload)
        {
            return GenerateCustomAuthToken(payload);
        }

        /// <summary>
        /// Get the Auth token with given custom payload
        /// </summary>
        /// <param name="payload"> The custom payload </param>
        /// <returns> Encoded Auth token </returns>
        public static string GenerateCustomAuthToken(object payload)
        {
            return "Bearer " + JWT.Encode(payload, jwtSecretKey, JwsAlgorithm.HS256);
        }
    }
}
