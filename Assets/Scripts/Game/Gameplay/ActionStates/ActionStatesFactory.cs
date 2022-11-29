using Cysharp.Threading.Tasks;
using Game.Engine.Core;
using Game.Logger;

namespace Game.Gameplay.ActionStates
{
    public class ActionStatesFactory
    {
        private readonly ActorsFactory actorsFactory;
        private readonly IBoard board;

        public ActionStatesFactory(ActorsFactory actorsFactory, IBoard board)
        {
            this.actorsFactory = actorsFactory;
            this.board = board;
        }

        public async UniTask Render(IState state)
        {
            var action = state.Action;
            var actionState = default(ActionStateBase);

            if (action is IFillAction)
            {
                actionState = new FillActionState();
            }
            else if (action is IEmptyAction)
            {
                actionState = new EmptyActionState();
            }
            else if (action is ISwapAction)
            {
                actionState = new SwapActionState();
            }
            else if (action is IDeleteAction)
            {
                actionState = new DeleteActionState();
            }
            else if (action is IGravityAction)
            {
                actionState = new GravityActionState();
            }

            if (actionState == null)
            {
                return;
            }

            Log.Info($"Render action: {action.GetType().Name}");

            await actionState.Render(actorsFactory, board, state);
        }
    }
}
