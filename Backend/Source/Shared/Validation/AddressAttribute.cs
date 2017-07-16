namespace Shared.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary>
    /// Attribute to annotate an address
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AddressAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validate the given value
        /// </summary>
        /// <param name="value"> Value to validate </param>
        /// <returns> Is the value valid </returns>
        public override bool IsValid(object value)
        {
            // TODO: add address validate logic
            return true;
        }
    }
}