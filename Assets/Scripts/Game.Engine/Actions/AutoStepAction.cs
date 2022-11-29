using System.Collections.Generic;
using Game.Engine.Core;
using Game.Engine.Utilities;
using Game.Utilities;

namespace Game.Engine.Actions
{
    public class AutoStepAction : IAutoStepAction
    {
        private readonly IMemory memory;

        public bool HasSuccessStep { get; private set; }

        public AutoStepAction(IMemory memory)
        {
            this.memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            if (TryGetSuccessStep(previousState.Field.Copy(), out var fromPoint, out var toPoint))
            {
                HasSuccessStep = true;
                memory.FromPoint = fromPoint;
                memory.ToPoint = toPoint;
            }

            return memory.StatesFactory.CreateState(previousState.Field.Copy(), this, previousState);
        }

        private bool TryGetSuccessStep(IField field, out Point fromPoint, out Point toPoint)
        {
            var allPoints = memory.Board.Points;
            var successSwapPoints = new List<(Point fromPoint, Point toPoint)>();

            for (var i = 0; i < allPoints.Count; i++)
            {
                var swapPoints = GetSwapPoints(allPoints[i]);

                for (var j = 0; j < swapPoints.Count; j++)
                {
                    var (from, to) = swapPoints[j];
                    var isSwapSuccess = field.SwapElements(from, to);

                    if (!isSwapSuccess)
                    {
                        continue;
                    }

                    if (MatchEngineUtility.HasMatch(field, from))
                    {
                        successSwapPoints.Add((from, to));
                    }

                    field.SwapElements(from, to);
                }
            }

            var randomSwapPoints = successSwapPoints.RandomItem();
            fromPoint = randomSwapPoints.fromPoint;
            toPoint = randomSwapPoints.toPoint;

            return successSwapPoints.Count > 0;
        }

        private static List<(Point, Point)> GetSwapPoints(Point inputPoint)
        {
            var row = inputPoint.Row;
            var column = inputPoint.Column;

            var points1 = (inputPoint, new Point(row + 1, column));
            var points2 = (inputPoint, new Point(row - 1, column));
            var points3 = (inputPoint, new Point(row, column + 1));
            var points4 = (inputPoint, new Point(row, column - 1));

            return new List<(Point, Point)> { points1, points2, points3, points4 };
        }
    }
}
