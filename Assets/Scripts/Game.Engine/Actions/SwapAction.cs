using Game.Engine.Core;

namespace Game.Engine.Actions
{
    public class SwapAction : ISwapAction
    {
        private readonly IMemory memory;

        public Point FromPoint { get; private set; }
        public Point ToPoint { get; private set; }

        public SwapAction(IMemory memory)
        {
            this.memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            FromPoint = memory.FromPoint;
            ToPoint = memory.ToPoint;

            var field = previousState.Field.Copy();
            field.SwapElements(memory.FromPoint, memory.ToPoint);

            return memory.StatesFactory.CreateState(field, this, previousState);
        }
    }
}
