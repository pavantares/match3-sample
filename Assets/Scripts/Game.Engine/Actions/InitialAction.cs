using System.Collections.Generic;
using Game.Engine.Core;
using Game.Engine.Core.Elements;
using Game.Engine.Extensions;

namespace Game.Engine.Actions
{
    public class InitialAction : IInitialAction
    {
        private readonly IMemory memory;

        public InitialAction(IMemory memory)
        {
            this.memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var field = memory.StateBySteps.Count > 0
                ? memory.StateBySteps[^1].GetLastState().Field.Copy()
                : new Field(new List<IElement>());

            return memory.StatesFactory.CreateState(field, this, previousState);
        }
    }
}
