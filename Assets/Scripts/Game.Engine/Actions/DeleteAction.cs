using System.Collections.Generic;
using Game.Engine.Core;
using Game.Engine.Core.Elements;
using Game.Engine.Utilities;

namespace Game.Engine.Actions
{
    public class DeleteAction : IDeleteAction
    {
        private readonly IMemory memory;

        public List<IElement> DeleteElements { get; private set; }

        public DeleteAction(IMemory memory)
        {
            this.memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var field = previousState.Field.Copy();
            var points = GetMatchPoints(field);

            DeleteElements = field.DeleteAndGetElementByPoints(points);

            return memory.StatesFactory.CreateState(field, this, previousState);
        }

        private List<Point> GetMatchPoints(IField field)
        {
            var matchPoints = new List<Point>();

            for (var i = 0; i < memory.Board.Points.Count; i++)
            {
                var point = memory.Board.Points[i];
                var points = MatchEngineUtility.GetMatchPoints(field, point);

                if (points.Count > 0)
                {
                    matchPoints.AddRange(points);
                }
            }

            return matchPoints;
        }
    }
}
