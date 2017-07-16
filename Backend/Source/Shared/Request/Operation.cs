namespace Shared.Request
{
    /// <summary>
    /// Types for operation a service can perform, CRUD
    /// </summary>
    public enum Operation
    {
#pragma warning disable CS1591
#pragma warning disable SA1602

        // supported DB operation
        Create,
        Delete,
        Update,
        Read,

        // supported user operation
        SignUp,
        LogIn,
        VerifyEmail,

        // supported queue operation
        Queue,

        // support for sending email
        Send,

        // support for verifying user
        VerifyUser

#pragma warning restore SA1602
#pragma warning restore CS1591
    }
}
