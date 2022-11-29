using Game.Engine.Core;
using UnityEngine;

namespace Game.Gameplay.Actors
{
    public class BoardActor : Actor
    {
        [SerializeField]
        private SpriteRenderer boardFrameRenderer;

        [SerializeField]
        private ActorsFactory actorsFactory;

        public void Build(Vector2Int boardSize)
        {
            actorsFactory.ClearBoardCells();

            boardFrameRenderer.size = Constants.CellStep * (new Vector2(boardSize.y, boardSize.x) + 0.24f * Vector2.one);
            boardFrameRenderer.transform.position = -(Constants.HalfCellStep + 0.04f) * Vector2.one;

            for (int x = 0; x < boardSize.x; x++)
            {
                for (int y = 0; y < boardSize.y; y++)
                {
                    actorsFactory.CreateBoardCellActor(new Point(x, y));
                }
            }
        }
    }
}
