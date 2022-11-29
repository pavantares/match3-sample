namespace Game.Engine.Core
{
    public interface ISwapAction : IAction
    {
        Point FromPoint { get; }
        Point ToPoint { get; }
    }
}
