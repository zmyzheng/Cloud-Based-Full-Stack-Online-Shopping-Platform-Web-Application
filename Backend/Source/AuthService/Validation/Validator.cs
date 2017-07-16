namespace AuthService.Validation
{
    using System;
    using System.Collections.Generic;
    using Shared.Authentication;

    /// <summary>
    /// Validator class for validate tokens with given type
    /// </summary>
    public static class Validator
    {
        private static Dictionary<AuthTokenType, Action<string>> validator = new Dictionary<AuthTokenType, Action<string>>()
        {
            [AuthTokenType.JWT] = (token) => AuthHelper.GetAuthPayload(token),
            [AuthTokenType.Facebook] = (token) => { }
        };

        /// <summary>
        /// Validate a token with the specific type
        /// </summary>
        /// <param name="type"> The token type </param>
        /// <param name="token"> The token </param>
        /// <returns> If the token is valid </returns>
        public static bool Validate(AuthTokenType type, string token)
        {
            try
            {
                validator[type](token);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}