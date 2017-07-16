namespace Shared.Test
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Request;
    using Shared.Validation;
    using Xunit;

    public class SearchPayloadValidatorTest
    {
        [Fact]
        public void ValidatorShouldSuccessForValidField()
        {
            var r = new SearchPayload()
            {
                SearchTerm = new[]
                {
                    new SearchTerm()
                    {
                        Field = "Field",
                        Operator = SearchOperator.EQ,
                        Value = "value"
                    }
                }
            };

            r.Validate();
        }

        [Fact]
        public void ValidatorShouldFailForInvalidField()
        {
            var r = new SearchPayload()
            {
                SearchTerm = new[]
                {
                    new SearchTerm()
                    {
                        Field = "Field",
                        Operator = (SearchOperator)100,
                        Value = "value"
                    }
                }
            };

            Assert.Throws(typeof(ValidationException), () => r.Validate());
        }
    }
}