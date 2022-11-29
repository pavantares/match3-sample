using Cysharp.Threading.Tasks;
using Game.Engine.Core;

namespace Game.Gameplay.ActionStates
{
    public class EmptyActionState : ActionStateBase
    {
        public override async UniTask Render(ActorsFactory actorsFactory, IBoard board, IState state)
        {
            var noAction = state.Action as IEmptyAction;
            var elementActor = actorsFactory.GetElementActor(noAction.Element.Id);

            if (elementActor != null)
            {
                await elementActor.RenderEmpty();
            }
        }
    }
}
