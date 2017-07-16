namespace Shared.Test
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Request;
    using Shared.Validation;
    using Xunit;

    public class RequestValidatorTest
    {
        [Fact]
        public void ValidatorShouldSuccessForValidOperation()
        {
            var r = new Request()
            {
                Operation = Operation.Create
            };

            r.Validate();
        }

        [Fact]
        public void ValidatorShouldFailForInvalidOperation()
        {
            var r = new Request()
            {
                Operation = (Operation)100
            };

            Assert.Throws(typeof(ValidationException), () => r.Validate());
        }
    }
}