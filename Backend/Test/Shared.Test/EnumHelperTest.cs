namespace Shared.Test
{
    using Shared.EnumHelper;
    using Xunit;

    public class EnumHelperTest
    {
        private enum Test
        {
            [StringValue("Test")]
            Test,

            [StringValue("Test2")]
            Test2
        }

        [Fact]
        public void EnumStringValueTest()
        {
            Assert.Equal("Test2", Test.Test2.GetStringValue());
        }
    }
}