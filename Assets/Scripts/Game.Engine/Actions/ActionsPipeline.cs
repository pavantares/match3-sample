using Game.Engine.Core;

namespace Game.Engine.Actions
{
    public class ActionsPipeline
    {
        private readonly IMemory memory;

        public ActionsPipeline(IMemory memory)
        {
            this.memory = memory;
        }

        public IState Build()
        {
            var initialState = ApplyAction<IInitialAction>();
            var gravityState = initialState;

            do
            {
                var fillState = ApplyAction<IFillAction>(gravityState);
                gravityState = ApplyAction<IGravityAction>(fillState);
            } while (gravityState.Field.Elements.Count != memory.Board.Points.Count);

            return gravityState;
        }

        public IState Apply()
        {
            var initialState = ApplyAction<IInitialAction>();

            return ApplyInternal(initialState);
        }

        public IState ApplyAutoStep()
        {
            var initialState = ApplyAction<IInitialAction>();
            var autoStepState = ApplyAction<IAutoStepAction>(initialState);

            return ApplyInternal(autoStepState);
        }

        private IState ApplyInternal(IState previousState)
        {
            if (memory.FromPoint.Equal(memory.ToPoint))
            {
                return ApplyAction<IEmptyAction>(previousState);
            }

            var swapState = ApplyAction<ISwapAction>(previousState);
            var deleteState = ApplyAction<IDeleteAction>(swapState);
            var anyIterationHappened = false;

            while (IsNextIterationAvailable(deleteState))
            {
                var gravityAction = ApplyAction<IGravityAction>(deleteState);
                var fillAction = ApplyAction<IFillAction>(gravityAction);
                gravityAction = ApplyAction<IGravityAction>(fillAction);
                deleteState = ApplyAction<IDeleteAction>(gravityAction);
                anyIterationHappened = true;
            }

            if (anyIterationHappened)
            {
                return deleteState;
            }

            return ApplyAction<ISwapAction>(deleteState);
        }

        private bool IsNextIterationAvailable(IState state)
        {
            return state.Action is IDeleteAction destroyAction && destroyAction.DeleteElements.Count > 0
                   || state.Field.Elements.Count != memory.Board.Points.Count;
        }

        private IState ApplyAction<TAction>(IState previousState = null) where TAction : IAction
        {
            return memory.ActionsFactory.CreateAction<TAction>(memory).Apply(previousState);
        }
    }
}
