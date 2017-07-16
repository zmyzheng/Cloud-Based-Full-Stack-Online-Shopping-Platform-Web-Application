namespace PaymentService
{
    using Stripe;
    using System;

    ///<summary>
    /// create charge use this class
    ///</summary>
    public class Charge
    {
        ///<summary>
        /// create a charge
        ///</summary>
        /// <returns> create status </returns>
        public static void createCharge(string tokenId, int amount)
        {
            StripeConfiguration.SetApiKey("sk_test_FUcSFJJpbnqyEdEAbzdjvkwg");
            var myCharge = new StripeChargeCreateOptions();
            myCharge.Amount = amount;
            myCharge.Currency = "usd";
            myCharge.SourceTokenOrExistingSourceId = tokenId;
            var chargeService = new StripeChargeService();
            StripeCharge stripeCharge = chargeService.Create(myCharge);
            var success = string.IsNullOrEmpty(stripeCharge.FailureCode) &&
                          string.IsNullOrEmpty(stripeCharge.FailureMessage);
            if (!success)
            {
                throw new Exception("payment creation failed");
            }
        }
    }
}