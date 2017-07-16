namespace Shared.Command
{
    using System;
    using System.Collections.Generic;
    using Shared.Interface;
    using Shared.Request;
    using Shared.Response;
    using SimpleInjector;

    /// <summary>
    /// Container service for commands
    /// </summary>
    public class CommandContainer
    {
        private readonly Dictionary<Operation, Type> commands = new Dictionary<Operation, Type>();
        private readonly Container container = new Container();

        /// <summary>
        /// Process the given request
        /// </summary>
        /// <param name="request"> The request to process </param>
        /// <returns> The processed response </returns>
        public Response Process(Request request)
        {
            if (!this.commands.ContainsKey(request.Operation))
            {
                throw new ArgumentException($"Operation {Enum.GetName(typeof(Operation), request.Operation)} is not supported.");
            }

            return (this.container.GetInstance(this.commands[request.Operation]) as ICommand).Invoke(request);
        }

        /// <summary>
        /// Process the given request with a specific operation
        /// </summary>
        /// <param name="request"> The request to process </param>
        /// <param name="operation"> The operation to process </param>
        /// <returns> The processed response </returns>
        public Response ProcessWith(Request request, Operation operation)
        {
            if (!this.commands.ContainsKey(operation))
            {
                throw new ArgumentException($"Operation {Enum.GetName(typeof(Operation), operation)} is not supported.");
            }

            return (this.container.GetInstance(this.commands[operation]) as ICommand).Invoke(request);
        }

        /// <summary>
        /// Register a requirement for the commands
        /// </summary>
        /// <typeparam name="TConcrete"> The type of the instance </typeparam>
        /// <returns> This for chaining call</returns>
        public CommandContainer RegisterRequirement<TConcrete>()
        where TConcrete : class
        {
            this.container.Register<TConcrete>();
            return this;
        }

        /// <summary>
        /// Register a requirement for the commands
        /// </summary>
        /// <typeparam name="TInterface"> The type of the interface </typeparam>
        /// <typeparam name="TConcrete"> The type of the instance </typeparam>
        /// <returns> This for chaining call</returns>
        public CommandContainer RegisterRequirement<TInterface, TConcrete>()
        where TInterface : class
        where TConcrete : class, TInterface
        {
            this.container.Register<TInterface, TConcrete>();
            return this;
        }

        /// <summary>
        /// Register a requirement for the commands
        /// </summary>
        /// <param name="func"> The func that returns the instance </param>
        /// <typeparam name="TType"> The type of the interface </typeparam>
        /// <returns> This for chaining call</returns>
        public CommandContainer RegisterRequirement<TType>(Func<TType> func)
        where TType : class
        {
            this.container.Register<TType>(func);
            return this;
        }

        /// <summary>
        /// Register a command with the key
        /// </summary>
        /// <param name="operation"> The Operation to register </param>
        /// <typeparam name="TCommand"> The type of the command </typeparam>
        /// <returns> This for chaining call </returns>
        public CommandContainer Register<TCommand>(Operation operation)
        where TCommand : class, ICommand
        {
            if (this.commands.ContainsKey(operation))
            {
                throw new ArgumentException($"{Enum.GetName(typeof(Operation), operation)} already registered.");
            }

            this.commands.Add(operation, typeof(TCommand));
            this.container.Register<TCommand>();

            return this;
        }
    }
}
