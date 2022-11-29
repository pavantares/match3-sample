using Game.Engine.Actions;
using Game.Engine.Core;

namespace Game.Engine
{
    public class ActionsFactory
    {
        public IAction CreateAction<TAction>(IMemory memory) where TAction : IAction
        {
            if (typeof(TAction) == typeof(IInitialAction))
            {
                return new InitialAction(memory);
            }

            if (typeof(TAction) == typeof(ISwapAction))
            {
                return new SwapAction(memory);
            }

            if (typeof(TAction) == typeof(IDeleteAction))
            {
                return new DeleteAction(memory);
            }

            if (typeof(TAction) == typeof(IGravityAction))
            {
                return new GravityAction(memory);
            }

            if (typeof(TAction) == typeof(IFillAction))
            {
                return new FillAction(memory);
            }

            if (typeof(TAction) == typeof(IEmptyAction))
            {
                return new EmptyAction(memory);
            }

            if (typeof(TAction) == typeof(IAutoStepAction))
            {
                return new AutoStepAction(memory);
            }

            return null;
        }
    }
}
