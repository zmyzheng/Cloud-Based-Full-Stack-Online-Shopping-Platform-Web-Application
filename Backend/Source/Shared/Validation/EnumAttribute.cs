namespace Shared.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Attribute for enum value to be validated
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class EnumAttribute : ValidationAttribute
    {
        /// <summary>
        /// Check if the given value is a valid enum
        /// </summary>
        /// <param name="value"> The value to be validated </param>
        /// <returns> If the value is valid </returns>
        public override bool IsValid(object value)
        {
            return Enum.IsDefined(value.GetType(), value);
        }
    }
}