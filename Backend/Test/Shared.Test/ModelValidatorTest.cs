namespace Shared.Test
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Model;
    using Shared.Validation;
    using Xunit;

    public class ModelValidatorTest
    {
        [Fact]
        public void ValidatorShouldSuccessForValidField()
        {
            var u = new User()
            {
                Id = 10,
                Email = "test@test.com",
                PwdHash = "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
                FirstName = "Test",
                LastName = "Test"
            };

            u.Validate();
        }

        [Fact]
        public void ValidatorShouldFailForInvalidField()
        {
            var u = new User()
            {
                Id = 1,
                Email = "test@test.com",
                PwdHash = "0",
                FirstName = "Test",
                LastName = "Test"
            };

            Assert.Throws(typeof(ValidationException), () => u.Validate());
        }
    }
}