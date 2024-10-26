namespace DB.Command.Interfaces
{

    public interface ICommandHandler<in TCommand, TCommandResult>
        {
            Task<TCommandResult> Handle(TCommand command);
        }
    
}
