using UnityEngine;

namespace Game.Screens
{
    public class Screen : MonoBehaviour
    {
        public virtual void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
