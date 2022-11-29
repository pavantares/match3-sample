using System.Collections.Generic;
using Game.Engine.Actions;
using Game.Engine.Core;
using Game.Engine.Extensions;
using UnityEngine;

namespace Game.Engine
{
    public class MatchEngine : IMatchEngine
    {
        private readonly IBoard board;
        private readonly ElementsFactory elementsFactory;
        private readonly ActionsFactory actionsFactory;
        private readonly StatesFactory statesFactory;
        private readonly List<IState> states;
        private readonly IMemory memory;
        private readonly ActionsPipeline actionsPipeline;

        public IBoard Board => board;

        public MatchEngine()
        {
            board = new Board();
            elementsFactory = new ElementsFactory();
            actionsFactory = new ActionsFactory();
            statesFactory = new StatesFactory();
            states = new List<IState>();
            memory = new Memory(board, elementsFactory, actionsFactory, statesFactory, states);
            actionsPipeline = new ActionsPipeline(memory);
        }

        public IState BuildLevel(Vector2Int boardSize)
        {
            board.Build(boardSize);
            states.Clear();

            var state = actionsPipeline.Build().GetFirstState();
            states.Add(state);

            return state;
        }

        public IState Step(IStepInput stepInput)
        {
            memory.FromPoint = stepInput.FromPoint;
            memory.ToPoint = stepInput.ToPoint;

            var state = actionsPipeline.Apply().GetFirstState();
            states.Add(state);

            return state;
        }

        public IState AutoStep()
        {
            var state = actionsPipeline.ApplyAutoStep().GetFirstState();
            states.Add(state);

            return state;
        }
    }
}
