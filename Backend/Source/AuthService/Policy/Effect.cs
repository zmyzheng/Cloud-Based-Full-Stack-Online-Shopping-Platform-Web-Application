namespace AuthService.Policy
{
    using Shared.EnumHelper;

    /// <summary>
    /// Type for the effect in the policy
    /// </summary>
    public enum Effect
    {
#pragma warning disable CS1591
#pragma warning disable SA1602

        [StringValue("Allow")]
        Allow,

        [StringValue("Deny")]
        Deny

#pragma warning restore SA1602
#pragma warning restore CS1591
    }
}
