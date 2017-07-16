namespace EmailService.Mailgun
{
    /// <summary>
    /// Helper for mailgun service
    /// </summary>
    public static class MailgunHelper
    {
        /// <summary>
        /// Gets the url to the api
        /// </summary>
        /// <returns> The URL </returns>
        public static string APIUrl { get; } = "https://api.mailgun.net/v3/mzhou.me/messages";

        /// <summary>
        /// Gets the from content
        /// </summary>
        /// <returns> The From content </returns>
        public static string From { get; } = "Team Typer2 <Typer2@mzhou.me>";
    }
}
