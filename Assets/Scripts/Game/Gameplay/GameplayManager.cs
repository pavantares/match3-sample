using System;
using Cysharp.Threading.Tasks;
using Game.Engine;
using Game.Engine.Core;
using UnityEngine;

namespace Game.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private MatchEngineRenderer matchEngineRenderer;

        private IMatchEngine matchEngine;
        private bool isAutoStepsEnabled;

        private void Awake()
        {
            matchEngine = new MatchEngineFactory().Create();
            matchEngineRenderer.Initialize(matchEngine.Board);
            matchEngineRenderer.OnStepInputUpdated.Subscribe(HandleStepInputUpdated).AddTo(this.GetCancellationTokenOnDestroy());
        }

        public void BuildLevel(Vector2Int boardSize)
        {
            if (matchEngineRenderer.AnyAnimation())
            {
                return;
            }

            var state = matchEngine.BuildLevel(boardSize);
            matchEngineRenderer.Build(boardSize, state);
        }

        public async UniTaskVoid SetAutoSteps(bool isAutoStepsEnabled)
        {
            this.isAutoStepsEnabled = isAutoStepsEnabled;

            while (this.isAutoStepsEnabled)
            {
                var state = matchEngine.AutoStep();
                await matchEngineRenderer.RenderStateAsync(state);
            }
        }

        private void HandleStepInputUpdated(IStepInput stepInput)
        {
            var state = matchEngine.Step(stepInput);
            matchEngineRenderer.RenderStateAsync(state).Forget();
        }
    }
}
