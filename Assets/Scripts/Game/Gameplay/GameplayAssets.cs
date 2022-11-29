using Game.Gameplay.Actors;
using Game.Gameplay.Actors.Elements;
using UnityEngine;

namespace Game.Gameplay
{
    public class GameplayAssets : ScriptableObject
    {
        [SerializeField]
        private BoardCellActor boardCellActorPrefab;

        [SerializeField]
        private AppleElementActor appleElementActorPrefab;

        [SerializeField]
        private FishElementActor fishElementActorPrefab;

        [SerializeField]
        private IceCreamElementActor iceCreamElementActorPrefab;

        [SerializeField]
        private JellyBearElementActor jellyBearElementActorPrefab;

        [SerializeField]
        private PieElementActor pieElementActorPrefab;

        public BoardCellActor BoardCellActorPrefab => boardCellActorPrefab;
        public AppleElementActor AppleElementActorPrefab => appleElementActorPrefab;
        public FishElementActor FishElementActorPrefab => fishElementActorPrefab;
        public IceCreamElementActor IceCreamElementActorPrefab => iceCreamElementActorPrefab;
        public JellyBearElementActor JellyBearElementActorPrefab => jellyBearElementActorPrefab;
        public PieElementActor PieElementActorPrefab => pieElementActorPrefab;
    }
}
