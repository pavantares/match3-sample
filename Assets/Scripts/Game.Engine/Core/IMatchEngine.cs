using UnityEngine;

namespace Game.Engine.Core
{
    public interface IMatchEngine
    {
        IBoard Board { get; }
        IState BuildLevel(Vector2Int boardSize);
        IState Step(IStepInput stepInput);
        IState AutoStep();
    }
}
