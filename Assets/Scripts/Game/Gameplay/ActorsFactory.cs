using System.Collections.Generic;
using Game.Engine.Core;
using Game.Engine.Core.Elements;
using Game.Gameplay.Actors;
using Game.Gameplay.Actors.Elements;
using Game.Utilities;
using UnityEngine;

namespace Game.Gameplay
{
    public class ActorsFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform boardRoot;

        [SerializeField]
        private Transform elementsRoot;

        [Space]
        [SerializeField]
        private GameplayAssets gameplayAssets;

        private readonly List<BoardCellActor> boardCellActors = new();
        private readonly List<ElementActor> elementActors = new();

        public IReadOnlyList<BoardCellActor> BoardCellActors => boardCellActors;
        public IReadOnlyList<ElementActor> ElementActors => elementActors;

        public BoardCellActor CreateBoardCellActor(Point point)
        {
            var boardCellActor = CreateActor(gameplayAssets.BoardCellActorPrefab, boardRoot);
            boardCellActor.SetPoint(point);
            boardCellActors.Add(boardCellActor);

            return boardCellActor;
        }

        public ElementActor CreateElementActor(IElement element)
        {
            if (element is IAppleElement appleElement)
            {
                return CreateElementActor(gameplayAssets.AppleElementActorPrefab, appleElement);
            }

            if (element is IFishElement fishElement)
            {
                return CreateElementActor(gameplayAssets.FishElementActorPrefab, fishElement);
            }

            if (element is IIceCreamElement iceCreamElement)
            {
                return CreateElementActor(gameplayAssets.IceCreamElementActorPrefab, iceCreamElement);
            }

            if (element is IJellyBearElement jellyBearElement)
            {
                return CreateElementActor(gameplayAssets.JellyBearElementActorPrefab, jellyBearElement);
            }

            if (element is IPieElement pieElement)
            {
                return CreateElementActor(gameplayAssets.PieElementActorPrefab, pieElement);
            }

            return null;
        }

        public ElementActor GetElementActor(string id)
        {
            return elementActors.Find(x => x.Id == id);
        }

        public bool AnyElementAnimation()
        {
            return elementActors.Exists(x => x.AnyAnimation);
        }

        public bool HasElementAt(string id)
        {
            return elementActors.Exists(x => x.Id == id);
        }

        public void DeleteElementActor(ElementActor deletedElementActor)
        {
            elementActors.Remove(deletedElementActor);
            Destroy(deletedElementActor.gameObject);
        }

        public bool AnyElementUseGravity()
        {
            return elementActors.Exists(x => x.IsGravity);
        }

        public void ClearBoardCells()
        {
            boardCellActors.Clear();
            boardRoot.DeleteChilds();
        }

        public void ClearElementActors()
        {
            elementActors.Clear();
            elementsRoot.DeleteChilds();
        }

        private TElementActor CreateElementActor<TElementActor, TElement>(TElementActor prefab, TElement element)
            where TElementActor : ElementActor<TElement>
            where TElement : IElement
        {
            var elementActor = CreateActor(prefab, elementsRoot);
            elementActor.SetElement(element);
            elementActors.Add(elementActor);

            return elementActor;
        }

        private TActor CreateActor<TActor>(TActor prefab, Transform root) where TActor : Actor
        {
            return Instantiate(prefab, root);
        }
    }
}
