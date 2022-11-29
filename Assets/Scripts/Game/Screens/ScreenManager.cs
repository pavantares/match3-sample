using Game.Screens.Screens.LevelBuilder;
using UnityEngine;

namespace Game.Screens
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField]
        private LevelBuilderScreen levelBuilderScreen;

        public LevelBuilderScreen LevelBuilderScreen => levelBuilderScreen;
    }
}
