using Game.Engine.Core.Elements;

namespace Game.Engine.Core
{
    public interface IEmptyAction : IAction
    {
        IElement Element { get; }
    }
}
