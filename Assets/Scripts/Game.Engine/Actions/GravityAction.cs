using Game.Engine.Core;
using Game.Engine.Core.Elements;

namespace Game.Engine.Actions
{
    public class GravityAction : IGravityAction
    {
        private readonly IMemory memory;

        public GravityAction(IMemory memory)
        {
            this.memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var field = previousState.Field.Copy();
            ApplyGravity(field);

            return memory.StatesFactory.CreateState(field, this, previousState);
        }

        private void ApplyGravity(IField field)
        {
            var board = memory.Board;
            var maxRow = board.Size.x;

            for (var i = 0; i < board.Points.Count; i++)
            {
                var point = board.Points[i];
                var element = field.GetElementAt(point);

                if (element != null)
                {
                    continue;
                }

                var closestElementInColumn = GetClosestElementInColumn(field, point, maxRow);

                if (closestElementInColumn == null)
                {
                    continue;
                }

                closestElementInColumn.Point = point;
            }
        }

        private IElement GetClosestElementInColumn(IField field, Point point, int maxRow)
        {
            var row = point.Row;
            var column = point.Column;

            while (row != maxRow)
            {
                var element = field.GetElementAt(new Point(++row, column));

                if (element != null)
                {
                    return element;
                }
            }

            return null;
        }
    }
}
