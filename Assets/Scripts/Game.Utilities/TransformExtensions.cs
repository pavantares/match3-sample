using UnityEngine;

namespace Game.Utilities
{
    public static class TransformExtensions
    {
        public static void DeleteChilds(this GameObject gameObject)
        {
            gameObject.transform.DeleteChilds();
        }

        public static void DeleteChilds(this Transform transform)
        {
            foreach (Transform item in transform)
            {
                Transform.Destroy(item.gameObject);
            }
        }
    }
}
