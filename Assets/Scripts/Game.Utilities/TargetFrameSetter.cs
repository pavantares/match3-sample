using UnityEngine;

namespace Game.Utilities
{
    public class TargetFrameSetter : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
