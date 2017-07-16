namespace Shared.DbAccess
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using MySql.Data.MySqlClient;
    using Shared.Interface;

    /// <summary>
    /// Helper class for DB access
    /// </summary>
    public static class DbHelper
    {
        private static string dbConnStr = "Server=coms6998.cjxpxg26eyfq.us-east-1.rds.amazonaws.com;Uid=admin;Pwd=columbia.edu;Database=coms6998;";

        /// <summary>
        /// Gets the underlying DB Connection
        /// </summary>
        /// <returns> The IDbConnection object </returns>
        public static IDbConnection Connection => new MySqlConnection(dbConnStr);

        /// <summary>
        /// Get the TableName from the model attribute
        /// </summary>
        /// <typeparam name="T"> The type for the data model </typeparam>
        /// <returns> The TableName </returns>
        public static string GetTableName<T>()
        where T : IModel
        {
            var attrs = typeof(T).GetTypeInfo().GetCustomAttributes(false).OfType<TableAttribute>();

            if (attrs.Count() == 0)
            {
                throw new ArgumentException($"{typeof(T).Name} does not have TableAttribute.");
            }

            return attrs.First().Name;
        }
    }
}