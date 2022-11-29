using Game.Engine.Core;

namespace Game.Engine
{
    public class StatesFactory
    {
        public IState CreateState(IField field, IAction action, IState previousState)
        {
            return new State(field, action, previousState);
        }
    }
}
