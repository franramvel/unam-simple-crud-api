using DB.Command.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DB.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand query)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
            return await handler.Handle(query);
        }
    }
}
