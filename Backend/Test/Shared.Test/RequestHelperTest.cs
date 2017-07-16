namespace Shared.Test
{
    using System;
    using Shared.Request;
    using Xunit;

    public class RequestHelperTest
    {
        [Fact]
        public void ComposeSearchExpShouldReturnCorrectValue()
        {
            var terms = new[]
            {
                new SearchTerm()
                {
                    Field = "Id", Operator = SearchOperator.LE, Value = "5"
                },
                new SearchTerm()
                {
                    Field = "Email", Operator = SearchOperator.LIKE, Value = "2"
                },
                new SearchTerm()
                {
                    Field = "Time", Operator = SearchOperator.GT, Value = "Jan 1, 2001"
                }
            };

            var exp = RequestHelper.ComposeSearchExp(terms, "Test", false);
            Assert.Equal("SELECT * FROM Test WHERE Id <= @Id AND Email LIKE @Email AND Time > @Time", exp);
        }

        [Fact]
        public void ComposeSearchExpShouldReturnCorrectValueWithPagingInfo()
        {
            var terms = new[]
            {
                new SearchTerm()
                {
                    Field = "Id", Operator = SearchOperator.LE, Value = "5"
                },
                new SearchTerm()
                {
                    Field = "Email", Operator = SearchOperator.LIKE, Value = "2"
                },
                new SearchTerm()
                {
                    Field = "Time", Operator = SearchOperator.GT, Value = "Jan 1, 2001"
                }
            };

            var exp = RequestHelper.ComposeSearchExp(terms, "Test", true);
            Assert.Equal("SELECT * FROM Test WHERE Id <= @Id AND Email LIKE @Email AND Time > @Time LIMIT @Start,@Count", exp);
        }

        [Fact]
        public void GetSearchObjectShouldReturnCorrectValue()
        {
            var terms = new[]
            {
                new SearchTerm()
                {
                    Field = "Id", Operator = SearchOperator.LE, Value = "5"
                },
                new SearchTerm()
                {
                    Field = "Email", Operator = SearchOperator.LIKE, Value = "2"
                },
                new SearchTerm()
                {
                    Field = "Time", Operator = SearchOperator.GT, Value = "Jan 1, 2001"
                }
            };

            var obj = (dynamic)RequestHelper.GetSearchObject(terms, null);
            Assert.Equal(5, obj.Id);
            Assert.Equal("%2%", obj.Email);
            Assert.Equal(new DateTime(2001, 1, 1), obj.Time);
        }

        [Fact]
        public void GetSearchObjectShouldReturnCorrectValueWithPagingInfo()
        {
            var terms = new[]
            {
                new SearchTerm()
                {
                    Field = "Id", Operator = SearchOperator.LE, Value = "5"
                },
                new SearchTerm()
                {
                    Field = "Email", Operator = SearchOperator.LIKE, Value = "2"
                },
                new SearchTerm()
                {
                    Field = "Time", Operator = SearchOperator.GT, Value = "Jan 1, 2001"
                }
            };
            var pi = new PagingInfo()
            {
                Start = 0,
                Count = 1
            };

            var obj = (dynamic)RequestHelper.GetSearchObject(terms, pi);
            Assert.Equal(5, obj.Id);
            Assert.Equal("%2%", obj.Email);
            Assert.Equal(new DateTime(2001, 1, 1), obj.Time);
            Assert.Equal(0, obj.Start);
            Assert.Equal(1, obj.Count);
        }
    }
}