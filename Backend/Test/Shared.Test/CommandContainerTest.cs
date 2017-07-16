namespace Shared.Test
{
    using System;
    using Shared.Command;
    using Shared.Interface;
    using Shared.Request;
    using Shared.Response;
    using Xunit;

    public class CommandContainerTest
    {
        [Fact]
        public void ContainerShouldWork()
        {
            var container = new CommandContainer();
            container.RegisterRequirement<ICommand, TestCommand>()
                     .Register<TestCommand2>(Operation.Create);
            var request = new Request() {
                Operation = Operation.Create
            };

            Assert.Equal(typeof(TestCommand), container.Process(request).Payload.GetType());
        }

        private class TestCommand : ICommand
        {
            public Response Invoke(Request request)
            {
                throw new NotImplementedException();
            }
        }

        private class TestCommand2 : ICommand
        {
            private ICommand command;

            public TestCommand2(ICommand command)
            {
                this.command = command;
            }

            public Response Invoke(Request request)
            {
                return new Response()
                {
                    Payload = this.command
                };
            }
        }
    }
}
