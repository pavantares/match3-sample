using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Engine.Core;
using Game.Engine.Core.Elements;
using Game.Gameplay.Extensions;
using UnityEngine;

namespace Game.Gameplay.Actors.Elements
{
    public class ElementActor : Actor
    {
        [SerializeField]
        private SpriteRenderer mainRenderer;

        private int animationsCounter;

        public virtual string Id { get; }
        public Point Point { get; private set; }
        public bool IsGravity { get; private set; }
        public bool AnyAnimation => animationsCounter > 0;

        public void SetPoint(Point point)
        {
            Point = point;
        }

        public async UniTask MoveTo(Point point)
        {
            AddAnimationCounter();
            SetPoint(point);

            mainRenderer.sortingOrder++;
            await transform.DOMove(point.ToVector2(), 0.15f).OnComplete(() => mainRenderer.sortingOrder--).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask MoveFrom(Point point)
        {
            AddAnimationCounter();
            SetPoint(point);

            mainRenderer.sortingOrder--;
            await transform.DOMove(point.ToVector2(), 0.15f).OnComplete(() => mainRenderer.sortingOrder++).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask RenderEmpty()
        {
            AddAnimationCounter();

            await transform.DOScale(0.6f, 0.05f).SetLoops(2, LoopType.Yoyo).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask RenderAppearing(Point point)
        {
            AddAnimationCounter();
            SetPoint(point);
            transform.position = point.ToVector2();
            transform.localScale = Vector3.zero;

            await transform.DOScale(1, Constants.GravityDurationInCell).SetEase(Ease.OutBack).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask RenderDelete()
        {
            AddAnimationCounter();

            await transform.DOScale(0, 0.1f).SetEase(Ease.InBack).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask RenderGravity(Point point)
        {
            AddAnimationCounter();
            var rowsDelta = Mathf.Abs(Point.Row - point.Row);
            SetPoint(point);
            IsGravity = true;

            await transform.DOMove(point.ToVector2(), Constants.GravityDurationInCell * rowsDelta).ToUniTask();

            IsGravity = false;
            RemoveAnimationCounter();
        }

        private void AddAnimationCounter()
        {
            animationsCounter++;
        }

        private void RemoveAnimationCounter()
        {
            animationsCounter = Mathf.Max(0, animationsCounter - 1);
        }
    }

    public class ElementActor<TElement> : ElementActor where TElement : IElement
    {
        public override string Id => Element.Id;
        public TElement Element { get; private set; }

        public void SetElement(TElement element)
        {
            Element = element;
            SetPoint(element.Point);
        }
    }
}
