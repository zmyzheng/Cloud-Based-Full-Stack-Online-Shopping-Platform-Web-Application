namespace Shared
{
    using System;
    using Shared.Http;

    /// <summary>
    /// Exception class for lambda contains http status code and message
    /// </summary>
    public class LambdaException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaException"/> class
        /// </summary>
        /// <param name="message"> Error message </param>
        public LambdaException(string message)
            : base($"{message}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaException"/> class
        /// </summary>
        /// <param name="code"> Http status code </param>
        /// <param name="message"> Error message </param>
        public LambdaException(HttpCode code, string message)
            : base($"{(int)code} | {message}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaException"/> class
        /// </summary>
        /// <param name="code"> Http status code </param>
        /// <param name="ex"> Exception </param>
        public LambdaException(HttpCode code, Exception ex)
            : base($"{(int)code} | {ex.Message}")
        {
        }
    }
}
