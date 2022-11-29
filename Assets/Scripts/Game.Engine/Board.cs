using System.Collections.Generic;
using Game.Engine.Core;
using UnityEngine;

namespace Game.Engine
{
    public class Board : IBoard
    {
        private readonly List<Point> points = new();

        public Vector2Int Size { get; private set; }
        public IReadOnlyList<Point> Points => points;

        public void Build(Vector2Int boardSize)
        {
            Size = boardSize;
            points.Clear();

            for (var x = 0; x < boardSize.x; x++)
            {
                for (var y = 0; y < boardSize.y; y++)
                {
                    points.Add(new Point(x, y));
                }
            }
        }
    }
}
