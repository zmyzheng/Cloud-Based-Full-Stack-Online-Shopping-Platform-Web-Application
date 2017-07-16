namespace Shared.Queue
{
    /// <summary>
    /// Type of the service that take queued request
    /// </summary>
    public enum Service
    {
#pragma warning disable CS1591
#pragma warning disable SA1602

        OrderService,

        PaymentService,

        UserService

#pragma warning restore SA1602
#pragma warning restore CS1591
    }
}
