using System;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Screens;
using UnityEngine;

namespace Game.Scenes.Main
{
    public class MainEntry : MonoBehaviour
    {
        [SerializeField]
        private ScreenManager screenManager;

        [SerializeField]
        private GameplayManager gameplayManager;

        private void Start()
        {
            var levelBuilderScreen = screenManager.LevelBuilderScreen;
            levelBuilderScreen.OnLevelBuilt.Subscribe(HandleBuiltLevel).AddTo(this.GetCancellationTokenOnDestroy());
            levelBuilderScreen.OnRandomStepsUpdated.Subscribe(HandleRandomStepUpdated).AddTo(this.GetCancellationTokenOnDestroy());
            levelBuilderScreen.Initialize();
        }

        private void HandleBuiltLevel(Vector2Int boardSize)
        {
            gameplayManager.BuildLevel(boardSize);
        }

        private void HandleRandomStepUpdated(bool isRandomStepsEnabled)
        {
            gameplayManager.SetAutoSteps(isRandomStepsEnabled).Forget();
        }
    }
}
