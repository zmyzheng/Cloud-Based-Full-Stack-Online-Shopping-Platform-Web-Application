namespace Shared.EnumHelper
{
    using System;

    /// <summary>
    /// Attribute for annotate enum with string value
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Field, Inherited = true)]
    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueAttribute"/> class.
        /// </summary>
        /// <param name="value"> The value for the enum </param>
        public StringValueAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value for the enum entry
        /// </summary>
        /// <returns> The value </returns>
        public string Value { get; }
    }
}