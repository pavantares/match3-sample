using Game.Engine.Core;
using UnityEngine;

namespace Game.Gameplay.Extensions
{
    public static class MatchEngineExtensions
    {
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(Constants.CellStep * point.Column, Constants.CellStep * point.Row);
        }
    }
}
