using Cysharp.Threading.Tasks;
using Game.Engine.Core;

namespace Game.Gameplay.ActionStates
{
    public class SwapActionState : ActionStateBase
    {
        public override async UniTask Render(ActorsFactory actorsFactory, IBoard board, IState state)
        {
            var swapAction = state.Action as ISwapAction;
            var fromPoint = swapAction.FromPoint;
            var toPoint = swapAction.ToPoint;
            var previousField = state.PreviousState.Field;

            var fromElement = previousField.GetElementAt(fromPoint);
            var toElement = previousField.GetElementAt(toPoint);

            var fromElementActor = actorsFactory.GetElementActor(fromElement.Id);
            var toElementActor = actorsFactory.GetElementActor(toElement.Id);
            var task0 = fromElementActor.MoveTo(toPoint);
            var task1 = toElementActor.MoveFrom(fromPoint);

            await UniTask.WhenAll(task0, task1);
        }
    }
}
