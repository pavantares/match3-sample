using Cysharp.Threading.Tasks;
using Game.Engine.Core;

namespace Game.Gameplay.ActionStates
{
    public abstract class ActionStateBase
    {
        public abstract UniTask Render(ActorsFactory actorsFactory, IBoard board, IState state);
    }
}
