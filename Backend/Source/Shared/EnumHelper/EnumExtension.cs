namespace Shared.EnumHelper
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extension methods for enum
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Extension method for enum to get the string values
        /// </summary>
        /// <param name="entry"> The enum entry </param>
        /// <returns> The string value </returns>
        public static string GetStringValue(this Enum entry)
        {
            var type = entry.GetType();
            var name = Enum.GetName(type, entry);
            var attrs = type.GetField(name).GetCustomAttributes(false).OfType<StringValueAttribute>();

            if (attrs.Count() == 0)
            {
                throw new ArgumentException($"{name} does not have a string value.");
            }

            return attrs.First().Value;
        }
    }
}