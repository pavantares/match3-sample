namespace Game.Engine.Core
{
    public interface IAction
    {
        IState Apply(IState previousState = null);
    }
}
