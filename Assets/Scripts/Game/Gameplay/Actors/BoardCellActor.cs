using Game.Engine.Core;
using Game.Gameplay.Extensions;
using UnityEngine;

namespace Game.Gameplay.Actors
{
    public class BoardCellActor : Actor
    {
        public Point Point { get; private set; }

        public void SetPoint(Point point)
        {
            Point = point;
            transform.position = point.ToVector2();
        }

        public bool Contains(Vector2 point)
        {
            var localPosition = point - (Vector2)transform.position;

            return localPosition.x < Constants.HalfCellStep
                   && localPosition.x > -Constants.HalfCellStep
                   && localPosition.y < Constants.HalfCellStep
                   && localPosition.y > -Constants.HalfCellStep;
        }
    }
}
