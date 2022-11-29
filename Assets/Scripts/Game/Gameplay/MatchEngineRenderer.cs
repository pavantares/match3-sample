using System;
using System.Reactive.Subjects;
using Cysharp.Threading.Tasks;
using Game.Engine;
using Game.Engine.Core;
using Game.Engine.Extensions;
using Game.Gameplay.ActionStates;
using Game.Gameplay.Actors;
using UnityEngine;

namespace Game.Gameplay
{
    public class MatchEngineRenderer : MonoBehaviour
    {
        [SerializeField]
        private ActorsFactory actorsFactory;

        [SerializeField]
        private BoardActor boardActor;

        [SerializeField]
        private GameCamera gameCamera;

        [SerializeField]
        private GameInput gameInput;

        private readonly ISubject<IStepInput> onStepInputUpdated = new Subject<IStepInput>();
        private readonly StepInputFactory stepInputFactory = new();

        private ActionStatesFactory actionStatesFactory;

        private Point fromPoint = Point.Undefined;

        public IObservable<IStepInput> OnStepInputUpdated => onStepInputUpdated;

        private void Awake()
        {
            gameInput.OnDownClicked.Subscribe(HandleDownClick).AddTo(this.GetCancellationTokenOnDestroy());
            gameInput.OnUpClicked.Subscribe(HandleUpClick).AddTo(this.GetCancellationTokenOnDestroy());
            gameInput.OnDragging.Subscribe(HandleDragging).AddTo(this.GetCancellationTokenOnDestroy());
        }

        private void Start()
        {
            gameInput.SetEnabled(true);
        }

        public void Initialize(IBoard board)
        {
            actionStatesFactory = new ActionStatesFactory(actorsFactory, board);
        }

        public void Build(Vector2Int boardSize, IState state)
        {
            var bounds = GetBoardBounds(boardSize);
            gameCamera.Setup(bounds);
            boardActor.Build(boardSize);
            actorsFactory.ClearElementActors();

            RenderStateAsync(state).Forget();
        }

        public async UniTask RenderStateAsync(IState state)
        {
            gameInput.SetEnabled(false);

            while (true)
            {
                if (state == null)
                {
                    break;
                }

                await actionStatesFactory.Render(state);

                state = state.NextState;
            }

            gameInput.SetEnabled(true);
        }

        public bool AnyAnimation()
        {
            return actorsFactory.AnyElementAnimation();
        }

        private void HandleDownClick(Vector2 screenPoint)
        {
            TryGetPoint(screenPoint, out fromPoint);
        }

        private void HandleUpClick(Vector2 screenPoint)
        {
            if (fromPoint.IsUndefined || !TryGetPoint(screenPoint, out var toPoint) || !toPoint.Equal(fromPoint))
            {
                return;
            }

            CallStepInputUpdated(toPoint);
        }

        private void HandleDragging(Vector2 screenPoint)
        {
            if (fromPoint.IsUndefined || !TryGetPoint(screenPoint, out var toPoint) || toPoint.Equal(fromPoint) || !toPoint.IsTheSameRowOrColumn(fromPoint))
            {
                return;
            }

            CallStepInputUpdated(toPoint);
        }

        private void CallStepInputUpdated(Point toPoint)
        {
            onStepInputUpdated.OnNext(stepInputFactory.Create(fromPoint, toPoint));
            fromPoint = Point.Undefined;
        }

        private bool TryGetPoint(Vector2 screenPoint, out Point point)
        {
            var worldPoint = gameCamera.GetWorldPoint(screenPoint);

            for (var i = 0; i < actorsFactory.BoardCellActors.Count; i++)
            {
                var boardCellActor = actorsFactory.BoardCellActors[i];

                if (boardCellActor.Contains(worldPoint))
                {
                    point = boardCellActor.Point;
                    return true;
                }
            }

            point = Point.Undefined;
            return false;
        }

        private Bounds GetBoardBounds(Vector2Int boardSize)
        {
            var bounds = new Bounds();
            var offset = 0.75f * Constants.CellStep * Vector2.one;
            var max = new Vector2(Constants.CellStep * (boardSize.y - 1), Constants.CellStep * (boardSize.x - 1)) + offset;
            var min = -offset;

            bounds.Encapsulate(max);
            bounds.Encapsulate(min);

            return bounds;
        }
    }
}
