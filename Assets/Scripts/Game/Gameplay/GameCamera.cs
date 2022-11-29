using UnityEngine;

namespace Game.Gameplay
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera gameCamera;

        public void Setup(Bounds bounds, float verticalOffset = 0)
        {
            var width = bounds.size.x;
            var orthographicSize = 0.5f * width / gameCamera.aspect;

            var center = bounds.center;
            var position = gameCamera.transform.position;
            position.x = center.x;
            position.y = center.y;

            gameCamera.orthographicSize = orthographicSize;
            gameCamera.transform.position = position + Vector3.up * verticalOffset;
        }

        public Vector2 GetWorldPoint(Vector2 screenPoint)
        {
            return gameCamera.ScreenToWorldPoint(screenPoint);
        }
    }
}
