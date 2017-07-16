using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "Shared.Test"
)]

namespace Shared
{
    using System;
    using System.IO;
    using Amazon.Lambda.Core;
    using Newtonsoft.Json;
    using Shared.Http;

    /// <summary>
    /// Custom serializer for lambda function to throw lambda exception when failing to parse the request
    /// </summary>
    public class LambdaSerializer : ILambdaSerializer
    {
        private readonly JsonSerializer serializer = new JsonSerializer();

        /// <summary>
        /// Serializes a particular object to a stream.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="response">Object to serialize.</param>
        /// <param name="responseStream">Output stream.</param>
        public void Serialize<T>(T response, Stream responseStream)
        {
            StreamWriter writer = new StreamWriter(responseStream);
            try
            {
                this.serializer.Serialize(writer, response);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.InternalServerError, ex.Message);
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a stream to a particular type.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize to.</typeparam>
        /// <param name="requestStream">Stream to serialize.</param>
        /// <returns>Deserialized object from stream.</returns>
        public T Deserialize<T>(Stream requestStream)
        {
            StreamReader reader = new StreamReader(requestStream);
            JsonReader jsonReader = new JsonTextReader(reader);

            T obj;
            try
            {
                obj = this.serializer.Deserialize<T>(jsonReader);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }

            if (obj == null)
            {
                throw new LambdaException(HttpCode.BadRequest, "Cannot deserialize the request object.");
            }

            return obj;
        }
    }
}