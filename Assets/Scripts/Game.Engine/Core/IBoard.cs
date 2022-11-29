using System.Collections.Generic;
using UnityEngine;

namespace Game.Engine.Core
{
    public interface IBoard
    {
        Vector2Int Size { get; }
        IReadOnlyList<Point> Points { get; }
        void Build(Vector2Int boardSize);
    }
}
