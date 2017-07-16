namespace AuthService.Policy
{
    using Shared.EnumHelper;

    /// <summary>
    /// Type for Actions in policy
    /// </summary>
    public enum Action
    {
#pragma warning disable CS1591
#pragma warning disable SA1602

        [StringValue("*")]
        All,

        [StringValue("Invoke")]
        Invoke,

        [StringValue("InvalidateCache")]
        InvalidateCache

#pragma warning restore SA1602
#pragma warning restore CS1591
    }
}
