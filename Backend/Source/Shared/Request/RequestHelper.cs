namespace Shared.Request
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Helper class for handling request
    /// </summary>
    public static class RequestHelper
    {
        private static Dictionary<SearchOperator, string> operatorMapping = new Dictionary<SearchOperator, string>()
        {
            [SearchOperator.EQ] = "=",
            [SearchOperator.GT] = ">",
            [SearchOperator.GE] = ">=",
            [SearchOperator.LT] = "<",
            [SearchOperator.LE] = "<=",
            [SearchOperator.NE] = "!=",
            [SearchOperator.LIKE] = "LIKE"
        };

        /// <summary>
        /// Compose the expression used in where method from the search terms
        /// </summary>
        /// <param name="searchTerms"> Search Terms from the request </param>
        /// <param name="tableName"> The table name for the search </param>
        /// <param name="hasPagingInfo"> Indicates if the paging info is presented </param>
        /// <returns> Composed expression </returns>
        public static string ComposeSearchExp(IEnumerable<SearchTerm> searchTerms, string tableName, bool hasPagingInfo)
        {
            StringBuilder searchStr = new StringBuilder();
            searchStr.Append($"SELECT * FROM {tableName} WHERE");

            var terms = searchTerms.ToArray();
            for (int i = 0; i < terms.Length; i++)
            {
                searchStr.Append($"{(i > 0 ? " AND" : string.Empty)} {terms[i].Field} {operatorMapping[terms[i].Operator]} @{terms[i].Field}");
            }

            if (hasPagingInfo)
            {
                searchStr.Append(" LIMIT @Start,@Count");
            }

            return searchStr.ToString();
        }

        /// <summary>
        /// Get the search object used in dapper query with paging info
        /// </summary>
        /// <param name="searchTerms"> Search Terms to contain </param>
        /// <param name="pagingInfo"> Paging info to contain </param>
        /// <returns> Generated object </returns>
        public static object GetSearchObject(IEnumerable<SearchTerm> searchTerms, PagingInfo pagingInfo)
        {
            dynamic obj = new ExpandoObject();
            var dict = obj as IDictionary<string, object>;

            foreach (var term in searchTerms)
            {
                switch (term.Operator)
                {
                    case SearchOperator.EQ:
                    case SearchOperator.GE:
                    case SearchOperator.GT:
                    case SearchOperator.LE:
                    case SearchOperator.LT:
                    case SearchOperator.NE:
                        decimal val;

                        if (decimal.TryParse(term.Value, out val))
                        {
                            dict.Add(term.Field, val);
                        }
                        else
                        {
                            dict.Add(term.Field, DateTime.Parse(term.Value));
                        }

                        break;
                    case SearchOperator.LIKE:
                        dict.Add(term.Field, "%" + term.Value + "%");
                        break;
                }
            }

            if (pagingInfo != null)
            {
                dict.Add("Start", pagingInfo.Start);
                dict.Add("Count", pagingInfo.Count);
            }

            return obj;
        }
    }
}