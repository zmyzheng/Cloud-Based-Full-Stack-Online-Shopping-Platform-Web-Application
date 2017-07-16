namespace Shared.Interface
{
    using Shared.Request;
    using Shared.Response;

    /// <summary>
    /// Interface hosting the schema for commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Invoke a command and get the result
        /// </summary>
        /// <param name="request"> Invoke command with request </param>
        /// <returns> The command response </returns>
        Response Invoke(Request request);
    }
}