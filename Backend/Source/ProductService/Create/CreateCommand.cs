namespace ProductService.Create
{
    using System;
    using System.Data;
    using Dapper.Contrib.Extensions;
    using ProductService.Model;
    using Shared;
    using Shared.Http;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// The command for Create Operation
    /// </summary>
    public class CreateCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public CreateCommand(IDbConnection connection)
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

            var payload = request.Payload.ToObject<CreatePayload>();
            payload.Validate();

            var product = new Product()
            {
                Name = payload.Name,
                Price = payload.Price,
                Description = payload.Description,
                PicUri = payload.PicUri
            };
            product.Validate();

            this.connection.Insert<Product>(product);

            return response;
        }
    }
}