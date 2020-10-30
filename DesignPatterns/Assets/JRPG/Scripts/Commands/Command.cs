public abstract class Command
{
    public abstract void ExecuteCommand();

    protected abstract void CommandCompleted();
}
