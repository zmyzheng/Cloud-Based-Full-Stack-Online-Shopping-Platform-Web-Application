namespace AuthService.Policy
{
    using Shared.EnumHelper;

    /// <summary>
    /// Type for resource in policy
    /// </summary>
    public enum Resource
    {
#pragma warning disable CS1591
#pragma warning disable SA1602

        [StringValue("*")]
        Any

#pragma warning restore SA1602
#pragma warning restore CS1591
    }
}
