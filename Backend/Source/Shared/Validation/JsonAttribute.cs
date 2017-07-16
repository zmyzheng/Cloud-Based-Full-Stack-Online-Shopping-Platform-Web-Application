namespace Shared.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary>
    /// Attribute to annotate json string
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class JsonAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validate the given value
        /// </summary>
        /// <param name="value"> Value to validate </param>
        /// <returns> Is the value valid </returns>
        public override bool IsValid(object value)
        {
            var str = value as string;
            return str == null || JsonConvert.DeserializeObject(str) != null;
        }
    }
}