using Game.Engine.Core;
using Game.Engine.Core.Elements;

namespace Game.Engine.Actions
{
    public class EmptyAction : IEmptyAction
    {
        private readonly IMemory memory;

        public IElement Element { get; private set; }

        public EmptyAction(IMemory memory)
        {
            this.memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var field = previousState.Field.Copy();
            Element = field.GetElementAt(memory.FromPoint);

            return memory.StatesFactory.CreateState(field, this, previousState);
        }
    }
}
