namespace ProductService.Update
{
    using System;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using ProductService.Model;
    using Shared.DbAccess;
    using Shared.Http;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// The command for Update Operation
    /// </summary>
    public class UpdateCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommand"/> class.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public UpdateCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="request"> The request for this command </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<UpdatePayload>();
            payload.Validate();

            var product = this.connection.Query<Product>(
                $"SELECT * FROM {DbHelper.GetTableName<Product>()} WHERE Id = @Id",
                new { Id = payload.Id }
            ).First();

            string val;
            decimal num;
            product.Name = payload.Change.TryGetValue("Name", out val) ? val : product.Name;
            product.PicUri = payload.Change.TryGetValue("PicUri", out val) ? val : product.PicUri;
            product.Description = payload.Change.TryGetValue("Description", out val) ? val : product.Description;
            if (payload.Change.TryGetValue("Price", out val))
            {
                if (decimal.TryParse(val, out num))
                {
                    product.Price = num;
                }
                else
                {
                    throw new Exception("Invalid input price!");
                }
            }
            else
            {
                product.Price = product.Price;
            }

            product.Price = decimal.TryParse(val, out num) ? num : product.Price;
            product.Validate();

            this.connection.Update<Product>(product);

            return response;
        }
    }
}