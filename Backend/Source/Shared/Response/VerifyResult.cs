namespace Shared.Response
{
    /// <summary>
    /// Types for response of user verification
    /// </summary>
    public enum VerifyResult
    {
#pragma warning disable CS1591
#pragma warning disable SA1602

        // Verification passed
        Allow,

        // Verification denied
        Deny

#pragma warning restore SA1602
#pragma warning restore CS1591
    }
}
