namespace Shared.Validation
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// Static class contains the extension method used for validating models
    /// </summary>
    public static class ModelValidator
    {
        /// <summary>
        /// Extension method used to validate the model
        /// </summary>
        /// <param name="model"> The model to validate </param>
        public static void Validate(this IModel model)
        {
            Validator.ValidateObject(model, new ValidationContext(model), true);
        }
    }
}