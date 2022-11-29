using System.Collections.Generic;
using Game.Engine.Core;

namespace Game.Engine
{
    public class Memory : IMemory
    {
        public IBoard Board { get; }
        public ElementsFactory ElementsFactory { get; }
        public ActionsFactory ActionsFactory { get; }
        public StatesFactory StatesFactory { get; }
        public List<IState> StateBySteps { get; }
        public Point FromPoint { get; set; }
        public Point ToPoint { get; set; }

        public Memory(IBoard board, ElementsFactory elementsFactory, ActionsFactory actionsFactory, StatesFactory statesFactory, List<IState> states)
        {
            Board = board;
            ElementsFactory = elementsFactory;
            ActionsFactory = actionsFactory;
            StatesFactory = statesFactory;
            StateBySteps = states;
        }
    }
}
