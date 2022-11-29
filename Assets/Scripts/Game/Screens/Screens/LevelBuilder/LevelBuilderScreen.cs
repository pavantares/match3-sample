using System;
using System.Reactive;
using System.Reactive.Subjects;
using Cysharp.Threading.Tasks;
using Game.Screens.Widgets;
using UnityEngine;

namespace Game.Screens.Screens.LevelBuilder
{
    public class LevelBuilderScreen : Screen
    {
        [SerializeField]
        private SliderWidget rowWidget;

        [SerializeField]
        private SliderWidget columnWidget;

        [SerializeField]
        private ButtonWidget buildLevelWidget;

        [SerializeField]
        private ToggleWidget autoStepsWidget;

        private readonly ISubject<Vector2Int> onLevelBuilt = new Subject<Vector2Int>();

        public IObservable<Vector2Int> OnLevelBuilt => onLevelBuilt;
        public IObservable<bool> OnRandomStepsUpdated => autoStepsWidget.OnClicked;

        private void Awake()
        {
            rowWidget.SetMessage("Rows");
            rowWidget.SetValues(8, 20, 10, true);

            columnWidget.SetMessage("Columns");
            columnWidget.SetValues(8, 20, 10, true);

            buildLevelWidget.SetMessage("Build Level");
            buildLevelWidget.OnClicked.Subscribe(HandleBuildLevel).AddTo(this.GetCancellationTokenOnDestroy());

            autoStepsWidget.SetMessage("Auto Steps");
        }

        public void Initialize()
        {
            SetActive(true);
            HandleBuildLevel(Unit.Default);
        }

        private void HandleBuildLevel(Unit unit)
        {
            autoStepsWidget.SetValue(false);

            var boardSize = new Vector2Int((int)rowWidget.Value, (int)columnWidget.Value);
            onLevelBuilt.OnNext(boardSize);
        }
    }
}
