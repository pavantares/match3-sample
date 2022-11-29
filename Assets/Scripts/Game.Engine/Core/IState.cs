namespace Game.Engine.Core
{
    public interface IState
    {
        IField Field { get; }
        IAction Action { get; }
        IState PreviousState { get; set; }
        IState NextState { get; set; }
    }
}
