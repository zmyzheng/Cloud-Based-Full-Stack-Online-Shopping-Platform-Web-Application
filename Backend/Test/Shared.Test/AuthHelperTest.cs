namespace Shared.Test
{
    using System;
    using Shared.Authentication;
    using Xunit;

    public class AuthHelperTest
    {
        [Fact]
        public void GetAuthPayloadTest()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjAsIkVtYWlsIjoiVGVzdEB0ZXN0LmNvbSIsIkZpcnN0TmFtZSI6IlRlc3QiLCJMYXN0TmFtZSI6IlRlc3QiLCJEYXRlVGltZSI6IjAwMDEtMDEtMDFUMDA6MDA6MDAifQ.NnHFVebJTMQ7xqx4s33CMpds1mEIIkgAyuebGbK5_2g";

            var payload = AuthHelper.GetAuthPayload(token);
            Assert.Equal(0, payload.UserId);
            Assert.Equal("Test@test.com", payload.Email);
            Assert.Equal("Test", payload.FirstName);
            Assert.Equal("Test", payload.LastName);
            Assert.Equal(default(DateTime), payload.DateTime);
        }

        [Fact]
        public void GetCustomAuthPayloadTest()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJUZXN0IjoiVGVzdCJ9.NNq_gjZsuFaw058z1P1iFNTKICE1E_OIBh3YTdZaDdc";

            var payload = AuthHelper.GetCustomAuthPayload<CustomAuthPayload>(token);
            Assert.Equal("Test", payload.Test);
        }

        [Fact]
        public void GenerateAuthTokenTest()
        {
            var payload = new AuthPayload()
            {
                UserId = 0,
                Email = "Test@test.com",
                FirstName = "Test",
                LastName = "Test",
                DateTime = default(DateTime)
            };

            var token = AuthHelper.GenerateAuthToken(payload);
            Assert.Equal("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjAsIkVtYWlsIjoiVGVzdEB0ZXN0LmNvbSIsIkZpcnN0TmFtZSI6IlRlc3QiLCJMYXN0TmFtZSI6IlRlc3QiLCJEYXRlVGltZSI6IjAwMDEtMDEtMDFUMDA6MDA6MDAifQ.NnHFVebJTMQ7xqx4s33CMpds1mEIIkgAyuebGbK5_2g", token);
        }

        [Fact]
        public void GenerateCustomAuthTokenTest()
        {
            var payload = new CustomAuthPayload()
            {
                Test = "Test"
            };

            var token = AuthHelper.GenerateCustomAuthToken(payload);
            Assert.Equal("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJUZXN0IjoiVGVzdCJ9.NNq_gjZsuFaw058z1P1iFNTKICE1E_OIBh3YTdZaDdc", token);
        }

        private class CustomAuthPayload
        {
            public string Test { get; set; }
        }
    }
}