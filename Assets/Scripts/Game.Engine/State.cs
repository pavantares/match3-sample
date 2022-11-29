using Game.Engine.Core;

namespace Game.Engine
{
    public class State : IState
    {
        public IField Field { get; }
        public IAction Action { get; }
        public IState PreviousState { get; set; }
        public IState NextState { get; set; }

        public State(IField field, IAction action, IState previousState)
        {
            Field = field;
            Action = action;

            if (previousState != null)
            {
                previousState.NextState = this;
                PreviousState = previousState;
            }
        }
    }
}
